// Generate a self-contained browser regression fixture:
// node test/js/tableSizing.browser.mjs <output.html>
// Open that file in Chromium and inspect window.tableSizingResults.
import { readFile, writeFile } from 'node:fs/promises';

async function checks() {
    const table = document.querySelector('table');
    const columns = table.querySelector('colgroup');
    const container = table.parentElement;
    const action = table.querySelector('button');
    const job = table.tBodies[0].rows[0].cells[0];
    const settle = () => new Promise(resolve => requestAnimationFrame(() => requestAnimationFrame(() => setTimeout(resolve, 30))));
    const widths = () => Array.from(table.tHead.rows[0].cells, cell => Math.round(cell.getBoundingClientRect().width));
    const equal = (actual, expected, name) => {
        if (JSON.stringify(actual) !== JSON.stringify(expected)) throw new Error(`${name}: ${JSON.stringify(actual)} != ${JSON.stringify(expected)}`);
    };
    const assert = (condition, name) => { if (!condition) throw new Error(name); };
    const results = [];
    const check = async (name, run) => { await run(); results.push(name); };
    try {
        await check('content sizing keeps attempts compact', async () => {
            initialize(table, columns, {});
            await settle();
            const sizes = widths();
            assert(sizes[0] > sizes[2] * 3, `job / attempt widths: ${sizes}`);
            assert(table.getBoundingClientRect().width <= container.clientWidth + 1, 'desktop fits container');
        });
        await check('small button changes preserve every width and focus', async () => {
            const before = widths();
            action.focus();
            action.textContent = 'Retrying';
            await settle();
            equal(widths(), before, 'small update');
            assert(document.activeElement === action, 'measurement retains focus');
            action.textContent = 'Retry';
            await settle();
            equal(widths(), before, 'shorter update');
        });
        await check('large action growth uses spare job width and then stabilizes', async () => {
            const before = widths();
            const originalColumns = Array.from(columns.children);
            action.textContent = 'Retry failed delivery';
            await settle();
            const grown = widths();
            assert(grown[4] > before[4], 'action grows');
            assert(originalColumns.every((col, i) => columns.children[i] === col), 'growth reuses column nodes');
            equal(grown.slice(1, 4), before.slice(1, 4), 'unaffected columns');
            assert(table.getBoundingClientRect().width <= container.clientWidth + 1, 'reclaims spare width');
            action.textContent = 'Retry';
            await settle();
            equal(widths(), grown, 'retains allocation');
        });
        await check('spanning details do not distort measurements', async () => {
            const before = widths();
            const row = table.tBodies[0].insertRow();
            const cell = row.insertCell();
            cell.colSpan = 5;
            cell.textContent = 'Very long details '.repeat(100);
            await settle();
            equal(widths(), before, 'detail row');
            row.remove();
            await settle();
        });
        await check('measurement preserves live radio selection', async () => {
            const radio = document.createElement('input');
            radio.type = 'radio';
            radio.name = 'job-choice';
            radio.checked = true;
            table.tBodies[0].rows[0].cells[4].append(radio);
            await settle();
            assert(radio.checked, 'measurement must not uncheck a live radio');
            radio.remove();
            await settle();
        });
        await check('measuring spanning controls preserves focus, selection, and inline styles', async () => {
            const row = table.tBodies[0].insertRow();
            const cell = row.insertCell();
            cell.colSpan = 5;
            cell.style.setProperty('max-width', '900px', 'important');
            const input = document.createElement('input');
            input.value = 'Editable details';
            input.style.width = '1400px';
            cell.append(input);
            input.focus();
            input.setSelectionRange(2, 7);
            const before = widths();
            const style = cell.style.cssText;
            await settle();
            equal(widths(), before, 'spanning input does not influence widths');
            assert(document.activeElement === input, 'spanning input keeps focus');
            equal([input.selectionStart, input.selectionEnd], [2, 7], 'selection');
            equal(cell.style.cssText, style, 'restores styled cell');
            row.remove();
            await settle();
        });
        await check('hidden columns do not leave empty allocations', async () => {
            for (const row of table.rows) row.cells[2].style.display = 'none';
            await settle();
            equal(columns.children.length, 4, 'visible column count');
            for (const row of table.rows) row.cells[2].style.display = '';
            await settle();
            equal(columns.children.length, 5, 'restored column count');
        });
        await check('narrow containers scroll and long outliers are capped', async () => {
            container.style.width = '360px';
            job.textContent = 'VeryLongJobName'.repeat(100);
            await settle();
            assert(widths()[0] <= 640, `outlier capped: ${widths()[0]}`);
            assert(container.scrollWidth > container.clientWidth, 'narrow table scrolls');
            const before = widths();
            job.textContent = 'ShortJob';
            await settle();
            equal(widths(), before, 'shorter page retains widths');
        });
        await check('measurement preserves scroll offsets without cloning or creating elements', async () => {
            container.style.height = '60px';
            container.style.overflow = 'auto';
            container.scrollLeft = 80;
            container.scrollTop = container.scrollHeight;
            const scroll = [container.scrollLeft, container.scrollTop];
            const nativeClone = Node.prototype.cloneNode;
            const nativeCreate = document.createElement;
            let allocations = 0;
            Node.prototype.cloneNode = function (...args) { allocations++; return nativeClone.apply(this, args); };
            document.createElement = function (...args) { allocations++; return nativeCreate.apply(this, args); };
            try {
                action.textContent = 'Retrying';
                await settle();
                equal([container.scrollLeft, container.scrollTop], scroll, 'scroll offsets');
                equal(allocations, 0, 'no DOM allocations on content updates');
            } finally {
                Node.prototype.cloneNode = nativeClone;
                document.createElement = nativeCreate;
                container.style.height = '';
                container.style.overflow = '';
                action.textContent = 'Retry';
            }
            await settle();
        });
        await check('container resizing and schema changes reset allocations', async () => {
            container.style.width = '1100px';
            await settle();
            assert(table.getBoundingClientRect().width <= 1101, 'resized table fits');
            table.tHead.rows[0].cells[4].textContent = 'Retry';
            await settle();
            assert(widths()[4] < 180, 'new schema releases old action width');
        });
        await check('hidden initialization defers until visible', async () => {
            destroy(table);
            container.style.display = 'none';
            initialize(table, columns, {});
            await settle();
            equal(columns.children.length, 0, 'hidden table');
            container.style.display = '';
            await settle();
            equal(columns.children.length, 5, 'visible table');
        });
        await check('authored columns and rowspans use native layout', async () => {
            const authored = document.createElement('colgroup');
            authored.innerHTML = '<col style="width:90px">';
            table.insertBefore(authored, columns);
            await settle();
            equal(columns.children.length, 0, 'authored columns');
            authored.remove();
            await settle();
            equal(columns.children.length, 5, 'adaptive restored');
            job.rowSpan = 2;
            await settle();
            equal(columns.children.length, 0, 'rowspan fallback');
            job.rowSpan = 1;
            await settle();
        });
        await check('cleanup restores styles and cancels pending measurements', async () => {
            job.textContent = 'Pending change';
            destroy(table);
            await settle();
            equal(columns.children.length, 0, 'cleanup columns');
            equal(table.style.width, '100%', 'restored width');
            equal(table.style.tableLayout, '', 'restored layout');
            job.textContent = 'IAutomationEngine.ProcessDueScheduledRuns';
            await settle();
            equal(columns.children.length, 0, 'observer detached');
        });
        await check('new application styles survive disabling adaptive layout', async () => {
            initialize(table, columns, {});
            await settle();
            table.style.width = '80%';
            destroy(table);
            equal(table.style.width, '80%', 'new width preserved');
            table.style.width = '100%';
        });
        await check('switching to fixed layout preserves the application style even when its value matches', async () => {
            for (const waitForObserver of [false, true]) {
                initialize(table, columns, {});
                await settle();
                table.style.setProperty('table-layout', 'fixed');
                if (waitForObserver) await settle();
                destroy(table);
                equal(table.style.tableLayout, 'fixed', 'fixed layout survives cleanup');
                equal(table.style.getPropertyPriority('table-layout'), '', 'application priority is restored');
                table.style.removeProperty('table-layout');
            }
        });
        table.tHead.rows[0].cells[4].textContent = 'Action';
        initialize(table, columns, {});
        await settle();
        window.tableSizingResults = { passed: results.length, tests: results, widths: widths() };
    } catch (error) {
        window.tableSizingResults = { passed: results.length, tests: results, error: error.message };
    }
    document.querySelector('#results').textContent = JSON.stringify(window.tableSizingResults, null, 2);
}

const source = await readFile(new URL('../../src/Soenneker.Quark.Suite/wwwroot/js/tablesinterop.js', import.meta.url), 'utf8');
const output = process.argv[2];
if (!output) throw new Error('Supply an output HTML path.');
await writeFile(output, `<!doctype html><html lang="en"><meta charset="utf-8"><title>Adaptive table regression</title>
<style>
*{box-sizing:border-box}body{margin:32px;font:14px system-ui;color:#18212c}
.container{width:1100px;overflow-x:auto}table{border-collapse:collapse}th,td{padding:12px 16px;text-align:left;border-bottom:1px solid #ddd;overflow-wrap:anywhere}
th{background:#f5f5f5}button{font:inherit;padding:7px 12px;border:1px solid #ccc;background:white;border-radius:6px;white-space:nowrap}
</style><h1>Adaptive table widths</h1><div class="container"><table style="width:100%"><colgroup></colgroup>
<thead><tr><th>Job</th><th>Status</th><th>Attempt</th><th>Updated</th><th>Action</th></tr></thead>
<tbody><tr><td>IAutomationEngine.ProcessDueScheduledRuns</td><td>Succeeded</td><td>1 / 1</td><td>13 seconds ago</td><td></td></tr>
<tr><td>PhoneLookupRefreshJob.Recover</td><td>Failed</td><td>1 / 3</td><td>1 minute ago</td><td><button>Retry</button></td></tr></tbody>
</table></div><pre id="results">Running checks...</pre><script type="module">${source}\n(${checks.toString()})();</script></html>`);
console.log(output);
