import { test } from 'node:test';
import assert from 'node:assert/strict';
import { update, destroy } from '../../src/Soenneker.Quark.Suite/wwwroot/js/collapseinterop.js';

test('collapse tracks content growth, axis changes and disconnects on disposal', () => {
    let notify;
    let disconnected = false;
    const original = globalThis.ResizeObserver;
    globalThis.ResizeObserver = class {
        constructor(callback) { notify = callback; }
        observe() {}
        disconnect() { disconnected = true; }
    };
    try {
        let height = 80;
        const content = { style: {}, getBoundingClientRect: () => ({ width: 300, height }) };
        const element = { style: {}, firstElementChild: content };
        update(element, false, false);
        assert.equal(element.style.maxHeight, '0px');
        update(element, true, false);
        assert.equal(element.style.maxHeight, '80px');
        height = 160;
        notify();
        assert.equal(element.style.maxHeight, '160px');
        update(element, true, true);
        assert.equal(element.style.maxHeight, '');
        assert.equal(element.style.maxWidth, '300px');
        assert.equal(content.style.width, 'max-content');
        update(element, false, true);
        height = 240;
        notify();
        assert.equal(element.style.maxWidth, '0px');
        update(element, true, false);
        assert.equal(content.style.width, '');
        assert.equal(element.style.maxWidth, '');
        assert.equal(element.style.maxHeight, '240px');
        destroy(element);
        assert.equal(disconnected, true);
    } finally {
        globalThis.ResizeObserver = original;
    }
});
