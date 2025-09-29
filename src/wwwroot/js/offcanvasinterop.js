export class OffcanvasInterop {
    constructor() {
        this._overlayCount = 0;
        this._cleanups = new Map(); // elementId -> { observer, entered }
        this._bodyClass = 'offcanvas-open';
    }

    create(elementId) {
        this._cleanups.set(elementId, {});
        this._createObserver(elementId);
    }

    open(elementId) {
        this._enter();
        const entry = this._cleanups.get(elementId) ?? {};
        entry.entered = true;
        this._cleanups.set(elementId, entry);
    }

    close(elementId) {
        const entry = this._cleanups.get(elementId);
        if (entry) entry.entered = false;
        this._exit();
    }

    dispose(elementId) {
        const entry = this._cleanups.get(elementId);
        if (entry?.observer) entry.observer.disconnect();

        // If it was left "entered", make sure to decrement
        if (entry?.entered) {
            this._overlayCount = Math.max(this._overlayCount - 1, 0);
            if (this._overlayCount === 0) {
                document.body.classList.remove(this._bodyClass);
            }
        }

        this._cleanups.delete(elementId);
    }

    _enter() {
        this._overlayCount++;
        document.body.classList.add(this._bodyClass);
    }

    _exit() {
        this._overlayCount = Math.max(this._overlayCount - 1, 0);
        if (this._overlayCount === 0) {
            document.body.classList.remove(this._bodyClass);
        }
    }

    _createObserver(elementId) {
        const target = document.getElementById(elementId);
        const entry = this._cleanups.get(elementId);
        if (!target || !target.parentNode || !entry) return;

        const observer = new MutationObserver((mutations) => {
            const removed = mutations.some(m => [...m.removedNodes].includes(target));
            if (removed) {
                this.dispose(elementId);
                observer.disconnect();
            }
        });

        observer.observe(target.parentNode, { childList: true });
        entry.observer = observer;
        this._cleanups.set(elementId, entry);
    }
}

window.OffcanvasInterop = new OffcanvasInterop();