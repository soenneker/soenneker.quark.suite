import { test } from 'node:test';
import assert from 'node:assert/strict';
import { initialize, destroy } from '../../src/Soenneker.Quark.Suite/wwwroot/js/scrollspyinterop.js';

class ElementStub {
  attributes = new Map();
  listeners = new Map();
  setAttribute(key, value) { this.attributes.set(key, value); }
  getAttribute(key) { return this.attributes.get(key) ?? null; }
  removeAttribute(key) { this.attributes.delete(key); }
  addEventListener(name, callback) { this.listeners.set(name, callback); }
  removeEventListener(name) { this.listeners.delete(name); }
  querySelector() { return null; }
}

function fixture(t, hash = '') {
  const frames = new Map(), timers = new Map(), scrolls = [], updates = [];
  let next = 1;
  const anchor = new ElementStub(), element = new ElementStub(), section = { offsetTop: 500 };
  anchor.setAttribute('data-scrollspy-anchor', 'section');
  element.querySelectorAll = () => [anchor];
  element.querySelector = () => anchor;
  element.contains = () => true;
  globalThis.HTMLElement = ElementStub;
  globalThis.document = { documentElement: Object.assign(new ElementStub(), { scrollHeight: 2000, clientHeight: 600 }),
    body: {}, getElementById: () => section };
  globalThis.window = Object.assign(new ElementStub(), {
    location: { hash, href: `https://example.test/demo${hash}` }, scrollY: 550, innerHeight: 600,
    requestAnimationFrame(callback) { const id = next++; frames.set(id, callback); return id; },
    cancelAnimationFrame(id) { frames.delete(id); },
    setTimeout(callback) { const id = next++; timers.set(id, callback); return id; },
    clearTimeout(id) { timers.delete(id); },
    scrollTo(options) { scrolls.push(options); },
    history: { replaceState(_, __, path) { window.location.hash = new URL(path, window.location.href).hash; } }
  });
  initialize(element, {}, { invokeMethodAsync(...args) { updates.push(args); return Promise.resolve(); } });
  t.after(() => destroy(element));
  return { element, anchor, frames, timers, scrolls, updates,
    scroll() { window.listeners.get('scroll')(); const callbacks = [...frames.values()]; frames.clear(); callbacks.forEach(callback => callback()); },
    initialize() { const callbacks = [...timers.values()]; timers.clear(); callbacks.forEach(callback => callback()); }
  };
}

test('scroll-generated hashes cannot trigger a startup scroll during an interaction', t => {
  const env = fixture(t);
  env.scroll();
  assert.equal(window.location.hash, '#section');
  env.initialize();
  assert.deepEqual(env.scrolls, []);
  assert.deepEqual(env.updates, [['HandleUpdate', 'section']]);
  assert.equal(env.anchor.getAttribute('aria-current'), 'location');
});

test('an incoming deep link still scrolls to its requested section', t => {
  const env = fixture(t, '#section');
  env.initialize();
  assert.deepEqual(env.scrolls, [{ top: 500, left: 0, behavior: 'smooth' }]);
});

test('explicit anchor navigation cancels the startup scroll', t => {
  const env = fixture(t, '#section');
  env.element.listeners.get('click')({ target: { closest: () => env.anchor }, preventDefault() {} });
  env.initialize();
  assert.equal(env.scrolls.length, 1);
  assert.equal(env.timers.size, 0);
});

test('destroy removes listeners and cancels both pending scroll and initialization', t => {
  const env = fixture(t, '#section');
  window.listeners.get('scroll')();
  destroy(env.element);
  assert.equal(env.frames.size, 0);
  assert.equal(env.timers.size, 0);
  assert.equal(window.listeners.size, 0);
  assert.equal(env.element.listeners.size, 0);
});
