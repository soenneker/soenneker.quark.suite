import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as floating from '../../src/Soenneker.Quark.Suite/wwwroot/js/floatingwindowinterop.js';
import * as color from '../../src/Soenneker.Quark.Suite/wwwroot/js/colorpickerinterop.js';

class ElementStub {
  listeners = new Map();
  dataset = {};
  writes = 0;
  reads = 0;
  queries = 0;
  capture = null;
  style = new Proxy({}, { set: (target, key, value) => { this.writes++; target[key] = value; return true; } });
  addEventListener(name, callback) {
    if (!this.listeners.has(name)) this.listeners.set(name, new Set());
    this.listeners.get(name).add(callback);
  }
  removeEventListener(name, callback) { this.listeners.get(name)?.delete(callback); }
  emit(name, properties = {}) {
    const event = { pointerId: 1, clientX: 110, clientY: 110, button: 0, isPrimary: true,
      target: this, preventDefault() {}, stopPropagation() {}, ...properties };
    this.listeners.get(name)?.forEach(callback => callback(event));
  }
  get offsetLeft() { return parseFloat(this.style.left) || 0; }
  get offsetTop() { return parseFloat(this.style.top) || 0; }
  get offsetWidth() { return parseFloat(this.style.width) || 400; }
  get offsetHeight() { return parseFloat(this.style.height) || 300; }
  getBoundingClientRect() {
    this.reads++;
    return { left: this.offsetLeft, top: this.offsetTop, width: this.offsetWidth,
      height: this.offsetHeight, right: this.offsetLeft + this.offsetWidth, bottom: this.offsetTop + this.offsetHeight };
  }
  getClientRects() { return [this.getBoundingClientRect()]; }
  closest() { return null; }
  focus() {}
  setPointerCapture(id) { this.capture = id; }
  hasPointerCapture(id) { return this.capture !== null && this.capture === id; }
  releasePointerCapture(id) { assert.equal(this.capture, id); this.capture = null; }
}

function environment() {
  const frames = new Map(), observers = [], calls = [];
  let next = 1;
  globalThis.requestAnimationFrame = callback => { const id = next++; frames.set(id, callback); return id; };
  globalThis.cancelAnimationFrame = id => frames.delete(id);
  globalThis.Element = ElementStub;
  globalThis.document = new ElementStub();
  globalThis.window = Object.assign(new ElementStub(), { innerWidth: 1000, innerHeight: 800 });
  globalThis.MutationObserver = globalThis.ResizeObserver = class {
    constructor(callback) { this.callback = callback; observers.push(this); }
    observe() {}
    disconnect() { this.disconnected = true; }
  };
  return { frames, observers, calls,
    receiver: { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } },
    flush() { const pending = [...frames.values()]; frames.clear(); pending.forEach(callback => callback()); }
  };
}

function floatingFixture(t) {
  const env = environment();
  const element = new ElementStub(), title = new ElementStub(), handle = new ElementStub();
  handle.dataset.direction = 'se';
  element.querySelector = selector => selector.includes('titlebar') ? title : null;
  element.querySelectorAll = () => [handle];
  document.getElementById = () => element;
  floating.create('window', JSON.stringify({ autoSizeToContent: false, centerOnShow: false }));
  floating.setCallbacks('window', env.receiver);
  t.after(() => floating.destroy('window'));
  element.writes = element.reads = 0;
  return { ...env, element, title, handle };
}

test('floating drag batches 100 moves into one geometry read and final position', t => {
  const env = floatingFixture(t);
  env.title.emit('pointerdown');
  for (let i = 1; i <= 100; i++) document.emit('pointermove', { clientX: 110 + i, clientY: 110 + i });
  assert.equal(env.frames.size, 1);
  assert.equal(env.element.writes, 0);
  assert.equal(env.element.reads, 0);
  env.flush();
  assert.equal(env.element.reads, 1);
  assert.equal(env.element.writes, 2);
  assert.deepEqual(floating.getPosition('window'), { x: 200, y: 200 });
});

for (const end of ['pointerup', 'pointercancel']) {
  test(`floating drag flushes the pending final movement on ${end}`, t => {
    const env = floatingFixture(t);
    env.title.emit('pointerdown');
    document.emit('pointermove', { clientX: 250, clientY: 230 });
    document.emit(end);
    assert.equal(env.frames.size, 0);
    assert.deepEqual(floating.getPosition('window'), { x: 240, y: 220 });
    assert.deepEqual(env.calls, [['InvokeOnDragStart'], ['InvokeOnDragEnd']]);
    const writes = env.element.writes;
    document.emit('pointermove', { clientX: 500 });
    env.flush();
    assert.equal(env.element.writes, writes);
  });
}

test('floating resize batches moves and flushes on release', t => {
  const env = floatingFixture(t);
  env.handle.emit('pointerdown');
  for (let i = 1; i <= 100; i++) document.emit('pointermove', { clientX: 110 + i, clientY: 110 + i });
  assert.equal(env.frames.size, 1);
  assert.equal(env.element.writes, 0);
  document.emit('pointerup');
  assert.deepEqual(floating.getSize('window'), { width: 500, height: 400 });
  assert.equal(env.element.writes, 4);
  assert.equal(env.frames.size, 0);
});

for (const operation of ['destroy', 'hide', 'updateOptions']) {
  test(`floating ${operation} cancels queued interaction writes`, async t => {
    const env = floatingFixture(t);
    env.title.emit('pointerdown');
    document.emit('pointermove', { clientX: 250, clientY: 230 });
    await floating[operation]('window', JSON.stringify({ draggable: false }));
    assert.equal(env.frames.size, 0);
    const writes = env.element.writes;
    env.flush();
    document.emit('pointermove');
    assert.equal(env.element.writes, writes);
  });
}

function colorFixture(t) {
  const env = environment();
  const root = new ElementStub(), canvas = new ElementStub(), thumb = new ElementStub();
  canvas.style.width = canvas.style.height = '100px';
  root.querySelector = () => canvas;
  canvas.querySelector = () => { canvas.queries++; return thumb; };
  canvas.getAttribute = name => name === 'data-hue' ? '30' : '0.5';
  color.registerCanvas(root, env.receiver, false);
  t.after(() => color.unregisterCanvas(root));
  return { ...env, root, canvas, thumb };
}

test('color picker batches thumb writes and keeps the last in-bounds color', t => {
  const env = colorFixture(t);
  env.canvas.emit('pointerdown', { clientX: 0, clientY: 0 });
  for (let i = 1; i <= 100; i++) env.canvas.emit('pointermove', { clientX: i, clientY: i / 2 });
  env.canvas.emit('pointermove', { clientX: 150, clientY: 150 });
  assert.equal(env.frames.size, 1);
  assert.equal(env.thumb.writes, 0);
  env.flush();
  assert.equal(env.thumb.writes, 3);
  assert.equal(env.canvas.queries, 1);
  assert.equal(env.thumb.style.background, 'hsl(30 100% 50% / 0.5)');
  env.canvas.emit('pointerup');
  assert.deepEqual(env.calls, [['SetCanvasColor', 100, 50]]);
});

for (const end of ['pointerup', 'pointercancel', 'lostpointercapture']) {
  test(`color picker commits pending color once on ${end}`, t => {
    const env = colorFixture(t);
    env.canvas.emit('pointerdown', { clientX: 25, clientY: 75 });
    env.canvas.emit(end);
    env.canvas.emit('pointerup');
    assert.equal(env.frames.size, 0);
    assert.equal(env.canvas.capture, null);
    assert.equal(env.thumb.writes, 3);
    assert.deepEqual(env.calls, [['SetCanvasColor', 25, 25]]);
  });
}

test('color picker unregister cancels frames and releases capture without committing', t => {
  const env = colorFixture(t);
  env.canvas.emit('pointerdown', { clientX: 25, clientY: 75 });
  color.unregisterCanvas(env.root);
  assert.equal(env.frames.size, 0);
  assert.equal(env.canvas.capture, null);
  env.canvas.emit('pointerup');
  env.flush();
  assert.equal(env.thumb.writes, 0);
  assert.equal(env.calls.length, 0);
  assert.ok(env.observers.every(observer => observer.disconnected));
});

test('another pointer cannot steal an active color drag', t => {
  const env = colorFixture(t);
  env.canvas.emit('pointerdown', { clientX: 25, clientY: 75 });
  env.canvas.emit('pointerdown', { pointerId: 2, clientX: 75, clientY: 25 });
  env.canvas.emit('pointermove', { pointerId: 2, clientX: 75, clientY: 25 });
  env.canvas.emit('pointerup');
  assert.deepEqual(env.calls, [['SetCanvasColor', 25, 25]]);
});
