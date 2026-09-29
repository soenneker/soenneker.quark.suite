import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as carousel from '../../src/Soenneker.Quark.Suite/wwwroot/js/carouselinterop.js';

test('carousel resize reports only changed measurements and ignores disposed registrations', () => {
  const observers = [], calls = [];
  globalThis.ResizeObserver = class {
    constructor(callback) { this.callback = callback; observers.push(this); }
    observe() {}
    disconnect() { this.disconnected = true; }
  };
  const target = { offsetLeft: 100, offsetTop: 80, offsetWidth: 100, offsetHeight: 80 };
  const root = {
    querySelector: () => ({ clientWidth: 200, clientHeight: 160 }),
    querySelectorAll: () => [{ offsetLeft: 0, offsetTop: 0 }, target]
  };
  const receiver = { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } };
  carousel.initialize(root, receiver, 1);
  for (let i = 0; i < 100; i++) observers[0].callback();
  assert.deepEqual(calls, [['OnCarouselMeasured', 100]]);
  target.offsetLeft = 120;
  observers[0].callback();
  assert.equal(calls.at(-1)[1], 120);
  // Explicit measurements are already returned to .NET; do not echo them on resize.
  assert.equal(carousel.measureOffset(root, 1, true), 80);
  observers[0].callback();
  assert.equal(calls.length, 2);
  carousel.dispose(root);
  observers[0].callback();
  assert.equal(calls.length, 2);
  assert.ok(observers[0].disconnected);
  carousel.initialize(root, receiver, 0);
  observers[0].callback();
  observers[1].callback();
  assert.deepEqual(calls.at(-1), ['OnCarouselMeasured', 0]);
  assert.equal(calls.length, 3);
  carousel.dispose(root);
});
