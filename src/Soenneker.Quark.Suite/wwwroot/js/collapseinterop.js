const collapses = new WeakMap();

export function update(element, expanded, horizontal) {
    let state = collapses.get(element);
    if (!state) {
        const content = element.firstElementChild;
        if (!content) return;
        state = { element, content, expanded, horizontal, observer: null };
        state.observer = new ResizeObserver(() => measure(state));
        collapses.set(element, state);
        state.observer.observe(content);
    }
    state.expanded = expanded;
    state.horizontal = horizontal;
    measure(state);
}

function measure(state) {
    const { element, content, expanded, horizontal } = state;
    // The inner box stays measurable even while the outer box is collapsed.
    content.style.width = horizontal ? "max-content" : "";
    element.style.maxHeight = horizontal ? "" : `${expanded ? content.getBoundingClientRect().height : 0}px`;
    element.style.maxWidth = horizontal ? `${expanded ? content.getBoundingClientRect().width : 0}px` : "";
}

export function destroy(element) {
    const state = collapses.get(element);
    state?.observer.disconnect();
    collapses.delete(element);
}
