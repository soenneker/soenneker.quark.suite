import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import { test } from 'node:test';
const source = await readFile(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/swipeareainterop.js', import.meta.url), 'utf8');
const { configure, destroy } = await import(`data:text/javascript;base64,${Buffer.from(source).toString('base64')}`);

class Target {
    listeners = new Map();
    addEventListener(name, fn) { const items = this.listeners.get(name) ?? []; items.push(fn); this.listeners.set(name, items); }
    removeEventListener(name, fn) { this.listeners.set(name, (this.listeners.get(name) ?? []).filter(item => item !== fn)); }
    emit(name, values = {}) {
        const event = { target: this, timeStamp: 100, cancelable: true, defaultPrevented: false,
            preventDefault() { this.defaultPrevented = true; }, stopPropagation() {}, ...values };
        for (const fn of this.listeners.get(name) ?? []) fn(event);
        return event;
    }
    closest() { return null; }
}
function setup(options = {}) {
    const doc = new Target(); doc.defaultView = new Target(); doc.documentElement = { clientWidth: 1000 };
    const element = new Target(); element.ownerDocument = doc; element.isConnected = true;
    element.getBoundingClientRect = () => ({ left: 0, right: 1000, top: 0, bottom: 1000 });
    const calls = [];
    const callback = { invokeMethodAsync: async (method, direction) => { calls.push([method, direction]); } };
    const settings = { left: true, right: true, up: true, down: true, threshold: 60, maxDuration: 1000,
        startEdge: 'none', edgeSize: 32, preserveNativeEdgeGestures: true, enableMouse: false, ...options };
    configure(element, callback, settings);
    const point = (x, y) => ({ clientX: x, clientY: y, identifier: 1 });
    return { element, doc, calls, callback, settings,
        start(x = 500, y = 500, extra = {}) { return element.emit('touchstart', { touches: [point(x, y)], ...extra }); },
        move(x, y, extra = {}) { return doc.emit('touchmove', { touches: [point(x, y)], timeStamp: 200, ...extra }); },
        end(x, y, extra = {}) { return doc.emit('touchend', { changedTouches: [point(x, y)], timeStamp: 300, ...extra }); } };
}
for (const [direction, x, y] of [['left', 400, 500], ['right', 600, 500], ['up', 500, 400], ['down', 500, 600]]) {
    test(`recognizes ${direction} exactly once on release`, () => {
        const s = setup(); s.start(); assert.equal(s.move(x, y).defaultPrevented, true);
        assert.equal(s.calls.length, 0); s.end(x, y); s.end(x, y);
        assert.deepEqual(s.calls, [['NotifySwipe', direction]]); destroy(s.element);
    });
}
test('preserves unhandled scrolling, diagonal motion, short drags, and slow gestures', () => {
    for (const [options, x, y, extra] of [[{ up: false }, 500, 400, {}], [{}, 600, 600, {}],
        [{}, 505, 500, {}], [{}, 600, 500, { timeStamp: 2000 }]]) {
        const s = setup(options); s.start();
        assert.equal(s.move(x, y, extra).defaultPrevented, false); s.end(x, y);
        assert.equal(s.calls.length, 0); destroy(s.element);
    }
});
test('requires configured start edge and reserves native viewport edges', () => {
    const s = setup({ startEdge: 'left', edgeSize: 50 });
    for (const x of [10, 500]) { s.start(x); s.move(x + 100, 500); s.end(x + 100, 500); }
    assert.equal(s.calls.length, 0);
    s.start(30); s.move(130, 500); s.end(130, 500);
    assert.equal(s.calls.length, 1); destroy(s.element);
});
test('ignores controls, multi-touch, canceled gestures, and noncancelable scrolling', () => {
    const s = setup();
    s.start(500, 500, { target: { closest: () => ({}) } }); s.move(600, 500); s.end(600, 500);
    s.start(); s.doc.emit('touchstart', { touches: [{}, {}] }); s.move(600, 500); s.end(600, 500);
    s.start(); s.move(600, 500); s.doc.emit('touchcancel'); s.end(600, 500);
    s.start(); s.move(600, 500, { cancelable: false }); s.end(600, 500);
    assert.equal(s.calls.length, 0); destroy(s.element);
});
test('unchanged configuration keeps active gestures; disable and disposal remove listeners', () => {
    const s = setup(); s.start(); configure(s.element, s.callback, s.settings); s.move(600, 500); s.end(600, 500);
    assert.equal(s.calls.length, 1);
    configure(s.element, s.callback, { ...s.settings, disabled: true });
    assert.equal(s.doc.listeners.get('touchmove').length, 0);
    configure(s.element, s.callback, s.settings); s.start(); destroy(s.element); s.move(600, 500); s.end(600, 500);
    assert.equal(s.calls.length, 1); assert.equal(s.doc.defaultView.listeners.get('blur').length, 0);
});
test('mouse is opt-in and drag clicks are suppressed without blocking keyboard clicks', () => {
    const s = setup({ enableMouse: true });
    s.element.emit('mousedown', { button: 0, clientX: 500, clientY: 500 });
    s.doc.emit('mousemove', { buttons: 1, clientX: 600, clientY: 500, timeStamp: 200 });
    s.doc.emit('mouseup', { clientX: 600, clientY: 500, timeStamp: 300 });
    assert.equal(s.calls.length, 1);
    assert.equal(s.element.emit('click', { detail: 1, timeStamp: 310 }).defaultPrevented, true);
    assert.equal(s.element.emit('click', { detail: 0, timeStamp: 310 }).defaultPrevented, false);
    destroy(s.element);
});

test('ignores reversed gestures and permits an explicit native-edge opt-out', () => {
    const s = setup({ preserveNativeEdgeGestures: false });
    s.start(); s.move(600, 500); s.end(400, 500);
    assert.equal(s.calls.length, 0);
    s.start(5); s.move(105, 500); s.end(105, 500);
    assert.equal(s.calls.length, 1); destroy(s.element);
});
test('nested areas do not dispatch the same swipe twice', () => {
    const outer = setup(); const inner = setup();
    inner.element.parentElement = outer.element;
    outer.start(500, 500, { target: inner.element }); outer.move(600, 500); outer.end(600, 500);
    assert.equal(outer.calls.length, 0);
    inner.start(); inner.move(600, 500); inner.end(600, 500);
    assert.equal(inner.calls.length, 1);
    destroy(inner.element); destroy(outer.element);
});
