import { test } from 'node:test';
import assert from 'node:assert/strict';
import { repair, observe, unobserve } from '../../src/Soenneker.Quark.Suite/wwwroot/js/fieldlayoutinterop.js';

function fixture(t) {
    const saved = { getComputedStyle: globalThis.getComputedStyle, ResizeObserver: globalThis.ResizeObserver, MutationObserver: globalThis.MutationObserver };
    t.after(() => Object.assign(globalThis, saved));
    globalThis.getComputedStyle = e => e.computed;
    const instances = [];
    class Observer {
        constructor(callback) { this.callback = callback; instances.push(this); }
        observe() {}
        disconnect() { this.disconnected = true; }
    }
    globalThis.ResizeObserver = globalThis.MutationObserver = Observer;
    function group({ height = 0, width = 200, display = 'flex', inline = '', priority = '' } = {}) {
        const state = { value: inline, priority, writes: [] };
        return {
            computed: { display, containerType: 'inline-size' }, children: [{}], offsetWidth: width,
            get offsetHeight() { return height; },
            style: {
                getPropertyValue: () => state.value,
                getPropertyPriority: () => state.priority,
                setProperty: (name, value, priority) => { state.value = value; state.priority = priority; state.writes.push(value); },
                removeProperty: () => { state.value = ''; state.priority = ''; }
            }, state
        };
    }
    return { group, instances, fieldset: groups => ({ isConnected: true, querySelectorAll: () => groups }) };
}

test('repairs only collapsed visible query containers and preserves caller display override', t => {
    const f = fixture(t);
    const collapsed = f.group({ inline: 'grid', priority: 'important', display: 'grid' });
    const healthy = f.group({ height: 200 });
    const hidden = f.group({ width: 0 });
    repair(f.fieldset([collapsed, healthy, hidden]));
    assert.deepEqual(collapsed.state.writes, ['block', 'grid']);
    assert.equal(collapsed.state.priority, 'important');
    assert.deepEqual(healthy.state.writes, []);
    assert.deepEqual(hidden.state.writes, []);
});

test('removes temporary display and leaves detached fieldsets untouched', t => {
    const f = fixture(t), group = f.group(), fieldset = f.fieldset([group]);
    repair(fieldset);
    assert.equal(group.state.value, '');
    fieldset.isConnected = false;
    repair(fieldset);
    assert.deepEqual(group.state.writes, ['block']);
});

test('owns one observer pair and disconnects both on disposal', t => {
    const f = fixture(t), fieldset = f.fieldset([]);
    observe(fieldset); observe(fieldset);
    assert.equal(f.instances.length, 2);
    unobserve(fieldset); unobserve(fieldset);
    assert.ok(f.instances.every(o => o.disconnected));
    observe(fieldset);
    assert.equal(f.instances.length, 4);
    unobserve(fieldset);
});
