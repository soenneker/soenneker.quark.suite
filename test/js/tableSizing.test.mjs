import { test } from 'node:test';
import assert from 'node:assert/strict';
import { allocateWidths } from '../../src/Soenneker.Quark.Suite/wwwroot/js/tablesinterop.js';

test('jobs receive spare width while counters stay compact', () => {
    const widths = allocateWidths([430, 120, 60, 150, 80], null, 1200);
    assert.deepEqual(widths, [694, 144, 84, 174, 104]);
});

test('button and timestamp updates inside the reserve do not shift any column', () => {
    const widths = allocateWidths([430, 120, 60, 150, 80], null, 1200);
    const expected = [...widths];
    assert.equal(allocateWidths([420, 130, 65, 165, 98], widths, 1200), widths);
    assert.deepEqual(widths, expected);
    assert.equal(allocateWidths([100, 60, 20, 70, 20], widths, 1200), widths);
    assert.deepEqual(widths, expected);
});

test('growth beyond the reserve allocates new headroom once and retains other columns', () => {
    const widths = allocateWidths([300, 80], null, 428);
    const grown = allocateWidths([300, 110], widths, 428);
    assert.equal(grown, widths);
    assert.deepEqual(grown, [324, 134]);
    assert.deepEqual(allocateWidths([300, 120], grown, 428), grown);
});

test('resetting an allocation buffer reuses it while releasing obsolete widths', () => {
    const widths = allocateWidths([430, 80], null, 1200);
    widths.fill(0);
    assert.equal(allocateWidths([200, 80], widths, 428), widths);
    assert.deepEqual(widths, [324, 104]);
});

test('outliers cap and narrow containers retain readable widths for horizontal scrolling', () => {
    assert.deepEqual(allocateWidths([3000, 80, 10], null, 320), [640, 104, 64]);
    assert.deepEqual(allocateWidths([3000, 80, 10], [640, 104, 64], 320), [640, 104, 64]);
});

test('real action growth uses spare room in the long column before scrolling', () => {
    const widths = allocateWidths([430, 80], null, 1200);
    assert.deepEqual(allocateWidths([430, 120], widths, 1200), [1056, 144]);
});

test('a container reset can shrink old allocations and applies custom limits', () => {
    assert.deepEqual(allocateWidths([120, 10], null, 100, { minWidth: 40, maxWidth: 100, padding: 10 }), [100, 40]);
    assert.deepEqual(allocateWidths([120, 10], null, 100, { minWidth: -1, maxWidth: NaN, padding: -2 }), [144, 64]);
    assert.deepEqual(allocateWidths([], null, 100), []);
});
