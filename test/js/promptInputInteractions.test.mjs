import { test } from 'node:test';
import assert from 'node:assert/strict';
import { registerTextarea, unregisterTextarea, registerAttachments, unregisterAttachments } from '../../src/Soenneker.Quark.Suite/wwwroot/js/promptinputinterop.js';

function element() {
  return { listeners: new Map(), dataset: {},
    addEventListener(name, callback) { this.listeners.set(name, callback); },
    removeEventListener(name) { this.listeners.delete(name); } };
}

test('IME composition does not submit or cancel confirmation Enter', () => {
  const textarea = element();
  registerTextarea(textarea);
  let stopped = false, prevented = false;
  textarea.listeners.get('keydown')({ key: 'Enter', isComposing: true,
    stopPropagation() { stopped = true; }, preventDefault() { prevented = true; } });
  assert.equal(stopped, true);
  assert.equal(prevented, false);
  unregisterTextarea(textarea);
  assert.equal(textarea.listeners.size, 0);
});

test('Blazor file inputs have one change owner and drop respects accept, multiple and disabled', async () => {
  const input = element(), form = element(), calls = [];
  input.dataset.quarkBlazorInputFile = 'true';
  input.closest = () => form;
  input.accept = '.png,image/jpeg';
  input.multiple = false;
  const receiver = { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } };
  registerAttachments(input, receiver, false);
  assert.equal(input.listeners.has('change'), false);
  const files = [
    { name: 'readme.txt', type: 'text/plain', size: 1 },
    { name: 'first.PNG', type: 'image/png', size: 2 },
    { name: 'second.jpg', type: 'image/jpeg', size: 3 }
  ];
  const event = { dataTransfer: { types: ['Files'], files }, preventDefault() {} };
  form.listeners.get('drop')(event);
  await Promise.resolve();
  assert.equal(calls.length, 1);
  assert.deepEqual(calls[0][1].map(file => file.name), ['first.PNG']);
  input.disabled = true;
  form.listeners.get('drop')(event);
  assert.equal(calls.length, 1);
  unregisterAttachments(input);
  assert.equal(form.listeners.size, 0);
});
