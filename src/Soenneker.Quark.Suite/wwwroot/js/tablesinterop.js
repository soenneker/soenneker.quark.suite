const tables = new WeakMap();
const observedAttributes = ['class', 'style', 'hidden', 'colspan', 'rowspan', 'src', 'width'];

// Reuse the previous allocation. Shorter content must not make polling tables jitter.
export function allocateWidths(measured, previous, available, options = {}) {
    const minWidth = Number.isFinite(options.minWidth) && options.minWidth > 0 ? options.minWidth : 64;
    const maxWidth = Math.max(minWidth, Number.isFinite(options.maxWidth) && options.maxWidth > 0 ? options.maxWidth : 640);
    const padding = Number.isFinite(options.padding) && options.padding >= 0 ? options.padding : 24;
    const widths = previous?.length === measured.length ? previous : new Array(measured.length);
    let total = 0, widest = 0, grew = false;
    for (let i = 0; i < measured.length; i++) {
        const required = Math.min(maxWidth, Math.max(minWidth, Math.ceil(measured[i])));
        const old = widths[i] || 0;
        const width = old >= required ? old : Math.min(maxWidth, Math.max(minWidth, Math.ceil(measured[i] + padding)));
        widths[i] = width;
        grew ||= width > old;
        total += width;
        if (width > widths[widest]) widest = i;
    }
    if (!widths.length) return widths;
    if (previous && grew && total > available) {
        // Reuse spare space in the long column before making the table scroll after real growth.
        const reserved = Math.min(maxWidth, Math.max(minWidth, Math.ceil(measured[widest] + padding)));
        const reclaim = Math.min(total - available, Math.max(0, widths[widest] - reserved));
        widths[widest] -= reclaim;
        total -= reclaim;
    }
    if (available > total) widths[widest] += available - total;
    return widths;
}

function saveStyle(element, name) {
    return [element.style.getPropertyValue(name), element.style.getPropertyPriority(name)];
}

function restoreStyle(element, name, saved) {
    if (saved[0]) element.style.setProperty(name, ...saved);
    else element.style.removeProperty(name);
}

function ownsStyle(element, name, value) {
    return element.style.getPropertyValue(name) === value && element.style.getPropertyPriority(name) === 'important';
}

function restoreLayout(state) {
    state.columns.replaceChildren();
    if (ownsStyle(state.table, 'table-layout', state.appliedLayout))
        restoreStyle(state.table, 'table-layout', state.layout);
    if (ownsStyle(state.table, 'width', state.appliedWidth))
        restoreStyle(state.table, 'width', state.width);
    state.appliedLayout = state.appliedWidth = null;
    state.widths = null;
}

function measure(state) {
    const { table, columns, measured, styled, styles } = state;
    const height = table.offsetHeight;
    const scrollLeft = state.container.scrollLeft;
    measured.length = 0;
    try {
        // Ask the browser for intrinsic widths on the existing DOM, then restore before paint.
        // No cloned controls, detached rows, or duplicate IDs; focus and selection stay in place.
        styled.push(table, columns);
        styles.push(table.style.cssText, columns.style.cssText);
        columns.style.setProperty('display', 'none', 'important');
        table.style.setProperty('table-layout', 'auto', 'important');
        table.style.setProperty('width', 'max-content', 'important');
        table.style.setProperty('min-width', '0', 'important');
        table.style.setProperty('max-width', 'none', 'important');
        // Prevent a temporarily shorter table from clamping the document's scroll position.
        table.style.setProperty('height', `${height}px`, 'important');
        if (table.caption) suppressContribution(state, table.caption);
        const rows = table.rows;
        for (let r = 0; r < rows.length; r++) {
            const cells = rows[r].cells;
            let spanning = false;
            for (let c = 0; c < cells.length; c++) spanning ||= cells[c].colSpan !== 1;
            if (spanning) {
                for (let c = 0; c < cells.length; c++) suppressContribution(state, cells[c]);
            }
        }
        // The native table algorithm aligns columns across rows, so read one complete visible row.
        // A wider/visible later row also handles headerless tables and hidden cells.
        for (let r = 0; r < rows.length; r++) {
            const cells = rows[r].cells;
            if (cells.length <= measured.length) continue;
            let spanning = false;
            for (let c = 0; c < cells.length; c++) spanning ||= cells[c].colSpan !== 1;
            if (spanning) continue;
            let index = 0;
            for (let c = 0; c < cells.length; c++) {
                const width = cells[c].getBoundingClientRect().width;
                if (width) measured[index++] = width;
            }
        }
        return measured;
    } finally {
        for (let i = 0; i < styled.length; i++) styled[i].style.cssText = styles[i];
        styled.length = styles.length = 0;
        state.container.scrollLeft = scrollLeft;
    }
}

function suppressContribution(state, cell) {
    state.styled.push(cell);
    state.styles.push(cell.style.cssText);
    // Ignore spanning detail/empty/footer content without hiding or detaching focused controls.
    cell.style.setProperty('width', '0', 'important');
    cell.style.setProperty('min-width', '0', 'important');
    cell.style.setProperty('max-width', '0', 'important');
    cell.style.setProperty('overflow', 'hidden', 'important');
}

function usesNativeLayout(table, columns) {
    for (let i = 0; i < table.children.length; i++) {
        const child = table.children[i];
        if (child.tagName === 'COLGROUP' && child !== columns) return true;
    }
    const rows = table.rows;
    for (let r = 0; r < rows.length; r++) {
        const cells = rows[r].cells;
        for (let c = 0; c < cells.length; c++) if (cells[c].rowSpan !== 1) return true;
    }
    return false;
}

function observe(state) {
    state.mutations.observe(state.table, { subtree: true, childList: true, characterData: true,
        attributes: true, attributeFilter: observedAttributes });
}

function update(state) {
    state.frame = 0;
    const { table, columns, container } = state;
    if (state.disposed || !table.isConnected || !table.getClientRects().length) return;
    const containerStyle = getComputedStyle(container);
    const available = Math.floor(container.clientWidth - parseFloat(containerStyle.paddingLeft) - parseFloat(containerStyle.paddingRight));
    if (available <= 0) return;

    // Pause observation for our own writes, so measuring and applying widths cannot feed back into themselves.
    state.mutations.disconnect();
    try {
        if (usesNativeLayout(table, columns)) {
            restoreLayout(state);
            return;
        }
        const measured = measure(state);
        // Do not relearn widths from an empty/loading/detail row between pages.
        if (!measured.length) return;
        const reset = state.reset || Math.abs(available - state.available) > 1 || measured.length !== state.widths?.length;
        // Reset the existing buffer instead of allocating a new array on resize.
        if (reset && state.widths) {
            state.widths.length = measured.length;
            state.widths.fill(0);
        }
        const widths = allocateWidths(measured, state.widths, available, state.options);
        state.widths = widths;
        state.available = available;
        state.reset = false;
        // Keep the same col nodes on refresh; only write widths that actually changed.
        while (columns.children.length > widths.length) columns.lastElementChild.remove();
        let total = 0;
        for (let i = 0; i < widths.length; i++) {
            let col = columns.children[i];
            if (!col) columns.append(col = document.createElement('col'));
            const width = `${widths[i]}px`;
            if (col.style.width !== width) col.style.width = width;
            total += widths[i];
        }
        // Preserve new styles supplied by a Blazor render while the observer is attached.
        if (!ownsStyle(table, 'table-layout', state.appliedLayout))
            state.layout = saveStyle(table, 'table-layout');
        if (!ownsStyle(table, 'width', state.appliedWidth))
            state.width = saveStyle(table, 'width');
        state.appliedLayout = 'fixed';
        state.appliedWidth = `${total}px`;
        if (table.style.tableLayout !== state.appliedLayout || table.style.getPropertyPriority('table-layout') !== 'important')
            table.style.setProperty('table-layout', state.appliedLayout, 'important');
        if (table.style.width !== state.appliedWidth || table.style.getPropertyPriority('width') !== 'important')
            table.style.setProperty('width', state.appliedWidth, 'important');
    } finally {
        if (!state.disposed) observe(state);
    }
}

export function initialize(table, columns, options) {
    if (!table?.isConnected || !table.parentElement || !columns || tables.has(table)) return;
    const state = { table, columns, container: table.parentElement, options, widths: null, measured: [], styled: [], styles: [], available: 0, reset: true,
        frame: 0, disposed: false, layout: saveStyle(table, 'table-layout'), width: saveStyle(table, 'width') };
    const schedule = () => {
        if (!state.disposed && !state.frame) state.frame = requestAnimationFrame(() => update(state));
    };
    state.mutations = new MutationObserver(records => {
        for (const record of records) {
            if (table.tHead?.contains(record.target)) state.reset = true;
        }
        schedule();
    });
    state.resize = new ResizeObserver(entries => {
        if (entries[0].contentRect.width !== state.observedWidth) {
            state.observedWidth = entries[0].contentRect.width;
            schedule();
        }
    });
    state.load = schedule;
    state.fonts = schedule;
    tables.set(table, state);
    observe(state);
    state.resize.observe(state.container);
    table.addEventListener('load', state.load, true);
    document.fonts?.addEventListener('loadingdone', state.fonts);
    schedule();
}

export function destroy(table) {
    const state = tables.get(table);
    if (!state) return;
    state.disposed = true;
    cancelAnimationFrame(state.frame);
    state.mutations.disconnect();
    state.resize.disconnect();
    table.removeEventListener('load', state.load, true);
    document.fonts?.removeEventListener('loadingdone', state.fonts);
    restoreLayout(state);
    tables.delete(table);
}
