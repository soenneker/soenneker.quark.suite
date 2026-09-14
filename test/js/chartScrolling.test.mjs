import { test } from 'node:test';
import assert from 'node:assert/strict';
import { initialize, destroy } from '../../src/Soenneker.Quark.Suite/wwwroot/js/chartscrollinterop.js';

function fixture(t) {
    const timeline = { currentTime: 0 };
    let update;
    const motion = { matches: false, addEventListener() {}, removeEventListener() {} };
    globalThis.matchMedia = () => motion;
    globalThis.document = { timeline };
    globalThis.MutationObserver = class {
        constructor(callback) { update = callback; }
        observe() {}
        disconnect() {}
    };
    globalThis.DOMMatrix = class { constructor(value) { this.m41 = Number(value); } };
    const position = animation => {
        if (!animation || animation.cancelled) return 0;
        const time = animation.pausedAt ?? timeline.currentTime;
        const fraction = Math.max(0, Math.min(1, (time - animation.startTime) / animation.options.duration));
        return animation.from + (animation.to - animation.from) * fraction;
    };
    globalThis.getComputedStyle = element => ({ transform: position(element.animation) });
    function element() {
        return { animate(frames, options) {
            const offset = frame => Number(frame.transform.match(/translateX\((.*)px\)/)[1]);
            return this.animation = {
                from: offset(frames[0]), to: offset(frames[1]), options,
                cancel() { this.cancelled = true; },
                pause() { this.pausedAt = timeline.currentTime; },
                play() { this.startTime += timeline.currentTime - this.pausedAt; this.pausedAt = null; }
            };
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
    return { root, plot, overlay, motion, update, position: () => position(plot.animation),
        time(value) { timeline.currentTime = value; },
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
    assert.equal(f.plot.animation.startTime, f.overlay.animation.startTime);
    assert.equal(f.plot.animation.options.easing, 'linear');
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
    assert.ok(f.plot.animation.cancelled);
});

test('reduced motion and destroy cancel both animations', t => {
    const f = fixture(t);
    f.sample(1);
    f.motion.matches = true;
    f.update();
    assert.ok(f.plot.animation.cancelled);
    assert.ok(f.overlay.animation.cancelled);
    f.motion.matches = false;
    f.sample(2);
    destroy(f.root);
    assert.ok(f.plot.animation.cancelled);
    assert.ok(f.overlay.animation.cancelled);
});
