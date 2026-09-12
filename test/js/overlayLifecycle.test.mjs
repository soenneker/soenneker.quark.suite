import { test } from 'node:test';
import assert from 'node:assert/strict';
import { activate, activateScrollLock, deactivate, releaseScrollLocks } from '../../src/Soenneker.Quark.Suite/wwwroot/js/overlayinterop.js';

function fixture(t) {
    let styleReads = 0;
    globalThis.document = { body: { style: { overflow: 'auto', paddingRight: '3px' } },
        documentElement: { clientWidth: 980 }, activeElement: null };
    globalThis.window = { innerWidth: 1000, getComputedStyle(element) {
        styleReads++;
        return { display: element.hidden ? 'none' : 'block', visibility: 'visible' };
    } };
    t.after(releaseScrollLocks);
    function element() {
        return { listeners: new Map(), children: [], hidden: false,
            focus() { document.activeElement = this; },
            hasAttribute() { return false; }, getAttribute() { return null; },
            querySelector() { return null; }, querySelectorAll() { return this.children; },
            contains(child) { return child === this || this.children.includes(child); },
            addEventListener(name, handler) { this.listeners.set(name, handler); },
            removeEventListener(name, handler) { if (this.listeners.get(name) === handler) this.listeners.delete(name); } };
    }
    return { element, get styleReads() { return styleReads; } };
}

test('closing a non-locking overlay cannot release another overlay lock', t => {
    fixture(t);
    activateScrollLock('modal');
    activate('popover', null, false, false);
    deactivate('popover', true);
    assert.equal(document.body.style.overflow, 'hidden');
    deactivate('modal', true);
    assert.deepEqual(document.body.style, { overflow: 'auto', paddingRight: '3px' });
});

test('repeated activation and lock-option changes retain exactly one owned lock', t => {
    fixture(t);
    activate('a', null, false, false);
    activateScrollLock('a');
    activate('a', null, false, true);
    activateScrollLock('b');
    activate('a', null, false, false);
    assert.equal(document.body.style.overflow, 'hidden');
    deactivate('b', true);
    assert.equal(document.body.style.overflow, 'auto');
    deactivate('a', true);
    assert.equal(document.body.style.paddingRight, '3px');
});

test('explicitly retained locks can be released after focus deactivation', t => {
    fixture(t);
    activateScrollLock('a');
    deactivate('a', false);
    assert.equal(document.body.style.overflow, 'hidden');
    deactivate('a', true);
    assert.equal(document.body.style.overflow, 'auto');
});

test('global cleanup without owned locks preserves caller body styles', t => {
    fixture(t);
    document.body.style.overflow = 'scroll';
    document.body.style.paddingRight = '9px';
    releaseScrollLocks();
    assert.deepEqual(document.body.style, { overflow: 'scroll', paddingRight: '9px' });
});

test('changing focus-trap options removes the previous container listener', t => {
    const env = fixture(t), first = env.element(), second = env.element();
    activate('a', first, true, false);
    activate('a', second, false, false);
    assert.equal(first.listeners.size, 0);
    assert.equal(second.listeners.size, 0);
    activate('a', second, true, false);
    deactivate('a', true);
    assert.equal(second.listeners.size, 0);
});

test('focus entry and Tab wrapping inspect only the required boundary candidates', t => {
    const env = fixture(t), container = env.element();
    container.children = Array.from({ length: 200 }, env.element);
    activate('a', container, true, false);
    assert.equal(env.styleReads, 1);
    assert.equal(document.activeElement, container.children[0]);
    let prevented = false;
    container.listeners.get('keydown')({ key: 'Tab', shiftKey: true, preventDefault() { prevented = true; } });
    assert.equal(env.styleReads, 3);
    assert.equal(prevented, true);
    assert.equal(document.activeElement, container.children.at(-1));
    container.children[0].hidden = true;
    container.children.at(-1).hidden = true;
    document.activeElement = container.children.at(-2);
    container.listeners.get('keydown')({ key: 'Tab', shiftKey: false, preventDefault() {} });
    assert.equal(document.activeElement, container.children[1]);
});

test('only the top overlay traps Tab and an empty container retains focus', t => {
    const env = fixture(t), first = env.element(), second = env.element();
    activate('a', first, true, false);
    activate('b', second, true, false);
    let prevented = 0;
    const event = { key: 'Tab', preventDefault() { prevented++; } };
    first.listeners.get('keydown')(event);
    assert.equal(prevented, 0);
    second.listeners.get('keydown')(event);
    assert.equal(prevented, 1);
    assert.equal(document.activeElement, second);
    releaseScrollLocks();
    assert.equal(first.listeners.size + second.listeners.size, 0);
});
