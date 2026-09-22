import { test } from 'node:test';
import assert from 'node:assert/strict';
import { initialize, destroy } from '../../src/Soenneker.Quark.Suite/wwwroot/js/chartscrollinterop.js';

function fixture(t) {
    let update, frame, now = 0;
    const motion = { matches: true, addEventListener() {}, removeEventListener() {} };
    globalThis.matchMedia = () => motion;
    t.mock.method(performance, 'now', () => now);
    globalThis.requestAnimationFrame = callback => { frame = callback; return 1; };
    globalThis.cancelAnimationFrame = () => { frame = null; };
    globalThis.MutationObserver = class {
        constructor(callback) { update = callback; }
        observe() {}
        disconnect() {}
    };
    const styles = new Map();
    const slice = {
        dataset: { radialGeometry: '100 100 80 -90 90 50' },
        style: { setProperty: (key, value) => styles.set(key, value), removeProperty: key => styles.delete(key) }
    };
    const root = {
        dataset: { radialAnimated: 'true' },
        querySelectorAll: () => [slice],
        addEventListener() {}, removeEventListener() {}
    };
    initialize(root);
    t.after(() => destroy(root));
    return {
        root, motion, styles,
        sample(sweep) { slice.dataset.radialGeometry = `100 100 80 -90 ${sweep} 50`; update(); },
        tick(time) { now = time; const callback = frame; frame = null; callback?.(now); },
        pending: () => frame !== null
    };
}

test('donut updates interpolate arcs across 180 degrees and settle on the rendered target', t => {
    const f = fixture(t);
    f.sample(270);
    const start = f.styles.get('d');
    f.tick(250);
    const middle = f.styles.get('d');
    assert.notEqual(middle, start);
    assert.match(middle, /A80,80 0 0 1/);
    f.tick(400);
    assert.match(f.styles.get('d'), /A80,80 0 1 1/);
    f.tick(500);
    assert.equal(f.styles.has('d'), false);
    assert.equal(f.pending(), false);
});

test('a new sample continues from the currently displayed arc', t => {
    const f = fixture(t);
    f.sample(270);
    f.tick(200);
    const displayed = f.styles.get('d');
    f.sample(45);
    assert.equal(f.styles.get('d'), displayed);
    f.tick(450);
    assert.notEqual(f.styles.get('d'), displayed);
    f.tick(700);
    assert.equal(f.styles.has('d'), false);
});

test('system reduced motion does not prevent animation; explicit disable and disposal clean up', t => {
    const f = fixture(t);
    f.sample(270);
    assert.equal(f.styles.has('d'), true);
    assert.equal(f.pending(), true);
    f.sample(90);
    assert.equal(f.pending(), true);
    f.root.dataset.radialAnimated = 'false';
    f.sample(180);
    assert.equal(f.styles.has('d'), false);
    assert.equal(f.pending(), false);
    f.root.dataset.radialAnimated = 'true';
    f.sample(90);
    f.sample(270);
    destroy(f.root);
    assert.equal(f.styles.has('d'), false);
    assert.equal(f.pending(), false);
});
