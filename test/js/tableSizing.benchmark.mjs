// Generate a browser benchmark: node test/js/tableSizing.benchmark.mjs <output.html> [baseline.js]
// Open the file in Chromium and inspect window.tableSizingBenchmark.
import { readFile, writeFile } from 'node:fs/promises';

async function benchmark() {
    const table = document.querySelector('table');
    const body = table.createTBody();
    for (let r = 0; r < 100; r++) {
        const row = body.insertRow();
        for (const text of ['IAutomationEngine.ProcessDueScheduledRuns', 'Succeeded', '1 / 1', '13 seconds ago', 'Retry'])
            row.insertCell().textContent = text;
    }
    const nativeClone = Node.prototype.cloneNode;
    const nativeRect = Element.prototype.getBoundingClientRect;
    const nativeCreate = document.createElement;
    let clones = 0, cellReads = 0, createdElements = 0;
    Node.prototype.cloneNode = function (deep) { clones++; return nativeClone.call(this, deep); };
    Element.prototype.getBoundingClientRect = function () {
        if (this.matches('td, th')) cellReads++;
        return nativeRect.call(this);
    };
    document.createElement = function (...args) { createdElements++; return nativeCreate.apply(this, args); };
    const settle = () => new Promise(resolve => requestAnimationFrame(() => requestAnimationFrame(resolve)));
    window.sizingDurations = [];
    try {
        initialize(table, table.querySelector('colgroup'), {});
        await settle();
        // Warm up layout and JIT before recording allocation counts and frame costs.
        for (let i = 0; i < 5; i++) {
            body.rows[0].cells[3].firstChild.data = `${13 + i} seconds ago`;
            await settle();
        }
        window.sizingDurations.length = 0;
        clones = cellReads = createdElements = 0;
        for (let i = 0; i < 40; i++) {
            body.rows[0].cells[3].firstChild.data = `${13 + i} seconds ago`;
            await settle();
        }
        const samples = window.sizingDurations.sort((a, b) => a - b);
        window.tableSizingBenchmark = {
            rows: 100, updates: 40, measuredFrames: samples.length,
            medianMs: samples[Math.floor(samples.length / 2)],
            p95Ms: samples[Math.floor(samples.length * .95)],
            tableClones: clones, clonedElements: clones * (table.querySelectorAll('*').length + 1),
            createdElements, cellGeometryReads: cellReads
        };
    } finally {
        destroy(table);
        Node.prototype.cloneNode = nativeClone;
        Element.prototype.getBoundingClientRect = nativeRect;
        document.createElement = nativeCreate;
    }
    document.querySelector('pre').textContent = JSON.stringify(window.tableSizingBenchmark, null, 2);
}

const [output, baseline] = process.argv.slice(2);
if (!output) throw new Error('Supply an output HTML path.');
let source = await readFile(baseline || new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/tablesinterop.js', import.meta.url), 'utf8');
const schedule = 'requestAnimationFrame(() => update(state))';
if (!source.includes(schedule)) throw new Error('Update benchmark instrumentation to match the sizing scheduler.');
source = source.replace(schedule, `requestAnimationFrame(() => {
    const started = performance.now();
    update(state);
    void table.offsetWidth; // Include final layout, not just the temporary measurement.
    window.sizingDurations.push(performance.now() - started);
})`);
await writeFile(output, `<!doctype html><html lang="en"><meta charset="utf-8"><title>Table sizing benchmark</title>
<style>body{font:14px system-ui}div{width:1100px;overflow:auto}table{border-collapse:collapse;width:100%}
th,td{padding:8px 12px;overflow-wrap:anywhere;border-bottom:1px solid #ddd}</style>
<pre>Running...</pre><div><table><colgroup></colgroup><thead><tr><th>Job</th><th>Status</th><th>Attempt</th><th>Updated</th><th>Action</th></tr></thead></table></div>
<script type="module">${source}\n(${benchmark.toString()})();</script></html>`);
console.log(output);
