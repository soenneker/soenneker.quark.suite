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
