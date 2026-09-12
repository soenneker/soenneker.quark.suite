import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import * as thread from '../../src/Soenneker.Quark.Suite/wwwroot/js/threadinterop.js';

function environment() {
  const frames = new Map(), observers = [], calls = [], scrolls = [];
  let next = 1;
  globalThis.requestAnimationFrame = callback => { const id = next++; frames.set(id, callback); return id; };
  globalThis.cancelAnimationFrame = id => frames.delete(id);
  globalThis.MutationObserver = globalThis.ResizeObserver = class {
    constructor(callback) { this.callback = callback; observers.push(this); }
    observe() {}
    disconnect() { this.disconnected = true; }
  };
  globalThis.window = { ResizeObserver };
  const element = {
    scrollHeight: 200, scrollTop: 100, clientHeight: 100,
    listeners: new Map(),
    addEventListener(name, callback) { this.listeners.set(name, callback); },
    removeEventListener(name) { this.listeners.delete(name); },
    scrollTo(options) { scrolls.push(options); this.scrollTop = this.scrollHeight - this.clientHeight; }
  };
  const receiver = { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } };
  return { frames, observers, calls, scrolls, element, receiver,
    flush() { const pending = [...frames.values()]; frames.clear(); pending.forEach(callback => callback()); }
  };
}

test('streaming mutations and resize callbacks share one frame', () => {
  const env = environment();
  thread.initialize(env.element, env.receiver, 'instant', 'auto');
  env.flush();
  for (let i = 0; i < 100; i++) {
    env.element.scrollHeight++;
    env.observers.forEach(observer => observer.callback());
  }
  assert.equal(env.frames.size, 1);
  env.flush();
  assert.equal(env.scrolls.length, 2);
  assert.equal(env.scrolls[1].top, 300);
  thread.dispose(env.element);
});

test('a user who scrolls away before the pending frame is not pulled back', () => {
  const env = environment();
  thread.initialize(env.element, env.receiver, 'instant');
  env.flush();
  env.observers[0].callback();
  env.element.scrollTop = 0;
  env.element.listeners.get('scroll')();
  env.flush();
  assert.equal(env.scrolls.length, 1);
  assert.deepEqual(env.calls.at(-1), ['SetIsAtBottom', false]);
  thread.dispose(env.element);
});

test('disposal cancels initial and streaming frames without stale interop', () => {
  const env = environment();
  thread.initialize(env.element, env.receiver);
  const stale = [...env.frames.values()][0];
  thread.dispose(env.element);
  assert.equal(env.frames.size, 0);
  stale();
  assert.equal(env.scrolls.length, 0);
  assert.equal(env.calls.length, 0);
  assert.ok(env.observers.every(observer => observer.disconnected));
});

test('node edges measure every unique endpoint before writing paths', async () => {
  const source = await readFile(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/nodeeditorinterop.js', import.meta.url), 'utf8');
  // Expose the pure scheduling boundary in the test module without changing its public API.
  const { updateEdges } = await import('data:text/javascript;base64,' + Buffer.from(source + '\nexport { updateEdges };').toString('base64'));
  const operations = [];
  const ports = new Map();
  for (let i = 0; i < 3; i++) ports.set(`${i}\0p`, {
    dataset: { placement: 'right' },
    getBoundingClientRect() { operations.push(`read${i}`); return { left: i * 100, top: 0, width: 10, height: 10 }; }
  });
  const edge = (from, to) => ({
    dataset: { sourceNode: String(from), sourcePort: 'p', targetNode: String(to), targetPort: 'p' },
    style: {},
    querySelector(selector) {
      if (selector === '[data-edge-path]' || selector === '[data-edge-hit]')
        return { setAttribute() { operations.push('write'); } };
      return null;
    }
  });
  updateEdges({ edgeLayer: {}, root: { getBoundingClientRect: () => ({ left: 0, top: 0 }) },
    ports, panX: 0, panY: 0, zoom: 1, edgeElements: [edge(0, 1), edge(1, 2)], addHandleElements: [] });
  assert.deepEqual(operations.slice(0, 3), ['read0', 'read1', 'read2']);
  assert.equal(operations.filter(op => op.startsWith('read')).length, 3);
  assert.ok(operations.slice(3).every(op => op === 'write'));
});
