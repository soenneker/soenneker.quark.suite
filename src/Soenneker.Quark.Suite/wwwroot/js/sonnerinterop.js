const hotkeyRegistrations = new WeakMap();
const swipeRegistrations = new WeakMap();
const swipeThreshold = 80;
const swipeIntentThreshold = 5;

export function measureToastHeights(section) {
    const heights = {};

    if (!section) {
        return heights;
    }

    const toasts = section.querySelectorAll("[data-sonner-toast][data-toast-id]");

    for (const toast of toasts) {
        const id = toast.getAttribute("data-toast-id");

        if (!id) {
            continue;
        }

        const originalHeight = toast.style.height;

        try {
            // Match Sonner's DOMRect measurement while temporarily ignoring animated/stacked height.
            toast.style.height = "auto";
            heights[id] = toast.getBoundingClientRect().height;
        } finally {
            toast.style.height = originalHeight;
        }
    }

    return heights;
}

export function registerHotkey(section, hotkey) {
    if (!section) {
        return;
    }

    unregisterHotkey(section);

    const keys = Array.isArray(hotkey) ? hotkey.map(String).filter(Boolean) : [];

    if (keys.length === 0) {
        return;
    }

    const handler = event => {
        if (!matchesHotkey(event, keys)) {
            return;
        }

        const toast = section.querySelector("[data-sonner-toast][data-visible='true']");

        if (!toast) {
            return;
        }

        event.preventDefault();
        toast.focus({ preventScroll: true });
    };

    document.addEventListener("keydown", handler);
    hotkeyRegistrations.set(section, handler);
}

export function unregisterHotkey(section) {
    if (!section) {
        return;
    }

    const handler = hotkeyRegistrations.get(section);

    if (!handler) {
        return;
    }

    document.removeEventListener("keydown", handler);
    hotkeyRegistrations.delete(section);
}

export function registerSwipeHandlers(section, callbackReference) {
    if (!section) {
        return;
    }

    unregisterSwipeHandlers(section);

    let activeSwipe = null;

    const pointerDown = event => {
        if (event.button !== 0 || event.defaultPrevented) {
            return;
        }

        const toast = event.target?.closest?.("[data-sonner-toast][data-toast-id]");

        if (!toast || !section.contains(toast) || toast.getAttribute("data-dismissible") !== "true") {
            return;
        }

        if (event.target?.closest?.("button, a, input, textarea, select, [data-close-button], [data-button]")) {
            return;
        }

        activeSwipe = {
            toast,
            pointerId: event.pointerId,
            startX: event.clientX,
            startY: event.clientY,
            started: false
        };

        try {
            toast.setPointerCapture(event.pointerId);
        } catch {
        }
    };

    const pointerMove = event => {
        if (!activeSwipe || activeSwipe.pointerId !== event.pointerId) {
            return;
        }

        const deltaX = event.clientX - activeSwipe.startX;
        const deltaY = event.clientY - activeSwipe.startY;

        if (!activeSwipe.started) {
            if (Math.abs(deltaX) < swipeIntentThreshold || Math.abs(deltaX) < Math.abs(deltaY)) {
                return;
            }

            activeSwipe.started = true;
            activeSwipe.toast.setAttribute("data-swiping", "true");
            activeSwipe.toast.setAttribute("data-swiped", "true");
        }

        event.preventDefault();
        activeSwipe.toast.style.setProperty("--swipe-amount", `${deltaX}px`);
    };

    const pointerEnd = event => {
        if (!activeSwipe || activeSwipe.pointerId !== event.pointerId) {
            return;
        }

        const swipe = activeSwipe;
        activeSwipe = null;

        try {
            swipe.toast.releasePointerCapture(event.pointerId);
        } catch {
        }

        const deltaX = event.clientX - swipe.startX;
        const shouldDismiss = swipe.started && passedSwipeThreshold(swipe.toast, deltaX);

        swipe.toast.setAttribute("data-swiping", "false");

        if (!shouldDismiss) {
            resetSwipeState(swipe.toast);
            return;
        }

        const width = swipe.toast.getBoundingClientRect().width || 356;
        const direction = deltaX < 0 ? -1 : 1;
        const toastId = swipe.toast.getAttribute("data-toast-id");

        swipe.toast.setAttribute("data-swiped", "true");
        swipe.toast.setAttribute("data-swipe-out", "true");
        swipe.toast.style.setProperty("--swipe-out-amount", `${direction * (width + 48)}px`);

        if (toastId) {
            void callbackReference.invokeMethodAsync("DismissToastFromSwipe", toastId);
        }
    };

    const pointerCancel = event => {
        if (!activeSwipe || activeSwipe.pointerId !== event.pointerId) {
            return;
        }

        const toast = activeSwipe.toast;
        activeSwipe = null;
        resetSwipeState(toast);
    };

    section.addEventListener("pointerdown", pointerDown);
    section.addEventListener("pointermove", pointerMove);
    section.addEventListener("pointerup", pointerEnd);
    section.addEventListener("pointercancel", pointerCancel);
    section.addEventListener("lostpointercapture", pointerCancel);
    section.__quarkSonnerSwipeRegistered = true;

    swipeRegistrations.set(section, {
        pointerDown,
        pointerMove,
        pointerEnd,
        pointerCancel
    });
}

export function unregisterSwipeHandlers(section) {
    if (!section) {
        return;
    }

    const registration = swipeRegistrations.get(section);

    if (!registration) {
        return;
    }

    section.removeEventListener("pointerdown", registration.pointerDown);
    section.removeEventListener("pointermove", registration.pointerMove);
    section.removeEventListener("pointerup", registration.pointerEnd);
    section.removeEventListener("pointercancel", registration.pointerCancel);
    section.removeEventListener("lostpointercapture", registration.pointerCancel);
    delete section.__quarkSonnerSwipeRegistered;
    swipeRegistrations.delete(section);
}

function passedSwipeThreshold(toast, deltaX) {
    const xPosition = toast.getAttribute("data-x-position");

    if (xPosition === "left") {
        return deltaX <= -swipeThreshold;
    }

    if (xPosition === "right") {
        return deltaX >= swipeThreshold;
    }

    return Math.abs(deltaX) >= swipeThreshold;
}

function resetSwipeState(toast) {
    toast.setAttribute("data-swiping", "false");
    toast.setAttribute("data-swiped", "false");
    toast.setAttribute("data-swipe-out", "false");
    toast.style.removeProperty("--swipe-amount");
    toast.style.removeProperty("--swipe-out-amount");
}

function matchesHotkey(event, hotkey) {
    for (const token of hotkey) {
        switch (token) {
            case "altKey":
                if (!event.altKey) {
                    return false;
                }
                continue;
            case "ctrlKey":
                if (!event.ctrlKey) {
                    return false;
                }
                continue;
            case "metaKey":
                if (!event.metaKey) {
                    return false;
                }
                continue;
            case "shiftKey":
                if (!event.shiftKey) {
                    return false;
                }
                continue;
            default:
                if (!matchesKeyOrCode(event, token)) {
                    return false;
                }
        }
    }

    return true;
}

function matchesKeyOrCode(event, token) {
    const normalizedToken = token.toLowerCase();
    return event.code.toLowerCase() === normalizedToken || event.key.toLowerCase() === normalizedToken;
}
