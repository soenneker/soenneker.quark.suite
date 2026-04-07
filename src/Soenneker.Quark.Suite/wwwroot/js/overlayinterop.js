const focusableSelectors = [
    'a[href]',
    'button:not([disabled])',
    'input:not([disabled])',
    'textarea:not([disabled])',
    'select:not([disabled])',
    '[tabindex]:not([tabindex="-1"])',
    'audio[controls]',
    'video[controls]'
].join(', ');

const traps = new Map();
const overlayStack = [];
let scrollLockCount = 0;
let originalBodyOverflow = '';
let originalBodyPaddingRight = '';

function removeOverlay(overlayId) {
    const index = overlayStack.lastIndexOf(overlayId);

    if (index !== -1) {
        overlayStack.splice(index, 1);
    }
}

function isFocusable(element) {
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

function getFocusableElements(container) {
    return Array.from(container.querySelectorAll(focusableSelectors))
        .filter((element) => isFocusable(element));
}

function resolveInitialFocusTarget(container, initialFocusSelector) {
    if (initialFocusSelector) {
        try {
            const explicitTarget = container.querySelector(initialFocusSelector);

            if (isFocusable(explicitTarget)) {
                return explicitTarget;
            }
        } catch {
        }
    }

    const autoFocusTarget = container.querySelector('[data-autofocus], [autofocus]');

    if (isFocusable(autoFocusTarget)) {
        return autoFocusTarget;
    }

    const focusableElements = getFocusableElements(container);
    return focusableElements.length > 0 ? focusableElements[0] : container;
}

function focusInitial(container, initialFocusSelector) {
    const target = resolveInitialFocusTarget(container, initialFocusSelector);

    if (target) {
        target.focus();
    } else {
        container.focus();
    }
}

function disposeFocusTrap(overlayId) {
    const trap = traps.get(overlayId);

    if (!trap) {
        return;
    }

    trap.container.removeEventListener('keydown', trap.handleKeyDown);
    traps.delete(overlayId);
}

function createFocusTrap(overlayId, container) {
    disposeFocusTrap(overlayId);

    const handleKeyDown = (event) => {
        if (event.key !== 'Tab') {
            return;
        }

        if (overlayStack[overlayStack.length - 1] !== overlayId) {
            return;
        }

        const focusableElements = getFocusableElements(container);

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
    traps.set(overlayId, { container, handleKeyDown });
}

function lockBodyScroll() {
    if (scrollLockCount === 0) {
        const scrollbarWidth = window.innerWidth - document.documentElement.clientWidth;
        originalBodyOverflow = document.body.style.overflow;
        originalBodyPaddingRight = document.body.style.paddingRight;

        document.body.style.overflow = 'hidden';

        if (scrollbarWidth > 0) {
            document.body.style.paddingRight = `${scrollbarWidth}px`;
        }
    }

    scrollLockCount++;
}

function unlockBodyScroll() {
    if (scrollLockCount === 0) {
        return;
    }

    scrollLockCount--;

    if (scrollLockCount === 0) {
        document.body.style.overflow = originalBodyOverflow;
        document.body.style.paddingRight = originalBodyPaddingRight;
    }
}

export function activate(overlayId, container, trapFocus, lockScroll, initialFocusSelector) {
    if (!overlayId || !container) {
        return;
    }

    const alreadyActive = overlayStack.includes(overlayId);
    removeOverlay(overlayId);
    overlayStack.push(overlayId);

    if (trapFocus) {
        createFocusTrap(overlayId, container);
    }

    if (lockScroll && !alreadyActive) {
        lockBodyScroll();
    }

    focusInitial(container, initialFocusSelector);
}

export function deactivate(overlayId, unlockScroll) {
    if (!overlayId) {
        return;
    }

    const wasActive = overlayStack.includes(overlayId);

    disposeFocusTrap(overlayId);
    removeOverlay(overlayId);

    if (unlockScroll && wasActive) {
        unlockBodyScroll();
    }
}
