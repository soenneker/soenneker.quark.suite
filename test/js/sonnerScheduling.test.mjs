import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as sonner from '../../src/Soenneker.Quark.Suite/wwwroot/js/sonnerinterop.js';

class ElementStub {
  attributes = new Map();
  listeners = new Map();
  styles = new Map();
  writes = [];
  capture = null;
  style = { height: '', setProperty: (key, value) => { this.styles.set(key, value); this.writes.push(key); },
    removeProperty: key => this.styles.delete(key) };
  getAttribute(key) { return this.attributes.get(key) ?? null; }
  setAttribute(key, value) { this.attributes.set(key, value); }
  addEventListener(name, callback) { this.listeners.set(name, callback); }
  removeEventListener(name) { this.listeners.delete(name); }
  setPointerCapture(id) { this.capture = id; }
  releasePointerCapture(id) { assert.equal(this.capture, id); this.capture = null; }
  getBoundingClientRect() { return { width: 356, height: 80 }; }
  closest(selector) { return selector.startsWith('[data-sonner-toast]') ? this : null; }
}

globalThis.Element = ElementStub;

test('toast heights prepare all styles before reads and restore after measurements', () => {
  const operations = [], section = new ElementStub();
  const toasts = ['a', 'b'].map((id, index) => {
    const toast = new ElementStub();
    toast.setAttribute('data-toast-id', id);
    let height = `${index + 1}px`;
    Object.defineProperty(toast.style, 'height', { get: () => height, set(value) { operations.push(`set:${id}:${value}`); height = value; } });
    toast.getBoundingClientRect = () => { operations.push(`read:${id}`); return { height: (index + 1) * 100 }; };
    return toast;
  });
  section.querySelectorAll = () => toasts;
  assert.deepEqual(sonner.measureToastHeights(section), { a: 100, b: 200 });
  assert.deepEqual(operations, ['set:a:auto', 'set:b:auto', 'read:a', 'read:b', 'set:a:1px', 'set:b:2px']);
});

test('measurement failures restore every prepared height and skip unidentified elements', () => {
  const section = new ElementStub(), a = new ElementStub(), b = new ElementStub(), ignored = new ElementStub();
  a.setAttribute('data-toast-id', 'a'); b.setAttribute('data-toast-id', 'b');
  a.style.height = '1px'; b.style.height = '2px'; ignored.style.height = '3px';
  b.getBoundingClientRect = () => { throw new Error('measurement failed'); };
  section.querySelectorAll = () => [ignored, a, b];
  assert.throws(() => sonner.measureToastHeights(section), /measurement failed/);
  assert.deepEqual([a.style.height, b.style.height, ignored.style.height], ['1px', '2px', '3px']);
});

function fixture(t, position = 'right') {
  const frames = new Map(), calls = [];
  let next = 1;
  globalThis.requestAnimationFrame = callback => { const id = next++; frames.set(id, callback); return id; };
  globalThis.cancelAnimationFrame = id => frames.delete(id);
  const section = new ElementStub(), toast = new ElementStub();
  section.contains = target => target === toast;
  toast.setAttribute('data-toast-id', 'toast');
  toast.setAttribute('data-dismissible', 'true');
  toast.setAttribute('data-x-position', position);
  sonner.registerSwipeHandlers(section, { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } });
  t.after(() => sonner.unregisterSwipeHandlers(section));
  return { section, toast, frames, calls,
    emit(type, x = 0, props = {}) { section.listeners.get(type)?.({ button: 0, isPrimary: true, pointerId: 1, clientX: x, clientY: 0,
      target: toast, preventDefault() {}, ...props }); },
    flush() { const callbacks = [...frames.values()]; frames.clear(); callbacks.forEach(callback => callback()); }
  };
}

test('100 swipe moves produce one style write and foreign pointers cannot overwrite the gesture', t => {
  const env = fixture(t);
  env.emit('pointerdown');
  for (let i = 1; i <= 100; i++) env.emit('pointermove', i);
  env.emit('pointermove', 999, { pointerId: 2 });
  assert.equal(env.frames.size, 1);
  assert.equal(env.toast.writes.length, 0);
  env.flush();
  assert.deepEqual(env.toast.writes, ['--swipe-amount']);
  assert.equal(env.toast.styles.get('--swipe-amount'), '100px');
});

for (const operation of ['pointercancel', 'lostpointercapture', 'unregister']) {
  test(`${operation} cancels swipe work, releases capture and restores state`, t => {
    const env = fixture(t);
    env.emit('pointerdown');
    env.emit('pointermove', 100);
    if (operation === 'unregister') sonner.unregisterSwipeHandlers(env.section);
    else env.emit(operation);
    env.flush();
    assert.equal(env.toast.capture, null);
    assert.equal(env.toast.getAttribute('data-swiped'), 'false');
    assert.equal(env.toast.styles.size, 0);
    assert.equal(env.toast.writes.length, 0);
    assert.equal(env.calls.length, 0);
  });
}

for (const [position, movement, dismiss] of [['left', -120, true], ['right', 120, true], ['right', -120, false], ['center', -120, true]]) {
  test(`${position} swipe of ${movement}px preserves directional dismissal`, t => {
    const env = fixture(t, position);
    env.emit('pointerdown');
    env.emit('pointermove', movement);
    env.emit('pointerup', movement);
    assert.equal(env.frames.size, 0);
    assert.equal(env.toast.capture, null);
    assert.equal(env.calls.length, dismiss ? 1 : 0);
    assert.equal(env.toast.getAttribute('data-swipe-out'), String(dismiss));
  });
}

test('second presses and non-horizontal motion cannot steal or start a swipe', t => {
  const env = fixture(t);
  env.emit('pointerdown');
  env.emit('pointerdown', 500, { pointerId: 2 });
  env.emit('pointermove', 20, { clientY: 40 });
  assert.equal(env.frames.size, 0);
  env.emit('pointermove', 100);
  env.emit('pointerup', 100);
  assert.deepEqual(env.calls, [['DismissToastFromSwipe', 'toast']]);
});
