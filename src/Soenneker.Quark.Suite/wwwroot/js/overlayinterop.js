class OverlayInterop {
    constructor() {
        this.focusableSelectors = [
            'a[href]',
            'button:not([disabled])',
            'input:not([disabled])',
            'textarea:not([disabled])',
            'select:not([disabled])',
            '[tabindex]:not([tabindex="-1"])',
            'audio[controls]',
            'video[controls]'
        ].join(', ');

        this._traps = new Map();
        this._overlayStack = [];
        this._scrollLockCount = 0;
        this._originalBodyOverflow = '';
        this._originalBodyPaddingRight = '';
    }

    activate(overlayId, container, trapFocus, lockScroll, initialFocusSelector) {
        if (!overlayId || !container) {
            return;
        }

        const alreadyActive = this._overlayStack.includes(overlayId);
        this._removeOverlay(overlayId);
        this._overlayStack.push(overlayId);

        if (trapFocus) {
            this._createFocusTrap(overlayId, container);
        }

        if (lockScroll && !alreadyActive) {
            this._lockBodyScroll();
        }

        this._focusInitial(container, initialFocusSelector);
    }

    deactivate(overlayId, unlockScroll) {
        if (!overlayId) {
            return;
        }

        const wasActive = this._overlayStack.includes(overlayId);

        this._disposeFocusTrap(overlayId);
        this._removeOverlay(overlayId);

        if (unlockScroll && wasActive) {
            this._unlockBodyScroll();
        }
    }

    _createFocusTrap(overlayId, container) {
        this._disposeFocusTrap(overlayId);

        const handleKeyDown = (event) => {
            if (event.key !== 'Tab') {
                return;
            }

            if (this._overlayStack[this._overlayStack.length - 1] !== overlayId) {
                return;
            }

            const focusableElements = this._getFocusableElements(container);

            if (focusableElements.length === 0) {
                event.preventDefault();
                container.focus();
                return;
            }

            const firstElement = focusableElements[0];
            const lastElement = focusableElements[focusableElements.length - 1];
            const activeElement = document.activeElement;

            if (event.shiftKey) {
                if (activeElement === firstElement || !container.contains(activeElement)) {
                    event.preventDefault();
                    lastElement.focus();
                }

                return;
            }

            if (activeElement === lastElement) {
                event.preventDefault();
                firstElement.focus();
            }
        };

        container.addEventListener('keydown', handleKeyDown);
        this._traps.set(overlayId, { container, handleKeyDown });
    }

    _disposeFocusTrap(overlayId) {
        const trap = this._traps.get(overlayId);

        if (!trap) {
            return;
        }

        trap.container.removeEventListener('keydown', trap.handleKeyDown);
        this._traps.delete(overlayId);
    }

    _focusInitial(container, initialFocusSelector) {
        const target = this._resolveInitialFocusTarget(container, initialFocusSelector);

        if (target) {
            target.focus();
        } else {
            container.focus();
        }
    }

    _resolveInitialFocusTarget(container, initialFocusSelector) {
        if (initialFocusSelector) {
            try {
                const explicitTarget = container.querySelector(initialFocusSelector);

                if (this._isFocusable(explicitTarget)) {
                    return explicitTarget;
                }
            } catch {
            }
        }

        const autoFocusTarget = container.querySelector('[data-autofocus], [autofocus]');

        if (this._isFocusable(autoFocusTarget)) {
            return autoFocusTarget;
        }

        const focusableElements = this._getFocusableElements(container);
        return focusableElements.length > 0 ? focusableElements[0] : container;
    }

    _getFocusableElements(container) {
        return Array.from(container.querySelectorAll(this.focusableSelectors))
            .filter((element) => this._isFocusable(element));
    }

    _isFocusable(element) {
        if (!element || typeof element.focus !== 'function') {
            return false;
        }

        if (element.hasAttribute('disabled')) {
            return false;
        }

        if (element.getAttribute('aria-hidden') === 'true') {
            return false;
        }

        const style = window.getComputedStyle(element);
        return style.display !== 'none' && style.visibility !== 'hidden';
    }

    _lockBodyScroll() {
        if (this._scrollLockCount === 0) {
            const scrollbarWidth = window.innerWidth - document.documentElement.clientWidth;
            this._originalBodyOverflow = document.body.style.overflow;
            this._originalBodyPaddingRight = document.body.style.paddingRight;

            document.body.style.overflow = 'hidden';

            if (scrollbarWidth > 0) {
                document.body.style.paddingRight = `${scrollbarWidth}px`;
            }
        }

        this._scrollLockCount++;
    }

    _unlockBodyScroll() {
        if (this._scrollLockCount === 0) {
            return;
        }

        this._scrollLockCount--;

        if (this._scrollLockCount === 0) {
            document.body.style.overflow = this._originalBodyOverflow;
            document.body.style.paddingRight = this._originalBodyPaddingRight;
        }
    }

    _removeOverlay(overlayId) {
        const index = this._overlayStack.lastIndexOf(overlayId);

        if (index !== -1) {
            this._overlayStack.splice(index, 1);
        }
    }
}

window.OverlayInterop = new OverlayInterop();
