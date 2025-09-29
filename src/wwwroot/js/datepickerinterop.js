export class DatePickerInterop {
    constructor() {
        this._handlers = new Map(); // containerId -> handler
    }

    attach(element) {
        if (!element) return;
        element.setAttribute('autocomplete', 'off');
    }

    registerOutsideClose(container, dotnet, method) {
        if (!dotnet || !method) return;

        const handler = (ev) => {
            const target = ev.target;
            if (target && target.closest && (target.closest('.quark-calendar-panel, .quark-date-container') || (container && container.id && target.closest(`#${container.id}`)))) {
                return;
            }
            try {
                dotnet.invokeMethodAsync(method);
            } catch (_) {
                // Swallow exceptions from .NET invocation
            }
        };

        document.addEventListener('mousedown', handler, true);

        const containerId = container?.id;
        if (containerId) {
            this._handlers.set(containerId, handler);
            this._createObserver(containerId, container);
        }
    }

    disposeOutsideClose(containerId) {
        const handler = this._handlers.get(containerId);
        if (handler) {
            document.removeEventListener('mousedown', handler, true);
        }
        this._handlers.delete(containerId);
    }

    _createObserver(containerId, containerEl) {
        if (!containerEl || !containerEl.parentNode) return;

        const observer = new MutationObserver((mutations) => {
            const removed = mutations.some(m => [...m.removedNodes].includes(containerEl));
            if (removed) {
                this.disposeOutsideClose(containerId);
                observer.disconnect();
            }
        });

        observer.observe(containerEl.parentNode, { childList: true });
    }
}

window.DatePickerInterop = new DatePickerInterop();


