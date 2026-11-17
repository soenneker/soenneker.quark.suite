const observers = new WeakMap();

export function destroy(sentinel) {
    const state = sentinel && observers.get(sentinel);
    if (!state) return;
    state.observer.disconnect();
    observers.delete(sentinel);
}

function scrollRoot(sentinel) {
    for (let parent = sentinel.parentElement; parent; parent = parent.parentElement) {
        if (/(auto|scroll|overlay)/.test(getComputedStyle(parent).overflowY))
            return parent;
    }
    return null;
}

export function observe(sentinel, callback, enabled, preloadDistance) {
    destroy(sentinel);
    if (!enabled || !sentinel?.isConnected) return;

    const state = {};
    state.observer = new IntersectionObserver(entries => {
        if (observers.get(sentinel) !== state || !entries.some(entry => entry.isIntersecting)) return;
        // Disarm before invoking .NET. Rendering after completion rearms even for a short page.
        destroy(sentinel);
        callback.invokeMethodAsync('RequestMore').catch(error => {
            if (sentinel.isConnected) console.error('InfiniteScroll load failed.', error);
        });
    }, { root: scrollRoot(sentinel), rootMargin: `0px 0px ${Math.max(0, preloadDistance)}px 0px` });
    observers.set(sentinel, state);
    state.observer.observe(sentinel);
}
