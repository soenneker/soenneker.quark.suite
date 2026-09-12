import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';

const source = await readFile(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/monacointerop.js', import.meta.url), 'utf8');
let moduleId = 0;

async function fixture(t) {
  const id = ++moduleId;
  const module = await import('data:text/javascript;base64,' + Buffer.from(source + `\n// fixture ${id}`).toString('base64'));
  const creations = [], observers = [], listeners = new Set();
  let ready;
  globalThis.monacoReady = new Promise(resolve => { ready = resolve; });
  globalThis.monaco = undefined;
  globalThis.document = { baseURI: 'https://example.test/', body: {} };
  globalThis.MutationObserver = class {
    constructor(callback) { this.callback = callback; observers.push(this); }
    observe() {}
    disconnect() { this.disconnected = true; }
  };
  const model = {
    lines: 1, disposed: false,
    getLineCount() { return this.lines; },
    onDidChangeContent(callback) {
      listeners.add(callback);
      return { dispose() { listeners.delete(callback); } };
    },
    dispose() { this.disposed = true; }
  };
  const editor = {
    layouts: 0, disposed: false,
    getModel: () => model,
    getOption: () => 20,
    layout() { this.layouts++; },
    dispose() { this.disposed = true; }
  };
  globalThis.monacoApi = {
    EditorOption: { lineHeight: 1 },
    defineTheme() {},
    create(container, options) { creations.push(options); return editor; }
  };
  module.ensureConfigured(`data:text/javascript,${encodeURIComponent('await globalThis.monacoReady; export const editor = globalThis.monacoApi;')}#${id}`,
    { editor: '/worker.js' });
  const container = { isConnected: true, style: {} };
  t.after(() => module.disposeEditor(container));
  return { module, container, creations, observers, listeners, model, editor, ready,
    change(lines) { model.lines = lines; listeners.forEach(callback => callback()); }
  };
}

test('typing at unchanged/clamped height does not repeatedly lay out Monaco', async t => {
  const env = await fixture(t);
  env.ready();
  await env.module.createEditor(env.container, '{}');
  await env.module.addContentChangeListener(env.container, 2, 10);
  assert.equal(env.editor.layouts, 1);
  assert.equal(env.container.style.height, '60px');
  for (let i = 0; i < 100; i++) env.change(1);
  assert.equal(env.editor.layouts, 1);
  env.change(5);
  assert.equal(env.container.style.height, '120px');
  env.change(20);
  assert.equal(env.container.style.height, '220px');
  env.change(21);
  assert.equal(env.editor.layouts, 3);
  env.module.layoutEditor(env.container);
  assert.equal(env.editor.layouts, 4);
});

for (const reason of ['dispose', 'detach']) {
  test(`Monaco loading cannot create an editor after ${reason}`, async t => {
    const env = await fixture(t);
    const creating = env.module.createEditor(env.container, '{}');
    if (reason === 'dispose') env.module.disposeEditor(env.container);
    else env.container.isConnected = false;
    env.ready();
    await creating;
    assert.equal(env.creations.length, 0);
    assert.equal(env.observers.length, 0);
  });
}

test('concurrent creates only construct the latest requested editor', async t => {
  const env = await fixture(t);
  const first = env.module.createEditor(env.container, '{"value":"old"}');
  const second = env.module.createEditor(env.container, '{"value":"new"}');
  env.ready();
  await Promise.all([first, second]);
  assert.deepEqual(env.creations, [{ value: 'new' }]);
});

test('disposal during listener registration does not attach to a disposed model', async t => {
  const env = await fixture(t);
  env.ready();
  await env.module.createEditor(env.container, '{}');
  const registering = env.module.addContentChangeListener(env.container, 2, 10);
  env.module.disposeEditor(env.container);
  await registering;
  assert.equal(env.listeners.size, 0);
  assert.ok(env.model.disposed);
  assert.ok(env.editor.disposed);
});

test('overlapping height-listener registration leaves one subscription', async t => {
  const env = await fixture(t);
  env.ready();
  await env.module.createEditor(env.container, '{}');
  await Promise.all([
    env.module.addContentChangeListener(env.container, 2, 10),
    env.module.addContentChangeListener(env.container, 4, 12)
  ]);
  assert.equal(env.listeners.size, 1);
  env.change(1);
  assert.equal(env.container.style.height, '100px');
  env.module.disposeEditor(env.container);
  assert.equal(env.listeners.size, 0);
  assert.ok(env.observers[0].disconnected);
});
