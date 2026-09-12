import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as resizable from '../../src/Soenneker.Quark.Suite/wwwroot/js/resizableinterop.js';

class ElementStub {
  dataset = {};
  listeners = new Map();
  capture = null;
  reads = 0;
  addEventListener(name, callback) {
    if (!this.listeners.has(name)) this.listeners.set(name, new Set());
    this.listeners.get(name).add(callback);
  }
  removeEventListener(name, callback) { this.listeners.get(name)?.delete(callback); }
  emit(name, properties = {}) {
    const event = { pointerId: 1, button: 0, isPrimary: true, clientX: 60, clientY: 120,
      preventDefault() {}, ...properties };
    this.listeners.get(name)?.forEach(callback => callback(event));
  }
  getBoundingClientRect() {
    this.reads++;
    return { left: 10, top: 20, width: 200, height: 400, right: 210, bottom: 420 };
  }
  setPointerCapture(id) { this.capture = id; }
  hasPointerCapture(id) { return this.capture === id; }
  releasePointerCapture(id) { assert.equal(this.capture, id); this.capture = null; }
}

function fixture(t, orientation = 'horizontal', direction = 'ltr') {
  const frames = new Map(), calls = [];
  let next = 1;
  globalThis.window = Object.assign(new ElementStub(), {
    requestAnimationFrame(callback) { const id = next++; frames.set(id, callback); return id; },
    cancelAnimationFrame(id) { frames.delete(id); },
    getComputedStyle() { return { direction }; }
  });
  const group = new ElementStub(), handle = new ElementStub(), other = new ElementStub();
  handle.dataset.slot = other.dataset.slot = 'resizable-handle';
  group.children = [new ElementStub(), handle, new ElementStub(), other];
  const receiver = { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } };
  resizable.registerHandle(handle, group, orientation, receiver, 0);
  resizable.registerHandle(other, group, orientation, receiver, 1);
  t.after(() => { resizable.unregisterHandle(handle); resizable.unregisterHandle(other); resizable.stopDrag(); });
  return { frames, calls, group, handle, other, receiver,
    listenerCount: () => [...window.listeners.values()].reduce((count, set) => count + set.size, 0),
    flush() { const callbacks = [...frames.values()]; frames.clear(); callbacks.forEach(callback => callback()); }
  };
}

test('idle handles have no global listeners; movement batches and preserves the owning pointer', t => {
  const env = fixture(t);
  assert.equal(env.listenerCount(), 0);
  env.handle.emit('pointerdown');
  assert.equal(env.listenerCount(), 5);
  const reads = env.group.reads;
  for (let i = 1; i <= 100; i++) window.emit('pointermove', { clientX: 10 + i });
  window.emit('pointermove', { pointerId: 2, clientX: 200 });
  window.emit('mousemove', { clientX: 200 });
  assert.equal(env.frames.size, 1);
  env.flush();
  assert.equal(env.group.reads - reads, 1);
  assert.deepEqual(env.calls, [['HandlePointerDragMove', 0, 50, 200]]);
});

for (const end of ['pointerup', 'pointercancel']) {
  test(`${end} commits final coordinates and releases capture and listeners`, t => {
    const env = fixture(t);
    env.handle.emit('pointerdown');
    window.emit('pointermove');
    window.emit(end, { pointerId: 2 });
    window.emit('mouseup');
    assert.equal(env.handle.capture, 1);
    window.emit(end, { clientX: 160 });
    assert.deepEqual(env.calls, [['HandlePointerDragEnd', 0, 75, 200]]);
    assert.equal(env.frames.size, 0);
    assert.equal(env.handle.capture, null);
    assert.equal(env.listenerCount(), 0);
  });
}

for (const operation of ['unregister', 'rebind', 'stop']) {
  test(`${operation} cancels an active handle even while another remains registered`, t => {
    const env = fixture(t);
    env.handle.emit('pointerdown');
    window.emit('pointermove');
    if (operation === 'unregister') resizable.unregisterHandle(env.handle);
    if (operation === 'rebind') resizable.registerHandle(env.handle, env.group, 'vertical', env.receiver, 0);
    if (operation === 'stop') resizable.stopDrag();
    env.flush();
    window.emit('pointerup');
    assert.equal(env.calls.length, 0);
    assert.equal(env.handle.capture, null);
    assert.equal(env.listenerCount(), 0);
    env.other.emit('pointerdown');
    window.emit('pointerup', { clientX: 160 });
    assert.deepEqual(env.calls, [['HandlePointerDragEnd', 1, 75, 200]]);
  });
}

test('another pointer cannot replace the current handle gesture', t => {
  const env = fixture(t);
  env.handle.emit('pointerdown');
  env.other.emit('pointerdown', { pointerId: 2 });
  env.other.emit('mousedown');
  window.emit('pointerup');
  assert.deepEqual(env.calls, [['HandlePointerDragEnd', 0, 25, 200]]);
  assert.equal(env.other.capture, null);
});

test('mouse-only fallback still moves and ends', t => {
  const env = fixture(t);
  env.handle.emit('mousedown');
  window.emit('mousemove');
  env.flush();
  window.emit('mouseup', { clientX: 160 });
  assert.deepEqual(env.calls, [['HandlePointerDragMove', 0, 25, 200], ['HandlePointerDragEnd', 0, 75, 200]]);
  assert.equal(env.listenerCount(), 0);
});

for (const [orientation, direction, percentage, size] of [['horizontal', 'rtl', 75, 200], ['vertical', 'ltr', 25, 400]]) {
  test(`${orientation} ${direction} metrics retain their coordinate semantics`, t => {
    const env = fixture(t, orientation, direction);
    env.handle.emit('pointerdown');
    window.emit('pointerup');
    assert.deepEqual(env.calls, [['HandlePointerDragEnd', 0, percentage, size]]);
  });
}

test('legacy start selects the requested direct handle without including panels', t => {
  const env = fixture(t);
  resizable.startDrag(env.group, 1, 60, 120, 'horizontal', env.receiver, 1);
  window.emit('pointerup');
  assert.deepEqual(env.calls, [['HandlePointerDragEnd', 1, 25, 200]]);
});

test('a disposed registration does not receive late diagnostic writes', async t => {
  const env = fixture(t);
  let finish;
  env.receiver.invokeMethodAsync = () => new Promise(resolve => { finish = resolve; });
  env.handle.emit('pointerdown');
  window.emit('pointermove');
  env.flush();
  resizable.unregisterHandle(env.handle);
  finish();
  await Promise.resolve();
  assert.equal(env.handle.dataset.resizableDotnet, undefined);
});
