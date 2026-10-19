// Minified and unminified adapters can coexist; share viewport ownership across both module URLs.
const viewportStates = globalThis[Symbol.for("quark.virtual.viewports")] ??= new WeakMap();
const estimatedHeight = 24;
const overscan = 8;

// Fenwick sums keep scrolling and individual row measurements logarithmic.
function rebuildHeights(state) {
    const keys = state.keys ?? [];
    state.indexes = new Map();
    state.tree = new Float64Array(keys.length + 1);
    for (let i = 0; i < keys.length; i++) {
        state.indexes.set(keys[i], i);
        state.tree[i + 1] += state.heights.get(keys[i]) ?? state.estimate;
        const parent = i + 1 + ((i + 1) & -(i + 1));
        if (parent < state.tree.length) state.tree[parent] += state.tree[i + 1];
    }
}
function prefix(state, count) {
    let sum = 0;
    for (let i = count; i > 0; i -= i & -i) sum += state.tree[i];
    return sum;
}
function indexAt(state, offset) {
    const count = state.keys?.length ?? 0;
    let index = 0, sum = 0;
    for (let bit = 2 ** Math.floor(Math.log2(Math.max(1, count))); bit >= 1; bit /= 2) {
        const next = index + bit;
        if (next <= count && sum + state.tree[next] <= offset) { index = next; sum += state.tree[next]; }
    }
    return Math.min(index, Math.max(0, count - 1));
}
function measure(state, key, height) {
    const previous = state.heights.get(key) ?? state.estimate;
    state.heights.set(key, height);
    const index = state.indexes?.get(key);
    if (index === undefined || height === previous) return;
    for (let i = index + 1; i < state.tree.length; i += i & -i) state.tree[i] += height - previous;
}

function contentTop(element, state) {
    const spacer = element.querySelector(state.spacerSelector);
    return spacer ? spacer.getBoundingClientRect().top - state.scroller.getBoundingClientRect().top + state.scroller.scrollTop - state.scroller.clientTop : 0;
}

function rememberAnchor(element, state) {
    if (!state.keys?.length) return;
    const offset = Math.max(0, state.scroller.scrollTop - contentTop(element, state));
    const index = indexAt(state, offset);
    state.anchor = { key: state.keys[index], offset: offset - prefix(state, index) };
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
        measure(state, row.getAttribute(state.keyAttribute), Math.max(1, row.getBoundingClientRect().height));
        if (!state.rows.has(row)) state.resize.observe(row);
    }
    state.rows = currentRows;
    const total = prefix(state, state.keys?.length ?? 0);
    if (state.hasAfter && total <= state.scroller.clientHeight + 100 && !state.loadingAfter && state.receiver && state.afterCursor !== (state.keys?.at(-1) ?? null)) {
        state.afterCursor = state.keys?.at(-1) ?? null;
        state.loadingAfter = true;
        state.receiver.invokeMethodAsync('RequestAfter').catch(() => {}).finally(() => { state.loadingAfter = false; });
    }
    const top = contentTop(element, state);
    let target = state.scroller.scrollTop;
    if (state.follow && state.autoScroll) target = state.keys ? Math.max(0, top + total - state.scroller.clientHeight) : state.scroller.scrollHeight;
    else if (state.anchor && state.keys) {
        const index = (state.indexes.get(state.anchor.key) ?? -1);
        if (index >= 0) target = Math.max(0, top + prefix(state, index) + state.anchor.offset);
    }
    if (state.scrollRequest && state.keys) {
        const index = (state.indexes.get(state.scrollRequest.key) ?? -1);
        if (index >= 0) {
            const rowTop = top + prefix(state, index);
            const rowBottom = top + prefix(state, index + 1);
            target = rowTop < target ? rowTop : rowBottom > target + state.scroller.clientHeight ? rowBottom - state.scroller.clientHeight : target;
        } else state.scrollRequest = null;
    }
    if (state.keys && state.receiver) {
        const first = indexAt(state, Math.max(0, target - top));
        const last = indexAt(state, Math.max(0, target - top + state.scroller.clientHeight));
        let start = Math.max(0, first - state.overscan);
        let end = Math.min(state.keys.length, last + state.overscan + 1);
        // Keep focused controls mounted while the user edits or drags an item.
        const focused = element.contains(globalThis.document?.activeElement) ? globalThis.document.activeElement.closest(state.rowSelector) : null;
        const focusIndex = focused ? (state.indexes.get(focused.getAttribute(state.keyAttribute)) ?? -1) : -1;
        const pinned = !state.scrollRequest && focusIndex >= 0 && (focusIndex < start || focusIndex >= end) ? focusIndex : -1;
        if (state.dragging && state.range) { start = Math.min(start, state.range[0]); end = Math.max(end, state.range[1]); }
        state.range = [start, end];
        const args = [start, end - start, prefix(state, start), total - prefix(state, end), !state.follow];
        if (state.pinFocus) {
            const before = pinned < 0 ? 0 : pinned < start ? prefix(state, pinned) : prefix(state, pinned) - prefix(state, end);
            const after = pinned < 0 ? 0 : pinned < start ? prefix(state, start) - prefix(state, pinned + 1) : total - prefix(state, pinned + 1);
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
    rebuildHeights(state);
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
    rebuildHeights(state);
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
