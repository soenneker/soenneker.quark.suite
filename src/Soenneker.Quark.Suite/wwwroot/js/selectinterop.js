const scrollAttachments = new Map();

/**
 * Radix-style scroll buttons: show when the viewport can scroll further in that direction.
 */
export function attachSelectScrollButtons(attachmentId, viewport, scrollUp, scrollDown) {
    if (!attachmentId || !viewport) {
        return;
    }

    detachSelectScrollButtons(attachmentId);

    const update = () => {
        const { scrollTop, scrollHeight, clientHeight } = viewport;
        const canScroll = scrollHeight > clientHeight + 1;
        const showUp = canScroll && scrollTop > 1;
        const showDown = canScroll && scrollTop + clientHeight < scrollHeight - 1;

        if (scrollUp) {
            scrollUp.hidden = !showUp;
            scrollUp.dataset.state = showUp ? "visible" : "hidden";
        }

        if (scrollDown) {
            scrollDown.hidden = !showDown;
            scrollDown.dataset.state = showDown ? "visible" : "hidden";
        }
    };

    viewport.addEventListener("scroll", update, { passive: true });

    const resizeObserver = typeof ResizeObserver === "function"
        ? new ResizeObserver(() => window.requestAnimationFrame(update))
        : null;

    if (resizeObserver) {
        resizeObserver.observe(viewport);
    }

    update();

    scrollAttachments.set(attachmentId, {
        update,
        resizeObserver,
        viewport
    });
}

export function detachSelectScrollButtons(attachmentId) {
    const prev = scrollAttachments.get(attachmentId);

    if (!prev) {
        return;
    }

    prev.viewport.removeEventListener("scroll", prev.update);

    if (prev.resizeObserver) {
        prev.resizeObserver.disconnect();
    }

    scrollAttachments.delete(attachmentId);
}

export function scrollIntoViewNearest(element) {
    if (!element || typeof element.scrollIntoView !== "function") {
        return;
    }

    element.scrollIntoView({ block: "nearest", inline: "nearest" });
}

export function scrollViewportBy(viewport, deltaY) {
    if (!viewport || !Number.isFinite(deltaY)) {
        return;
    }

    viewport.scrollTop += deltaY;
}
