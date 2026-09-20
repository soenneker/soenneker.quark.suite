import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import vm from 'node:vm';

const source = readFileSync(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/themeinterop.js', import.meta.url), 'utf8')
    .replaceAll('export function', 'function');

function fixture(dark, stored, blocked = false) {
    const media = new EventTarget();
    media.matches = dark;
    const window = new EventTarget();
    window.matchMedia = () => media;
    const root = { style: {}, classList: { toggle(_, value) { root.dark = value; } } };
    const storage = new Map(stored === undefined ? [] : [['quark-theme', stored]]);
    const check = () => { if (blocked) throw new Error('Storage disabled'); };
    const context = vm.createContext({ window, document: { documentElement: root }, CustomEvent,
        localStorage: {
            getItem(key) { check(); return storage.get(key) ?? null; },
            setItem(key, value) { check(); storage.set(key, value); },
            removeItem(key) { check(); storage.delete(key); }
        }
    });
    vm.runInContext(source, context);
    return { context, root, storage, window,
        changeSystem(value) { media.matches = value; media.dispatchEvent(new Event('change')); },
        changeStorage(value) {
            if (value == null) storage.delete('quark-theme'); else storage.set('quark-theme', value);
            const event = new Event('storage');
            event.key = 'quark-theme';
            window.dispatchEvent(event);
        }
    };
}

for (const dark of [false, true]) {
    test(`system ${dark ? 'dark' : 'light'} is the default without persisting it`, () => {
        const f = fixture(dark);
        assert.equal(f.context.initialize(), dark);
        assert.equal(f.storage.size, 0);
        assert.equal(f.root.style.colorScheme, dark ? 'dark' : 'light');
        f.changeSystem(!dark);
        assert.equal(f.root.dark, !dark);
        assert.equal(f.storage.size, 0);
    });
}

test('manual choices survive OS changes; useSystem clears the override', () => {
    const f = fixture(true);
    f.context.initialize();
    assert.equal(f.context.toggle(), false);
    assert.equal(f.storage.get('quark-theme'), 'light');
    f.changeSystem(false);
    f.changeSystem(true);
    assert.equal(f.root.dark, false);
    assert.equal(f.context.useSystem(), true);
    assert.equal(f.storage.size, 0);
    f.changeSystem(false);
    assert.equal(f.root.dark, false);
});

test('saved preferences take precedence; invalid preferences follow the system', () => {
    assert.equal(fixture(false, 'dark').context.initialize(), true);
    assert.equal(fixture(true, 'light').context.initialize(), false);
    assert.equal(fixture(true, 'invalid').context.initialize(), true);
});

test('storage changes in another tab update the active theme and resume system mode', () => {
    const f = fixture(true);
    f.context.initialize();
    f.changeStorage('light');
    assert.equal(f.root.dark, false);
    f.changeStorage(null);
    assert.equal(f.root.dark, true);
});

test('blocked storage still supports manual choice and returning to system', () => {
    const f = fixture(true, undefined, true);
    assert.equal(f.context.initialize(), true);
    assert.equal(f.context.toggle(), false);
    f.changeSystem(true);
    assert.equal(f.root.dark, false);
    assert.equal(f.context.useSystem(), true);
});

test('a failed storage write does not restore a stale saved preference', () => {
    const f = fixture(false, 'dark');
    f.context.localStorage.setItem = () => { throw new Error('Read only'); };
    f.context.initialize();
    assert.equal(f.context.toggle(), false);
    assert.equal(f.context.resolveIsDark(), false);
});

test('repeated initialization does not duplicate OS listeners and callbacks can be removed', async () => {
    const f = fixture(false);
    const updates = [];
    const ref = { _id: 1, invokeMethodAsync(method, value) { updates.push([method, value]); return Promise.resolve(); } };
    f.context.initialize();
    f.context.initialize();
    f.context.registerThemeChangedCallback(ref);
    f.changeSystem(true);
    assert.deepEqual(updates, [['OnThemeChanged', true]]);
    f.context.unregisterThemeChangedCallback(ref);
    f.changeSystem(false);
    assert.equal(updates.length, 1);
});

for (const dark of [false, true]) {
    test(`explicit modes and preference callbacks with system dark=${dark}`, () => {
        const f = fixture(dark);
        const updates = [];
        f.context.registerThemeChangedCallback({ _id: 2, invokeMethodAsync(...args) { updates.push(args); return Promise.resolve(); } });
        f.context.initialize();
        assert.equal(f.context.getMode(), 'system');
        for (const mode of ['light', 'dark', 'system']) {
            assert.equal(f.context.setMode(mode), mode === 'system' ? dark : mode === 'dark');
            assert.equal(f.context.getMode(), mode);
            assert.equal(updates.at(-1)[2], mode);
            assert.equal(f.storage.get('quark-theme'), mode === 'system' ? undefined : mode);
        }
        f.changeSystem(!dark);
        assert.equal(f.root.dark, !dark);
        assert.equal(f.context.getMode(), 'system');
    });
}

test('explicit mode selection works with blocked storage and rejects invalid modes', () => {
    const f = fixture(true, undefined, true);
    f.context.setMode('light');
    assert.equal(f.context.getMode(), 'light');
    f.changeSystem(true);
    assert.equal(f.root.dark, false);
    f.context.setMode('system');
    assert.equal(f.context.getMode(), 'system');
    assert.equal(f.root.dark, true);
    assert.throws(() => f.context.setMode('invalid'));
});
