import { test } from 'node:test';
import assert from 'node:assert/strict';
import { mountPortal, unmountPortal } from '../../src/Soenneker.Quark.Suite/wwwroot/js/portalinterop.js';

function fixture(t) {
    const saved = { Node: globalThis.Node, HTMLElement: globalThis.HTMLElement, document: globalThis.document };
    class Element {
        children = [];
        parentNode = null;
        appendChild(child) { return this.insertBefore(child, null); }
        insertBefore(child, sibling) {
            assert.ok(child !== this && !child.contains(this), 'DOM cycle');
            if (child.parentNode) child.parentNode.children.splice(child.parentNode.children.indexOf(child), 1);
            const index = sibling ? this.children.indexOf(sibling) : this.children.length;
            this.children.splice(index, 0, child);
            child.parentNode = this;
            return child;
        }
        contains(other) { return other === this || this.children.some(child => child.contains(other)); }
        get nextSibling() { return this.parentNode?.children[this.parentNode.children.indexOf(this) + 1] ?? null; }
    }
    const body = new Element();
    const surface = body.appendChild(new Element());
    const outside = body.appendChild(new Element());
    const listeners = new Map();
    const mounted = [];
    globalThis.Node = globalThis.HTMLElement = Element;
    globalThis.document = {
        body, fullscreenElement: null, activeElement: null,
        querySelector: selector => selector === '#explicit' ? outside : null,
        addEventListener: (name, callback) => { assert.ok(!listeners.has(name)); listeners.set(name, callback); },
        removeEventListener: (name, callback) => { assert.equal(listeners.get(name), callback); listeners.delete(name); }
    };
    const portal = (parent, selector, container) => {
        const element = parent.appendChild(new Element());
        mounted.push(element);
        mountPortal(element, selector, container);
        return element;
    };
    const fullscreen = element => {
        document.fullscreenElement = element;
        listeners.get('fullscreenchange')?.();
    };
    t.after(() => {
        for (const element of mounted.reverse()) unmountPortal(element);
        assert.equal(listeners.size, 0);
        Object.assign(globalThis, saved);
    });
    return { Element, body, surface, outside, portal, fullscreen, listeners };
}

test('existing and newly opened popups follow fullscreen and return to body', t => {
    const f = fixture(t);
    const popup = f.portal(f.surface);
    assert.equal(popup.parentNode, f.body);
    f.fullscreen(f.surface);
    assert.equal(popup.parentNode, f.surface);
    const second = f.portal(f.surface);
    assert.equal(second.parentNode, f.surface);
    f.fullscreen(null);
    assert.equal(popup.parentNode, f.body);
    assert.equal(second.parentNode, f.body);
});

test('unrelated popups and explicit containers stay outside fullscreen', t => {
    const f = fixture(t);
    const unrelated = f.portal(f.outside);
    const explicit = f.portal(f.surface, '#explicit');
    const explicitElement = f.portal(f.surface, null, f.outside);
    f.fullscreen(f.surface);
    assert.equal(unrelated.parentNode, f.body);
    assert.equal(explicit.parentNode, f.outside);
    assert.equal(explicitElement.parentNode, f.outside);
});

test('nested portals follow their logical origin when fullscreen changes', t => {
    const f = fixture(t);
    const first = f.portal(f.surface);
    const child = first.appendChild(new f.Element());
    const second = f.portal(child);
    f.fullscreen(f.surface);
    assert.equal(first.parentNode, f.surface);
    assert.equal(second.parentNode, f.surface);
    f.fullscreen(f.outside);
    assert.equal(first.parentNode, f.body);
    assert.equal(second.parentNode, f.body);
});

test('unmount restores sibling order and removes fullscreen listener', t => {
    const f = fixture(t);
    const before = f.surface.appendChild(new f.Element());
    const element = f.surface.appendChild(new f.Element());
    const after = f.surface.appendChild(new f.Element());
    mountPortal(element);
    f.fullscreen(f.surface);
    unmountPortal(element);
    assert.deepEqual(f.surface.children, [before, element, after]);
    assert.equal(f.listeners.size, 0);
});

test('a portal containing the fullscreen target is not moved into itself', t => {
    const f = fixture(t);
    const element = f.portal(f.surface);
    const target = element.appendChild(new f.Element());
    f.fullscreen(target);
    assert.equal(element.parentNode, f.body);
    const nested = f.portal(target);
    assert.equal(nested.parentNode, target);
});
