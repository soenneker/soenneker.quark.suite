const registrations = new WeakMap();
const defaultOptions = {};
const behaviors = new WeakMap();

// Optional owner-specific positioning/visual hooks; registration and native file transfer stay here.
export function configure(target, options) {
    if (options) behaviors.set(target, options);
    else behaviors.delete(target);
}

// Shared by FileDrop and CodeEditor. Resolve the input on each event so Blazor may replace it.
export function register(target, inputId, options = defaultOptions) {
    const previous = registrations.get(target);
    if (previous?.inputId === inputId && previous.options === options) return;
    unregister(target);
    if (!target) return;

    let depth = 0, disposed = false, pending = false;
    const hooks = () => behaviors.get(target) ?? options;
    const input = () => document.getElementById(inputId);
    const enabled = () => target.dataset.fileDropDisabled !== 'true' && input() && !input().disabled;
    const hasFiles = event => Array.from(event.dataTransfer?.types ?? []).includes('Files');
    const active = value => {
        target.dataset.dragActive = value ? 'true' : 'false';
        hooks().onActive?.(value);
    };
    const reset = () => { depth = 0; active(false); };
    const consume = event => { event.preventDefault(); event.stopPropagation(); };
    const enter = event => {
        if (!hasFiles(event)) return;
        consume(event);
        if (!enabled()) { reset(); return; }
        depth++;
        active(true);
        hooks().onHover?.(event);
    };
    const over = event => {
        if (!hasFiles(event)) return;
        consume(event);
        event.dataTransfer.dropEffect = enabled() ? 'copy' : 'none';
        if (!enabled()) reset();
        else hooks().onHover?.(event);
    };
    const leave = event => {
        if (depth === 0) return;
        consume(event);
        depth = Math.max(0, depth - 1);
        if (depth === 0) active(false);
    };
    const drop = async event => {
        if (!hasFiles(event)) return;
        consume(event);
        reset();
        if (pending || !enabled() || !event.dataTransfer.files.length) return;
        // The browser protects DataTransfer after the event returns, so snapshot before awaiting .NET.
        const transfer = new DataTransfer();
        for (const file of event.dataTransfer.files) transfer.items.add(file);
        pending = true;
        try {
            const dropHooks = hooks();
            const preparation = dropHooks.beforeDrop?.(event);
            if (preparation?.then) await preparation;
            if (disposed || hooks() !== dropHooks || !enabled()) return;
            const fileInput = input();
            fileInput.value = '';
            fileInput.files = transfer.files;
            fileInput.dispatchEvent(new Event('change', { bubbles: true }));
        } catch (error) {
            if (!disposed) console.error('File drop failed.', error);
        } finally { pending = false; }
    };
    const handlers = { dragenter: enter, dragover: over, dragleave: leave, drop, dragend: reset };
    for (const [name, handler] of Object.entries(handlers)) target.addEventListener(name, handler, true);
    const observer = new MutationObserver(() => { if (!enabled()) reset(); });
    observer.observe(target, { attributes: true, attributeFilter: ['data-file-drop-disabled'] });
    registrations.set(target, { inputId, options, cleanup: () => {
        disposed = true;
        for (const [name, handler] of Object.entries(handlers)) target.removeEventListener(name, handler, true);
        observer.disconnect();
        reset();
        target.removeAttribute('data-drag-active');
    }});
}

export function unregister(target) {
    registrations.get(target)?.cleanup();
    registrations.delete(target);
}
