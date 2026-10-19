const { configure: configureFileDrop } = await import(new URL(import.meta.url).pathname.endsWith(".min.js") ? "./filedropinterop.min.js" : "./filedropinterop.js");

const owners = new Map();
const registrations = new Map();

export function insertionTarget(rows, clientY, excludedId = null) {
    for (const row of rows) {
        if (row.dataset.fileId === excludedId) continue;
        const rect = row.getBoundingClientRect();
        if (clientY < rect.top + rect.height / 2) return row.dataset.fileId;
    }
    return null;
}

export function initialize(owner, root, callback) {
    registrations.get(owner)?.();
    if (!root) return;
    const controller = new AbortController();
    const placeholder = root.querySelector('[data-slot="file-drop-zone-placeholder"]');
    const ghost = root.querySelector('[data-slot="file-drop-zone-ghost"]');
    const rows = () => root.querySelectorAll('[data-slot="file-drop-zone-file"]');
    const canReorder = () => root.dataset.canReorder === 'true';
    let drag = null, frame = 0, pending = false, external = false;
    let resizeObserver = null, mutationObserver = null;

    const listen = (target, name, fn, options = {}) => target.addEventListener(name, fn, { ...options, signal: controller.signal });
    const clear = () => {
        if (frame) cancelAnimationFrame(frame);
        frame = 0;
        resizeObserver?.disconnect();
        mutationObserver?.disconnect();
        resizeObserver = mutationObserver = null;
        if (drag) {
            drag.row.style.opacity = '';
            drag.handle.removeAttribute('aria-pressed');
            if (drag.handle.hasPointerCapture?.(drag.pointerId)) drag.handle.releasePointerCapture(drag.pointerId);
        }
        drag = null;
        external = false;
        placeholder.hidden = true;
        ghost.hidden = true;
    };
    const showTarget = (beforeId, excludedId, items) => {
        let next = null, last = null;
        for (const row of items) {
            if (row.dataset.fileId === excludedId) continue;
            last = row;
            if (!next && row.dataset.fileId === beforeId) next = row;
        }
        if (!last) {
            placeholder.hidden = true;
            return;
        }
        const rect = root.getBoundingClientRect();
        const target = (next ?? last).getBoundingClientRect();
        const y = next ? target.top - 4 : target.bottom + 4;
        setStyle(placeholder, 'left', '8px');
        setStyle(placeholder, 'right', '8px');
        setStyle(placeholder, 'top', `${y - rect.top - root.clientTop + root.scrollTop - 4}px`);
        if (placeholder.hidden) placeholder.hidden = false;
    };
    const focusHandle = id => {
        for (const row of rows()) {
            if (row.dataset.fileId !== id) continue;
            row.querySelector('[data-file-drag-handle]')?.focus({ preventScroll: true });
            return;
        }
    };
    const move = async (id, value, method = "MoveFile") => {
        if (pending || !canReorder()) return;
        pending = true;
        try {
            await callback.invokeMethodAsync(method, id, value);
            if (!controller.signal.aborted) focusHandle(id);
        } catch (error) {
            if (!controller.signal.aborted) console.error('File drop zone reorder failed.', error);
        } finally { pending = false; }
    };
    const setStyle = (element, property, value) => {
        if (element.style[property] !== value) element.style[property] = value;
    };
    const scheduleDrag = () => {
        if (drag?.active && !frame) frame = requestAnimationFrame(renderDrag);
    };
    const observeDragLayout = () => {
        resizeObserver = new ResizeObserver(scheduleDrag);
        resizeObserver.observe(root);
        resizeObserver.observe(document.documentElement);
        for (const row of rows()) resizeObserver.observe(row);
        mutationObserver = new MutationObserver(records => {
            if (!drag?.active) return;
            if (!root.isConnected || !canReorder()) { clear(); return; }
            // Ignore our overlay writes; observe application changes to row content and layout.
            if (!records.some(record => record.target !== placeholder && record.target !== ghost)) return;
            for (const row of rows()) resizeObserver.observe(row);
            scheduleDrag();
        });
        mutationObserver.observe(root, { childList: true, characterData: true, subtree: true,
            attributes: true, attributeFilter: ['class', 'style', 'hidden', 'data-can-reorder'] });
        // Ancestor removal and surrounding content changes can move the drop target.
        mutationObserver.observe(document.documentElement, { childList: true, subtree: true });
    };
    const renderDrag = () => {
        frame = 0;
        if (!drag?.active) return;
        if (!canReorder() || !drag.row.isConnected) { clear(); return; }
        const items = rows();
        const before = insertionTarget(items, drag.y, drag.id);
        const left = `${Math.max(8, Math.min(innerWidth - ghost.offsetWidth - 8, drag.x + 12))}px`;
        const top = `${Math.max(8, Math.min(innerHeight - ghost.offsetHeight - 8, drag.y + 12))}px`;
        showTarget(before, drag.id, items);
        setStyle(ghost, 'left', left);
        setStyle(ghost, 'top', top);
        const scroll = drag.y < 48 ? -12 : drag.y > innerHeight - 48 ? 12 : 0;
        if (scroll) {
            const previous = window.scrollY;
            window.scrollBy(0, scroll);
            // Continue only while autoscroll moves. Scroll events also cover smooth scrolling.
            if (window.scrollY !== previous) scheduleDrag();
        }
    };
    listen(window, 'scroll', scheduleDrag, { capture: true, passive: true });
    listen(window, 'resize', scheduleDrag);
    listen(root, 'pointerdown', event => {
        const handle = event.target.closest?.('[data-file-drag-handle]');
        if (!handle || !root.contains(handle) || !canReorder() || pending || event.button !== 0) return;
        const row = handle.closest('[data-slot="file-drop-zone-file"]');
        if (!row) return;
        event.preventDefault();
        handle.focus({ preventScroll: true });
        drag = { id: row.dataset.fileId, row, handle, pointerId: event.pointerId, startX: event.clientX, startY: event.clientY, x: event.clientX, y: event.clientY, active: false };
        handle.setPointerCapture?.(event.pointerId);
    });
    listen(document, 'pointermove', event => {
        if (!drag || event.pointerId !== drag.pointerId) return;
        if (drag.x === event.clientX && drag.y === event.clientY) {
            if (drag.active) event.preventDefault();
            return;
        }
        drag.x = event.clientX; drag.y = event.clientY;
        if (!drag.active && Math.hypot(drag.x - drag.startX, drag.y - drag.startY) >= 6) {
            drag.active = true;
            drag.row.style.opacity = '0.35';
            drag.handle.setAttribute('aria-pressed', 'true');
            ghost.textContent = drag.handle.getAttribute('aria-label')?.replace(/^Reorder /, '') ?? 'File';
            ghost.style.width = `${Math.min(drag.row.getBoundingClientRect().width, 280)}px`;
            ghost.hidden = false;
            observeDragLayout();
        }
        if (drag.active) { event.preventDefault(); scheduleDrag(); }
    }, { passive: false });
    listen(document, 'pointerup', event => {
        if (!drag || event.pointerId !== drag.pointerId) return;
        const completed = drag;
        const rect = root.getBoundingClientRect();
        const inside = event.clientX >= rect.left && event.clientX <= rect.right && event.clientY >= rect.top && event.clientY <= rect.bottom;
        const before = insertionTarget(rows(), event.clientY, completed.id);
        clear();
        if (completed.active && inside) void move(completed.id, before);
    });
    listen(document, 'pointercancel', event => { if (drag?.pointerId === event.pointerId) clear(); });
    listen(window, 'blur', clear);
    listen(document, 'keydown', event => {
        if (event.key === 'Escape' && (drag || external)) { event.preventDefault(); clear(); return; }
        const handle = event.target.closest?.('[data-file-drag-handle]');
        if (!handle || !root.contains(handle) || !canReorder() || pending || drag || event.altKey || event.ctrlKey || event.metaKey) return;
        if (event.isComposing || (event.key !== 'ArrowUp' && event.key !== 'ArrowDown' && event.key !== 'Home' && event.key !== 'End')) return;
        const row = handle.closest('[data-slot="file-drop-zone-file"]');
        if (!row) return;
        event.preventDefault();
        void move(row.dataset.fileId, event.key, 'MoveFileByKey');
    });
    configureFileDrop(root, {
        onActive: active => {
            const target = root.querySelector('[data-slot="file-drop-zone-target"]');
            if (target) target.dataset.dragging = active ? 'true' : 'false';
            if (!active) clear();
        },
        onHover: event => {
            external = true;
            const items = rows();
            showTarget(insertionTarget(items, event.clientY), undefined, items);
        },
        beforeDrop: event => callback.invokeMethodAsync('SetDropTarget', insertionTarget(rows(), event.clientY))
    });
    registrations.set(owner, () => {
        clear();
        configureFileDrop(root, null);
        controller.abort();
        registrations.delete(owner);
    });
}

export function createPreview(owner, inputId, index, fileId, contentType) {
    const file = document.getElementById(inputId)?.files?.[index];
    if (!file || !(contentType.startsWith('image/') || contentType.startsWith('video/') || contentType === 'application/pdf')) return null;
    let urls = owners.get(owner);
    if (!urls) owners.set(owner, urls = new Map());
    if (urls.has(fileId)) URL.revokeObjectURL(urls.get(fileId));
    // Slice supplies a MIME type for files whose browser metadata is empty; it does not copy the bytes.
    const url = URL.createObjectURL(file.type === contentType ? file : file.slice(0, file.size, contentType));
    urls.set(fileId, url);
    return url;
}

export function retain(owner, fileIds) {
    const urls = owners.get(owner);
    if (!urls) return;
    const keep = new Set(fileIds);
    for (const [id, url] of urls) {
        if (keep.has(id)) continue;
        URL.revokeObjectURL(url);
        urls.delete(id);
    }
    if (!urls.size) owners.delete(owner);
}

export function dispose(owner) {
    registrations.get(owner)?.();
    retain(owner, []);
}
