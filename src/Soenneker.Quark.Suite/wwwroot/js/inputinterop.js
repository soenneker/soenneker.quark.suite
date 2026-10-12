export function getSelection(input) {
    if (!input || typeof input.selectionStart !== "number" || typeof input.selectionEnd !== "number") {
        return null;
    }

    return {
        start: input.selectionStart,
        end: input.selectionEnd,
        value: input.value
    };
}

export function restoreSelection(input, start, end, value) {
    if (!input || document.activeElement !== input || typeof input.setSelectionRange !== "function") {
        return;
    }

    if (typeof value === "string" && input.value !== value) {
        return;
    }

    const length = input.value.length;
    const safeStart = Math.max(0, Math.min(start, length));
    const safeEnd = Math.max(safeStart, Math.min(end, length));

    input.setSelectionRange(safeStart, safeEnd);
}

const comboboxKeyHandlers = new WeakMap();

export function registerComboboxKeys(input) {
    if (!input || comboboxKeyHandlers.has(input)) return;

    const onKeyDown = event => {
        // Blazor's key event does not expose composition state. Keep IME confirmation
        // away from selection handlers, without interfering with the native input.
        if (event.isComposing || event.keyCode === 229) {
            event.stopPropagation();
            return;
        }
        if (input.disabled || input.readOnly || event.altKey || event.ctrlKey || event.metaKey) return;

        const open = input.getAttribute("aria-expanded") === "true";
        const key = event.key;
        if (key === "ArrowDown" || key === "ArrowUp" ||
            (open && (key === "Home" || key === "End" || key === "Enter" || key === "Escape")) ||
            (key === "Backspace" && input.value === "" && input.getAttribute("data-combobox-multiple") === "true")) {
            event.preventDefault();
        }
    };

    comboboxKeyHandlers.set(input, onKeyDown);
    input.addEventListener("keydown", onKeyDown);
}

export function unregisterComboboxKeys(input) {
    const handler = input && comboboxKeyHandlers.get(input);
    if (!handler) return;
    input.removeEventListener("keydown", handler);
    comboboxKeyHandlers.delete(input);
}

export function registerTagInputKeys(input, delimiters) {
    if (!input) return;
    unregisterComboboxKeys(input);
    const delimiterKeys = new Set(delimiters);
    const onKeyDown = event => {
        if (event.isComposing || event.keyCode === 229) {
            event.stopPropagation();
            return;
        }
        if (input.disabled || input.readOnly || event.altKey || event.ctrlKey || event.metaKey) return;
        const key = event.key;
        const open = input.getAttribute("aria-expanded") === "true";
        if (key === "Enter" || delimiterKeys.has(key) ||
            (open && (key === "ArrowUp" || key === "ArrowDown")) ||
            (input.value === "" && (key === "Backspace" || key === "Delete" || key === "ArrowLeft" || key === "ArrowRight" || key === "Home" || key === "End")))
            event.preventDefault();
    };
    comboboxKeyHandlers.set(input, onKeyDown);
    input.addEventListener("keydown", onKeyDown);
}

export function registerNavigationKeys(element, keys) {
    if (!element) return;
    unregisterComboboxKeys(element);
    const navigationKeys = new Set(keys);
    const onKeyDown = event => {
        if (!event.isComposing && !event.altKey && !event.ctrlKey && !event.metaKey && navigationKeys.has(event.key))
            event.preventDefault();
    };
    comboboxKeyHandlers.set(element, onKeyDown);
    element.addEventListener("keydown", onKeyDown);
}
