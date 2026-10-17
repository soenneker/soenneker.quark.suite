import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import test from 'node:test';
import { runInNewContext } from 'node:vm';

const source = readFileSync(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/consolepanelinterop.js', import.meta.url), 'utf8').replaceAll('export function ', 'function ');

function fixture(count = 10000) {
    const frames = new Map();
    let frameId = 0;
    let rows = [];
    let windowArgs;
    let olderCalls = 0;
    let disposed = 0;
    const listeners = new Map();
    const element = {
        scrollTop: 0, clientTop: 0, clientHeight: 480, scrollHeight: count * 24,
        style: { overflowAnchor: 'auto' },
        getBoundingClientRect: () => ({ top: 0 }),
        querySelector: () => ({ getBoundingClientRect: () => ({ top: -element.scrollTop }) }),
        querySelectorAll: () => rows,
        addEventListener: (name, handler) => listeners.set(name, handler),
        removeEventListener: name => listeners.delete(name)
    };
    let keys = Array.from({ length: count }, (_, i) => String(i));
    const receiver = { invokeMethodAsync: async (name, ...args) => {
        if (name === 'RequestOlder') { olderCalls++; return; }
        windowArgs = args;
        rows = keys.slice(args[0], args[0] + args[1]).map(key => ({ dataset: { consoleKey: key }, getBoundingClientRect: () => ({ height: 24 }) }));
    }};
    let resize;
    const context = {
        window: { requestAnimationFrame: callback => { frames.set(++frameId, callback); return frameId; }, cancelAnimationFrame: id => frames.delete(id) },
        ResizeObserver: class { constructor(callback) { resize = callback; } observe() {} unobserve() {} disconnect() { disposed++; } },
        MutationObserver: class { observe() {} disconnect() { disposed++; } },
        Node: { ELEMENT_NODE: 1 }
    };
    runInNewContext(source + '\nthis.api = {updateViewport, jumpToLatest, destroyAutoScroll};', context);
    const update = (next = keys, follow = true) => { keys = next; context.api.updateViewport(element, receiver, keys, follow, true); };
    async function flush() {
        for (let i = 0; frames.size && i < 30; i++) {
            const callbacks = [...frames.values()]; frames.clear();
            for (const callback of callbacks) callback();
            await Promise.resolve(); await Promise.resolve(); await Promise.resolve();
        }
        assert.equal(frames.size, 0, 'viewport should settle without a render loop');
    }
    return { element, update, flush, get args() { return windowArgs; }, get keys() { return keys; },
        get olderCalls() { return olderCalls; }, get disposed() { return disposed; },
        scroll(top) { element.scrollTop = top; listeners.get('scroll')(); },
        expand() { rows[0].getBoundingClientRect = () => ({ height: 240 }); resize(); },
        latest() { context.api.jumpToLatest(element); }, destroy() { context.api.destroyAutoScroll(element); }, listeners };
}

test('large consoles render a bounded window and follow appended output', async () => {
    const f = fixture(); f.update(); await f.flush();
    assert.ok(f.args[1] < 50);
    assert.equal(f.args[0] + f.args[1], 10000);
    f.update([...f.keys, '10000']); await f.flush();
    assert.equal(f.args[0] + f.args[1], 10001);
    assert.equal(f.args[4], false);
});

test('reading history pauses following, survives prepends and resumes explicitly', async () => {
    const f = fixture(); f.update(); await f.flush();
    f.scroll(2400); await f.flush();
    const position = f.element.scrollTop;
    f.update([...f.keys, '10000']); await f.flush();
    assert.equal(f.element.scrollTop, position);
    assert.equal(f.args[4], true);
    f.update(['older-1', 'older-2', ...f.keys]); await f.flush();
    assert.equal(f.element.scrollTop, position + 48);
    f.latest(); await f.flush();
    assert.equal(f.args[0] + f.args[1], f.keys.length);
    assert.equal(f.args[4], false);
});

test('expanding an overscan row preserves the visible anchor and paging is upward only', async () => {
    const f = fixture(); f.update(); await f.flush();
    f.scroll(2400); await f.flush();
    f.expand(); await f.flush();
    assert.equal(f.element.scrollTop, 2400 + 216);
    f.scroll(50); await f.flush();
    assert.equal(f.olderCalls, 1);
    f.scroll(70); await f.flush();
    assert.equal(f.olderCalls, 1);
});

test('disabled following starts at the beginning and disposal releases observers', async () => {
    const f = fixture(); f.update(f.keys, false); await f.flush();
    assert.equal(f.args[0], 0);
    assert.equal(f.element.scrollTop, 0);
    f.destroy(); await f.flush();
    assert.equal(f.disposed, 2);
    assert.equal(f.listeners.size, 0);
    assert.equal(f.element.style.overflowAnchor, 'auto');
});
