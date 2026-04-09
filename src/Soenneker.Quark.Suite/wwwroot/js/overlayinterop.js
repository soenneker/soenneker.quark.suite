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
const escapeHandlers = new Map();
/** @type {Map<string, Element>} overlay content roots (for Escape deferral to nested floating layers) */
const overlayContainers = new Map();
const overlayStack = [];
let modalAccessibilityRestore = null;
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

/** Layers that handle Escape before the modal (must stay in sync with Quark data-slot / data-state). */
const openFloatingLayerSelector = [
    '[data-slot="popover-content"][data-state="open"]',
    '[data-slot="select-content"][data-state="open"]',
    '[data-slot="dropdown-menu-content"][data-state="open"]',
    '[data-slot="dropdown-menu-sub-content"][data-state="open"]',
    '[data-slot="combobox-content"][data-state="open"]',
    '[data-slot="hover-card-content"][data-state="open"]',
    '[data-slot="context-menu-content"][data-state="open"]',
    '[data-slot="context-menu-sub-content"][data-state="open"]',
    '[data-slot="menubar-content"][data-state="open"]',
    '[data-slot="menubar-sub-content"][data-state="open"]',
    '[data-slot="navigation-menu-viewport"][data-state="open"]',
    '[data-slot="navigation-menu-content"][data-state="open"]',
    '[data-slot="datepicker-content"][data-state="open"]',
    '[data-slot="tooltip-content"][data-state="open"]',
    '[data-slot="dialog-content"][data-state="open"]',
    '[data-slot="alert-dialog-content"][data-state="open"]',
    '[data-slot="sheet-content"][data-state="open"]',
    '[data-slot="drawer-content"][data-state="open"]'
].join(',');

function hasOpenFloatingDescendant(modalRoot) {
    if (!modalRoot || typeof modalRoot.querySelector !== 'function') {
        return false;
    }

    return !!modalRoot.querySelector(openFloatingLayerSelector);
}

function hideSiblingsOfPortalHost() {
    const host = document.getElementById('quark-overlay-portal-host');

    if (!host?.parentElement) {
        return () => {};
    }

    const parent = host.parentElement;
    const hidden = [];

    for (const child of parent.children) {
        if (child === host) {
            continue;
        }

        const prevAria = child.getAttribute('aria-hidden');
        const hadInert = 'inert' in child && child.inert === true;

        child.setAttribute('data-q-modal-hidden', '');
        child.setAttribute('aria-hidden', 'true');

        if ('inert' in child) {
            child.inert = true;
        }

        hidden.push({ node: child, prevAria, hadInert });
    }

    return () => {
        for (const { node, prevAria, hadInert } of hidden) {
            node.removeAttribute('data-q-modal-hidden');

            if (prevAria === null) {
                node.removeAttribute('aria-hidden');
            }
            else {
                node.setAttribute('aria-hidden', prevAria);
            }

            if ('inert' in node) {
                node.inert = hadInert;
            }
        }
    };
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

export function activate(overlayId, container, trapFocus, lockScroll, initialFocusSelector, dotNetRef) {
    if (!overlayId || !container) {
        return;
    }

    overlayContainers.set(overlayId, container);

    const existingEscape = escapeHandlers.get(overlayId);
    if (existingEscape) {
        document.removeEventListener('keydown', existingEscape, true);
        escapeHandlers.delete(overlayId);
    }

    const alreadyActive = overlayStack.includes(overlayId);
    const wasStackEmpty = overlayStack.length === 0;
    removeOverlay(overlayId);
    overlayStack.push(overlayId);

    if (trapFocus) {
        createFocusTrap(overlayId, container);
    }

    if (lockScroll && !alreadyActive) {
        lockBodyScroll();
    }

    if (wasStackEmpty) {
        modalAccessibilityRestore = hideSiblingsOfPortalHost();
    }

    if (dotNetRef) {
        const escapeHandler = async (ev) => {
            if (ev.key !== 'Escape') {
                return;
            }

            if (overlayStack[overlayStack.length - 1] !== overlayId) {
                return;
            }

            const modalRoot = overlayContainers.get(overlayId);
            if (modalRoot && hasOpenFloatingDescendant(modalRoot)) {
                return;
            }

            try {
                const handled = await dotNetRef.invokeMethodAsync('OnEscapeKeyAsync');
                if (handled) {
                    ev.preventDefault();
                    ev.stopPropagation();
                }
            } catch {
            }
        };

        document.addEventListener('keydown', escapeHandler, true);
        escapeHandlers.set(overlayId, escapeHandler);
    }

    focusInitial(container, initialFocusSelector);
}

export function deactivate(overlayId, unlockScroll) {
    if (!overlayId) {
        return;
    }

    const wasActive = overlayStack.includes(overlayId);

    overlayContainers.delete(overlayId);

    const escapeHandler = escapeHandlers.get(overlayId);
    if (escapeHandler) {
        document.removeEventListener('keydown', escapeHandler, true);
        escapeHandlers.delete(overlayId);
    }

    disposeFocusTrap(overlayId);
    removeOverlay(overlayId);

    if (overlayStack.length === 0 && modalAccessibilityRestore) {
        modalAccessibilityRestore();
        modalAccessibilityRestore = null;
    }

    if (unlockScroll && wasActive) {
        unlockBodyScroll();
    }
}

/** Bubble-phase Escape → C# closes open tooltips (works with hover-only tooltips when modal capture defers first). */
const tooltipEscapeStack = [];

function onTooltipEscapeBubble(ev) {
    if (ev.key !== 'Escape' || tooltipEscapeStack.length === 0) {
        return;
    }

    const top = tooltipEscapeStack[tooltipEscapeStack.length - 1];
    void top.invokeMethodAsync('OnTooltipDocumentEscape').catch(() => {});
}

let tooltipEscapeBubbleAttached = false;

export function registerTooltipEscape(dotNetRef) {
    tooltipEscapeStack.push(dotNetRef);

    if (!tooltipEscapeBubbleAttached) {
        tooltipEscapeBubbleAttached = true;
        document.addEventListener('keydown', onTooltipEscapeBubble, false);
    }
}

export function unregisterTooltipEscape() {
    tooltipEscapeStack.pop();
}
