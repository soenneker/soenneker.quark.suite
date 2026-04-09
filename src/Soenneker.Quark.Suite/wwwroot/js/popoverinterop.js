const observers = new Map();
const viewportPadding = 8;

function clamp(value, min, max) {
    return Math.min(Math.max(value, min), max);
}

function computeHorizontalAlignment(align, triggerRect, contentRect) {
    if (align === "start") {
        return triggerRect.left;
    }

    if (align === "end") {
        return triggerRect.right - contentRect.width;
    }

    return triggerRect.left + (triggerRect.width / 2) - (contentRect.width / 2);
}

function computeVerticalAlignment(align, triggerRect, contentRect) {
    if (align === "start") {
        return triggerRect.top;
    }

    if (align === "end") {
        return triggerRect.bottom - contentRect.height;
    }

    return triggerRect.top + (triggerRect.height / 2) - (contentRect.height / 2);
}

function computeAvailableHeight(side, triggerRect, viewportHeight, padding, offset) {
    switch (side) {
        case "top":
            return triggerRect.top - padding - offset;
        case "left":
        case "right":
            return viewportHeight - (padding * 2);
        default:
            return viewportHeight - triggerRect.bottom - padding - offset;
    }
}

function resolveSide(side, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset) {
    const preferred = (side || "bottom").toLowerCase();
    const spaceAbove = triggerRect.top - padding;
    const spaceBelow = viewportHeight - triggerRect.bottom - padding;
    const spaceLeft = triggerRect.left - padding;
    const spaceRight = viewportWidth - triggerRect.right - padding;

    if (preferred === "bottom" && spaceBelow < contentRect.height + offset && spaceAbove > spaceBelow) {
        return "top";
    }

    if (preferred === "top" && spaceAbove < contentRect.height + offset && spaceBelow > spaceAbove) {
        return "bottom";
    }

    if (preferred === "right" && spaceRight < contentRect.width + offset && spaceLeft > spaceRight) {
        return "left";
    }

    if (preferred === "left" && spaceLeft < contentRect.width + offset && spaceRight > spaceLeft) {
        return "right";
    }

    return preferred;
}

function computePosition(side, align, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset) {
    let top = 0;
    let left = 0;
    const normalizedAlign = (align || "center").toLowerCase();

    switch (side) {
        case "top":
            top = triggerRect.top - contentRect.height - offset;
            left = computeHorizontalAlignment(normalizedAlign, triggerRect, contentRect);
            break;
        case "left":
            top = computeVerticalAlignment(normalizedAlign, triggerRect, contentRect);
            left = triggerRect.left - contentRect.width - offset;
            break;
        case "right":
            top = computeVerticalAlignment(normalizedAlign, triggerRect, contentRect);
            left = triggerRect.right + offset;
            break;
        default:
            top = triggerRect.bottom + offset;
            left = computeHorizontalAlignment(normalizedAlign, triggerRect, contentRect);
            break;
    }

    const maxLeft = Math.max(padding, viewportWidth - contentRect.width - padding);
    const maxTop = Math.max(padding, viewportHeight - contentRect.height - padding);

    return {
        top: clamp(top, padding, maxTop),
        left: clamp(left, padding, maxLeft)
    };
}

function adjustItemAligned(trigger, content, resolvedSide, sideOffset) {
    const viewport = content.querySelector('[data-slot="select-viewport"]');
    if (!viewport) {
        return;
    }

    const item =
        viewport.querySelector("[data-highlighted]") ||
        viewport.querySelector('[aria-selected="true"]') ||
        viewport.querySelector('[data-slot="select-item"]');

    if (!item) {
        return;
    }

    const offset = Number.isFinite(sideOffset) ? sideOffset : 4;
    const triggerRect = trigger.getBoundingClientRect();
    const itemRect = item.getBoundingClientRect();
    const normalizedSide = (resolvedSide || "bottom").toLowerCase();

    let delta = 0;
    if (normalizedSide === "bottom") {
        const desiredItemTop = triggerRect.bottom + offset;
        delta = itemRect.top - desiredItemTop;
    } else if (normalizedSide === "top") {
        const desiredItemBottom = triggerRect.top - offset;
        delta = itemRect.bottom - desiredItemBottom;
    } else {
        return;
    }

    const parsedTop = parseFloat(content.style.top);
    if (!Number.isFinite(parsedTop)) {
        return;
    }

    content.style.top = `${Math.round(parsedTop - delta)}px`;

    const contentRect = content.getBoundingClientRect();
    const padding = viewportPadding;
    const viewportWidth = window.innerWidth;
    const viewportHeight = window.innerHeight;
    const maxLeft = Math.max(padding, viewportWidth - contentRect.width - padding);
    const maxTop = Math.max(padding, viewportHeight - contentRect.height - padding);
    const nextTop = clamp(parseFloat(content.style.top), padding, maxTop);
    const nextLeft = clamp(parseFloat(content.style.left), padding, maxLeft);
    content.style.top = `${Math.round(nextTop)}px`;
    content.style.left = `${Math.round(nextLeft)}px`;
}

function updatePosition(trigger, content, side, align, sideOffset, positionMode) {
    if (!document.body.contains(trigger) || !document.body.contains(content)) {
        return;
    }

    const triggerRect = trigger.getBoundingClientRect();
    const contentRect = content.getBoundingClientRect();
    const padding = viewportPadding;
    const offset = Number.isFinite(sideOffset) ? sideOffset : 4;
    const viewportWidth = window.innerWidth;
    const viewportHeight = window.innerHeight;

    const resolvedSide = resolveSide(side, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset);
    const { top, left } = computePosition(resolvedSide, align, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset);
    const availableHeight = computeAvailableHeight(resolvedSide, triggerRect, viewportHeight, padding, offset);

    content.style.position = "fixed";
    content.style.top = `${Math.round(top)}px`;
    content.style.left = `${Math.round(left)}px`;
    content.style.visibility = "visible";
    content.style.setProperty("--quark-trigger-width", `${Math.round(triggerRect.width)}px`);
    content.style.setProperty("--radix-select-trigger-width", `${Math.round(triggerRect.width)}px`);
    content.style.setProperty("--radix-select-trigger-height", `${Math.round(triggerRect.height)}px`);
    content.style.setProperty("--quark-available-height", `${Math.max(0, Math.floor(availableHeight))}px`);
    content.style.setProperty("--radix-select-content-available-height", `${Math.max(0, Math.floor(availableHeight))}px`);
    content.dataset.side = resolvedSide;
    content.dataset.align = align || "center";

    const mode = positionMode || "popper";
    if (mode === "item-aligned") {
        window.requestAnimationFrame(() => {
            if (!document.body.contains(trigger) || !document.body.contains(content)) {
                return;
            }

            adjustItemAligned(trigger, content, resolvedSide, sideOffset);
        });
    }
}

export function observePosition(popoverId, trigger, content, callbackReference, side, align, sideOffset, positionMode) {
    if (!popoverId || !trigger || !content) {
        return;
    }

    stopObserving(popoverId);

    const mode = positionMode || "popper";

    const update = () => {
        updatePosition(trigger, content, side, align, sideOffset, mode);
    };

    const scheduleUpdate = () => {
        window.requestAnimationFrame(update);
    };

    const resizeObserver = typeof ResizeObserver === "function"
        ? new ResizeObserver(scheduleUpdate)
        : null;

    if (resizeObserver) {
        resizeObserver.observe(trigger);
        resizeObserver.observe(content);
    }

    window.addEventListener("resize", scheduleUpdate);
    window.addEventListener("scroll", scheduleUpdate, true);

    const handleInteractOutside = async (event) => {
        const target = event.target;

        if (!target) {
            return;
        }

        if (trigger.contains(target) || content.contains(target)) {
            return;
        }

        if (callbackReference) {
            await callbackReference.invokeMethodAsync("Close");
        }
    };

    document.addEventListener("pointerdown", handleInteractOutside, true);
    document.addEventListener("focusin", handleInteractOutside, true);

    observers.set(popoverId, {
        resizeObserver,
        scheduleUpdate,
        handleInteractOutside
    });

    scheduleUpdate();
}

export function nudgePosition(popoverId) {
    const observer = observers.get(popoverId);

    if (!observer || !observer.scheduleUpdate) {
        return;
    }

    observer.scheduleUpdate();
}

export function stopObserving(popoverId) {
    const observer = observers.get(popoverId);

    if (!observer) {
        return;
    }

    window.removeEventListener("resize", observer.scheduleUpdate);
    window.removeEventListener("scroll", observer.scheduleUpdate, true);
    document.removeEventListener("pointerdown", observer.handleInteractOutside, true);
    document.removeEventListener("focusin", observer.handleInteractOutside, true);

    if (observer.resizeObserver) {
        observer.resizeObserver.disconnect();
    }

    observers.delete(popoverId);
}
