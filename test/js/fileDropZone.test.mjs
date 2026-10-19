import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createPreview, retain, dispose, insertionTarget, initialize } from '../../src/Soenneker.Quark.Suite/wwwroot/js/filedropzoneinterop.js';

test('drop placement uses row midpoints and excludes the dragged file', () => {
    const rows = ['a', 'b', 'c'].map((id, i) => ({ dataset: { fileId: id }, getBoundingClientRect: () => ({ top: i * 60, height: 50 }) }));
    assert.equal(insertionTarget(rows, -10), 'a');
    assert.equal(insertionTarget(rows, 60), 'b');
    assert.equal(insertionTarget(rows, 110), 'c');
    assert.equal(insertionTarget(rows, 300), null);
    assert.equal(insertionTarget(rows, 0, 'a'), 'b');
    assert.equal(insertionTarget([], 0), null);
});

test('previews retain their object URLs after input removal and revoke on removal/disposal', () => {
    const revoked = [];
    const originalCreate = URL.createObjectURL, originalRevoke = URL.revokeObjectURL;
    const originalDocument = globalThis.document;
    let next = 0;
    URL.createObjectURL = () => `blob:test-${++next}`;
    URL.revokeObjectURL = url => revoked.push(url);
    globalThis.document = { getElementById: () => ({ files: [new Blob(['image'], { type: 'image/png' })] }) };
    try {
        assert.equal(createPreview('one', 'input', 0, 'a', 'image/png'), 'blob:test-1');
        assert.equal(createPreview('one', 'input', 0, 'b', 'image/png'), 'blob:test-2');
        assert.equal(createPreview('two', 'input', 0, 'c', 'image/png'), 'blob:test-3');
        globalThis.document.getElementById = () => null;
        retain('one', ['a', 'b']);
        assert.deepEqual(revoked, []);
        retain('one', ['b']);
        assert.deepEqual(revoked, ['blob:test-1']);
        dispose('one');
        dispose('one');
        assert.deepEqual(revoked, ['blob:test-1', 'blob:test-2']);
        dispose('two');
        assert.equal(revoked.length, 3);
        assert.equal(createPreview('one', 'missing', 0, 'd', 'image/png'), null);
    } finally {
        URL.createObjectURL = originalCreate;
        URL.revokeObjectURL = originalRevoke;
        globalThis.document = originalDocument;
    }
});

test('MIME fallback handles PDFs without copying through .NET and unsupported types are skipped', () => {
    const originalDocument = globalThis.document, originalCreate = URL.createObjectURL;
    const file = new Blob(['%PDF'], { type: '' });
    globalThis.document = { getElementById: () => ({ files: [file] }) };
    URL.createObjectURL = blob => { assert.equal(blob.type, 'application/pdf'); return 'blob:pdf'; };
    try {
        assert.equal(createPreview('pdf', 'input', 0, 'pdf', 'application/pdf'), 'blob:pdf');
        assert.equal(createPreview('pdf', 'input', 0, 'text', 'text/plain'), null);
    } finally {
        dispose('pdf');
        globalThis.document = originalDocument;
        URL.createObjectURL = originalCreate;
    }
});


test('FileDropZone decorates shared drops with insertion position and cleans up its hooks', async t => {
    const { register, unregister } = await import('../../src/Soenneker.Quark.Suite/wwwroot/js/filedropinterop.js');
    const originals = { document: globalThis.document, window: globalThis.window, DataTransfer: globalThis.DataTransfer, MutationObserver: globalThis.MutationObserver };
    const order = [], handlers = new Map();
    const input = { disabled: false, dispatchEvent() { order.push('change'); } };
    const placeholder = { hidden: true, style: {} }, ghost = { hidden: true }, target = { dataset: {} };
    const root = {
        dataset: {}, clientTop: 0, scrollTop: 0,
        querySelector(selector) { return selector.includes('placeholder') ? placeholder : selector.includes('ghost') ? ghost : target; },
        querySelectorAll() { return [{ dataset: { fileId: 'existing' }, getBoundingClientRect: () => ({ top: 30, height: 50, bottom: 80 }) }]; },
        getBoundingClientRect: () => ({ top: 0, bottom: 100 }),
        addEventListener(name, handler) { assert.ok(!handlers.has(name)); handlers.set(name, handler); },
        removeEventListener(name) { handlers.delete(name); },
        removeAttribute() {}
    };
    globalThis.document = Object.assign(new EventTarget(), { getElementById: () => input });
    globalThis.window = new EventTarget();
    globalThis.MutationObserver = class { observe() {} disconnect() {} };
    globalThis.DataTransfer = class { files = []; items = { add: file => this.files.push(file) }; };
    t.after(() => { dispose('shared'); unregister(root); Object.assign(globalThis, originals); });
    register(root, 'input');
    initialize('shared', root, { async invokeMethodAsync(method, before) { order.push([method, before]); } });
    const event = { clientY: 10, dataTransfer: { types: ['Files'], files: [{ name: 'image.png' }] }, preventDefault() {}, stopPropagation() {} };
    handlers.get('dragenter')(event);
    assert.equal(placeholder.hidden, false);
    assert.equal(target.dataset.dragging, 'true');
    await handlers.get('drop')(event);
    assert.deepEqual(order, [['SetDropTarget', 'existing'], 'change']);
    assert.equal(placeholder.hidden, true);
    assert.equal(target.dataset.dragging, 'false');
    assert.deepEqual(input.files, event.dataTransfer.files);
    dispose('shared');
    order.length = 0;
    await handlers.get('drop')(event);
    assert.deepEqual(order, ['change']);
});


test('keyboard reorder delegates intent, gates pending calls, restores focus and removes listeners', async t => {
    const originals = { document: globalThis.document, window: globalThis.window };
    const calls = [], focus = [];
    let complete;
    const row = { dataset: { fileId: 'b' }, querySelector: () => handle };
    const handle = { closest: selector => selector === '[data-file-drag-handle]' ? handle : row,
        focus: () => focus.push('b') };
    const root = Object.assign(new EventTarget(), {
        dataset: { canReorder: 'true' }, contains: target => target === handle,
        querySelector: () => ({ hidden: true }), querySelectorAll: () => [row]
    });
    const handlers = new Map();
    globalThis.document = { addEventListener(name, handler, options) {
        handlers.set(name, handler);
        options.signal.addEventListener('abort', () => handlers.delete(name));
    } };
    globalThis.window = new EventTarget();
    t.after(() => { dispose('keyboard'); Object.assign(globalThis, originals); });
    initialize('keyboard', root, { invokeMethodAsync(...args) {
        calls.push(args); return new Promise(resolve => complete = resolve);
    } });
    let prevented = 0;
    const key = (value, extra = {}) => handlers.get('keydown')({ key: value, target: handle,
        preventDefault() { prevented++; }, ...extra });
    key('ArrowDown', { isComposing: true });
    key('ArrowDown', { ctrlKey: true });
    key('Tab');
    assert.equal(calls.length, 0);
    key('ArrowDown');
    assert.deepEqual(calls, [['MoveFileByKey', 'b', 'ArrowDown']]);
    assert.equal(prevented, 1);
    key('Home');
    assert.equal(calls.length, 1);
    complete();
    await Promise.resolve();
    assert.deepEqual(focus, ['b']);
    root.dataset.canReorder = 'false';
    key('End');
    assert.equal(calls.length, 1);
    root.dataset.canReorder = 'true';
    key('Home');
    dispose('keyboard');
    complete();
    await Promise.resolve();
    assert.deepEqual(focus, ['b']);
    assert.equal(handlers.size, 0);
});


test('drag frames sleep when stationary, coalesce invalidations, autoscroll and cancel cleanly', t => {
    const names = ['document', 'window', 'innerWidth', 'innerHeight', 'ResizeObserver', 'MutationObserver', 'requestAnimationFrame', 'cancelAnimationFrame'];
    const originals = Object.fromEntries(names.map(name => [name, globalThis[name]]));
    const frames = new Map(), observers = [], handlers = new Map();
    let nextFrame = 0, measurements = 0, writes = 0;
    const style = () => new Proxy({}, { set(target, key, value) { writes++; target[key] = value; return true; } });
    const placeholder = { hidden: true, style: style() };
    const ghost = { hidden: true, style: style(), offsetWidth: 100, offsetHeight: 40 };
    const handle = { closest: selector => selector === '[data-file-drag-handle]' ? handle : row,
        focus() {}, setPointerCapture() {}, hasPointerCapture: () => false,
        setAttribute() {}, removeAttribute() {}, getAttribute: () => 'Reorder file' };
    const row = { dataset: { fileId: 'a' }, style: style(), isConnected: true,
        getBoundingClientRect() { measurements++; return { top: 100, bottom: 160, height: 60, width: 200 }; } };
    const other = { dataset: { fileId: 'b' }, getBoundingClientRect() {
        measurements++; return { top: 160, bottom: 220, height: 60, width: 200 };
    } };
    const surface = prefix => ({ addEventListener(name, fn, options) {
        handlers.set(prefix + name, fn);
        options.signal.addEventListener('abort', () => handlers.delete(prefix + name));
    } });
    const root = Object.assign(surface('root:'), {
        isConnected: true, dataset: { canReorder: 'true' }, clientTop: 0, scrollTop: 0,
        contains: target => target === handle,
        querySelector: selector => selector.includes('placeholder') ? placeholder : ghost,
        querySelectorAll: () => [row, other],
        getBoundingClientRect() { measurements++; return { top: 90, bottom: 240, left: 0, right: 300 }; }
    });
    globalThis.document = Object.assign(surface('doc:'), { documentElement: {} });
    globalThis.window = Object.assign(surface('win:'), { scrollY: 50, scrollBy(x, y) { this.scrollY = Math.max(0, this.scrollY + y); } });
    globalThis.innerWidth = 800; globalThis.innerHeight = 600;
    globalThis.ResizeObserver = globalThis.MutationObserver = class {
        constructor(callback) { this.callback = callback; this.disconnected = false; observers.push(this); }
        observe() {} disconnect() { this.disconnected = true; }
    };
    globalThis.requestAnimationFrame = callback => { frames.set(++nextFrame, callback); return nextFrame; };
    globalThis.cancelAnimationFrame = id => frames.delete(id);
    const flush = () => { const pending = [...frames.values()]; frames.clear(); pending.forEach(callback => callback()); };
    const pointer = (name, x, y, id = 1) => handlers.get(name)({ target: handle, button: 0, pointerId: id,
        clientX: x, clientY: y, preventDefault() {} });
    t.after(() => { dispose('frames'); Object.assign(globalThis, originals); });
    initialize('frames', root, { invokeMethodAsync: () => Promise.resolve() });
    assert.equal(observers.length, 0);
    pointer('root:pointerdown', 100, 130);
    pointer('doc:pointermove', 110, 140);
    pointer('doc:pointermove', 120, 150);
    assert.equal(frames.size, 1);
    flush();
    assert.equal(frames.size, 0);
    const idleMeasurements = measurements, idleWrites = writes;
    pointer('doc:pointermove', 120, 150);
    assert.equal(frames.size, 0);
    assert.equal(measurements, idleMeasurements);
    handlers.get('win:scroll')(); observers[0].callback();
    assert.equal(frames.size, 1);
    flush();
    assert.ok(measurements > idleMeasurements);
    assert.equal(writes, idleWrites);
    observers[1].callback([{ target: placeholder }, { target: ghost }]);
    assert.equal(frames.size, 0);
    observers[1].callback([{ target: other }]);
    assert.equal(frames.size, 1);
    flush();
    pointer('doc:pointermove', 120, 10);
    flush();
    assert.equal(window.scrollY, 38);
    assert.equal(frames.size, 1);
    while (frames.size) flush();
    assert.equal(window.scrollY, 0);
    pointer('doc:pointermove', 130, 150);
    pointer('doc:pointercancel', 130, 150, 2);
    assert.equal(frames.size, 1);
    pointer('doc:pointercancel', 130, 150);
    assert.equal(frames.size, 0);
    assert.ok(observers.every(observer => observer.disconnected));
    assert.equal(ghost.hidden, true);
    assert.equal(placeholder.hidden, true);
    pointer('root:pointerdown', 100, 130);
    pointer('doc:pointermove', 110, 140);
    root.isConnected = false;
    observers.at(-1).callback([{ target: document.documentElement }]);
    assert.equal(frames.size, 0);
    assert.ok(observers.every(observer => observer.disconnected));
    dispose('frames');
    assert.equal(handlers.size, 0);
});
