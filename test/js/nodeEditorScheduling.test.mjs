import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';

const source = await readFile(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/nodeeditorinterop.js', import.meta.url), 'utf8');
const editor = await import('data:text/javascript;base64,' + Buffer.from(source +
  '\nexport { schedulePointerMove, handlePointerUp, cancelActiveInteraction, renderEditorFrame, scheduleEdgeUpdate, editors };').toString('base64'));

function fixture(t, kind = 'node') {
  const frames = new Map(), timers = new Map(), calls = [], operations = [];
  let next = 1;
  globalThis.requestAnimationFrame = callback => { const id = next++; frames.set(id, callback); return id; };
  globalThis.cancelAnimationFrame = id => frames.delete(id);
  globalThis.setTimeout = callback => { const id = next++; timers.set(id, callback); return id; };
  globalThis.clearTimeout = id => timers.delete(id);
  const classList = { add() {}, remove() {}, toggle() {} };
  const node = { dataset: { nodeId: 'a', nodeX: '20', nodeY: '30', selectable: 'true', disabled: 'false' },
    style: new Proxy({}, { set(target, key, value) { operations.push(`node:${key}`); target[key] = value; return true; } }),
    setAttribute() {}, getBoundingClientRect() { operations.push('node:read'); return { left: 20, top: 30, right: 40, bottom: 50 }; }
  };
  const state = { root: { id: 'test-editor', dataset: {}, classList,
      style: { setProperty() {} }, getBoundingClientRect() { operations.push('root:read'); return { left: 0, top: 0 }; },
      querySelectorAll() { return []; } },
    viewport: { style: {} }, background: null, edgeLayer: {}, edgeElements: [], addHandleElements: [],
    selectionRectangle: { classList, style: {} }, nodeElements: [node], selectedNodeIds: new Set(['a']), selectedNodeId: 'a', selectedEdgeId: null,
    panX: 0, panY: 0, zoom: 1, options: { snapToGrid: false }, edgeFrame: 0, edgesDirty: false, renderingFrame: false,
    hasPendingPointerMove: false, pendingPointerMove: { type: 'pointermove', clientX: 0, clientY: 0, pointerId: 0 },
    viewportTimer: 0, destroyed: false, cleanup: [], validationSequence: 0,
    dotNetRef: { invokeMethodAsync(...args) { calls.push(args); return Promise.resolve(); } },
    interaction: { kind, pointerId: 1, startClientX: 0, startClientY: 0, startX: 20, startY: 30,
      startPanX: 0, startPanY: 0, moved: false, nodes: [{ node, startX: 20, startY: 30 }], node,
      rootRect: { left: 0, top: 0, width: 1000, height: 1000 }, baseSelection: new Set(['a']), mode: 'replace' }
  };
  state.renderFrame = () => editor.renderEditorFrame(state);
  editor.editors.set(state.root.id, state);
  t.after(() => editor.destroy(state.root.id));
  return { state, node, operations, calls, frames, timers,
    move(x, y = x, pointerId = 1) { editor.schedulePointerMove(state, { clientX: x, clientY: y, pointerId }); },
    end(type = 'pointerup', pointerId = 1) { editor.handlePointerUp(state, { type, pointerId, clientX: 999, clientY: 999 }); },
    flush() { const callbacks = [...frames.values()]; frames.clear(); callbacks.forEach(callback => callback()); }
  };
}

test('100 node moves share a frame with edge geometry and preserve the owning pointer', t => {
  const env = fixture(t);
  for (let i = 1; i <= 100; i++) env.move(i);
  env.move(900, 900, 2);
  assert.equal(env.frames.size, 1);
  assert.deepEqual(env.operations, []);
  env.flush();
  assert.equal(env.node.dataset.nodeX, '120');
  assert.equal(env.node.dataset.nodeY, '130');
  assert.deepEqual(env.operations, ['node:transform', 'root:read']);
  assert.equal(env.frames.size, 0);
});

test('release flushes the last movement before notifying managed code', t => {
  const env = fixture(t);
  env.move(50, 60);
  env.end();
  assert.deepEqual(env.calls, [['InvokeNodesMoved', [{ nodeId: 'a', x: 70, y: 90, previousX: 20, previousY: 30 }]]]);
  assert.equal(env.state.interaction, null);
  env.flush();
  assert.equal(env.operations.filter(op => op === 'node:transform').length, 1);
});

test('a gesture that crosses the threshold then returns is not mistaken for a click', t => {
  const env = fixture(t);
  env.move(30);
  env.move(0);
  env.end();
  assert.equal(env.calls[0][0], 'InvokeNodesMoved');
  assert.equal(env.calls[0][1][0].x, 20);
});

test('sub-threshold movements do not schedule work or commit a drag', t => {
  const env = fixture(t);
  env.move(1, 1);
  env.end();
  assert.equal(env.frames.size, 0);
  assert.equal(env.calls.length, 0);
});

for (const cancel of ['pointercancel', 'blur', 'destroy']) {
  test(`${cancel} discards pending movement`, t => {
    const env = fixture(t);
    env.move(40);
    if (cancel === 'pointercancel') env.end(cancel);
    if (cancel === 'blur') editor.cancelActiveInteraction(env.state);
    if (cancel === 'destroy') editor.destroy(env.state.root.id);
    env.flush();
    assert.equal(env.node.dataset.nodeX, '20');
    assert.equal(env.calls.length, 0);
  });
}

test('foreign pointer release cannot consume the pending owner movement', t => {
  const env = fixture(t);
  env.move(40);
  env.end('pointerup', 2);
  assert.equal(env.calls.length, 0);
  assert.equal(env.state.hasPendingPointerMove, true);
  env.end();
  assert.equal(env.calls[0][1][0].x, 60);
});

test('pan and already queued geometry share a frame', t => {
  const env = fixture(t, 'pan');
  editor.scheduleEdgeUpdate(env.state);
  for (let i = 0; i <= 100; i++) env.move(i);
  env.flush();
  assert.equal(env.state.panX, 100);
  assert.equal(env.state.viewport.style.transform, 'translate3d(100px, 100px, 0) scale(1)');
  assert.equal(env.timers.size, 1);
  assert.deepEqual(env.operations, ['root:read']);
  assert.equal(env.frames.size, 0);
});

test('marquee scans nodes once per frame and commits final selection', t => {
  const env = fixture(t, 'marquee');
  for (let i = 1; i <= 100; i++) env.move(i);
  env.flush();
  assert.equal(env.operations.filter(op => op === 'node:read').length, 1);
  env.end();
  assert.deepEqual(env.calls, [['InvokeNodeSelectionChanged', ['a']]]);
});

test('connection preview batches without rebuilding unrelated edge geometry', t => {
  const env = fixture(t);
  env.state.interaction = null;
  env.state.connection = { pointerId: 1, startX: 0, startY: 0, sourcePlacement: 'bottom' };
  env.state.preview = { setAttribute(name, value) { env.operations.push(`preview:${name}`); this[name] = value; }, classList: { add() {} } };
  for (let i = 1; i <= 100; i++) env.move(i);
  env.flush();
  assert.deepEqual(env.operations, ['root:read', 'preview:d']);
  assert.equal(env.state.connection.currentX, 100);
});

test('zoom applies pending node movement at the preceding scale', t => {
  const env = fixture(t);
  Object.assign(env.state.options, { minZoom: 0.1, maxZoom: 4 });
  Object.assign(env.state.root, { clientWidth: 1000, clientHeight: 1000 });
  env.move(100);
  editor.zoomBy(env.state.root.id, 1);
  assert.equal(env.node.dataset.nodeX, '120');
  assert.equal(env.state.zoom, 2);
  env.flush();
  assert.equal(env.node.dataset.nodeX, '120');
});

test('reset view does not get overwritten by an earlier queued pan', t => {
  const env = fixture(t, 'pan');
  Object.assign(env.state.options, { minZoom: 0.1, maxZoom: 4, initialZoom: 1, initialX: 5, initialY: 10 });
  env.move(100);
  editor.resetView(env.state.root.id);
  env.flush();
  assert.equal(env.state.panX, 5);
  assert.equal(env.state.panY, 10);
});
