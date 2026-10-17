// Minified and unminified adapters can coexist; share viewport ownership across both module URLs.
const viewportStates = globalThis[Symbol.for("quark.virtual.viewports")] ??= new WeakMap();
const estimatedHeight = 24;
const overscan = 8;

function offsets(state) {
    const result = [0];
    for (const key of state.keys ?? []) result.push(result[result.length - 1] + (state.heights.get(key) ?? state.estimate));
    return result;
}

function indexAt(positions, offset) {
    let low = 0, high = positions.length - 1;
    while (low < high) {
        const mid = Math.floor((low + high + 1) / 2);
        if (positions[mid] <= offset) low = mid;
        else high = mid - 1;
    }
    return Math.min(low, Math.max(0, positions.length - 2));
}

function contentTop(element, state) {
    const spacer = element.querySelector(state.spacerSelector);
    return spacer ? spacer.getBoundingClientRect().top - state.scroller.getBoundingClientRect().top + state.scroller.scrollTop - state.scroller.clientTop : 0;
}

function rememberAnchor(element, state) {
    if (!state.keys?.length) return;
    const positions = offsets(state);
    const offset = Math.max(0, state.scroller.scrollTop - contentTop(element, state));
    const index = indexAt(positions, offset);
    state.anchor = { key: state.keys[index], offset: offset - positions[index] };
}

function schedule(element, state) {
    if (!state.frame && !state.destroyed) state.frame = window.requestAnimationFrame(() => refresh(element, state));
}

function refresh(element, state) {
    state.frame = 0;
    if (state.destroyed) return;
    const rows = [...element.querySelectorAll(state.rowSelector)].filter(row => !row.parentElement || row.parentElement === element);
    const currentRows = new Set(rows);
    for (const row of state.rows) if (!currentRows.has(row)) state.resize.unobserve(row);
    for (const row of rows) {
        state.heights.set(row.getAttribute(state.keyAttribute), Math.max(1, row.getBoundingClientRect().height));
        if (!state.rows.has(row)) state.resize.observe(row);
    }
    state.rows = currentRows;
    const positions = offsets(state);
    const total = positions[positions.length - 1];
    if (state.hasAfter && total <= state.scroller.clientHeight + 100 && !state.loadingAfter && state.receiver && state.afterCursor !== (state.keys?.at(-1) ?? null)) {
        state.afterCursor = state.keys?.at(-1) ?? null;
        state.loadingAfter = true;
        state.receiver.invokeMethodAsync('RequestAfter').catch(() => {}).finally(() => { state.loadingAfter = false; });
    }
    const top = contentTop(element, state);
    let target = state.scroller.scrollTop;
    if (state.follow && state.autoScroll) target = state.keys ? Math.max(0, top + total - state.scroller.clientHeight) : state.scroller.scrollHeight;
    else if (state.anchor && state.keys) {
        const index = state.keys.indexOf(state.anchor.key);
        if (index >= 0) target = Math.max(0, top + positions[index] + state.anchor.offset);
    }
    if (state.scrollRequest && state.keys) {
        const index = state.keys.indexOf(state.scrollRequest.key);
        if (index >= 0) {
            const rowTop = top + positions[index];
            const rowBottom = top + positions[index + 1];
            target = rowTop < target ? rowTop : rowBottom > target + state.scroller.clientHeight ? rowBottom - state.scroller.clientHeight : target;
        } else state.scrollRequest = null;
    }
    if (state.keys && state.receiver) {
        const first = indexAt(positions, Math.max(0, target - top));
        const last = indexAt(positions, Math.max(0, target - top + state.scroller.clientHeight));
        let start = Math.max(0, first - state.overscan);
        let end = Math.min(state.keys.length, last + state.overscan + 1);
        // Keep focused controls mounted while the user edits or drags an item.
        const focused = element.contains(globalThis.document?.activeElement) ? globalThis.document.activeElement.closest(state.rowSelector) : null;
        const focusIndex = focused ? state.keys.indexOf(focused.getAttribute(state.keyAttribute)) : -1;
        const pinned = !state.scrollRequest && focusIndex >= 0 && (focusIndex < start || focusIndex >= end) ? focusIndex : -1;
        if (state.dragging && state.range) { start = Math.min(start, state.range[0]); end = Math.max(end, state.range[1]); }
        state.range = [start, end];
        const args = [start, end - start, positions[start], total - positions[end], !state.follow];
        if (state.pinFocus) {
            const before = pinned < 0 ? 0 : pinned < start ? positions[pinned] : positions[pinned] - positions[end];
            const after = pinned < 0 ? 0 : pinned < start ? positions[start] - positions[pinned + 1] : total - positions[pinned + 1];
            args.push(pinned, before, after);
        }
        const signature = JSON.stringify(args);
        if (signature !== state.signature && !state.pending) {
            state.signature = signature;
            state.pending = true;
            state.receiver.invokeMethodAsync(state.pinFocus ? 'SetVirtualWindow' : 'SetWindow', ...args).catch(() => {}).finally(() => {
                state.pending = false;
                schedule(element, state);
            });
        }
    } else if (state.receiver && state.paused !== !state.follow) {
        state.paused = !state.follow;
        state.receiver.invokeMethodAsync('SetWindow', 0, 0, 0, 0, state.paused).catch(() => {});
    }
    state.scroller.scrollTop = target;
    state.expectedTop = state.scroller.scrollTop;
    if (state.scrollRequest) {
        const row = [...rows].find(row => row.getAttribute(state.keyAttribute) === state.scrollRequest.key);
        if (row) {
            if (state.scrollRequest.focusSelector) (row.matches(state.scrollRequest.focusSelector) ? row : row.querySelector(state.scrollRequest.focusSelector))?.focus({ preventScroll: true });
            state.scrollRequest = null;
            rememberAnchor(element, state);
        }
    }
}

function createState(element, options = {}) {
    let scroller = element;
    if (options.parentScroll) {
        scroller = element.parentElement;
        while (scroller?.parentElement && !/(auto|scroll)/.test(getComputedStyle(scroller).overflowY)) scroller = scroller.parentElement;
    }
    const state = { scroller, parentScroll: !!options.parentScroll, estimate: options.estimate ?? estimatedHeight, overscan: options.overscan ?? overscan,
        rowSelector: options.rowSelector ?? '[data-console-key]', keyAttribute: options.keyAttribute ?? 'data-console-key',
        spacerSelector: options.spacerSelector ?? '[data-console-spacer="top"]', keys: null, heights: new Map(), rows: new Set(), follow: true, autoScroll: true, frame: 0, pending: false, destroyed: false };
    state.resize = new ResizeObserver(() => schedule(element, state));
    state.resize.observe(state.scroller);
    state.observer = new MutationObserver(mutations => {
        if (mutations.some(m => !(m.target.nodeType === Node.ELEMENT_NODE ? m.target : m.target.parentElement)?.closest('[data-slot="console-actions"]')))
            schedule(element, state);
    });
    state.observer.observe(element, { childList: true, subtree: true, characterData: true });
    state.scroll = () => {
        if (Math.abs(state.scroller.scrollTop - state.expectedTop) < 1) return;
        const upward = state.scroller.scrollTop < (state.expectedTop ?? state.scroller.scrollTop);
        state.expectedTop = state.scroller.scrollTop;
        state.follow = state.scroller.scrollHeight - state.scroller.clientHeight - state.scroller.scrollTop <= 32;
        rememberAnchor(element, state);
        schedule(element, state);
        if (!upward && state.scroller.scrollHeight - state.scroller.clientHeight - state.scroller.scrollTop <= 100 && state.hasAfter && !state.loadingAfter && state.receiver) {
            state.loadingAfter = true;
            state.receiver.invokeMethodAsync('RequestAfter').catch(() => {}).finally(() => { state.loadingAfter = false; });
        }
        if (upward && state.scroller.scrollTop <= 100 && state.hasOlder && !state.loading && state.receiver) {
            state.loading = true;
            state.receiver.invokeMethodAsync('RequestOlder').catch(() => {}).finally(() => { state.loading = false; });
        }
    };
    state.scroller.addEventListener('scroll', state.scroll, { passive: true });
    // Browser anchoring competes with the measured key-based anchor during prepends.
    state.previousOverflowAnchor = state.scroller.style.overflowAnchor;
    state.scroller.style.overflowAnchor = 'none';
    viewportStates.set(element, state);
    return state;
}

export function updateViewport(element, receiver, keys, autoScroll, hasOlderItems, options = {}) {
    let state = viewportStates.get(element);
    if (state && state.parentScroll !== !!options.parentScroll) { destroyAutoScroll(element); state = null; }
    state ??= createState(element, options);
    if (state.keys !== null && keys === null) state.signature = null;
    const retained = new Set(keys ?? []);
    for (const key of state.heights.keys()) if (!retained.has(key)) state.heights.delete(key);
    state.keys = keys;
    state.receiver = receiver;
    state.autoScroll = autoScroll;
    state.hasOlder = hasOlderItems;
    state.hasAfter = options.hasAfter;
    state.disabledKeys = new Set(options.disabledKeys ?? []);
    state.pinFocus = !!options.pinFocus;
    schedule(element, state);
}

export function jumpToLatest(element) {
    const state = viewportStates.get(element);
    if (!state) return;
    state.follow = true;
    state.anchor = null;
    schedule(element, state);
}

export function initializeAutoScroll(element) {
    destroyAutoScroll(element);
    schedule(element, createState(element));
}

export function destroyAutoScroll(element) {
    const state = viewportStates.get(element);
    if (!state) return;
    state.destroyed = true;
    state.observer.disconnect();
    state.resize.disconnect();
    state.scroller.removeEventListener('scroll', state.scroll);
    state.scroller.style.overflowAnchor = state.previousOverflowAnchor;
    if (state.frame) window.cancelAnimationFrame(state.frame);
    viewportStates.delete(element);
}

export function scrollToIndex(element, index, focusSelector = null) {
    const state = viewportStates.get(element);
    if (!state || !state.keys || index < 0 || index >= state.keys.length) return;
    state.follow = false;
    state.anchor = null;
    state.scrollRequest = { key: state.keys[index], focusSelector };
    schedule(element, state);
}

export function getViewport(element) { return viewportStates.get(element); }
export function setDragging(element, dragging) {
    const state = viewportStates.get(element);
    if (state) { state.dragging = dragging; schedule(element, state); }
}

export function updateItems(element, receiver, keys, follow, before, after, parentScroll, disabledKeys = []) {
    updateViewport(element, receiver, keys, follow, before, { parentScroll, hasAfter: after, disabledKeys, pinFocus: true,
        rowSelector: '[data-virtual-key]', keyAttribute: 'data-virtual-key', spacerSelector: '[data-virtual-spacer="top"]' });
}
