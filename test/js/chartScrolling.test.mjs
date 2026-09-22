import { test } from 'node:test';
import assert from 'node:assert/strict';
import { initialize, destroy } from '../../src/Soenneker.Quark.Suite/wwwroot/js/chartscrollinterop.js';

function fixture(t) {
    const timeline = { currentTime: 0 };
    const frames = new Map();
    let frameId = 0;
    globalThis.requestAnimationFrame = callback => { frames.set(++frameId, callback); return frameId; };
    globalThis.cancelAnimationFrame = id => frames.delete(id);
    let update;
    const motion = { matches: false, addEventListener() {}, removeEventListener() {} };
    globalThis.matchMedia = () => motion;
    globalThis.document = { timeline };
    globalThis.MutationObserver = class {
        constructor(callback) { update = callback; }
        observe() {}
        disconnect() {}
    };
    const position = element => Number(element.style.transform?.match(/translateX\((.*)px\)/)[1] ?? 0);
    globalThis.getComputedStyle = () => { throw new Error('Scrolling must not force style/layout during a data update'); };
    function element() {
        return { style: {
            setProperty(name, value) { this[name] = value; },
            removeProperty(name) { delete this[name]; }
        } };
    }
    const plot = element(), overlay = element();
    const root = {
        dataset: { scrollVersion: '0', scrollEnabled: 'true', scrollPaused: 'false', scrollDuration: '1000',
            scrollXMin: '0', scrollXMax: '60', scrollYMin: '0', scrollYMax: '100', scrollWidth: '600', scrollHeight: '200' },
        addEventListener() {}, removeEventListener() {},
        querySelector: selector => selector.includes('overlay') ? overlay : plot
    };
    initialize(root);
    t.after(() => destroy(root));
    return { root, plot, overlay, motion, update, frames, position: () => position(plot),
        time(value) {
            timeline.currentTime = value;
            const pending = [...frames.values()];
            frames.clear();
            for (const callback of pending) callback(value);
        },
        sample(min) { Object.assign(root.dataset, { scrollVersion: String(Number(root.dataset.scrollVersion) + 1),
            scrollXMin: String(min), scrollXMax: String(min + 60) }); update(); }
    };
}

test('late samples preserve position and velocity without a pause at the interval boundary', t => {
    const f = fixture(t);
    f.sample(1);
    f.time(1000);
    assert.equal(f.position(), 0);
    f.time(1250);
    assert.equal(f.position(), -2.5);
    const before = f.position();
    f.sample(2);
    // Geometry shifts left by ten units as the domain advances.
    assert.equal(f.position() - 10, before);
    f.time(1500);
    assert.equal(f.position(), 5);
    assert.equal(f.plot.style.transform, f.overlay.style.transform);
});

test('early samples preserve velocity and a stopped feed has bounded movement', t => {
    const f = fixture(t);
    f.sample(1);
    f.time(750);
    const before = f.position();
    f.sample(2);
    assert.equal(f.position() - 10, before);
    f.time(1000);
    assert.equal(f.position(), 10);
    f.time(10000);
    assert.equal(f.position(), -10);
});

test('sample updates compensate geometry immediately and keep only one frame scheduled', t => {
    const f = fixture(t);
    f.sample(1);
    for (let second = 1; second <= 60; second++) {
        f.time(second * 1000);
        const before = f.position();
        f.sample(second + 1);
        assert.equal(f.position() - 10, before);
        assert.equal(f.plot.style.transform, f.overlay.style.transform);
        assert.equal(f.frames.size, 1);
    }
});

test('pause freezes continuation and scale changes cancel it', t => {
    const f = fixture(t);
    f.sample(1);
    f.time(1250);
    f.root.dataset.scrollPaused = 'true';
    f.update();
    f.time(2000);
    assert.equal(f.position(), -2.5);
    f.root.dataset.scrollPaused = 'false';
    f.update();
    f.time(2250);
    assert.equal(f.position(), -5);
    f.root.dataset.scrollYMax = '200';
    f.sample(2);
    assert.equal(f.plot.style.transform, undefined);
    assert.equal(f.frames.size, 0);
});

test('reduced motion and destroy clear both transforms and cancel scheduled frames', t => {
    const f = fixture(t);
    f.sample(1);
    f.motion.matches = true;
    f.update();
    assert.equal(f.plot.style.transform, undefined);
    assert.equal(f.overlay.style.transform, undefined);
    assert.equal(f.frames.size, 0);
    f.motion.matches = false;
    f.sample(2);
    destroy(f.root);
    assert.equal(f.plot.style.transform, undefined);
    assert.equal(f.overlay.style.transform, undefined);
    assert.equal(f.frames.size, 0);
});

test('each frame advances both SVG groups at constant speed and stops when the feed stalls', t => {
    const f = fixture(t);
    f.sample(1);
    for (const time of [16, 33, 51, 100, 499, 1001, 1500, 2000, 3000]) {
        f.time(time);
        assert.ok(Math.abs(f.position() - Math.max(-10, 10 - time * 0.01)) < 1e-9);
        assert.equal(f.plot.style.transform, f.overlay.style.transform);
    }
    assert.equal(f.frames.size, 0);
});

test('resuming after a suspended mobile tab does not retain half a window of scroll offset', t => {
    const f = fixture(t);
    f.sample(1);
    f.time(30000);
    // Flywheel advances all elapsed buckets and uses their duration on the first resumed render.
    f.root.dataset.scrollDuration = '30000';
    f.sample(31);
    assert.equal(f.position(), 0);
    assert.equal(f.frames.size, 0);

    f.time(31000);
    f.root.dataset.scrollDuration = '1000';
    f.sample(32);
    assert.equal(f.position(), 10);
    f.time(32000);
    assert.equal(f.position(), 0);
    assert.equal(f.plot.style.transform, f.overlay.style.transform);
});
