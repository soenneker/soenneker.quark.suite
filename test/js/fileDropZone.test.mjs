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
