const observers = new WeakMap();

function updateHeight(container, content) {
    if (!container || !content) {
        return;
    }

    const height = content.scrollHeight;
    container.style.setProperty("--radix-accordion-content-height", `${height}px`);
}

export function observeContentHeight(container, content) {
    if (!container || !content) {
        return;
    }

    stopObservingContentHeight(container);
    updateHeight(container, content);

    const observer = typeof ResizeObserver === "function"
        ? new ResizeObserver(() => updateHeight(container, content))
        : null;

    observer?.observe(content);

    const rafId = requestAnimationFrame(() => updateHeight(container, content));

    observers.set(container, { observer, rafId });
}

export function stopObservingContentHeight(container) {
    if (!container) {
        return;
    }

    const entry = observers.get(container);

    if (!entry) {
        return;
    }

    entry.observer?.disconnect();

    if (entry.rafId) {
        cancelAnimationFrame(entry.rafId);
    }

    observers.delete(container);
}
