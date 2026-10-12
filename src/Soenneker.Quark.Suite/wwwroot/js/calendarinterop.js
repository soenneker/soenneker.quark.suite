const calendars = new WeakMap();
const navigationKeys = new Set(["ArrowLeft", "ArrowRight", "ArrowUp", "ArrowDown", "Home", "End", "PageUp", "PageDown"]);

export function initialize(element, receiver) {
    destroy(element);
    const state = { disposed: false, pending: Promise.resolve(), keydown: null };
    state.keydown = event => {
        const button = event.target.closest("button[data-calendar-date]");
        if (!button || !element.contains(button) || button.disabled || event.isComposing || event.altKey || event.ctrlKey || event.metaKey || !navigationKeys.has(event.key)) return;
        event.preventDefault();
        const key = event.key, shift = event.shiftKey;
        state.pending = state.pending.then(async () => {
            if (state.disposed) return;
            const focused = element.contains(document.activeElement) && document.activeElement.dataset.calendarDate
                ? document.activeElement : button;
            await receiver.invokeMethodAsync("NavigateCalendar", focused.dataset.calendarDate, key, shift);
        }).catch(() => {});
    };
    element.addEventListener("keydown", state.keydown);
    calendars.set(element, state);
}

export function focus(element, date) {
    if (!/^\d{4}-\d{2}-\d{2}$/.test(date)) return;
    const buttons = element.querySelectorAll(`button[data-calendar-date="${date}"]:not(:disabled)`);
    for (const button of buttons) {
        if (button.dataset.calendarOutside !== "true") {
            button.focus({ preventScroll: true });
            return;
        }
    }
    buttons[0]?.focus({ preventScroll: true });
}

export function destroy(element) {
    const state = calendars.get(element);
    if (!state) return;
    state.disposed = true;
    element.removeEventListener("keydown", state.keydown);
    calendars.delete(element);
}
