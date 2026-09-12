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
const scrollLockOwners = new Set();
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

function findFocusableElement(elements, fromEnd = false) {
    for (let index = fromEnd ? elements.length - 1 : 0;
        fromEnd ? index >= 0 : index < elements.length;
        index += fromEnd ? -1 : 1) {
        if (isFocusable(elements[index])) {
            return elements[index];
        }
    }
    return null;
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

    return findFocusableElement(container.querySelectorAll(focusableSelectors)) ?? container;
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
    const handleKeyDown = (event) => {
        if (event.key !== 'Tab') {
            return;
        }

        if (overlayStack[overlayStack.length - 1] !== overlayId) {
            return;
        }

        const candidates = container.querySelectorAll(focusableSelectors);
        const firstElement = findFocusableElement(candidates);

        if (!firstElement) {
            event.preventDefault();
            container.focus();
            return;
        }

        const lastElement = findFocusableElement(candidates, true);
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

function lockBodyScroll(overlayId) {
    if (scrollLockOwners.has(overlayId)) {
        return;
    }

    if (scrollLockOwners.size === 0) {
        const scrollbarWidth = window.innerWidth - document.documentElement.clientWidth;
        originalBodyOverflow = document.body.style.overflow;
        originalBodyPaddingRight = document.body.style.paddingRight;

        document.body.style.overflow = 'hidden';

        if (scrollbarWidth > 0) {
            document.body.style.paddingRight = `${scrollbarWidth}px`;
        }
    }

    scrollLockOwners.add(overlayId);
}

function unlockBodyScroll(overlayId) {
    if (!scrollLockOwners.delete(overlayId)) {
        return;
    }

    if (scrollLockOwners.size === 0) {
        document.body.style.overflow = originalBodyOverflow;
        document.body.style.paddingRight = originalBodyPaddingRight;
    }
}

export function activate(overlayId, container, trapFocus, lockScroll, initialFocusSelector) {
    if (!overlayId) {
        return;
    }

    removeOverlay(overlayId);
    overlayStack.push(overlayId);

    if (lockScroll) {
        lockBodyScroll(overlayId);
    } else {
        unlockBodyScroll(overlayId);
    }

    disposeFocusTrap(overlayId);

    if (!container) {
        return;
    }

    if (trapFocus) {
        createFocusTrap(overlayId, container);
    }

    focusInitial(container, initialFocusSelector);
}

export function activateScrollLock(overlayId) {
    if (!overlayId) {
        return;
    }

    removeOverlay(overlayId);
    overlayStack.push(overlayId);

    lockBodyScroll(overlayId);
}

export function deactivate(overlayId, unlockScroll) {
    if (!overlayId) {
        return;
    }

    disposeFocusTrap(overlayId);
    removeOverlay(overlayId);

    if (unlockScroll) {
        unlockBodyScroll(overlayId);
    }
}

export function releaseScrollLocks() {
    traps.forEach((trap, overlayId) => disposeFocusTrap(overlayId));
    overlayStack.length = 0;

    if (scrollLockOwners.size > 0) {
        scrollLockOwners.clear();
        document.body.style.overflow = originalBodyOverflow;
        document.body.style.paddingRight = originalBodyPaddingRight;
    }
}
