import { test } from 'node:test';
import assert from 'node:assert/strict';
import { registerComboboxKeys, unregisterComboboxKeys, registerTagInputKeys, registerNavigationKeys } from '../../src/Soenneker.Quark.Suite/wwwroot/js/inputinterop.js';

function inputFixture(t) {
    const originalObserver = globalThis.MutationObserver;
    const observers = [];
    globalThis.MutationObserver = class {
        constructor(callback) { this.callback = callback; observers.push(this); }
        observe(target, options) { this.target = target; this.options = options; }
        disconnect() { this.disconnected = true; }
    };
    t.after(() => {
        if (originalObserver === undefined) delete globalThis.MutationObserver;
        else globalThis.MutationObserver = originalObserver;
    });
    const handlers = new Map();
    const attributes = new Map([['aria-expanded', 'false']]);
    const input = {
        value: '', disabled: false, readOnly: false,
        getAttribute: key => attributes.get(key),
        addEventListener(name, handler) { assert.equal(handlers.has(name), false); handlers.set(name, handler); },
        removeEventListener(name, handler) { assert.equal(handlers.get(name), handler); handlers.delete(name); }
    };
    registerComboboxKeys(input);
    t.after(() => unregisterComboboxKeys(input));
    return { input, attributes, handlers, observers, fire(key, options = {}) {
        const event = { key, preventDefault() { this.prevented = true; }, stopPropagation() { this.stopped = true; }, ...options };
        handlers.get('keydown')?.(event);
        return event;
    } };
}

test('navigation is prevented synchronously without blocking subsequent typing or closed-input editing', t => {
    const env = inputFixture(t);
    assert.ok(env.fire('ArrowDown').prevented);
    assert.equal(env.fire('a').prevented, undefined);
    assert.equal(env.fire('Home').prevented, undefined);
    assert.equal(env.fire('Enter').prevented, undefined);
    env.attributes.set('aria-expanded', 'true');
    for (const key of ['ArrowUp', 'Home', 'End', 'Enter', 'Escape']) assert.ok(env.fire(key).prevented);
    assert.equal(env.fire('b').prevented, undefined);
});

test('active option observation scrolls only open lists and disconnects on disposal', t => {
    const env = inputFixture(t);
    const originalDocument = globalThis.document;
    const calls = [];
    globalThis.document = { getElementById: id => id === 'option-2' ? { scrollIntoView: options => calls.push(options) } : null };
    t.after(() => {
        if (originalDocument === undefined) delete globalThis.document;
        else globalThis.document = originalDocument;
    });
    const observer = env.observers[0];
    assert.equal(observer.target, env.input);
    assert.deepEqual(observer.options.attributeFilter, ['aria-activedescendant', 'aria-expanded']);
    env.attributes.set('aria-activedescendant', 'option-2');
    observer.callback();
    assert.equal(calls.length, 0);
    env.attributes.set('aria-expanded', 'true');
    observer.callback();
    assert.deepEqual(calls, [{ block: 'nearest', inline: 'nearest' }]);
    unregisterComboboxKeys(env.input);
    assert.equal(observer.disconnected, true);
});

test('IME confirmation and keyboard editing shortcuts are preserved', t => {
    const env = inputFixture(t);
    env.attributes.set('aria-expanded', 'true');
    const composing = env.fire('Enter', { isComposing: true });
    assert.ok(composing.stopped);
    assert.equal(composing.prevented, undefined);
    assert.ok(env.fire('Enter', { keyCode: 229 }).stopped);
    for (const modifier of ['ctrlKey', 'metaKey', 'altKey'])
        assert.equal(env.fire('Home', { [modifier]: true }).prevented, undefined);
    env.input.readOnly = true;
    assert.equal(env.fire('ArrowDown').prevented, undefined);
    env.input.readOnly = false;
    env.input.disabled = true;
    assert.equal(env.fire('ArrowDown').prevented, undefined);
});

test('chip removal only intercepts empty multiple inputs and listeners are idempotent and removable', t => {
    const env = inputFixture(t);
    assert.equal(env.fire('Backspace').prevented, undefined);
    env.attributes.set('data-combobox-multiple', 'true');
    assert.ok(env.fire('Backspace').prevented);
    env.input.value = 'search';
    assert.equal(env.fire('Backspace').prevented, undefined);
    registerComboboxKeys(env.input);
    assert.equal(env.handlers.size, 1);
    unregisterComboboxKeys(env.input);
    assert.equal(env.handlers.size, 0);
    registerComboboxKeys(env.input);
    assert.equal(env.handlers.size, 1);
});

test('tag delimiters prevent native submit and separator insertion without swallowing editing', t => {
    const env = inputFixture(t);
    registerTagInputKeys(env.input, [',', ';']);
    for (const key of ['Enter', ',', ';', 'Backspace', 'ArrowLeft']) assert.ok(env.fire(key).prevented);
    env.input.value = 'text';
    assert.equal(env.fire('ArrowLeft').prevented, undefined);
    assert.equal(env.fire('Backspace').prevented, undefined);
    assert.equal(env.fire('a').prevented, undefined);
    assert.equal(env.fire('ArrowDown').prevented, undefined);
    env.attributes.set('aria-expanded', 'true');
    assert.ok(env.fire('ArrowDown').prevented);
    assert.equal(env.fire('Enter', { isComposing: true }).prevented, undefined);
    assert.ok(env.fire('Enter', { isComposing: true }).stopped);
});

test('composite navigation prevents only registered unmodified keys and cleans up', t => {
    const env = inputFixture(t);
    registerNavigationKeys(env.input, ['ArrowDown', 'Enter']);
    assert.ok(env.fire('ArrowDown').prevented);
    assert.ok(env.fire('Enter').prevented);
    assert.equal(env.fire('Tab').prevented, undefined);
    assert.equal(env.fire('a').prevented, undefined);
    assert.equal(env.fire('ArrowDown', { ctrlKey: true }).prevented, undefined);
    unregisterComboboxKeys(env.input);
    assert.equal(env.handlers.size, 0);
    registerComboboxKeys(env.input);
});
