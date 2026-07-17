const floatingWindows = new Map();
let nextZIndex = 1000;
let viewportResizeFrame = 0;
let viewportListenersAttached = false;

export function create(id, optionsJson) {
    const element = document.getElementById(id);

    if (!element) {
        console.error("FloatingWindowInterop.create: Element not found", id);
        return;
    }

    if (floatingWindows.has(id)) {
        destroy(id);
    }

    const options = normalizeOptions(JSON.parse(optionsJson));
    const windowData = {
        element,
        options,
        dotNetRef: null,
        isOpen: false,
        isDragging: false,
        isResizing: false,
        activePointerId: null,
        dragStart: { x: 0, y: 0 },
        resizeStart: { width: 0, height: 0, x: 0, y: 0, pointerX: 0, pointerY: 0 },
        resizeDirection: null,
        resizeObserver: null,
        cleanupDragging: null,
        cleanupResizing: [],
        cleanupActivation: null,
        cleanupActiveInteraction: null,
        showFrame: 0
    };

    floatingWindows.set(id, windowData);
    attachViewportListeners();

    element.style.position = "fixed";
    applyZIndex(windowData, options.zIndex);
    applySizing(windowData);
    applyInitialPosition(windowData);
    configureResizeObserver(id);
    setupActivation(id);
    configureInteractions(id);

    if (!options.enabled) {
        setHiddenWithoutCallback(windowData);
    }

    if (options.constrainToViewport) {
        reconcileToViewport(windowData);
    }
}

export function updateOptions(id, optionsJson) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const previous = windowData.options;
    const next = normalizeOptions(JSON.parse(optionsJson));
    windowData.options = next;

    cancelActiveInteraction(windowData, true);

    if (previous.zIndex !== next.zIndex) {
        applyZIndex(windowData, next.zIndex);
    }

    if (sizingChanged(previous, next)) {
        applySizing(windowData);
    }

    if (previous.initialX !== next.initialX && next.initialX !== undefined) {
        windowData.element.style.left = `${next.initialX}px`;
    }

    if (previous.initialY !== next.initialY && next.initialY !== undefined) {
        windowData.element.style.top = `${next.initialY}px`;
    }

    configureResizeObserver(id);
    configureInteractions(id);

    if (!next.enabled) {
        setHiddenWithoutCallback(windowData);
        return;
    }

    if (next.constrainToViewport) {
        reconcileToViewport(windowData);
    }
}

function normalizeOptions(options) {
    const read = (camelName, pascalName, fallback = undefined) => options?.[camelName] ?? options?.[pascalName] ?? fallback;

    return {
        draggable: read("draggable", "Draggable", true),
        resizable: read("resizable", "Resizable", true),
        showCloseButton: read("showCloseButton", "ShowCloseButton", true),
        showTitleBar: read("showTitleBar", "ShowTitleBar", true),
        enabled: read("enabled", "Enabled", true),
        width: read("width", "Width", 400),
        height: read("height", "Height", 300),
        initialX: read("initialX", "InitialX", 100),
        initialY: read("initialY", "InitialY", 100),
        minWidth: read("minWidth", "MinWidth", 200),
        minHeight: read("minHeight", "MinHeight", 150),
        maxWidth: read("maxWidth", "MaxWidth"),
        maxHeight: read("maxHeight", "MaxHeight"),
        constrainToViewport: read("constrainToViewport", "ConstrainToViewport", true),
        centerOnShow: read("centerOnShow", "CenterOnShow", true),
        focusOnShow: read("focusOnShow", "FocusOnShow", true),
        useCdn: read("useCdn", "UseCdn", true),
        autoSizeToContent: read("autoSizeToContent", "AutoSizeToContent", true),
        dynamicAutoSizeToContent: read("dynamicAutoSizeToContent", "DynamicAutoSizeToContent", false),
        recenterOnResize: read("recenterOnResize", "RecenterOnResize", false),
        zIndex: read("zIndex", "ZIndex", 1000)
    };
}

function applyZIndex(windowData, requestedZIndex) {
    const zIndex = Number.isFinite(requestedZIndex) ? requestedZIndex : nextZIndex;
    windowData.element.style.zIndex = `${zIndex}`;
    nextZIndex = Math.max(nextZIndex, zIndex + 1);
}

function applySizing(windowData) {
    const { element, options } = windowData;
    const viewport = getViewportSize();
    const minWidth = Math.min(options.minWidth, viewport.width);
    const minHeight = Math.min(options.minHeight, viewport.height);

    element.style.minWidth = `${Math.max(0, minWidth)}px`;
    element.style.minHeight = `${Math.max(0, minHeight)}px`;

    if (options.autoSizeToContent) {
        element.style.width = "auto";
        element.style.height = "auto";
    } else {
        element.style.width = options.width == null ? "" : `${options.width}px`;
        element.style.height = options.height == null ? "" : `${options.height}px`;
    }

    applyMaximumDimensions(windowData, viewport);
}

function applyMaximumDimensions(windowData, viewport = getViewportSize()) {
    const { element, options } = windowData;
    const maxWidth = options.constrainToViewport
        ? Math.min(options.maxWidth ?? viewport.width, viewport.width)
        : options.maxWidth;
    const maxHeight = options.constrainToViewport
        ? Math.min(options.maxHeight ?? viewport.height, viewport.height)
        : options.maxHeight;

    element.style.maxWidth = maxWidth == null ? "" : `${Math.max(0, maxWidth)}px`;
    element.style.maxHeight = maxHeight == null ? "" : `${Math.max(0, maxHeight)}px`;
}

function applyInitialPosition(windowData) {
    const { element, options } = windowData;

    if (options.initialX !== undefined) {
        element.style.left = `${options.initialX}px`;
    }

    if (options.initialY !== undefined) {
        element.style.top = `${options.initialY}px`;
    }
}

function sizingChanged(previous, next) {
    return previous.autoSizeToContent !== next.autoSizeToContent ||
        previous.width !== next.width ||
        previous.height !== next.height ||
        previous.minWidth !== next.minWidth ||
        previous.minHeight !== next.minHeight ||
        previous.maxWidth !== next.maxWidth ||
        previous.maxHeight !== next.maxHeight ||
        previous.constrainToViewport !== next.constrainToViewport;
}

function configureInteractions(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.cleanupDragging?.();
    windowData.cleanupDragging = null;
    windowData.cleanupResizing?.forEach(cleanup => cleanup());
    windowData.cleanupResizing = [];

    if (windowData.options.draggable) {
        setupDragging(id);
    }

    if (windowData.options.resizable) {
        setupResizing(id);
    }
}

function setupActivation(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.cleanupActivation?.();

    const onPointerDown = event => {
        if (isPrimaryInteraction(event) && windowData.options.enabled) {
            bringToFront(id);
        }
    };

    windowData.element.addEventListener("pointerdown", onPointerDown);
    windowData.cleanupActivation = () => windowData.element.removeEventListener("pointerdown", onPointerDown);
}

function setupDragging(id) {
    const windowData = floatingWindows.get(id);
    const titleBar = windowData?.element.querySelector(".quark-floating-window-titlebar");

    if (!windowData || !titleBar) {
        return;
    }

    const onPointerDown = event => {
        if (!isPrimaryInteraction(event) || isInteractiveTitleBarTarget(event.target)) {
            return;
        }

        event.preventDefault();
        startDragging(id, event);
    };

    titleBar.addEventListener("pointerdown", onPointerDown);
    titleBar.style.cursor = "move";
    titleBar.style.touchAction = "none";
    windowData.cleanupDragging = () => {
        titleBar.removeEventListener("pointerdown", onPointerDown);
        titleBar.style.touchAction = "";
    };
}

function setupResizing(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const resizeHandles = windowData.element.querySelectorAll(".quark-floating-window-resize-handle");

    resizeHandles.forEach(handle => {
        const direction = handle.dataset.direction;
        const onPointerDown = event => {
            if (!isPrimaryInteraction(event)) {
                return;
            }

            event.preventDefault();
            startResizing(id, event, direction);
        };

        handle.addEventListener("pointerdown", onPointerDown);
        handle.style.touchAction = "none";
        windowData.cleanupResizing.push(() => {
            handle.removeEventListener("pointerdown", onPointerDown);
            handle.style.touchAction = "";
        });
    });
}

function isPrimaryInteraction(event) {
    return event.isPrimary !== false && (event.pointerType === "touch" || event.button === 0);
}

function isInteractiveTitleBarTarget(target) {
    return target instanceof Element && target.closest("button, a, input, textarea, select, [role='button'], [contenteditable='true']") !== null;
}

function startDragging(id, event) {
    const windowData = floatingWindows.get(id);

    if (!windowData || !windowData.options.enabled) {
        return;
    }

    cancelActiveInteraction(windowData, false);
    windowData.isDragging = true;
    windowData.activePointerId = event.pointerId;
    windowData.dragStart = {
        x: event.clientX - windowData.element.offsetLeft,
        y: event.clientY - windowData.element.offsetTop
    };

    windowData.dotNetRef?.invokeMethodAsync("InvokeOnDragStart").catch(console.error);

    const onPointerMove = moveEvent => {
        if (moveEvent.pointerId === windowData.activePointerId && windowData.isDragging) {
            moveEvent.preventDefault();
            updateDragPosition(id, moveEvent);
        }
    };

    const onPointerEnd = endEvent => {
        if (endEvent.pointerId !== windowData.activePointerId) {
            return;
        }

        finishDragging(windowData, true);
    };

    document.addEventListener("pointermove", onPointerMove, { passive: false });
    document.addEventListener("pointerup", onPointerEnd);
    document.addEventListener("pointercancel", onPointerEnd);
    windowData.cleanupActiveInteraction = () => {
        document.removeEventListener("pointermove", onPointerMove);
        document.removeEventListener("pointerup", onPointerEnd);
        document.removeEventListener("pointercancel", onPointerEnd);
    };
}

function startResizing(id, event, direction) {
    const windowData = floatingWindows.get(id);

    if (!windowData || !windowData.options.enabled || !direction) {
        return;
    }

    cancelActiveInteraction(windowData, false);
    windowData.isResizing = true;
    windowData.activePointerId = event.pointerId;
    windowData.resizeDirection = direction;
    windowData.resizeStart = {
        width: windowData.element.offsetWidth,
        height: windowData.element.offsetHeight,
        x: parseInt(windowData.element.style.left, 10) || 0,
        y: parseInt(windowData.element.style.top, 10) || 0,
        pointerX: event.clientX,
        pointerY: event.clientY
    };

    const onPointerMove = moveEvent => {
        if (moveEvent.pointerId === windowData.activePointerId && windowData.isResizing) {
            moveEvent.preventDefault();
            updateResizePosition(id, moveEvent);
        }
    };

    const onPointerEnd = endEvent => {
        if (endEvent.pointerId !== windowData.activePointerId) {
            return;
        }

        finishResizing(windowData);
    };

    document.addEventListener("pointermove", onPointerMove, { passive: false });
    document.addEventListener("pointerup", onPointerEnd);
    document.addEventListener("pointercancel", onPointerEnd);
    windowData.cleanupActiveInteraction = () => {
        document.removeEventListener("pointermove", onPointerMove);
        document.removeEventListener("pointerup", onPointerEnd);
        document.removeEventListener("pointercancel", onPointerEnd);
    };
}

function finishDragging(windowData, notify) {
    const wasDragging = windowData.isDragging;
    windowData.isDragging = false;
    windowData.activePointerId = null;
    windowData.cleanupActiveInteraction?.();
    windowData.cleanupActiveInteraction = null;

    if (notify && wasDragging) {
        windowData.dotNetRef?.invokeMethodAsync("InvokeOnDragEnd").catch(console.error);
    }
}

function finishResizing(windowData) {
    windowData.isResizing = false;
    windowData.resizeDirection = null;
    windowData.activePointerId = null;
    windowData.cleanupActiveInteraction?.();
    windowData.cleanupActiveInteraction = null;
}

function cancelActiveInteraction(windowData, notifyDragEnd) {
    if (windowData.isDragging) {
        finishDragging(windowData, notifyDragEnd);
    } else if (windowData.isResizing) {
        finishResizing(windowData);
    } else {
        windowData.cleanupActiveInteraction?.();
        windowData.cleanupActiveInteraction = null;
        windowData.activePointerId = null;
    }
}

function updateDragPosition(id, event) {
    const windowData = floatingWindows.get(id);

    if (!windowData?.isDragging) {
        return;
    }

    let newX = event.clientX - windowData.dragStart.x;
    let newY = event.clientY - windowData.dragStart.y;

    if (windowData.options.constrainToViewport) {
        const viewport = getViewportSize();
        const rect = windowData.element.getBoundingClientRect();
        newX = clamp(newX, 0, Math.max(0, viewport.width - rect.width));
        newY = clamp(newY, 0, Math.max(0, viewport.height - rect.height));
    }

    windowData.element.style.left = `${newX}px`;
    windowData.element.style.top = `${newY}px`;
}

function updateResizePosition(id, event) {
    const windowData = floatingWindows.get(id);

    if (!windowData?.isResizing) {
        return;
    }

    const { options, resizeStart } = windowData;
    const deltaX = event.clientX - resizeStart.pointerX;
    const deltaY = event.clientY - resizeStart.pointerY;
    const direction = windowData.resizeDirection;
    const viewport = getViewportSize();
    const minWidth = Math.min(options.minWidth, viewport.width);
    const minHeight = Math.min(options.minHeight, viewport.height);
    const maxWidth = Math.min(options.maxWidth ?? Number.POSITIVE_INFINITY, options.constrainToViewport ? viewport.width : Number.POSITIVE_INFINITY);
    const maxHeight = Math.min(options.maxHeight ?? Number.POSITIVE_INFINITY, options.constrainToViewport ? viewport.height : Number.POSITIVE_INFINITY);
    let newWidth = resizeStart.width;
    let newHeight = resizeStart.height;
    let newX = resizeStart.x;
    let newY = resizeStart.y;

    if (direction.includes("e")) {
        newWidth = clamp(resizeStart.width + deltaX, minWidth, maxWidth);
    }

    if (direction.includes("w")) {
        newWidth = clamp(resizeStart.width - deltaX, minWidth, maxWidth);
        newX = resizeStart.x + resizeStart.width - newWidth;
    }

    if (direction.includes("s")) {
        newHeight = clamp(resizeStart.height + deltaY, minHeight, maxHeight);
    }

    if (direction.includes("n")) {
        newHeight = clamp(resizeStart.height - deltaY, minHeight, maxHeight);
        newY = resizeStart.y + resizeStart.height - newHeight;
    }

    if (options.constrainToViewport) {
        newX = clamp(newX, 0, Math.max(0, viewport.width - minWidth));
        newY = clamp(newY, 0, Math.max(0, viewport.height - minHeight));
        newWidth = Math.min(newWidth, viewport.width - newX);
        newHeight = Math.min(newHeight, viewport.height - newY);
    }

    windowData.element.style.width = `${newWidth}px`;
    windowData.element.style.height = `${newHeight}px`;
    windowData.element.style.left = `${newX}px`;
    windowData.element.style.top = `${newY}px`;
}

function configureResizeObserver(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.resizeObserver?.disconnect();
    windowData.resizeObserver = null;

    if (!windowData.options.dynamicAutoSizeToContent) {
        return;
    }

    const content = windowData.element.querySelector(".quark-floating-window-content");

    if (!content) {
        return;
    }

    windowData.resizeObserver = new ResizeObserver(() => {
        windowData.element.style.width = "auto";
        windowData.element.style.height = "auto";

        if (windowData.options.constrainToViewport) {
            reconcileToViewport(windowData);
        }

        if (windowData.options.recenterOnResize && windowData.dotNetRef) {
            windowData.dotNetRef.invokeMethodAsync("OnContentResized").catch(console.error);
        }
    });
    windowData.resizeObserver.observe(content);
}

export function setCallbacks(id, dotNetRef) {
    const windowData = floatingWindows.get(id);

    if (windowData) {
        windowData.dotNetRef = dotNetRef;
    }
}

export function show(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData?.options.enabled) {
        return;
    }

    if (windowData.isOpen) {
        bringToFront(id);
        focusWindow(windowData);
        return;
    }

    cancelShowFrame(windowData);
    const element = windowData.element;
    element.style.display = "flex";

    if (windowData.options.centerOnShow) {
        element.style.visibility = "hidden";
        element.style.left = "-9999px";
        element.style.top = "-9999px";
        windowData.showFrame = requestAnimationFrame(() => {
            windowData.showFrame = 0;
            centerVisibleWindow(windowData);
            finishShow(id, windowData);
        });
        return;
    }

    finishShow(id, windowData);
}

function finishShow(id, windowData) {
    if (!windowData.options.enabled) {
        setHiddenWithoutCallback(windowData);
        return;
    }

    if (windowData.options.constrainToViewport) {
        reconcileToViewport(windowData);
    }

    windowData.element.style.visibility = "visible";
    windowData.element.dataset.state = "open";
    windowData.isOpen = true;
    bringToFront(id);
    focusWindow(windowData);
    windowData.dotNetRef?.invokeMethodAsync("InvokeOnShow").catch(console.error);
}

function focusWindow(windowData) {
    if (windowData.options.focusOnShow) {
        windowData.element.focus({ preventScroll: true });
    }
}

export function hide(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const wasOpen = windowData.isOpen || windowData.showFrame !== 0;
    cancelShowFrame(windowData);
    cancelActiveInteraction(windowData, true);
    setHiddenWithoutCallback(windowData);

    if (wasOpen) {
        windowData.dotNetRef?.invokeMethodAsync("InvokeOnHide").catch(console.error);
    }
}

function setHiddenWithoutCallback(windowData) {
    cancelShowFrame(windowData);
    windowData.element.style.display = "none";
    windowData.element.style.visibility = "hidden";
    windowData.element.dataset.state = "closed";
    windowData.isOpen = false;
}

function cancelShowFrame(windowData) {
    if (windowData.showFrame) {
        cancelAnimationFrame(windowData.showFrame);
        windowData.showFrame = 0;
    }
}

export function toggle(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    if (windowData.isOpen || windowData.showFrame) {
        hide(id);
    } else {
        show(id);
    }
}

export function close(id) {
    hide(id);
}

export function destroy(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    cancelShowFrame(windowData);
    cancelActiveInteraction(windowData, false);
    windowData.cleanupDragging?.();
    windowData.cleanupResizing?.forEach(cleanup => cleanup());
    windowData.cleanupActivation?.();
    windowData.resizeObserver?.disconnect();
    floatingWindows.delete(id);
    detachViewportListenersIfUnused();
}

export function getPosition(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return { x: 0, y: 0 };
    }

    return {
        x: parseInt(windowData.element.style.left, 10) || 0,
        y: parseInt(windowData.element.style.top, 10) || 0
    };
}

export function setPosition(id, x, y) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.element.style.left = `${x}px`;
    windowData.element.style.top = `${y}px`;

    if (windowData.options.constrainToViewport) {
        reconcileToViewport(windowData);
    }
}

export function getSize(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return { width: 0, height: 0 };
    }

    return {
        width: windowData.element.offsetWidth,
        height: windowData.element.offsetHeight
    };
}

export function setSize(id, width, height) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.element.style.width = `${width}px`;
    windowData.element.style.height = `${height}px`;

    if (windowData.options.constrainToViewport) {
        reconcileToViewport(windowData);
    }
}

export function bringToFront(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const currentZIndex = parseInt(windowData.element.style.zIndex, 10) || 0;
    nextZIndex = Math.max(nextZIndex, currentZIndex + 1);
    windowData.element.style.zIndex = `${nextZIndex++}`;
}

export function getViewportSize() {
    const viewport = window.visualViewport;
    return {
        width: Math.round(viewport?.width ?? window.innerWidth),
        height: Math.round(viewport?.height ?? window.innerHeight)
    };
}

export function centerInViewport(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const element = windowData.element;
    const previousDisplay = element.style.display;
    const previousVisibility = element.style.visibility;
    element.style.display = "flex";
    element.style.visibility = "hidden";
    centerVisibleWindow(windowData);
    element.style.display = previousDisplay;
    element.style.visibility = previousVisibility;
}

function centerVisibleWindow(windowData) {
    if (windowData.options.constrainToViewport) {
        reconcileToViewport(windowData);
    }

    const viewport = getViewportSize();
    const x = Math.max(0, Math.round((viewport.width - windowData.element.offsetWidth) / 2));
    const y = Math.max(0, Math.round((viewport.height - windowData.element.offsetHeight) / 2));
    windowData.element.style.left = `${x}px`;
    windowData.element.style.top = `${y}px`;
}

function reconcileToViewport(windowData) {
    if (!windowData.options.constrainToViewport) {
        return;
    }

    const { element, options } = windowData;
    const viewport = getViewportSize();
    element.style.minWidth = `${Math.max(0, Math.min(options.minWidth, viewport.width))}px`;
    element.style.minHeight = `${Math.max(0, Math.min(options.minHeight, viewport.height))}px`;
    applyMaximumDimensions(windowData, viewport);

    // Preserve the configured or last-known position while the window is not measurable.
    // It will be reconciled after display is enabled by show().
    if (element.getClientRects().length === 0) {
        return;
    }

    if (!options.autoSizeToContent && element.offsetWidth > viewport.width) {
        element.style.width = `${viewport.width}px`;
    }

    if (!options.autoSizeToContent && element.offsetHeight > viewport.height) {
        element.style.height = `${viewport.height}px`;
    }

    const rect = element.getBoundingClientRect();
    const x = clamp(rect.left, 0, Math.max(0, viewport.width - rect.width));
    const y = clamp(rect.top, 0, Math.max(0, viewport.height - rect.height));
    element.style.left = `${Math.round(x)}px`;
    element.style.top = `${Math.round(y)}px`;
}

function attachViewportListeners() {
    if (viewportListenersAttached) {
        return;
    }

    window.addEventListener("resize", scheduleViewportReconciliation);
    window.visualViewport?.addEventListener("resize", scheduleViewportReconciliation);
    viewportListenersAttached = true;
}

function detachViewportListenersIfUnused() {
    if (!viewportListenersAttached || floatingWindows.size !== 0) {
        return;
    }

    window.removeEventListener("resize", scheduleViewportReconciliation);
    window.visualViewport?.removeEventListener("resize", scheduleViewportReconciliation);
    cancelAnimationFrame(viewportResizeFrame);
    viewportResizeFrame = 0;
    viewportListenersAttached = false;
}

function scheduleViewportReconciliation() {
    cancelAnimationFrame(viewportResizeFrame);
    viewportResizeFrame = requestAnimationFrame(() => {
        viewportResizeFrame = 0;

        floatingWindows.forEach(windowData => {
            if (windowData.options.enabled && windowData.options.constrainToViewport) {
                reconcileToViewport(windowData);
            }
        });
    });
}

function clamp(value, minimum, maximum) {
    return Math.max(minimum, Math.min(value, maximum));
}
