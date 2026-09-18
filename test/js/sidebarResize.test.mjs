import { test } from 'node:test';
import assert from 'node:assert/strict';
import { registerResizeHandle, unregisterResizeHandle } from '../../src/Soenneker.Quark.Suite/wwwroot/js/sidebarinterop.js';

class Element {
  listeners = new Map();
  attributes = new Map();
  capture = null;
  style = {
    transition: '',
    getPropertyValue(name) { return this[name] || ''; },
    setProperty(name, value) { this[name] = value; },
    removeProperty(name) { delete this[name]; }
  };
  addEventListener(name, callback) {
    if (!this.listeners.has(name)) this.listeners.set(name, new Set());
    this.listeners.get(name).add(callback);
  }
  removeEventListener(name, callback) { this.listeners.get(name)?.delete(callback); }
  emit(name, properties = {}) {
    const event = { pointerId: 1, button: 0, isPrimary: true, clientX: 100, preventDefault() {}, ...properties };
    this.listeners.get(name)?.forEach(callback => callback(event));
  }
  setAttribute(name, value) { this.attributes.set(name, value); }
  focus() {}
  setPointerCapture(id) { this.capture = id; }
  hasPointerCapture(id) { return this.capture === id; }
  releasePointerCapture() { this.capture = null; }
}

function fixture(t, right = false, staticSidebar = false, storageKey = null) {
  globalThis.document = new Element();
  let disconnected = false;
  globalThis.ResizeObserver = class { observe() {} disconnect() { disconnected = true; } };
  const root = new Element(), gap = new Element(), handle = new Element();
  const container = staticSidebar ? root : new Element();
  container.style.transition = 'width 200ms';
  gap.style.transition = 'width 200ms';
  container.getBoundingClientRect = () => ({ width: parseFloat(root.style.getPropertyValue('--sidebar-width')) || 256 });
  root.querySelector = () => staticSidebar ? null : gap;
  handle.closest = selector => selector.includes('resize-root') ? root : staticSidebar ? null : container;
  const calls = [];
  const receiver = { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } };
  registerResizeHandle(handle, receiver, 192, 480, right, storageKey);
  t.after(() => unregisterResizeHandle(handle));
  return { root, gap, container, handle, calls, receiver, disconnected: () => disconnected };
}

for (const right of [false, true]) {
  test(`drag clamps both limits and commits once (${right ? 'right' : 'left'})`, t => {
    const e = fixture(t, right);
    e.handle.emit('pointerdown');
    e.handle.emit('pointermove', { pointerId: 2, clientX: 900 });
    assert.equal(e.root.style.getPropertyValue('--sidebar-width'), '');
    e.handle.emit('pointermove', { clientX: right ? -900 : 900 });
    assert.equal(e.root.style.getPropertyValue('--sidebar-width'), '480px');
    assert.equal(e.calls.length, 0);
    e.handle.emit('pointerup', { clientX: right ? 900 : -900 });
    assert.equal(e.root.style.getPropertyValue('--sidebar-width'), '192px');
    assert.deepEqual(e.calls, [['OnWidthChanged', 192]]);
    assert.equal(e.handle.capture, null);
    assert.equal(e.container.style.transition, 'width 200ms');
    assert.equal(document.listeners.get('selectstart').size, 0);
  });

  test(`keyboard supports physical edge direction, shift, and endpoints (${right})`, t => {
    const e = fixture(t, right, true);
    e.handle.emit('keydown', { key: right ? 'ArrowLeft' : 'ArrowRight' });
    e.handle.emit('keydown', { key: right ? 'ArrowLeft' : 'ArrowRight', shiftKey: true });
    e.handle.emit('keydown', { key: 'End' });
    e.handle.emit('keydown', { key: 'Home' });
    assert.deepEqual(e.calls.map(c => c[1]), [264, 296, 480, 192]);
    assert.equal(e.handle.attributes.get('aria-valuenow'), '192');
  });
}

for (const operation of ['pointercancel', 'lostpointercapture', 'Escape', 'unregister', 'rebind']) {
  test(`${operation} restores the starting width and releases drag resources`, t => {
    const e = fixture(t);
    e.root.style.setProperty('--sidebar-width', '280px');
    e.handle.emit('pointerdown');
    e.handle.emit('pointermove', { clientX: 200 });
    if (operation === 'unregister') unregisterResizeHandle(e.handle);
    else if (operation === 'rebind') registerResizeHandle(e.handle, e.receiver, 192, 480, false);
    else if (operation === 'Escape') e.handle.emit('keydown', { key: 'Escape' });
    else e.handle.emit(operation);
    assert.equal(e.root.style.getPropertyValue('--sidebar-width'), '280px');
    assert.equal(e.handle.capture, null);
    assert.equal(e.calls.length, 0);
    assert.equal(e.gap.style.transition, 'width 200ms');
    assert.equal(document.listeners.get('selectstart').size, 0);
    if (operation === 'unregister') {
      assert.equal(e.disconnected(), true);
      assert.equal([...e.handle.listeners.values()].reduce((n, set) => n + set.size, 0), 0);
    }
  });
}

test('multiple handles keep independent widths and registrations', t => {
  const a = fixture(t), b = fixture(t, true);
  unregisterResizeHandle(a.handle);
  b.handle.emit('keydown', { key: 'ArrowLeft' });
  assert.deepEqual(b.calls, [['OnWidthChanged', 264]]);
  assert.equal(a.root.style.getPropertyValue('--sidebar-width'), '');
});

function storage(t, entries = []) {
  const values = new Map(entries), writes = [];
  globalThis.localStorage = {
    getItem(key) { return values.get(key) ?? null; },
    setItem(key, value) { writes.push([key, value]); values.set(key, value); }
  };
  t.after(() => { delete globalThis.localStorage; });
  return { values, writes };
}

test('committed width survives remount and updates the bound width on restoration', t => {
  const saved = storage(t);
  const first = fixture(t, false, false, 'sidebar-a');
  first.handle.emit('pointerdown');
  first.handle.emit('pointermove', { clientX: 200 });
  assert.equal(saved.writes.length, 0);
  first.handle.emit('pointerup', { clientX: 200 });
  assert.deepEqual(saved.writes, [['sidebar-a', '356']]);
  unregisterResizeHandle(first.handle);
  const next = fixture(t, false, false, 'sidebar-a');
  assert.equal(next.root.style.getPropertyValue('--sidebar-width'), '356px');
  assert.deepEqual(next.calls, [['OnWidthChanged', 356]]);
  next.handle.emit('keydown', { key: 'ArrowRight' });
  assert.equal(saved.values.get('sidebar-a'), '364');
});

test('restoration clamps to current limits and rebind does not overwrite a newer width', t => {
  storage(t, [['sidebar-a', '900']]);
  const e = fixture(t, false, false, 'sidebar-a');
  assert.deepEqual(e.calls, [['OnWidthChanged', 480]]);
  e.root.style.setProperty('--sidebar-width', '320px');
  registerResizeHandle(e.handle, e.receiver, 192, 480, false, 'sidebar-a');
  assert.equal(e.root.style.getPropertyValue('--sidebar-width'), '320px');
  assert.equal(e.calls.length, 1);
});

for (const value of ['', ' ', 'broken', 'NaN', 'Infinity', '-1', '0']) {
  test(`invalid saved width ${JSON.stringify(value)} is ignored`, t => {
    storage(t, [['sidebar-a', value]]);
    const e = fixture(t, false, false, 'sidebar-a');
    assert.equal(e.root.style.getPropertyValue('--sidebar-width'), '');
    assert.equal(e.calls.length, 0);
  });
}

test('storage keys isolate sidebars and changing keys restores the new preference', t => {
  const saved = storage(t, [['sidebar-a', '300'], ['sidebar-b', '400']]);
  const a = fixture(t, false, false, 'sidebar-a');
  const b = fixture(t, true, false, 'sidebar-b');
  a.handle.emit('keydown', { key: 'Home' });
  assert.equal(saved.values.get('sidebar-b'), '400');
  registerResizeHandle(a.handle, a.receiver, 192, 480, false, 'sidebar-b');
  assert.equal(a.root.style.getPropertyValue('--sidebar-width'), '400px');
  assert.equal(b.root.style.getPropertyValue('--sidebar-width'), '400px');
});

test('canceled drags and disabled persistence never write storage', t => {
  const saved = storage(t);
  const a = fixture(t, false, false, 'sidebar-a');
  a.handle.emit('pointerdown');
  a.handle.emit('pointermove', { clientX: 200 });
  a.handle.emit('pointercancel');
  const b = fixture(t);
  b.handle.emit('keydown', { key: 'End' });
  assert.equal(saved.writes.length, 0);
});

test('blocked reads and failed writes do not break resizing or callbacks', t => {
  globalThis.localStorage = {
    getItem() { throw new Error('blocked'); },
    setItem() { throw new Error('quota'); }
  };
  t.after(() => { delete globalThis.localStorage; });
  const e = fixture(t, false, false, 'sidebar-a');
  e.handle.emit('keydown', { key: 'End' });
  assert.equal(e.root.style.getPropertyValue('--sidebar-width'), '480px');
  assert.deepEqual(e.calls, [['OnWidthChanged', 480]]);
});
