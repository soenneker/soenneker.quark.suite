const autoScrollObservers = new WeakMap();
const estimatedHeight = 24;
const overscan = 8;

function offsets(state) {
    const result = [0];
    for (const key of state.keys ?? []) result.push(result[result.length - 1] + (state.heights.get(key) ?? estimatedHeight));
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

function contentTop(element) {
    const spacer = element.querySelector('[data-console-spacer="top"]');
    return spacer ? spacer.getBoundingClientRect().top - element.getBoundingClientRect().top + element.scrollTop - element.clientTop : 0;
}

function rememberAnchor(element, state) {
    if (!state.keys?.length) return;
    const positions = offsets(state);
    const offset = Math.max(0, element.scrollTop - contentTop(element));
    const index = indexAt(positions, offset);
    state.anchor = { key: state.keys[index], offset: offset - positions[index] };
}

function schedule(element, state) {
    if (!state.frame && !state.destroyed) state.frame = window.requestAnimationFrame(() => refresh(element, state));
}

function refresh(element, state) {
    state.frame = 0;
    if (state.destroyed) return;
    const rows = element.querySelectorAll('[data-console-key]');
    const currentRows = new Set(rows);
    for (const row of state.rows) if (!currentRows.has(row)) state.resize.unobserve(row);
    for (const row of rows) {
        state.heights.set(row.dataset.consoleKey, Math.max(1, row.getBoundingClientRect().height));
        if (!state.rows.has(row)) state.resize.observe(row);
    }
    state.rows = currentRows;
    const positions = offsets(state);
    const total = positions[positions.length - 1];
    const top = contentTop(element);
    let target = element.scrollTop;
    if (state.follow && state.autoScroll) target = state.keys ? Math.max(0, top + total - element.clientHeight) : element.scrollHeight;
    else if (state.anchor && state.keys) {
        const index = state.keys.indexOf(state.anchor.key);
        if (index >= 0) target = Math.max(0, top + positions[index] + state.anchor.offset);
    }
    if (state.keys && state.receiver) {
        const first = indexAt(positions, Math.max(0, target - top));
        const last = indexAt(positions, Math.max(0, target - top + element.clientHeight));
        const start = Math.max(0, first - overscan);
        const end = Math.min(state.keys.length, last + overscan + 1);
        const args = [start, end - start, positions[start], total - positions[end], !state.follow];
        const signature = JSON.stringify(args);
        if (signature !== state.signature && !state.pending) {
            state.signature = signature;
            state.pending = true;
            state.receiver.invokeMethodAsync('SetWindow', ...args).catch(() => {}).finally(() => {
                state.pending = false;
                schedule(element, state);
            });
        }
    } else if (state.receiver && state.paused !== !state.follow) {
        state.paused = !state.follow;
        state.receiver.invokeMethodAsync('SetWindow', 0, 0, 0, 0, state.paused).catch(() => {});
    }
    element.scrollTop = target;
    state.expectedTop = element.scrollTop;
}

function createState(element) {
    const state = { keys: null, heights: new Map(), rows: new Set(), follow: true, autoScroll: true, frame: 0, pending: false, destroyed: false };
    state.resize = new ResizeObserver(() => schedule(element, state));
    state.resize.observe(element);
    state.observer = new MutationObserver(mutations => {
        if (mutations.some(m => !(m.target.nodeType === Node.ELEMENT_NODE ? m.target : m.target.parentElement)?.closest('[data-slot="console-actions"]')))
            schedule(element, state);
    });
    state.observer.observe(element, { childList: true, subtree: true, characterData: true });
    state.scroll = () => {
        if (Math.abs(element.scrollTop - state.expectedTop) < 1) return;
        const upward = element.scrollTop < (state.expectedTop ?? element.scrollTop);
        state.expectedTop = element.scrollTop;
        state.follow = element.scrollHeight - element.clientHeight - element.scrollTop <= 32;
        rememberAnchor(element, state);
        schedule(element, state);
        if (upward && element.scrollTop <= 100 && state.hasOlder && !state.loading && state.receiver) {
            state.loading = true;
            state.receiver.invokeMethodAsync('RequestOlder').catch(() => {}).finally(() => { state.loading = false; });
        }
    };
    element.addEventListener('scroll', state.scroll, { passive: true });
    // Browser anchoring competes with the measured key-based anchor during prepends.
    state.previousOverflowAnchor = element.style.overflowAnchor;
    element.style.overflowAnchor = 'none';
    autoScrollObservers.set(element, state);
    return state;
}

export function updateViewport(element, receiver, keys, autoScroll, hasOlderItems) {
    const state = autoScrollObservers.get(element) ?? createState(element);
    if (state.keys !== null && keys === null) state.signature = null;
    const retained = new Set(keys ?? []);
    for (const key of state.heights.keys()) if (!retained.has(key)) state.heights.delete(key);
    state.keys = keys;
    state.receiver = receiver;
    state.autoScroll = autoScroll;
    state.hasOlder = hasOlderItems;
    schedule(element, state);
}

export function jumpToLatest(element) {
    const state = autoScrollObservers.get(element);
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
    const state = autoScrollObservers.get(element);
    if (!state) return;
    state.destroyed = true;
    state.observer.disconnect();
    state.resize.disconnect();
    element.removeEventListener('scroll', state.scroll);
    element.style.overflowAnchor = state.previousOverflowAnchor;
    if (state.frame) window.cancelAnimationFrame(state.frame);
    autoScrollObservers.delete(element);
}

export function download(fileName, contentType, content) {
    const blob = new Blob([content ?? ""], { type: contentType || "text/plain;charset=utf-8" });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement("a");

    anchor.href = url;
    anchor.download = fileName || "console.log";
    anchor.style.display = "none";

    document.body.appendChild(anchor);
    anchor.click();
    anchor.remove();

    window.setTimeout(() => URL.revokeObjectURL(url), 1000);
}
