import { test } from 'node:test';
import assert from 'node:assert/strict';
import { create, show, hide, destroy, setCallbacks } from '../../src/Soenneker.Quark.Suite/wwwroot/js/floatingwindowinterop.js';

function fixture(t) {
    let transitions = [];
    const callbacks = [];
    const element = {
        style: {}, dataset: { state: 'closed' },
        addEventListener() {}, removeEventListener() {},
        getAnimations: () => transitions
    };
    globalThis.document = { getElementById: () => element };
    globalThis.window = { innerWidth: 1000, innerHeight: 800, addEventListener() {}, removeEventListener() {} };
    globalThis.getComputedStyle = () => ({ opacity: '1' });
    globalThis.cancelAnimationFrame = () => {};
    create('motion-test', JSON.stringify({ draggable: false, resizable: false, centerOnShow: false,
        focusOnShow: false, constrainToViewport: false, autoSizeToContent: false }));
    setCallbacks('motion-test', { invokeMethodAsync: async name => { callbacks.push(name); } });
    t.after(() => destroy('motion-test'));
    return { element, callbacks, pending() {
        const deferred = Promise.withResolvers();
        transitions = [{ transitionProperty: 'opacity', finished: deferred.promise }];
        return deferred;
    } };
}

test('floating window stays mounted until its closing transition ends', async t => {
    const f = fixture(t);
    show('motion-test');
    const transition = f.pending();
    const closing = hide('motion-test');
    assert.equal(f.element.style.display, 'flex');
    assert.equal(f.element.dataset.state, 'closed');
    assert.equal(f.element.inert, true);
    await hide('motion-test'); // Duplicate close cannot swallow the first callback.
    transition.resolve();
    await closing;
    assert.equal(f.element.style.display, 'none');
    assert.deepEqual(f.callbacks, ['InvokeOnShow', 'InvokeOnHiding', 'InvokeOnHide']);
});

test('reopening cancels a pending hide without a stale hide callback', async t => {
    const f = fixture(t);
    show('motion-test');
    const transition = f.pending();
    const closing = hide('motion-test');
    show('motion-test');
    transition.reject(new Error('transition reversed'));
    await closing;
    assert.equal(f.element.style.display, 'flex');
    assert.equal(f.element.dataset.state, 'open');
    assert.equal(f.element.inert, false);
    assert.deepEqual(f.callbacks, ['InvokeOnShow', 'InvokeOnHiding', 'InvokeOnShow']);
});

test('no-transition closing completes immediately and disposal suppresses late callbacks', async t => {
    const f = fixture(t);
    show('motion-test');
    await hide('motion-test');
    assert.equal(f.element.style.display, 'none');
    show('motion-test');
    const transition = f.pending();
    const closing = hide('motion-test');
    destroy('motion-test');
    transition.resolve();
    await closing;
    assert.deepEqual(f.callbacks, ['InvokeOnShow', 'InvokeOnHiding', 'InvokeOnHide', 'InvokeOnShow', 'InvokeOnHiding']);
});
