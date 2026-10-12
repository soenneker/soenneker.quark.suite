import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as calendar from '../../src/Soenneker.Quark.Suite/wwwroot/js/calendarinterop.js';

function setup() {
  const listeners = new Map(), calls = [];
  const button = { dataset: { calendarDate: '2026-01-31' }, disabled: false, closest() { return this; } };
  const element = { addEventListener(name, callback) { listeners.set(name, callback); },
    removeEventListener(name) { listeners.delete(name); }, contains(node) { return node === button; } };
  globalThis.document = { activeElement: button };
  calendar.initialize(element, { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } });
  return { element, button, listeners, calls };
}

test('calendar prevents navigation scrolling and forwards date navigation keys', async () => {
  const env = setup();
  let prevented = false;
  env.listeners.get('keydown')({ target: env.button, key: 'PageDown', shiftKey: true, preventDefault() { prevented = true; } });
  await new Promise(resolve => setImmediate(resolve));
  assert.equal(prevented, true);
  assert.deepEqual(env.calls, [['NavigateCalendar', '2026-01-31', 'PageDown', true]]);
  calendar.destroy(env.element);
});

test('calendar leaves activation, editing composition, and modifier shortcuts native', async () => {
  const env = setup();
  let prevented = 0;
  for (const extra of [{ key: 'Enter' }, { key: 'ArrowRight', isComposing: true }, { key: 'ArrowLeft', ctrlKey: true }])
    env.listeners.get('keydown')({ target: env.button, preventDefault() { prevented++; }, ...extra });
  env.button.disabled = true;
  env.listeners.get('keydown')({ target: env.button, key: 'ArrowRight', preventDefault() { prevented++; } });
  await new Promise(resolve => setImmediate(resolve));
  assert.equal(prevented, 0);
  assert.equal(env.calls.length, 0);
  calendar.destroy(env.element);
});

test('calendar disposal cancels queued keyboard interop and removes listener', async () => {
  const env = setup();
  env.listeners.get('keydown')({ target: env.button, key: 'ArrowRight', preventDefault() {} });
  calendar.destroy(env.element);
  await new Promise(resolve => setImmediate(resolve));
  assert.equal(env.calls.length, 0);
  assert.equal(env.listeners.size, 0);
});

test('calendar focuses the primary month occurrence of a duplicated outside date', () => {
  const focused = [];
  const outside = { dataset: { calendarOutside: 'true' }, focus() { focused.push('outside'); } };
  const primary = { dataset: {}, focus() { focused.push('primary'); } };
  calendar.focus({ querySelectorAll() { return [outside, primary]; } }, '2026-02-01');
  assert.deepEqual(focused, ['primary']);
});
