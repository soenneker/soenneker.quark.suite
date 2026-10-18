export function loadViews(key) { return localStorage.getItem("quark.views." + key); }
export function saveViews(key, value) { localStorage.setItem("quark.views." + key, value); }

const interactions = new WeakMap();
export function updateInteractions(element) {
    for (const input of element.querySelectorAll("input[data-mixed]")) input.indeterminate = input.dataset.mixed === "true";
}
export function mountInteractions(element, callback) {
    unmountInteractions(element);
    let gesture = null, suppressClick = false;
    const invoke = (...args) => callback.invokeMethodAsync(...args).catch(error => console.warn("Quark interaction failed", error));
    const down = event => {
        if (event.button !== 0 || !event.isPrimary) return;
        const handle = event.target.closest("[data-column-resize], [data-touch-event]");
        if (!handle || !element.contains(handle) || handle.disabled) return;
        const column = handle.dataset.columnResize;
        if (column === undefined && event.pointerType === "mouse") return;
        gesture = { handle, column, id: event.pointerId, x: event.clientX, y: event.clientY,
            width: handle.closest("th")?.getBoundingClientRect().width, moved: false };
        handle.setPointerCapture(event.pointerId);
        event.preventDefault();
    };
    const move = event => {
        if (!gesture || event.pointerId !== gesture.id) return;
        const delta = event.clientX - gesture.x;
        gesture.moved ||= Math.abs(delta) + Math.abs(event.clientY - gesture.y) > 6;
        if (gesture.column !== undefined) {
            const direction = getComputedStyle(element).direction === "rtl" ? -1 : 1;
            gesture.nextWidth = Math.max(64, Math.min(1200, Math.round(gesture.width + direction * delta)));
            const style = gesture.handle.closest("th").style;
            style.width = style.minWidth = style.maxWidth = gesture.nextWidth + "px";
        } else gesture.handle.style.opacity = gesture.moved ? "0.6" : "";
        event.preventDefault();
    };
    const finish = event => {
        if (!gesture || event.pointerId !== gesture.id) return;
        const current = gesture; gesture = null;
        current.handle.style.opacity = "";
        if (current.handle.hasPointerCapture(current.id)) current.handle.releasePointerCapture(current.id);
        if (event.type === "pointercancel" || !current.moved) {
            if (current.column !== undefined) { const style = current.handle.closest("th").style; style.width = style.minWidth = style.maxWidth = current.width + "px"; }
            return;
        }
        suppressClick = true; setTimeout(() => suppressClick = false, 0);
        if (current.column !== undefined) void invoke("OnColumnResize", current.column, current.nextWidth || current.width);
        else {
            const lane = document.elementsFromPoint(event.clientX, event.clientY).find(el => el.matches("[data-scheduler-lane]"));
            if (!lane || !element.contains(lane)) return;
            const box = lane.getBoundingClientRect();
            const minute = Math.max(0, Math.min(1440, Math.round((event.clientY - box.top) / box.height * 1440 / 30) * 30));
            void invoke("OnTouchDrop", current.handle.dataset.touchEvent, current.handle.dataset.touchResize === "true", lane.dataset.schedulerLane, minute, lane.dataset.resource || null);
        }
    };
    const click = event => { if (suppressClick) { event.preventDefault(); event.stopImmediatePropagation(); } };
    const keydown = event => {
        const handle = event.target.closest("[data-column-resize]");
        if (!handle || !["ArrowLeft", "ArrowRight", "Home", "End"].includes(event.key)) return;
        event.preventDefault();
        const width = handle.closest("th").getBoundingClientRect().width;
        const direction = getComputedStyle(element).direction === "rtl" ? -1 : 1;
        const next = event.key === "Home" ? 64 : event.key === "End" ? 1200 : width + (event.key === "ArrowLeft" ? -8 : 8) * direction;
        void invoke("OnColumnResize", handle.dataset.columnResize, Math.max(64, Math.min(1200, Math.round(next))));
    };
    element.addEventListener("pointerdown", down); element.addEventListener("pointermove", move);
    element.addEventListener("pointerup", finish); element.addEventListener("pointercancel", finish);
    element.addEventListener("click", click, true); element.addEventListener("keydown", keydown);
    interactions.set(element, () => {
        if (gesture?.handle.hasPointerCapture(gesture.id)) gesture.handle.releasePointerCapture(gesture.id);
        element.removeEventListener("pointerdown", down); element.removeEventListener("pointermove", move);
        element.removeEventListener("pointerup", finish); element.removeEventListener("pointercancel", finish);
        element.removeEventListener("click", click, true); element.removeEventListener("keydown", keydown);
    });
    updateInteractions(element);
}
export function unmountInteractions(element) { interactions.get(element)?.(); interactions.delete(element); }
