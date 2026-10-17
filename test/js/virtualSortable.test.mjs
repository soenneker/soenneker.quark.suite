import assert from 'node:assert/strict';
import test from 'node:test';
import { readFileSync } from 'node:fs';
import { runInNewContext } from 'node:vm';
const script = readFileSync(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/sortableinterop.js', import.meta.url), 'utf8')
    .replace(/^import .*;$/m, '').replaceAll('export async function ', 'async function ').replaceAll('export function ', 'function ');
const context = {};
runInNewContext(script + '\nthis.pointerArgs = virtualPointerArgs;', context);
function host(id) { return { getAttribute: () => id, __quarkItemSelector: '[data-virtual-key]' }; }
function row(id) { return { getAttribute: () => id, matches: () => true }; }

test('same-column drops report full indexes and preserve all unmounted IDs', () => {
    const keys = Array.from({ length: 1000 }, (_, i) => `card-${i}`);
    const list = host('ready');
    const item = { ...row('card-500'), nextElementSibling: row('card-801'), previousElementSibling: row('card-800') };
    const result = context.pointerArgs({ item, from: list, to: list }, { keys }, { keys }, '[data-virtual-key]');
    assert.equal(result.oldIndex, 500);
    assert.equal(result.newIndex, 800);
    assert.equal(result.toItemIds.length, 1000);
    assert.equal(result.toItemIds[800], 'card-500');
    assert.equal(new Set(result.toItemIds).size, 1000);
});

test('cross-column drops preserve both complete backing collections', () => {
    const source = Array.from({ length: 1000 }, (_, i) => `a-${i}`);
    const destination = Array.from({ length: 1000 }, (_, i) => `b-${i}`);
    const item = { ...row('a-750'), nextElementSibling: row('b-600'), previousElementSibling: row('b-599') };
    const result = context.pointerArgs({ item, from: host('ready'), to: host('done') }, { keys: source }, { keys: destination }, '[data-virtual-key]');
    assert.equal(result.oldIndex, 750);
    assert.equal(result.newIndex, 600);
    assert.equal(result.fromItemIds.length, 999);
    assert.equal(result.toItemIds.length, 1001);
    assert.equal(result.toItemIds[600], 'a-750');
    assert.equal(result.fromItemIds.includes('a-750'), false);
});

test('dropping into an empty virtual column inserts at zero', () => {
    const result = context.pointerArgs({ item: row('a'), from: host('ready'), to: host('done') }, { keys: ['a'] }, { keys: [], range: [0, 0] }, '[data-virtual-key]');
    assert.equal(result.newIndex, 0);
    assert.equal(result.fromItemIds.length, 0);
    assert.equal(result.toItemIds[0], 'a');
});
