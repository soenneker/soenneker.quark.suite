const observers = new WeakMap();

export function repair(fieldset) {
    if (!fieldset?.isConnected) return;
    for (const group of fieldset.querySelectorAll('[class~="@container/field-group"]')) {
        if (!group.children.length || group.offsetHeight !== 0 || group.offsetWidth === 0) continue;
        const computed = getComputedStyle(group);
        if (computed.containerType !== "inline-size" || !["flex", "grid"].includes(computed.display)) continue;

        // Chromium can retain an empty layout tree for a query container in a fieldset
        // after a sibling crosses a display breakpoint. Rebuild that tree without
        // removing the container query, changing the DOM, or hiding the focused control.
        const value = group.style.getPropertyValue("display");
        const priority = group.style.getPropertyPriority("display");
        group.style.setProperty("display", "block", "important");
        void group.offsetHeight;
        if (value) group.style.setProperty("display", value, priority);
        else group.style.removeProperty("display");
    }
}

export function observe(fieldset) {
    if (!fieldset || observers.has(fieldset)) return;
    const resize = new ResizeObserver(() => repair(fieldset));
    const mutation = new MutationObserver(() => repair(fieldset));
    resize.observe(fieldset);
    mutation.observe(fieldset, { childList: true, subtree: true });
    observers.set(fieldset, { resize, mutation });
    repair(fieldset);
}

export function unobserve(fieldset) {
    const state = observers.get(fieldset);
    if (!state) return;
    state.resize.disconnect();
    state.mutation.disconnect();
    observers.delete(fieldset);
}
