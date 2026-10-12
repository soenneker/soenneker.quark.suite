import { test } from 'node:test';
import assert from 'node:assert/strict';
import { initializeSidebar, cleanup } from '../../src/Soenneker.Quark.Suite/wwwroot/js/sidebarinterop.js';

test('sidebar providers own independent media and keyboard registrations', t => {
    const oldWindow = globalThis.window, oldDocument = globalThis.document;
    const keys = new Set(), queries = [], callsA = [], callsB = [];
    globalThis.window = { matchMedia() {
        const listeners = new Set();
        const query = { matches: false, listeners,
            addEventListener: (_, handler) => listeners.add(handler),
            removeEventListener: (_, handler) => listeners.delete(handler) };
        queries.push(query);
        return query;
    } };
    globalThis.document = { addEventListener: (_, handler) => keys.add(handler), removeEventListener: (_, handler) => keys.delete(handler) };
    t.after(() => { cleanup('a'); cleanup('b'); globalThis.window = oldWindow; globalThis.document = oldDocument; });
    const reference = calls => ({ invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } });
    initializeSidebar(reference(callsA), 'a', 'a');
    initializeSidebar(reference(callsB), 'b', 'b');
    assert.equal(keys.size, 2);
    assert.equal(queries[0].listeners.size, 1);
    cleanup('a');
    assert.equal(keys.size, 1);
    assert.equal(queries[0].listeners.size, 0);
    assert.equal(queries[1].listeners.size, 1);
    const event = { key: 'b', ctrlKey: true, preventDefault() { this.defaultPrevented = true; } };
    for (const handler of keys) handler(event);
    assert.equal(event.defaultPrevented, true);
    assert.deepEqual(callsB.at(-1), ['OnToggleShortcut']);
    assert.equal(callsA.length, 1);
    initializeSidebar(reference(callsB), 'b', 'b');
    assert.equal(keys.size, 1);
    assert.equal(queries[1].listeners.size, 0);
    cleanup('b');
    assert.equal(keys.size, 0);
    assert.equal(queries[2].listeners.size, 0);
});
