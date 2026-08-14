const editors = new Map();

export function initialize(id, optionsJson, dotNetRef) {
    destroy(id);

    const root = document.getElementById(id);
    if (!root) {
        throw new Error(`Node editor '${id}' was not found.`);
    }

    const state = {
        root,
        viewport: root.querySelector("[data-slot='node-editor-viewport']"),
        background: root.querySelector("[data-slot='node-editor-background']"),
        edgeLayer: root.querySelector("[data-slot='node-editor-edge-layer']"),
        preview: root.querySelector("[data-connection-preview]"),
        dotNetRef,
        options: normalizeOptions(JSON.parse(optionsJson)),
        panX: 0,
        panY: 0,
        zoom: 1,
        selectedNodeId: null,
        selectedEdgeId: null,
        copiedNodeId: null,
        interaction: null,
        connection: null,
        resizeObserver: null,
        edgeFrame: 0,
        viewportTimer: 0,
        validationSequence: 0,
        destroyed: false,
        ports: new Map(),
        nodeElements: [],
        edgeElements: [],
        edgeElementsById: new Map(),
        connectionCounts: new Map(),
        addHandleElements: [],
        cleanup: []
    };

    state.panX = state.options.initialX;
    state.panY = state.options.initialY;
    state.zoom = clamp(state.options.initialZoom, state.options.minZoom, state.options.maxZoom);

    const onPointerDown = event => handlePointerDown(state, event);
    const onPointerMove = event => handlePointerMove(state, event);
    const onPointerUp = event => handlePointerUp(state, event);
    const onWheel = event => handleWheel(state, event);
    const onKeyDown = event => handleKeyDown(state, event);
    const onClick = event => handleClick(state, event);
    const onDoubleClick = event => handleDoubleClick(state, event);
    const onDragStart = event => handlePaletteDragStart(state, event);
    const onDragEnd = event => handlePaletteDragEnd(state, event);
    const onDragOver = event => handlePaletteDragOver(state, event);
    const onDrop = event => handlePaletteDrop(state, event);
    const onBlur = () => cancelActiveInteraction(state);

    root.addEventListener("pointerdown", onPointerDown);
    root.addEventListener("wheel", onWheel, { passive: false });
    root.addEventListener("keydown", onKeyDown);
    root.addEventListener("click", onClick);
    root.addEventListener("dblclick", onDoubleClick);
    root.addEventListener("dragover", onDragOver);
    root.addEventListener("drop", onDrop);
    document.addEventListener("dragstart", onDragStart);
    document.addEventListener("dragend", onDragEnd);
    window.addEventListener("pointermove", onPointerMove);
    window.addEventListener("pointerup", onPointerUp);
    window.addEventListener("pointercancel", onPointerUp);
    window.addEventListener("blur", onBlur);

    state.cleanup.push(
        () => root.removeEventListener("pointerdown", onPointerDown),
        () => root.removeEventListener("wheel", onWheel),
        () => root.removeEventListener("keydown", onKeyDown),
        () => root.removeEventListener("click", onClick),
        () => root.removeEventListener("dblclick", onDoubleClick),
        () => root.removeEventListener("dragover", onDragOver),
        () => root.removeEventListener("drop", onDrop),
        () => document.removeEventListener("dragstart", onDragStart),
        () => document.removeEventListener("dragend", onDragEnd),
        () => window.removeEventListener("pointermove", onPointerMove),
        () => window.removeEventListener("pointerup", onPointerUp),
        () => window.removeEventListener("pointercancel", onPointerUp),
        () => window.removeEventListener("blur", onBlur)
    );

    state.resizeObserver = new ResizeObserver(() => scheduleEdgeUpdate(state));
    state.resizeObserver.observe(root);
    editors.set(id, state);

    applyViewport(state);
    refresh(id, optionsJson, null, null);

    if (state.options.fitViewOnInitialize) {
        requestAnimationFrame(() => {
            requestAnimationFrame(() => {
                if (!state.destroyed) {
                    fitView(id);
                    revealViewport(state);
                }
            });
        });
    } else {
        revealViewport(state);
    }
}

export function refresh(id, optionsJson, selectedNodeId, selectedEdgeId) {
    const state = editors.get(id);
    if (!state) {
        return;
    }

    if (state.root.dataset.connectionPending === "true") {
        state.validationSequence++;
        state.root.dataset.connectionPending = "false";
    }

    state.options = normalizeOptions(JSON.parse(optionsJson));
    state.selectedNodeId = selectedNodeId;
    state.selectedEdgeId = selectedEdgeId;
    state.viewport = state.root.querySelector("[data-slot='node-editor-viewport']");
    state.background = state.root.querySelector("[data-slot='node-editor-background']");
    state.edgeLayer = state.root.querySelector("[data-slot='node-editor-edge-layer']");
    state.preview = state.root.querySelector("[data-connection-preview]");
    rebuildGeometryIndex(state);
    focusEdgeLabelEditor(state);

    state.nodeElements.forEach(node => {
        node.dataset.selected = node.dataset.nodeId === selectedNodeId ? "true" : "false";
        node.setAttribute("aria-selected", node.dataset.selected);
    });

    state.resizeObserver?.disconnect();
    state.resizeObserver?.observe(state.root);
    state.nodeElements.forEach(node => state.resizeObserver?.observe(node));

    state.edgeElements.forEach(edge => {
        edge.dataset.selected = edge.dataset.edgeId === selectedEdgeId ? "true" : "false";
        edge.setAttribute("aria-pressed", edge.dataset.selected);
        const path = edge.querySelector("[data-edge-path]");
        if (path) {
            path.classList.toggle("stroke-primary", edge.dataset.selected === "true");
            path.classList.toggle("stroke-muted-foreground/35", edge.dataset.selected !== "true");
            path.setAttribute("stroke-width", edge.dataset.selected === "true" ? "2" : "1.5");
        }
    });

    updatePortCapacities(state);
    state.zoom = clamp(state.zoom, state.options.minZoom, state.options.maxZoom);
    applyViewport(state);
    if (state.root.dataset.initialized === "true") {
        revealViewport(state);
    }
    scheduleEdgeUpdate(state);
}

export function zoomBy(id, delta) {
    const state = editors.get(id);
    if (!state) {
        return;
    }

    zoomAt(state, state.root.clientWidth / 2, state.root.clientHeight / 2, state.zoom + delta);
}

export function resetView(id) {
    const state = editors.get(id);
    if (!state) {
        return;
    }

    state.panX = state.options.initialX;
    state.panY = state.options.initialY;
    state.zoom = clamp(state.options.initialZoom, state.options.minZoom, state.options.maxZoom);
    applyViewport(state, true);
}

export function fitView(id) {
    const state = editors.get(id);
    if (!state) {
        return;
    }

    const nodes = [...state.root.querySelectorAll("[data-slot='node-editor-node']")];
    if (nodes.length === 0) {
        resetView(id);
        return;
    }

    let minX = Infinity;
    let minY = Infinity;
    let maxX = -Infinity;
    let maxY = -Infinity;

    nodes.forEach(node => {
        const x = number(node.dataset.nodeX);
        const y = number(node.dataset.nodeY);
        minX = Math.min(minX, x);
        minY = Math.min(minY, y);
        maxX = Math.max(maxX, x + node.offsetWidth);
        maxY = Math.max(maxY, y + node.offsetHeight);
    });

    state.root.querySelectorAll("[data-slot='node-editor-add-handle']").forEach(handle => {
        const x = number(handle.dataset.handleX);
        const y = number(handle.dataset.handleY);
        minX = Math.min(minX, x - 12);
        minY = Math.min(minY, y - 12);
        maxX = Math.max(maxX, x + 12);
        maxY = Math.max(maxY, y + 12);
    });

    const padding = 72;
    const width = Math.max(1, maxX - minX);
    const height = Math.max(1, maxY - minY);
    const availableWidth = Math.max(1, state.root.clientWidth - padding * 2);
    const availableHeight = Math.max(1, state.root.clientHeight - padding * 2);

    state.zoom = clamp(Math.min(availableWidth / width, availableHeight / height), state.options.minZoom, Math.min(1.15, state.options.maxZoom));
    state.panX = (state.root.clientWidth - width * state.zoom) / 2 - minX * state.zoom;
    state.panY = (state.root.clientHeight - height * state.zoom) / 2 - minY * state.zoom;
    applyViewport(state, true);
}

export function clientToGraphPoint(id, clientX, clientY) {
    const state = editors.get(id);
    if (!state) {
        throw new Error(`Node editor '${id}' is not initialized.`);
    }

    return clientToGraph(state, clientX, clientY);
}

export function destroy(id) {
    const state = editors.get(id);
    if (!state) {
        return;
    }

    state.cleanup.forEach(cleanup => cleanup());
    state.destroyed = true;
    state.validationSequence++;
    if (state.edgeFrame) cancelAnimationFrame(state.edgeFrame);
    if (state.viewportTimer) clearTimeout(state.viewportTimer);
    state.resizeObserver?.disconnect();
    state.root.classList.remove("cursor-grabbing");
    editors.delete(id);
}

function handlePointerDown(state, event) {
    if (event.button !== 0) {
        return;
    }

    if (state.root.dataset.connectionPending === "true") {
        return;
    }

    if (event.target.closest("[data-edge-label-editor]")) {
        return;
    }

    const endpoint = event.target.closest("[data-edge-endpoint]");
    if (endpoint && state.options.connectionsEnabled) {
        event.preventDefault();
        event.stopPropagation();
        beginReconnect(state, endpoint, event);
        return;
    }

    if (event.target.closest("[data-slot='node-editor-add-handle']")) {
        return;
    }

    const port = event.target.closest("[data-slot='node-editor-port']");
    if (port && state.options.connectionsEnabled && port.dataset.portType === "source") {
        if (!portAvailable(state, port, null)) {
            rejectInteraction(state, portCapacityMessage(port));
            return;
        }

        event.preventDefault();
        event.stopPropagation();
        beginConnection(state, port, event);
        return;
    }

    const edge = event.target.closest("[data-edge-id]");
    if (edge) {
        if (edge.dataset.disabled === "true" || edge.dataset.selectable !== "true") {
            return;
        }

        event.preventDefault();
        selectEdge(state, edge.dataset.edgeId);
        edge.focus({ preventScroll: true });
        return;
    }

    const node = event.target.closest("[data-slot='node-editor-node']");
    if (node && state.root.contains(node)) {
        if (node.dataset.disabled === "true") {
            return;
        }

        if (node.dataset.selectable === "true") {
            selectNode(state, node.dataset.nodeId);
            node.focus({ preventScroll: true });
        }

        if (!state.options.draggableNodes || event.target.closest("button,input,textarea,select,a,[contenteditable='true']")) {
            return;
        }

        event.preventDefault();
        state.interaction = {
            kind: "node",
            pointerId: event.pointerId,
            node,
            startClientX: event.clientX,
            startClientY: event.clientY,
            startX: number(node.dataset.nodeX),
            startY: number(node.dataset.nodeY),
            moved: false
        };
        node.dataset.dragging = "true";
        return;
    }

    if (event.target.closest("[data-slot='node-editor-controls']")) {
        return;
    }

    selectNode(state, null);
    state.root.focus({ preventScroll: true });
    if (!state.options.panEnabled) {
        return;
    }

    event.preventDefault();
    state.interaction = {
        kind: "pan",
        pointerId: event.pointerId,
        startClientX: event.clientX,
        startClientY: event.clientY,
        startPanX: state.panX,
        startPanY: state.panY
    };
    state.root.classList.add("cursor-grabbing");
}

function handlePointerMove(state, event) {
    if (state.connection) {
        if (state.connection.pointerId !== null && state.connection.pointerId !== event.pointerId) {
            return;
        }

        const point = clientToGraph(state, event.clientX, event.clientY);
        state.connection.currentX = point.x;
        state.connection.currentY = point.y;
        updateConnectionPreview(state);
        return;
    }

    const interaction = state.interaction;
    if (!interaction || interaction.pointerId !== event.pointerId) {
        return;
    }

    if (interaction.kind === "pan") {
        state.panX = interaction.startPanX + event.clientX - interaction.startClientX;
        state.panY = interaction.startPanY + event.clientY - interaction.startClientY;
        applyViewport(state, true);
        return;
    }

    if (!interaction.moved && Math.hypot(event.clientX - interaction.startClientX, event.clientY - interaction.startClientY) < 3) {
        return;
    }

    interaction.moved = true;
    let x = interaction.startX + (event.clientX - interaction.startClientX) / state.zoom;
    let y = interaction.startY + (event.clientY - interaction.startClientY) / state.zoom;

    if (state.options.snapToGrid) {
        x = Math.round(x / state.options.gridSize) * state.options.gridSize;
        y = Math.round(y / state.options.gridSize) * state.options.gridSize;
    }

    setNodePosition(interaction.node, x, y);
    scheduleEdgeUpdate(state);
}

function handlePointerUp(state, event) {
    if (state.connection) {
        if (state.connection.pointerId !== null && state.connection.pointerId !== event.pointerId) {
            return;
        }

        if (event.type === "pointercancel") {
            cancelConnection(state);
        } else {
            finishConnection(state, event);
        }
        return;
    }

    const interaction = state.interaction;
    if (!interaction || interaction.pointerId !== event.pointerId) {
        return;
    }

    state.interaction = null;
    state.root.classList.remove("cursor-grabbing");

    if (interaction.kind === "node") {
        interaction.node.dataset.dragging = "false";
        if (interaction.moved) {
            state.dotNetRef.invokeMethodAsync("InvokeNodeMoved", {
                nodeId: interaction.node.dataset.nodeId,
                x: number(interaction.node.dataset.nodeX),
                y: number(interaction.node.dataset.nodeY),
                previousX: interaction.startX,
                previousY: interaction.startY
            }).catch(console.error);
        }
    }
}

function cancelActiveInteraction(state) {
    if (state.connection) {
        cancelConnection(state);
    }

    const interaction = state.interaction;
    state.interaction = null;
    state.root.classList.remove("cursor-grabbing");
    if (interaction?.kind === "node") {
        interaction.node.dataset.dragging = "false";
        setNodePosition(interaction.node, interaction.startX, interaction.startY);
        scheduleEdgeUpdate(state);
    }
}

function handleWheel(state, event) {
    if (!state.options.zoomEnabled) {
        return;
    }

    event.preventDefault();
    const rect = state.root.getBoundingClientRect();
    const delta = Math.max(-0.18, Math.min(0.18, -event.deltaY * 0.0012));
    zoomAt(state, event.clientX - rect.left, event.clientY - rect.top, state.zoom * (1 + delta));
}

function handleClick(state, event) {
    const addHandle = event.target.closest("[data-slot='node-editor-add-handle']");
    if (addHandle && !addHandle.disabled) {
        if (addHandle.dataset.pending === "true") {
            return;
        }

        event.preventDefault();
        addHandle.dataset.pending = "true";
        state.dotNetRef.invokeMethodAsync("InvokeAddRequested", {
            handleId: addHandle.dataset.addHandleId,
            sourceNodeId: addHandle.dataset.sourceNode,
            sourcePortId: addHandle.dataset.sourcePort,
            x: number(addHandle.dataset.handleX),
            y: number(addHandle.dataset.handleY)
        }).catch(console.error).finally(() => {
            if (addHandle.isConnected) {
                addHandle.dataset.pending = "false";
            }
        });
        return;
    }

    const port = event.target.closest("[data-slot='node-editor-port']");
    if (!port || !state.options.connectionsEnabled || event.detail !== 0) {
        return;
    }

    if (port.dataset.portType === "source") {
        if (!portAvailable(state, port, null)) {
            rejectInteraction(state, portCapacityMessage(port));
            return;
        }

        beginConnection(state, port, null);
    } else if (state.connection) {
        completeConnection(state, port);
    }
}

function handleDoubleClick(state, event) {
    if (!state.options.inlineEdgeLabelEditingEnabled || event.target.closest("[data-edge-label-editor]")) {
        return;
    }

    const edge = event.target.closest("[data-edge-id]");
    if (!edge || edge.dataset.disabled === "true" || edge.dataset.selectable !== "true" ||
        edge.dataset.labelEditable !== "true") {
        return;
    }

    event.preventDefault();
    event.stopPropagation();
    state.dotNetRef.invokeMethodAsync("InvokeEdgeLabelEditRequested", edge.dataset.edgeId).catch(console.error);
}

function handleKeyDown(state, event) {
    const isEditing = event.target.matches("input,textarea,select,[contenteditable='true']");
    if (isEditing) {
        return;
    }

    if (event.key === "Escape" && state.connection) {
        event.preventDefault();
        cancelConnection(state);
        return;
    }

    const modifier = event.ctrlKey || event.metaKey;
    if (modifier && state.options.commandShortcutsEnabled) {
        const key = event.key.toLowerCase();
        if (key === "z") {
            event.preventDefault();
            state.dotNetRef.invokeMethodAsync(event.shiftKey ? "InvokeRedoRequested" : "InvokeUndoRequested").catch(console.error);
            return;
        }

        if (key === "y") {
            event.preventDefault();
            state.dotNetRef.invokeMethodAsync("InvokeRedoRequested").catch(console.error);
            return;
        }

        if (key === "d" && state.selectedNodeId) {
            event.preventDefault();
            state.dotNetRef.invokeMethodAsync("InvokeDuplicateRequested", state.selectedNodeId).catch(console.error);
            return;
        }

        if (key === "c" && state.selectedNodeId) {
            event.preventDefault();
            state.copiedNodeId = state.selectedNodeId;
            return;
        }

        if (key === "v" && state.copiedNodeId) {
            event.preventDefault();
            state.dotNetRef.invokeMethodAsync("InvokeDuplicateRequested", state.copiedNodeId).catch(console.error);
            return;
        }
    }

    if ((event.key === "Delete" || event.key === "Backspace") && state.options.deleteKeyEnabled && (state.selectedNodeId || state.selectedEdgeId)) {
        event.preventDefault();
        state.dotNetRef.invokeMethodAsync("InvokeDeleteRequested", {
            nodeId: state.selectedNodeId,
            edgeId: state.selectedEdgeId
        }).catch(console.error);
        return;
    }

    const edge = event.target.closest("[data-edge-id]");
    if (edge && event.key === "F2" && state.options.inlineEdgeLabelEditingEnabled &&
        edge.dataset.disabled !== "true" && edge.dataset.selectable === "true" &&
        edge.dataset.labelEditable === "true") {
        event.preventDefault();
        state.dotNetRef.invokeMethodAsync("InvokeEdgeLabelEditRequested", edge.dataset.edgeId).catch(console.error);
        return;
    }

    if (edge && edge.dataset.disabled !== "true" && edge.dataset.selectable === "true" && (event.key === "Enter" || event.key === " ")) {
        event.preventDefault();
        selectEdge(state, edge.dataset.edgeId);
        return;
    }

    const node = event.target.closest("[data-slot='node-editor-node']");
    if (node && event.target === node && node.dataset.selectable === "true" && (event.key === "Enter" || event.key === " ")) {
        event.preventDefault();
        selectNode(state, node.dataset.nodeId);
        return;
    }

    if (!node || !state.options.draggableNodes || !["ArrowLeft", "ArrowRight", "ArrowUp", "ArrowDown"].includes(event.key)) {
        return;
    }

    event.preventDefault();
    const amount = event.shiftKey ? 10 : 1;
    let x = number(node.dataset.nodeX);
    let y = number(node.dataset.nodeY);
    const previousX = x;
    const previousY = y;

    if (event.key === "ArrowLeft") x -= amount;
    if (event.key === "ArrowRight") x += amount;
    if (event.key === "ArrowUp") y -= amount;
    if (event.key === "ArrowDown") y += amount;

    if (state.options.snapToGrid) {
        if (event.key === "ArrowLeft" || event.key === "ArrowRight") {
            x = Math.round(x / state.options.gridSize) * state.options.gridSize;
        } else {
            y = Math.round(y / state.options.gridSize) * state.options.gridSize;
        }
    }

    setNodePosition(node, x, y);
    scheduleEdgeUpdate(state);
    state.dotNetRef.invokeMethodAsync("InvokeNodeMoved", {
        nodeId: node.dataset.nodeId,
        x,
        y,
        previousX,
        previousY
    }).catch(console.error);
}

function beginConnection(state, port, event) {
    if (state.connection) {
        cancelConnection(state);
    }

    const start = portCenterInGraph(state, port);
    state.connection = {
        mode: "create",
        edgeId: null,
        movingEndpoint: null,
        sourceNodeId: port.dataset.nodeId,
        sourcePortId: port.dataset.portId,
        sourcePlacement: port.dataset.placement,
        startX: start.x,
        startY: start.y,
        currentX: start.x,
        currentY: start.y,
        pointerId: event?.pointerId ?? null
    };

    state.preview?.classList.remove("hidden");
    port.setAttribute("aria-pressed", "true");
    markConnectionTargets(state, "target");
    updateConnectionPreview(state);
}

function beginReconnect(state, endpoint, event) {
    const edge = endpoint.closest("[data-edge-id]");
    if (!edge || edge.dataset.disabled === "true") {
        return;
    }

    const source = findPort(state, edge.dataset.sourceNode, edge.dataset.sourcePort);
    const target = findPort(state, edge.dataset.targetNode, edge.dataset.targetPort);
    if (!source || !target) {
        return;
    }

    if (state.connection) {
        cancelConnection(state);
    }

    const sourcePoint = portCenterInGraph(state, source);
    const targetPoint = portCenterInGraph(state, target);
    const movingEndpoint = endpoint.dataset.edgeEndpoint;
    state.connection = {
        mode: "change",
        edgeId: edge.dataset.edgeId,
        movingEndpoint,
        sourceNodeId: edge.dataset.sourceNode,
        sourcePortId: edge.dataset.sourcePort,
        targetNodeId: edge.dataset.targetNode,
        targetPortId: edge.dataset.targetPort,
        sourcePlacement: source.dataset.placement,
        targetPlacement: target.dataset.placement,
        startX: sourcePoint.x,
        startY: sourcePoint.y,
        endX: targetPoint.x,
        endY: targetPoint.y,
        currentX: movingEndpoint === "source" ? sourcePoint.x : targetPoint.x,
        currentY: movingEndpoint === "source" ? sourcePoint.y : targetPoint.y,
        pointerId: event.pointerId
    };

    state.preview?.classList.remove("hidden");
    endpoint.setAttribute("aria-pressed", "true");
    markConnectionTargets(state, movingEndpoint === "source" ? "source" : "target");
    updateConnectionPreview(state);
}

function finishConnection(state, event) {
    const expectedType = state.connection?.movingEndpoint === "source" ? "source" : "target";
    const port = document.elementFromPoint(event.clientX, event.clientY)?.closest(`[data-slot='node-editor-port'][data-port-type='${expectedType}']`);
    if (port && state.root.contains(port)) {
        completeConnection(state, port);
    } else {
        cancelConnection(state);
    }
}

async function completeConnection(state, port) {
    const connection = state.connection;
    if (!connection || port.dataset.disabled === "true") {
        cancelConnection(state);
        return;
    }

    const candidate = {
        edgeId: connection.edgeId,
        sourceNodeId: connection.movingEndpoint === "source" ? port.dataset.nodeId : connection.sourceNodeId,
        sourcePortId: connection.movingEndpoint === "source" ? port.dataset.portId : connection.sourcePortId,
        targetNodeId: connection.movingEndpoint === "target" || connection.mode === "create" ? port.dataset.nodeId : connection.targetNodeId,
        targetPortId: connection.movingEndpoint === "target" || connection.mode === "create" ? port.dataset.portId : connection.targetPortId
    };

    if (candidate.sourceNodeId === candidate.targetNodeId) {
        cancelConnection(state);
        rejectInteraction(state, "A node cannot connect to itself.");
        return;
    }

    const source = findPort(state, candidate.sourceNodeId, candidate.sourcePortId);
    const target = findPort(state, candidate.targetNodeId, candidate.targetPortId);
    if (!source || !target || !portAvailable(state, source, connection.edgeId) || !portAvailable(state, target, connection.edgeId)) {
        cancelConnection(state);
        rejectInteraction(state, !source || !portAvailable(state, source, connection.edgeId)
            ? portCapacityMessage(source)
            : portCapacityMessage(target));
        return;
    }

    cancelConnection(state);
    const validationSequence = ++state.validationSequence;
    state.root.dataset.connectionPending = "true";

    try {
        const validation = await state.dotNetRef.invokeMethodAsync("InvokeConnectionValidationRequested", candidate);
        if (state.destroyed || validationSequence !== state.validationSequence) {
            return;
        }

        const allowed = validation?.allowed ?? validation?.Allowed ?? true;
        if (!allowed) {
            return;
        }

        if (candidate.edgeId) {
            await state.dotNetRef.invokeMethodAsync("InvokeConnectionChangeRequested", candidate);
        } else {
            await state.dotNetRef.invokeMethodAsync("InvokeConnectionRequested", candidate);
        }
    } catch (error) {
        console.error(error);
    } finally {
        if (!state.destroyed && validationSequence === state.validationSequence) {
            state.root.dataset.connectionPending = "false";
        }
    }
}

function cancelConnection(state) {
    state.root.querySelectorAll("[aria-pressed='true'][data-slot='node-editor-port'], [aria-pressed='true'][data-edge-endpoint]")
        .forEach(element => element.removeAttribute("aria-pressed"));
    state.connection = null;
    state.preview?.classList.add("hidden");
    markConnectionTargets(state, null);
}

function markConnectionTargets(state, expectedType) {
    const fixedNodeId = expectedType === "target"
        ? state.connection?.sourceNodeId
        : state.connection?.targetNodeId;

    state.ports.forEach(port => {
        const eligible = expectedType !== null &&
            port.dataset.portType === expectedType &&
            port.dataset.nodeId !== fixedNodeId &&
            port.dataset.disabled !== "true" &&
            portAvailable(state, port, state.connection?.edgeId ?? null);
        port.dataset.connectionTarget = eligible ? "true" : "false";
    });
}

function updateConnectionPreview(state) {
    if (!state.connection || !state.preview) {
        return;
    }

    const connection = state.connection;
    state.preview.setAttribute("d", connection.movingEndpoint === "source"
        ? edgePath(connection.currentX, connection.currentY, connection.endX, connection.endY, "bottom", connection.targetPlacement)
        : edgePath(connection.startX, connection.startY, connection.currentX, connection.currentY, connection.sourcePlacement, "top"));
}

function handlePaletteDragStart(state, event) {
    const item = event.target.closest("[data-slot='node-editor-palette-item']");
    if (!item || !event.dataTransfer) {
        return;
    }

    event.dataTransfer.effectAllowed = "copy";
    event.dataTransfer.setData("application/x-quark-node", JSON.stringify({
        type: item.dataset.paletteType,
        data: item.dataset.paletteData || null
    }));
    item.dataset.dragging = "true";
}

function handlePaletteDragEnd(state, event) {
    const item = event.target.closest("[data-slot='node-editor-palette-item']");
    if (item) {
        item.dataset.dragging = "false";
    }

    state.root.dataset.dropTarget = "false";
}

function handlePaletteDragOver(state, event) {
    if (!event.dataTransfer?.types.includes("application/x-quark-node")) {
        return;
    }

    event.preventDefault();
    event.dataTransfer.dropEffect = "copy";
    state.root.dataset.dropTarget = "true";
}

function handlePaletteDrop(state, event) {
    const serialized = event.dataTransfer?.getData("application/x-quark-node");
    state.root.dataset.dropTarget = "false";
    if (!serialized) {
        return;
    }

    event.preventDefault();
    try {
        const payload = JSON.parse(serialized);
        if (!payload.type) {
            return;
        }

        const point = clientToGraph(state, event.clientX, event.clientY);
        const edge = document.elementFromPoint(event.clientX, event.clientY)?.closest("[data-edge-id]");
        state.dotNetRef.invokeMethodAsync("InvokeNodeDropRequested", {
            type: payload.type,
            data: payload.data,
            x: point.x,
            y: point.y,
            edgeId: edge && state.root.contains(edge) ? edge.dataset.edgeId : null
        }).catch(console.error);
    } catch (error) {
        console.error("Could not read dropped node editor palette data.", error);
    }
}

function selectNode(state, nodeId) {
    if (state.selectedNodeId === nodeId && state.selectedEdgeId === null) {
        return;
    }

    state.selectedNodeId = nodeId;
    state.selectedEdgeId = null;
    state.dotNetRef.invokeMethodAsync("InvokeNodeSelected", nodeId).catch(console.error);
}

function selectEdge(state, edgeId) {
    if (state.selectedEdgeId === edgeId && state.selectedNodeId === null) {
        return;
    }

    state.selectedNodeId = null;
    state.selectedEdgeId = edgeId;
    state.dotNetRef.invokeMethodAsync("InvokeEdgeSelected", edgeId).catch(console.error);
}

function setNodePosition(node, x, y) {
    node.dataset.nodeX = `${round(x)}`;
    node.dataset.nodeY = `${round(y)}`;
    node.style.transform = `translate3d(${round(x)}px, ${round(y)}px, 0)`;
}

function applyViewport(state, notify = false) {
    if (!state.viewport) {
        return;
    }

    state.viewport.style.transform = `translate3d(${round(state.panX)}px, ${round(state.panY)}px, 0) scale(${round(state.zoom)})`;
    state.root.style.setProperty("--node-editor-zoom", `${state.zoom}`);

    if (state.background) {
        const dotSpacing = 20 * state.zoom;
        state.background.style.backgroundSize = `${round(dotSpacing)}px ${round(dotSpacing)}px`;
        state.background.style.backgroundPosition = `${round(state.panX)}px ${round(state.panY)}px`;
    }

    scheduleEdgeUpdate(state);
    if (notify) {
        scheduleViewportChanged(state);
    }
}

function revealViewport(state) {
    state.root.dataset.initialized = "true";
    state.viewport?.classList.remove("opacity-0");
    state.viewport?.classList.add("opacity-100");
}

function zoomAt(state, viewportX, viewportY, requestedZoom) {
    const nextZoom = clamp(requestedZoom, state.options.minZoom, state.options.maxZoom);
    const graphX = (viewportX - state.panX) / state.zoom;
    const graphY = (viewportY - state.panY) / state.zoom;

    state.zoom = nextZoom;
    state.panX = viewportX - graphX * nextZoom;
    state.panY = viewportY - graphY * nextZoom;
    applyViewport(state, true);
}

function scheduleEdgeUpdate(state) {
    if (state.destroyed || state.edgeFrame) {
        return;
    }

    state.edgeFrame = requestAnimationFrame(() => {
        state.edgeFrame = 0;
        if (!state.destroyed) {
            updateEdges(state);
        }
    });
}

function scheduleViewportChanged(state) {
    if (state.viewportTimer) {
        clearTimeout(state.viewportTimer);
    }

    state.viewportTimer = setTimeout(() => {
        state.viewportTimer = 0;
        if (!state.destroyed) {
            state.dotNetRef.invokeMethodAsync("InvokeViewportChanged", {
                x: round(state.panX),
                y: round(state.panY),
                zoom: round(state.zoom)
            }).catch(console.error);
        }
    }, 120);
}

function rebuildGeometryIndex(state) {
    state.ports = new Map();
    state.root.querySelectorAll("[data-slot='node-editor-port']").forEach(port => {
        const key = portKey(port.dataset.nodeId, port.dataset.portId);
        if (state.ports.has(key)) {
            throw new Error(`Node editor '${state.root.id}' contains duplicate port '${port.dataset.portId}' on node '${port.dataset.nodeId}'.`);
        }

        state.ports.set(key, port);
    });

    state.nodeElements = [...state.root.querySelectorAll("[data-node-id][data-slot='node-editor-node']")];
    state.edgeElements = [...state.root.querySelectorAll("[data-edge-id]")];
    state.edgeElementsById = new Map();
    state.connectionCounts = new Map();

    state.edgeElements.forEach(edge => {
        const sourceKey = portKey(edge.dataset.sourceNode, edge.dataset.sourcePort);
        const targetKey = portKey(edge.dataset.targetNode, edge.dataset.targetPort);
        if (!state.ports.has(sourceKey)) {
            throw new Error(`Edge '${edge.dataset.edgeId}' references missing source port '${edge.dataset.sourcePort}' on node '${edge.dataset.sourceNode}'.`);
        }

        if (!state.ports.has(targetKey)) {
            throw new Error(`Edge '${edge.dataset.edgeId}' references missing target port '${edge.dataset.targetPort}' on node '${edge.dataset.targetNode}'.`);
        }

        state.edgeElementsById.set(edge.dataset.edgeId, edge);
        incrementConnectionCount(state.connectionCounts, "source", edge.dataset.sourceNode, edge.dataset.sourcePort);
        incrementConnectionCount(state.connectionCounts, "target", edge.dataset.targetNode, edge.dataset.targetPort);
    });

    state.addHandleElements = [...state.root.querySelectorAll("[data-add-handle-edge]")];
    state.addHandleElements.forEach(handle => {
        const sourceKey = portKey(handle.dataset.sourceNode, handle.dataset.sourcePort);
        if (!state.ports.has(sourceKey)) {
            throw new Error(`Add handle '${handle.dataset.addHandleEdge}' references missing source port '${handle.dataset.sourcePort}' on node '${handle.dataset.sourceNode}'.`);
        }
    });
}

function updateEdges(state) {
    if (!state.edgeLayer) {
        return;
    }

    const rootRect = state.root.getBoundingClientRect();
    const portCenters = new Map();
    const getPortCenter = port => {
        let center = portCenters.get(port);
        if (!center) {
            center = portCenterInGraph(state, port, rootRect);
            portCenters.set(port, center);
        }
        return center;
    };

    state.edgeElements.forEach(edge => {
        const source = state.ports.get(portKey(edge.dataset.sourceNode, edge.dataset.sourcePort));
        const target = state.ports.get(portKey(edge.dataset.targetNode, edge.dataset.targetPort));
        const path = edge.querySelector("[data-edge-path]");
        const hit = edge.querySelector("[data-edge-hit]");

        if (!source || !target || !path || !hit) {
            edge.style.display = "none";
            return;
        }

        edge.style.display = "";
        const start = getPortCenter(source);
        const end = getPortCenter(target);
        const d = edgePath(start.x, start.y, end.x, end.y, source.dataset.placement, target.dataset.placement);

        path.setAttribute("d", d);
        hit.setAttribute("d", d);
        const sourceEndpoint = edge.querySelector("[data-edge-endpoint='source']");
        const targetEndpoint = edge.querySelector("[data-edge-endpoint='target']");
        sourceEndpoint?.setAttribute("cx", `${round(start.x)}`);
        sourceEndpoint?.setAttribute("cy", `${round(start.y)}`);
        targetEndpoint?.setAttribute("cx", `${round(end.x)}`);
        targetEndpoint?.setAttribute("cy", `${round(end.y)}`);

        const label = edge.querySelector("[data-edge-label]");
        positionPathLabel(path, label);
    });

    state.addHandleElements.forEach(placeholder => {
        const source = state.ports.get(portKey(placeholder.dataset.sourceNode, placeholder.dataset.sourcePort));
        const path = placeholder.querySelector("[data-add-handle-path]");
        if (!source || !path) {
            placeholder.style.display = "none";
            return;
        }

        placeholder.style.display = "";
        const start = getPortCenter(source);
        const targetX = number(placeholder.dataset.targetX);
        const targetY = number(placeholder.dataset.targetY);
        path.setAttribute("d", edgePath(start.x, start.y, targetX, targetY, source.dataset.placement, oppositePlacement(source.dataset.placement)));
        positionPathLabel(path, placeholder.querySelector("[data-edge-label]"));
    });
}

function portKey(nodeId, portId) {
    return `${nodeId ?? ""}\u0000${portId ?? ""}`;
}

function portCenterInGraph(state, port, rootRect = state.root.getBoundingClientRect()) {
    const rect = port.getBoundingClientRect();
    return {
        x: ((rect.left + rect.width / 2) - rootRect.left - state.panX) / state.zoom,
        y: ((rect.top + rect.height / 2) - rootRect.top - state.panY) / state.zoom
    };
}

function findPort(state, nodeId, portId) {
    return state.ports.get(portKey(nodeId, portId)) ?? null;
}

function updatePortCapacities(state) {
    state.ports.forEach(port => {
        const atCapacity = !portAvailable(state, port, null);
        port.dataset.atCapacity = atCapacity ? "true" : "false";
        if (atCapacity) {
            port.setAttribute("aria-description", portCapacityMessage(port));
        } else {
            port.removeAttribute("aria-description");
        }
    });
}

function portAvailable(state, port, excludedEdgeId) {
    if (!port || port.dataset.disabled === "true") {
        return false;
    }

    const maximum = Number.parseInt(port.dataset.maxConnections, 10);
    if (!Number.isFinite(maximum)) {
        return true;
    }

    const nodeId = port.dataset.nodeId;
    const portId = port.dataset.portId;
    const endpoint = port.dataset.portType === "source" ? "source" : "target";
    let count = state.connectionCounts.get(connectionCountKey(endpoint, nodeId, portId)) ?? 0;
    const excludedEdge = excludedEdgeId ? state.edgeElementsById.get(excludedEdgeId) : null;
    if (excludedEdge && excludedEdge.dataset[`${endpoint}Node`] === nodeId && excludedEdge.dataset[`${endpoint}Port`] === portId) {
        count--;
    }
    return count < maximum;
}

function connectionCountKey(endpoint, nodeId, portId) {
    return `${endpoint}\u0000${portKey(nodeId, portId)}`;
}

function incrementConnectionCount(counts, endpoint, nodeId, portId) {
    const key = connectionCountKey(endpoint, nodeId, portId);
    counts.set(key, (counts.get(key) ?? 0) + 1);
}

function portCapacityMessage(port) {
    if (!port) {
        return "That connection endpoint is unavailable.";
    }

    const label = port.getAttribute("aria-label") || "This port";
    return `${label} has reached its connection limit.`;
}

function rejectInteraction(state, message) {
    state.dotNetRef.invokeMethodAsync("InvokeInteractionRejected", message).catch(console.error);
}

function positionPathLabel(path, label) {
    if (!label || !path.getAttribute("d")) {
        return;
    }

    const middle = path.getPointAtLength(path.getTotalLength() / 2);
    label.setAttribute("transform", `translate(${round(middle.x)} ${round(middle.y)})`);

    const text = label.querySelector("[data-edge-label-text]");
    const background = label.querySelector("[data-edge-label-background]");
    if (text && background) {
        const measuredWidth = typeof text.getComputedTextLength === "function" ? text.getComputedTextLength() : 0;
        const width = Math.max(30, measuredWidth > 0 ? measuredWidth + 16 : text.textContent.trim().length * 6.8 + 16);
        background.setAttribute("x", `${-width / 2}`);
        background.setAttribute("width", `${width}`);
    }
}

function focusEdgeLabelEditor(state) {
    const input = state.root.querySelector("[data-edge-label-input]");
    if (!input || input.dataset.editorFocused === "true") {
        return;
    }

    input.dataset.editorFocused = "true";
    requestAnimationFrame(() => {
        if (!state.destroyed && input.isConnected) {
            input.focus({ preventScroll: true });
            input.select();
        }
    });
}

function clientToGraph(state, clientX, clientY) {
    const rect = state.root.getBoundingClientRect();
    return {
        x: (clientX - rect.left - state.panX) / state.zoom,
        y: (clientY - rect.top - state.panY) / state.zoom
    };
}

function edgePath(x1, y1, x2, y2, sourcePlacement, targetPlacement) {
    const source = direction(sourcePlacement);
    const target = direction(targetPlacement);
    const distance = Math.hypot(x2 - x1, y2 - y1);
    const preferredLead = clamp(distance * 0.12, 24, 48);
    const placementsOppose = source.x === -target.x && source.y === -target.y;
    const forwardDistance = (x2 - x1) * source.x + (y2 - y1) * source.y;
    const lead = placementsOppose && forwardDistance > 0
        ? Math.min(preferredLead, forwardDistance / 3)
        : preferredLead;
    const startLead = { x: x1 + source.x * lead, y: y1 + source.y * lead };
    const endLead = { x: x2 + target.x * lead, y: y2 + target.y * lead };
    const points = [{ x: x1, y: y1 }, startLead];

    const sourceIsHorizontal = source.x !== 0;
    const targetIsHorizontal = target.x !== 0;

    if (sourceIsHorizontal === targetIsHorizontal) {
        if (sourceIsHorizontal) {
            const targetIsAhead = (endLead.x - startLead.x) * source.x > 0;
            if (targetIsAhead) {
                const middleX = (startLead.x + endLead.x) / 2;
                points.push({ x: middleX, y: startLead.y }, { x: middleX, y: endLead.y });
            } else {
                const side = y2 === y1 ? 1 : Math.sign(y2 - y1);
                const middleY = y1 + side * Math.max(72, lead * 1.5);
                points.push({ x: startLead.x, y: middleY }, { x: endLead.x, y: middleY });
            }
        } else {
            const targetIsAhead = (endLead.y - startLead.y) * source.y > 0;
            if (targetIsAhead) {
                const middleY = (startLead.y + endLead.y) / 2;
                points.push({ x: startLead.x, y: middleY }, { x: endLead.x, y: middleY });
            } else {
                const side = x2 === x1 ? 1 : Math.sign(x2 - x1);
                const middleX = x1 + side * Math.max(72, lead * 1.5);
                points.push({ x: middleX, y: startLead.y }, { x: middleX, y: endLead.y });
            }
        }
    } else {
        points.push({ x: endLead.x, y: startLead.y });
    }

    points.push(endLead, { x: x2, y: y2 });
    return roundedOrthogonalPath(points, 12);
}

function roundedOrthogonalPath(points, requestedRadius) {
    const cleaned = [];

    points.forEach(point => {
        const previous = cleaned[cleaned.length - 1];
        if (!previous || previous.x !== point.x || previous.y !== point.y) {
            cleaned.push(point);
        }
    });

    for (let index = cleaned.length - 2; index > 0; index--) {
        const previous = cleaned[index - 1];
        const current = cleaned[index];
        const next = cleaned[index + 1];
        const continuesVertically = previous.x === current.x && current.x === next.x &&
            (current.y - previous.y) * (next.y - current.y) > 0;
        const continuesHorizontally = previous.y === current.y && current.y === next.y &&
            (current.x - previous.x) * (next.x - current.x) > 0;
        if (continuesVertically || continuesHorizontally) {
            cleaned.splice(index, 1);
        }
    }

    if (cleaned.length < 2) {
        return "";
    }

    let path = `M ${round(cleaned[0].x)} ${round(cleaned[0].y)}`;

    for (let index = 1; index < cleaned.length - 1; index++) {
        const previous = cleaned[index - 1];
        const current = cleaned[index];
        const next = cleaned[index + 1];
        const incomingLength = Math.hypot(current.x - previous.x, current.y - previous.y);
        const outgoingLength = Math.hypot(next.x - current.x, next.y - current.y);
        const radius = Math.min(requestedRadius, incomingLength / 2, outgoingLength / 2);
        const before = {
            x: current.x + (previous.x - current.x) / incomingLength * radius,
            y: current.y + (previous.y - current.y) / incomingLength * radius
        };
        const after = {
            x: current.x + (next.x - current.x) / outgoingLength * radius,
            y: current.y + (next.y - current.y) / outgoingLength * radius
        };

        path += ` L ${round(before.x)} ${round(before.y)} Q ${round(current.x)} ${round(current.y)} ${round(after.x)} ${round(after.y)}`;
    }

    const end = cleaned[cleaned.length - 1];
    return `${path} L ${round(end.x)} ${round(end.y)}`;
}

function direction(placement) {
    if (placement === "top") return { x: 0, y: -1 };
    if (placement === "right") return { x: 1, y: 0 };
    if (placement === "left") return { x: -1, y: 0 };
    return { x: 0, y: 1 };
}

function oppositePlacement(placement) {
    if (placement === "top") return "bottom";
    if (placement === "right") return "left";
    if (placement === "left") return "right";
    return "top";
}

function normalizeOptions(options) {
    const get = (camel, pascal, fallback) => options[camel] ?? options[pascal] ?? fallback;
    const minZoom = Math.max(0.01, finiteNumber(get("minZoom", "MinZoom", 0.35), 0.35));
    const maxZoom = Math.max(minZoom, finiteNumber(get("maxZoom", "MaxZoom", 2), 2));
    return {
        draggableNodes: get("draggableNodes", "DraggableNodes", true),
        panEnabled: get("panEnabled", "PanEnabled", true),
        zoomEnabled: get("zoomEnabled", "ZoomEnabled", true),
        connectionsEnabled: get("connectionsEnabled", "ConnectionsEnabled", true),
        deleteKeyEnabled: get("deleteKeyEnabled", "DeleteKeyEnabled", true),
        commandShortcutsEnabled: get("commandShortcutsEnabled", "CommandShortcutsEnabled", true),
        inlineEdgeLabelEditingEnabled: get("inlineEdgeLabelEditingEnabled", "InlineEdgeLabelEditingEnabled", true),
        minZoom,
        maxZoom,
        initialZoom: finiteNumber(get("initialZoom", "InitialZoom", 1), 1),
        initialX: finiteNumber(get("initialX", "InitialX", 0), 0),
        initialY: finiteNumber(get("initialY", "InitialY", 0), 0),
        gridSize: Math.max(1, finiteNumber(get("gridSize", "GridSize", 16), 16)),
        snapToGrid: get("snapToGrid", "SnapToGrid", false),
        fitViewOnInitialize: get("fitViewOnInitialize", "FitViewOnInitialize", false)
    };
}

function finiteNumber(value, fallback) {
    const parsed = Number(value);
    return Number.isFinite(parsed) ? parsed : fallback;
}

function number(value) {
    const parsed = Number.parseFloat(value);
    return Number.isFinite(parsed) ? parsed : 0;
}

function round(value) {
    return Math.round(value * 1000) / 1000;
}

function clamp(value, minimum, maximum) {
    return Math.max(minimum, Math.min(maximum, value));
}
