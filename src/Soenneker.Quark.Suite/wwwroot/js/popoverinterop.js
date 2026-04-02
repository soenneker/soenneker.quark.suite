class PopoverInterop {
    constructor() {
        this._observers = new Map();
        this._viewportPadding = 8;
    }

    observePosition(popoverId, trigger, content, side, align, sideOffset) {
        if (!popoverId || !trigger || !content) {
            return;
        }

        this.stopObserving(popoverId);

        const update = () => {
            this._updatePosition(trigger, content, side, align, sideOffset);
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

        this._observers.set(popoverId, {
            resizeObserver,
            scheduleUpdate
        });

        scheduleUpdate();
    }

    stopObserving(popoverId) {
        const observer = this._observers.get(popoverId);

        if (!observer) {
            return;
        }

        window.removeEventListener("resize", observer.scheduleUpdate);
        window.removeEventListener("scroll", observer.scheduleUpdate, true);

        if (observer.resizeObserver) {
            observer.resizeObserver.disconnect();
        }

        this._observers.delete(popoverId);
    }

    _updatePosition(trigger, content, side, align, sideOffset) {
        if (!document.body.contains(trigger) || !document.body.contains(content)) {
            return;
        }

        const triggerRect = trigger.getBoundingClientRect();
        const contentRect = content.getBoundingClientRect();
        const padding = this._viewportPadding;
        const offset = Number.isFinite(sideOffset) ? sideOffset : 4;
        const viewportWidth = window.innerWidth;
        const viewportHeight = window.innerHeight;

        const resolvedSide = this._resolveSide(side, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset);
        const { top, left } = this._computePosition(resolvedSide, align, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset);
        const availableHeight = this._computeAvailableHeight(resolvedSide, triggerRect, viewportHeight, padding, offset);

        content.style.position = "fixed";
        content.style.top = `${Math.round(top)}px`;
        content.style.left = `${Math.round(left)}px`;
        content.style.visibility = "visible";
        content.style.setProperty("--quark-trigger-width", `${Math.round(triggerRect.width)}px`);
        content.style.setProperty("--quark-available-height", `${Math.max(0, Math.floor(availableHeight))}px`);
        content.dataset.side = resolvedSide;
        content.dataset.align = align || "center";
    }

    _computeAvailableHeight(side, triggerRect, viewportHeight, padding, offset) {
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

    _resolveSide(side, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset) {
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

    _computePosition(side, align, triggerRect, contentRect, viewportWidth, viewportHeight, padding, offset) {
        let top = 0;
        let left = 0;
        const normalizedAlign = (align || "center").toLowerCase();

        switch (side) {
            case "top":
                top = triggerRect.top - contentRect.height - offset;
                left = this._computeHorizontalAlignment(normalizedAlign, triggerRect, contentRect);
                break;
            case "left":
                top = this._computeVerticalAlignment(normalizedAlign, triggerRect, contentRect);
                left = triggerRect.left - contentRect.width - offset;
                break;
            case "right":
                top = this._computeVerticalAlignment(normalizedAlign, triggerRect, contentRect);
                left = triggerRect.right + offset;
                break;
            default:
                top = triggerRect.bottom + offset;
                left = this._computeHorizontalAlignment(normalizedAlign, triggerRect, contentRect);
                break;
        }

        const maxLeft = Math.max(padding, viewportWidth - contentRect.width - padding);
        const maxTop = Math.max(padding, viewportHeight - contentRect.height - padding);

        return {
            top: this._clamp(top, padding, maxTop),
            left: this._clamp(left, padding, maxLeft)
        };
    }

    _computeHorizontalAlignment(align, triggerRect, contentRect) {
        if (align === "start") {
            return triggerRect.left;
        }

        if (align === "end") {
            return triggerRect.right - contentRect.width;
        }

        return triggerRect.left + (triggerRect.width / 2) - (contentRect.width / 2);
    }

    _computeVerticalAlignment(align, triggerRect, contentRect) {
        if (align === "start") {
            return triggerRect.top;
        }

        if (align === "end") {
            return triggerRect.bottom - contentRect.height;
        }

        return triggerRect.top + (triggerRect.height / 2) - (contentRect.height / 2);
    }

    _clamp(value, min, max) {
        return Math.min(Math.max(value, min), max);
    }
}

window.PopoverInterop = new PopoverInterop();
