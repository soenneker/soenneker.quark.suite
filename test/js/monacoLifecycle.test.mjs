import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';

const source = (await readFile(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/monacointerop.js', import.meta.url), 'utf8'))
  .replace(/(['"])\.\/filedropinterop(?:\.min)?\.js\1/g,
    JSON.stringify(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/filedropinterop.js', import.meta.url).href));
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
    updateOptions(options) { this.options = options; },
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

test('runtime options update without recreating Monaco and disabling auto-height removes its listener', async t => {
  const env = await fixture(t);
  env.ready();
  env.container.style.height = '300px';
  await env.module.createEditor(env.container, '{}');
  await env.module.updateEditorOptions(env.container, true, true, false, true, 2, 5, 'Source');
  assert.deepEqual(env.editor.options, { readOnly: true, wordWrap: 'on', minimap: { enabled: false }, ariaLabel: 'Source' });
  assert.equal(env.listeners.size, 1);
  assert.equal(env.container.style.height, '60px');
  await env.module.updateEditorOptions(env.container, false, false, true, false, 2, 5, 'Source');
  assert.equal(env.editor.options.readOnly, false);
  assert.equal(env.listeners.size, 0);
  assert.equal(env.container.style.height, '300px');
  assert.equal(env.creations.length, 1);
});

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


test('offscreen editors notify once on intersection without loading Monaco', async t => {
  const env = await fixture(t);
  const original = globalThis.IntersectionObserver;
  let observer, notifications = 0;
  globalThis.IntersectionObserver = class {
    constructor(callback) { this.callback = callback; observer = this; }
    observe(element) { this.element = element; }
    disconnect() { this.disconnected = true; }
  };
  t.after(() => { globalThis.IntersectionObserver = original; });
  env.module.observeVisibility(env.container, { invokeMethodAsync(name) {
    assert.equal(name, 'OnCodeEditorVisible'); notifications++; return Promise.resolve();
  } });
  observer.callback([{ isIntersecting: false }]);
  assert.equal(notifications, 0);
  assert.equal(env.creations.length, 0);
  observer.callback([{ isIntersecting: true }]);
  observer.callback([{ isIntersecting: true }]);
  assert.equal(notifications, 1);
  assert.equal(observer.disconnected, true);
  assert.equal(env.creations.length, 0);
});

for (const reason of ['dispose', 'detach']) {
  test(`visibility notification is suppressed after ${reason}`, async t => {
    const env = await fixture(t);
    const original = globalThis.IntersectionObserver;
    let observer, notifications = 0;
    globalThis.IntersectionObserver = class {
      constructor(callback) { this.callback = callback; observer = this; }
      observe() {}
      disconnect() {}
    };
    t.after(() => { globalThis.IntersectionObserver = original; });
    env.module.observeVisibility(env.container, { invokeMethodAsync() { notifications++; return Promise.resolve(); } });
    if (reason === 'dispose') env.module.unobserveVisibility(env.container);
    else env.container.isConnected = false;
    observer.callback([{ isIntersecting: true }]);
    assert.equal(notifications, 0);
  });
}

test('visibility observation falls back to eager loading when unsupported', async t => {
  const env = await fixture(t);
  const original = globalThis.IntersectionObserver;
  globalThis.IntersectionObserver = undefined;
  t.after(() => { globalThis.IntersectionObserver = original; });
  let notifications = 0;
  env.module.observeVisibility(env.container, { invokeMethodAsync() { notifications++; return Promise.resolve(); } });
  assert.equal(notifications, 1);
});
