import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createPreview, retain, dispose, insertionTarget } from '../../src/Soenneker.Quark.Suite/wwwroot/js/filedropzoneinterop.js';

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
