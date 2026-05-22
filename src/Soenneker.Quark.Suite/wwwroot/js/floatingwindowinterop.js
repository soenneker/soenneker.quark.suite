const floatingWindows = new Map();
let nextZIndex = 1000;

export async function create(id, optionsJson) {
    const options = normalizeOptions(JSON.parse(optionsJson));
    const element = document.getElementById(id);

    if (!element) {
        console.error("FloatingWindowInterop.create: Element not found", id);
        return;
    }

    if (!options?.enabled) {
        element.style.display = "none";
        return;
    }

    element.style.position = "fixed";
    element.style.zIndex = options.zIndex || nextZIndex++;

    if (options.autoSizeToContent) {
        element.style.width = "auto";
        element.style.height = "auto";
        element.style.maxWidth = "";
        element.style.maxHeight = "";
        element.style.minWidth = "";
        element.style.minHeight = "";
    } else {
        if (options.width) {
            element.style.width = `${options.width}px`;
        }

        if (options.height) {
            element.style.height = `${options.height}px`;
        }
    }

    if (options.initialX !== undefined) {
        element.style.left = `${options.initialX}px`;
    }

    if (options.initialY !== undefined) {
        element.style.top = `${options.initialY}px`;
    }

    let resizeObserver = null;

    if (options.dynamicAutoSizeToContent) {
        const content = element.querySelector(".quark-floating-window-content");

        if (content) {
            resizeObserver = new ResizeObserver(() => {
                element.style.width = "auto";
                element.style.height = "auto";

                const windowData = floatingWindows.get(id);

                if (options.recenterOnResize && windowData?.dotNetRef) {
                    windowData.dotNetRef.invokeMethodAsync("OnContentResized").catch(console.error);
                }
            });

            resizeObserver.observe(content);
        }
    }

    floatingWindows.set(id, {
        element,
        options,
        isDragging: false,
        isResizing: false,
        dragStart: { x: 0, y: 0 },
        resizeStart: { width: 0, height: 0, x: 0, y: 0 },
        resizeDirection: null,
        resizeObserver
    });

    if (options.draggable) {
        setupDragging(id);
    }

    if (options.resizable) {
        setupResizing(id);
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

function setupDragging(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const titleBar = windowData.element.querySelector(".quark-floating-window-titlebar");

    if (!titleBar) {
        return;
    }

    const onMouseDown = (event) => {
        event.preventDefault();
        startDragging(id, event);
    };

    titleBar.addEventListener("mousedown", onMouseDown);
    titleBar.style.cursor = "move";
    windowData.cleanupDragging = () => titleBar.removeEventListener("mousedown", onMouseDown);
}

function setupResizing(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const resizeHandles = windowData.element.querySelectorAll(".quark-floating-window-resize-handle");
    windowData.cleanupResizing = [];

    resizeHandles.forEach((handle) => {
        const direction = handle.dataset.direction;
        const onMouseDown = (event) => {
            event.preventDefault();
            startResizing(id, event, direction);
        };

        handle.addEventListener("mousedown", onMouseDown);
        windowData.cleanupResizing.push(() => handle.removeEventListener("mousedown", onMouseDown));
    });
}

function startDragging(id, event) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.isDragging = true;
    windowData.dragStart = {
        x: event.clientX - windowData.element.offsetLeft,
        y: event.clientY - windowData.element.offsetTop
    };

    bringToFront(id);
    windowData.dotNetRef?.invokeMethodAsync("InvokeOnDragStart").catch(console.error);

    const onMouseMove = (moveEvent) => {
        if (windowData.isDragging) {
            updateDragPosition(id, moveEvent);
        }
    };

    const onMouseUp = () => {
        windowData.isDragging = false;
        document.removeEventListener("mousemove", onMouseMove);
        document.removeEventListener("mouseup", onMouseUp);
        windowData.dotNetRef?.invokeMethodAsync("InvokeOnDragEnd").catch(console.error);
    };

    document.addEventListener("mousemove", onMouseMove);
    document.addEventListener("mouseup", onMouseUp);
}

function startResizing(id, event, direction) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.isResizing = true;
    windowData.resizeDirection = direction;
    windowData.resizeStart = {
        width: windowData.element.offsetWidth,
        height: windowData.element.offsetHeight,
        x: parseInt(windowData.element.style.left, 10) || 0,
        y: parseInt(windowData.element.style.top, 10) || 0,
        mouseX: event.clientX,
        mouseY: event.clientY
    };

    bringToFront(id);

    const onMouseMove = (moveEvent) => {
        if (windowData.isResizing) {
            updateResizePosition(id, moveEvent);
        }
    };

    const onMouseUp = () => {
        windowData.isResizing = false;
        windowData.resizeDirection = null;
        document.removeEventListener("mousemove", onMouseMove);
        document.removeEventListener("mouseup", onMouseUp);
    };

    document.addEventListener("mousemove", onMouseMove);
    document.addEventListener("mouseup", onMouseUp);
}

function updateDragPosition(id, event) {
    const windowData = floatingWindows.get(id);

    if (!windowData?.isDragging) {
        return;
    }

    let newX = event.clientX - windowData.dragStart.x;
    let newY = event.clientY - windowData.dragStart.y;

    if (windowData.options.constrainToViewport) {
        const rect = windowData.element.getBoundingClientRect();
        newX = Math.max(0, Math.min(newX, window.innerWidth - rect.width));
        newY = Math.max(0, Math.min(newY, window.innerHeight - rect.height));
    }

    windowData.element.style.left = `${newX}px`;
    windowData.element.style.top = `${newY}px`;
}

function updateResizePosition(id, event) {
    const windowData = floatingWindows.get(id);

    if (!windowData?.isResizing) {
        return;
    }

    const deltaX = event.clientX - windowData.resizeStart.mouseX;
    const deltaY = event.clientY - windowData.resizeStart.mouseY;
    const direction = windowData.resizeDirection;
    let newWidth = windowData.resizeStart.width;
    let newHeight = windowData.resizeStart.height;
    let newX = windowData.resizeStart.x;
    let newY = windowData.resizeStart.y;

    if (direction.includes("e")) {
        newWidth = Math.max(windowData.options.minWidth, windowData.resizeStart.width + deltaX);

        if (windowData.options.maxWidth) {
            newWidth = Math.min(windowData.options.maxWidth, newWidth);
        }
    }

    if (direction.includes("w")) {
        const maxWidthChange = windowData.resizeStart.width - windowData.options.minWidth;
        const clampedDeltaX = Math.max(-maxWidthChange, Math.min(deltaX, maxWidthChange));
        newWidth = windowData.resizeStart.width - clampedDeltaX;
        newX = windowData.resizeStart.x + (windowData.resizeStart.width - newWidth);
    }

    if (direction.includes("s")) {
        newHeight = Math.max(windowData.options.minHeight, windowData.resizeStart.height + deltaY);

        if (windowData.options.maxHeight) {
            newHeight = Math.min(windowData.options.maxHeight, newHeight);
        }
    }

    if (direction.includes("n")) {
        const maxHeightChange = windowData.resizeStart.height - windowData.options.minHeight;
        const clampedDeltaY = Math.max(-maxHeightChange, Math.min(deltaY, maxHeightChange));
        newHeight = windowData.resizeStart.height - clampedDeltaY;
        newY = windowData.resizeStart.y + (windowData.resizeStart.height - newHeight);
    }

    if (windowData.options.constrainToViewport) {
        const minWidth = windowData.options.minWidth || 200;
        const minHeight = windowData.options.minHeight || 150;

        if (newX + newWidth > window.innerWidth) {
            newWidth = Math.max(minWidth, window.innerWidth - newX);
        }

        if (newY + newHeight > window.innerHeight) {
            newHeight = Math.max(minHeight, window.innerHeight - newY);
        }

        if (newX < 0) {
            newX = 0;
            newWidth = Math.min(newWidth, window.innerWidth);
        }

        if (newY < 0) {
            newY = 0;
            newHeight = Math.min(newHeight, window.innerHeight);
        }
    }

    windowData.element.style.width = `${newWidth}px`;
    windowData.element.style.height = `${newHeight}px`;
    windowData.element.style.left = `${newX}px`;
    windowData.element.style.top = `${newY}px`;
}

export function setCallbacks(id, dotNetRef) {
    const windowData = floatingWindows.get(id);

    if (windowData) {
        windowData.dotNetRef = dotNetRef;
    }
}

export function show(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    const element = windowData.element;

    if (windowData.options.centerOnShow) {
        element.style.display = "flex";
        element.style.visibility = "hidden";
        element.style.left = "-9999px";
        element.style.top = "-9999px";

        requestAnimationFrame(() => {
            const x = Math.max(0, Math.round((window.innerWidth - element.offsetWidth) / 2));
            const y = Math.max(0, Math.round((window.innerHeight - element.offsetHeight) / 2));
            element.style.left = `${x}px`;
            element.style.top = `${y}px`;
            element.style.visibility = "visible";
            element.dataset.state = "open";
            windowData.dotNetRef?.invokeMethodAsync("InvokeOnShow").catch(console.error);
        });

        return;
    }

    element.style.display = "flex";
    element.style.visibility = "visible";
    element.dataset.state = "open";
    windowData.dotNetRef?.invokeMethodAsync("InvokeOnShow").catch(console.error);
}

export function hide(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    windowData.element.style.display = "none";
    windowData.element.dataset.state = "closed";
    windowData.dotNetRef?.invokeMethodAsync("InvokeOnHide").catch(console.error);
}

export function toggle(id) {
    const windowData = floatingWindows.get(id);

    if (!windowData) {
        return;
    }

    if (windowData.element.dataset.state === "open") {
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

    windowData.cleanupDragging?.();
    windowData.cleanupResizing?.forEach((cleanup) => cleanup());
    windowData.resizeObserver?.disconnect();
    floatingWindows.delete(id);
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
}

export function bringToFront(id) {
    const windowData = floatingWindows.get(id);

    if (windowData) {
        windowData.element.style.zIndex = nextZIndex++;
    }
}

export function getViewportSize() {
    return {
        width: window.innerWidth,
        height: window.innerHeight
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

    const x = Math.max(0, Math.round((window.innerWidth - element.offsetWidth) / 2));
    const y = Math.max(0, Math.round((window.innerHeight - element.offsetHeight) / 2));
    element.style.left = `${x}px`;
    element.style.top = `${y}px`;
    element.style.display = previousDisplay;
    element.style.visibility = previousVisibility;
}
