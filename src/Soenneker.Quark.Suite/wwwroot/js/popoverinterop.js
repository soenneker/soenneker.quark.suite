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

function updatePosition(trigger, content, side, align, sideOffset) {
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
    content.style.setProperty("--quark-available-height", `${Math.max(0, Math.floor(availableHeight))}px`);
    content.dataset.side = resolvedSide;
    content.dataset.align = align || "center";
}

export function observePosition(popoverId, trigger, content, side, align, sideOffset) {
    if (!popoverId || !trigger || !content) {
        return;
    }

    stopObserving(popoverId);

    const update = () => {
        updatePosition(trigger, content, side, align, sideOffset);
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

    observers.set(popoverId, {
        resizeObserver,
        scheduleUpdate
    });

    scheduleUpdate();
}

export function stopObserving(popoverId) {
    const observer = observers.get(popoverId);

    if (!observer) {
        return;
    }

    window.removeEventListener("resize", observer.scheduleUpdate);
    window.removeEventListener("scroll", observer.scheduleUpdate, true);

    if (observer.resizeObserver) {
        observer.resizeObserver.disconnect();
    }

    observers.delete(popoverId);
}
