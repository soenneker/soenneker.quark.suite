import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import { test } from 'node:test';

const source = await readFile(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/infinitescrollinterop.js', import.meta.url), 'utf8');
const { observe, destroy } = await import(`data:text/javascript;base64,${Buffer.from(source).toString('base64')}`);
const instances = [];
globalThis.getComputedStyle = element => ({ overflowY: element.overflowY ?? 'visible' });
globalThis.IntersectionObserver = class {
    constructor(callback, options) { this.callback = callback; this.options = options; instances.push(this); }
    observe(element) { this.element = element; }
    disconnect() { this.disconnected = true; }
    intersect() { this.callback([{ isIntersecting: true }]); }
};

test('prefetches in nearest scroll root, suppresses duplicates, and rearms short lists', async () => {
    const root = { overflowY: 'auto', parentElement: null };
    const sentinel = { isConnected: true, parentElement: { parentElement: root } };
    let calls = 0;
    const callback = { invokeMethodAsync: async name => { assert.equal(name, 'RequestMore'); calls++; } };
    observe(sentinel, callback, true, 300);
    const first = instances.at(-1);
    assert.equal(first.options.root, root);
    assert.equal(first.options.rootMargin, '0px 0px 300px 0px');
    first.intersect(); first.intersect();
    assert.equal(calls, 1);
    assert.equal(first.disconnected, true);
    observe(sentinel, callback, true, 300);
    first.intersect();
    instances.at(-1).intersect();
    assert.equal(calls, 2);
    destroy(sentinel);
});

test('disabled and disposed observers ignore queued notifications', () => {
    const sentinel = { isConnected: true, parentElement: null };
    let calls = 0;
    const callback = { invokeMethodAsync: async () => { calls++; } };
    observe(sentinel, callback, true, -1);
    const first = instances.at(-1);
    assert.equal(first.options.root, null);
    assert.equal(first.options.rootMargin, '0px 0px 0px 0px');
    observe(sentinel, callback, false, 300);
    first.intersect();
    assert.equal(calls, 0);
    observe(sentinel, callback, true, 300);
    const second = instances.at(-1);
    destroy(sentinel);
    second.intersect();
    assert.equal(calls, 0);
    assert.equal(second.disconnected, true);
});
