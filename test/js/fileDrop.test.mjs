import { test } from 'node:test';
import assert from 'node:assert/strict';
import { register, unregister, configure } from '../../src/Soenneker.Quark.Suite/wwwroot/js/filedropinterop.js';

function fixture(t) {
    const originals = { document: globalThis.document, DataTransfer: globalThis.DataTransfer, MutationObserver: globalThis.MutationObserver };
    const handlers = new Map(), observers = [];
    const target = {
        dataset: {},
        addEventListener(name, handler) { assert.ok(!handlers.has(name)); handlers.set(name, handler); },
        removeEventListener(name, handler) { assert.equal(handlers.get(name), handler); handlers.delete(name); },
        removeAttribute() { delete this.dataset.dragActive; }
    };
    const changes = [];
    let input = { disabled: false, value: 'previous', dispatchEvent(event) { changes.push(event); } };
    globalThis.document = { getElementById: () => input };
    globalThis.DataTransfer = class {
        files = [];
        items = { add: file => this.files.push(file) };
    };
    globalThis.MutationObserver = class {
        constructor(callback) { this.callback = callback; observers.push(this); }
        observe() {}
        disconnect() { this.disconnected = true; }
    };
    t.after(() => { unregister(target); Object.assign(globalThis, originals); });
    return { target, handlers, changes, observers, get input() { return input; },
        replaceInput(value) { input = value; },
        fire(name, files = [{ name: 'test.png' }], types = ['Files']) {
            const event = { dataTransfer: { types, files }, preventDefault() { this.prevented = true; }, stopPropagation() {} };
            event.completed = handlers.get(name)?.(event);
            return event;
        }
    };
}

test('nested enters and rerenders preserve active state, repeated drops dispatch change, cleanup removes listeners', t => {
    const env = fixture(t);
    register(env.target, 'input');
    env.fire('dragenter'); env.fire('dragenter'); env.fire('dragleave', [], []);
    assert.equal(env.target.dataset.dragActive, 'true');
    register(env.target, 'input');
    assert.equal(env.target.dataset.dragActive, 'true');
    env.fire('dragleave', [], []);
    assert.equal(env.target.dataset.dragActive, 'false');
    const files = [{ name: 'one' }, { name: 'two' }];
    env.fire('drop', files); env.fire('drop', files);
    assert.deepEqual(env.input.files, files);
    assert.equal(env.input.value, '');
    assert.equal(env.changes.length, 2);
    assert.ok(env.changes.every(event => event.type === 'change' && event.bubbles));
    unregister(env.target);
    assert.equal(env.handlers.size, 0);
    assert.ok(env.observers[0].disconnected);
});

test('disabled targets suppress file navigation and updates reset active state', t => {
    const env = fixture(t);
    register(env.target, 'input');
    env.fire('dragenter');
    env.target.dataset.fileDropDisabled = 'true';
    env.observers[0].callback();
    assert.equal(env.target.dataset.dragActive, 'false');
    assert.ok(env.fire('drop').prevented);
    assert.equal(env.fire('dragover').dataTransfer.dropEffect, 'none');
    assert.equal(env.changes.length, 0);
    env.target.dataset.fileDropDisabled = 'false';
    env.input.disabled = true;
    env.fire('drop');
    assert.equal(env.changes.length, 0);
});

test('text drags are untouched and missing or replaced inputs are handled', t => {
    const env = fixture(t);
    register(env.target, 'input');
    assert.equal(env.fire('dragenter', [], ['text/plain']).prevented, undefined);
    assert.equal(env.fire('drop', [], ['text/plain']).prevented, undefined);
    env.replaceInput(null);
    assert.ok(env.fire('drop').prevented);
    const replacement = { dispatchEvent() { this.changed = true; } };
    env.replaceInput(replacement);
    env.fire('drop');
    assert.ok(replacement.changed);
});

test('editor hook records drop position before the input change and resets the overlay', t => {
    const env = fixture(t);
    const order = [];
    env.input.dispatchEvent = () => order.push('change');
    register(env.target, 'input', { beforeDrop: () => order.push('position'), onActive: value => order.push(value) });
    env.fire('dragenter'); env.fire('drop');
    assert.deepEqual(order, [true, false, 'position', 'change']);
});


test('async preparation snapshots files and serializes drops before dispatching change', async t => {
    const env = fixture(t);
    let resume;
    const ready = new Promise(resolve => { resume = resolve; });
    register(env.target, 'input');
    configure(env.target, { beforeDrop: () => ready });
    const event = env.fire('drop', [{ name: 'original' }]);
    event.dataTransfer.files.length = 0;
    await env.fire('drop', [{ name: 'second' }]).completed;
    assert.equal(env.changes.length, 0);
    resume();
    await event.completed;
    assert.deepEqual(env.input.files, [{ name: 'original' }]);
    assert.equal(env.changes.length, 1);
});

for (const reason of ['disabled', 'unregister', 'ownerDisposed', 'inputChanged']) {
    test(`pending drop is discarded after ${reason}`, async t => {
        const env = fixture(t);
        let resume;
        register(env.target, 'input');
        configure(env.target, { beforeDrop: () => new Promise(resolve => { resume = resolve; }) });
        const event = env.fire('drop');
        if (reason === 'disabled') env.target.dataset.fileDropDisabled = 'true';
        if (reason === 'unregister') unregister(env.target);
        if (reason === 'ownerDisposed') configure(env.target, null);
        if (reason === 'inputChanged') register(env.target, 'next-input');
        resume();
        await event.completed;
        assert.equal(env.changes.length, 0);
    });
}
