const owners = new Map();
const registrations = new Map();

export function insertionTarget(rows, clientY, excludedId = null) {
    return rows.find(row => row.dataset.fileId !== excludedId && clientY < row.getBoundingClientRect().top + row.getBoundingClientRect().height / 2)?.dataset.fileId ?? null;
}

export function initialize(owner, root, callback) {
    registrations.get(owner)?.();
    if (!root) return;
    const controller = new AbortController();
    const placeholder = root.querySelector('[data-slot="file-drop-zone-placeholder"]');
    const ghost = root.querySelector('[data-slot="file-drop-zone-ghost"]');
    const rows = () => [...root.querySelectorAll('[data-slot="file-drop-zone-file"]')];
    const canSelect = () => root.dataset.canSelect === 'true';
    const canReorder = () => root.dataset.canReorder === 'true';
    let drag = null, frame = 0, pending = false, external = false;

    const listen = (target, name, fn, options = {}) => target.addEventListener(name, fn, { ...options, signal: controller.signal });
    const clear = () => {
        if (frame) cancelAnimationFrame(frame);
        frame = 0;
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
    const showTarget = (beforeId, excludedId) => {
        const items = rows().filter(row => row.dataset.fileId !== excludedId);
        const next = items.find(row => row.dataset.fileId === beforeId);
        const rect = root.getBoundingClientRect();
        const target = next?.getBoundingClientRect();
        const last = items.at(-1)?.getBoundingClientRect();
        const y = target ? target.top - 4 : last ? last.bottom + 4 : rect.bottom - 12;
        placeholder.style.left = '8px';
        placeholder.style.right = '8px';
        placeholder.style.top = `${y - rect.top - root.clientTop + root.scrollTop - 4}px`;
        placeholder.hidden = false;
    };
    const focusHandle = id => rows().find(row => row.dataset.fileId === id)?.querySelector('[data-file-drag-handle]')?.focus({ preventScroll: true });
    const move = async (id, before) => {
        if (pending || !canReorder()) return;
        pending = true;
        try {
            await callback.invokeMethodAsync('MoveFile', id, before);
            if (!controller.signal.aborted) focusHandle(id);
        } catch (error) {
            if (!controller.signal.aborted) console.error('File drop zone reorder failed.', error);
        } finally { pending = false; }
    };
    const renderDrag = () => {
        frame = 0;
        if (!drag?.active) return;
        if (!canReorder() || !drag.row.isConnected) { clear(); return; }
        drag.before = insertionTarget(rows(), drag.y, drag.id);
        showTarget(drag.before, drag.id);
        ghost.style.left = `${Math.max(8, Math.min(innerWidth - ghost.offsetWidth - 8, drag.x + 12))}px`;
        ghost.style.top = `${Math.max(8, Math.min(innerHeight - ghost.offsetHeight - 8, drag.y + 12))}px`;
        if (drag.y < 48) window.scrollBy(0, -12);
        else if (drag.y > innerHeight - 48) window.scrollBy(0, 12);
        frame = requestAnimationFrame(renderDrag);
    };
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
        drag.x = event.clientX; drag.y = event.clientY;
        if (!drag.active && Math.hypot(drag.x - drag.startX, drag.y - drag.startY) >= 6) {
            drag.active = true;
            drag.row.style.opacity = '0.35';
            drag.handle.setAttribute('aria-pressed', 'true');
            ghost.textContent = drag.handle.getAttribute('aria-label')?.replace(/^Reorder /, '') ?? 'File';
            ghost.style.width = `${Math.min(drag.row.getBoundingClientRect().width, 280)}px`;
            ghost.hidden = false;
            frame = requestAnimationFrame(renderDrag);
        }
        if (drag.active) event.preventDefault();
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
    listen(document, 'pointercancel', clear);
    listen(window, 'blur', clear);
    listen(document, 'keydown', event => {
        if (event.key === 'Escape' && (drag || external)) { event.preventDefault(); clear(); return; }
        const handle = event.target.closest?.('[data-file-drag-handle]');
        if (!handle || !root.contains(handle) || !canReorder() || pending || drag || event.altKey || event.ctrlKey || event.metaKey) return;
        const items = rows(), row = handle.closest('[data-slot="file-drop-zone-file"]'), index = items.indexOf(row);
        let target = index;
        if (event.key === 'ArrowUp') target = Math.max(0, index - 1);
        else if (event.key === 'ArrowDown') target = Math.min(items.length - 1, index + 1);
        else if (event.key === 'Home') target = 0;
        else if (event.key === 'End') target = items.length - 1;
        else return;
        event.preventDefault();
        if (target === index || index < 0) return;
        const remaining = items.filter(item => item !== row);
        void move(row.dataset.fileId, remaining[target]?.dataset.fileId ?? null);
    });
    const isFiles = event => [...(event.dataTransfer?.types ?? [])].includes('Files');
    const hover = event => {
        if (!isFiles(event)) return;
        event.preventDefault();
        if (!canSelect()) { clear(); event.dataTransfer.dropEffect = 'none'; return; }
        external = true;
        event.dataTransfer.dropEffect = 'copy';
        showTarget(insertionTarget(rows(), event.clientY));
    };
    listen(root, 'dragenter', hover);
    listen(root, 'dragover', hover);
    listen(root, 'dragleave', event => { if (!root.contains(event.relatedTarget)) clear(); });
    listen(document, 'dragend', clear);
    listen(root, 'drop', async event => {
        if (!isFiles(event)) return;
        event.preventDefault();
        event.stopPropagation();
        const allowed = canSelect();
        const before = insertionTarget(rows(), event.clientY);
        const transfer = new DataTransfer();
        if (allowed) for (const file of event.dataTransfer.files) transfer.items.add(file);
        clear();
        if (!allowed || !transfer.files.length) return;
        try {
            await callback.invokeMethodAsync('SetDropTarget', before);
            if (controller.signal.aborted || !canSelect()) return;
            const input = root.querySelector('input[type="file"]:not([disabled])');
            if (!input) return;
            input.files = transfer.files;
            input.dispatchEvent(new Event('change', { bubbles: true }));
        } catch (error) {
            if (!controller.signal.aborted) console.error('File drop zone drop failed.', error);
        }
    }, { capture: true });
    registrations.set(owner, () => { clear(); controller.abort(); registrations.delete(owner); });
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
