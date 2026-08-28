const autoScrollObservers = new WeakMap();

function scheduleScrollToBottom(element, state) {
    if (state.frame) {
        return;
    }

    state.frame = window.requestAnimationFrame(() => {
        state.frame = 0;
        element.scrollTop = element.scrollHeight;
    });
}

export function initializeAutoScroll(element) {
    destroyAutoScroll(element);

    const state = { observer: null, frame: 0 };

    const observer = new MutationObserver((mutations) => {
        const hasConsolePanelMutation = mutations.some((mutation) => {
            const target = mutation.target.nodeType === Node.ELEMENT_NODE
                ? mutation.target
                : mutation.target.parentElement;

            return !target?.closest('[data-slot="console-actions"]');
        });

        if (hasConsolePanelMutation) {
            scheduleScrollToBottom(element, state);
        }
    });

    state.observer = observer;

    observer.observe(element, {
        childList: true,
        subtree: true,
        characterData: true
    });

    autoScrollObservers.set(element, state);
    scheduleScrollToBottom(element, state);
}

export function destroyAutoScroll(element) {
    const state = autoScrollObservers.get(element);

    if (!state) {
        return;
    }

    state.observer?.disconnect();
    if (state.frame) {
        window.cancelAnimationFrame(state.frame);
    }
    autoScrollObservers.delete(element);
}

export function download(fileName, contentType, content) {
    const blob = new Blob([content ?? ""], { type: contentType || "text/plain;charset=utf-8" });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement("a");

    anchor.href = url;
    anchor.download = fileName || "console.log";
    anchor.style.display = "none";

    document.body.appendChild(anchor);
    anchor.click();
    anchor.remove();

    window.setTimeout(() => URL.revokeObjectURL(url), 1000);
}
