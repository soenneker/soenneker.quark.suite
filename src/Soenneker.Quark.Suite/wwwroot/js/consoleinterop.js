const autoScrollObservers = new WeakMap();

function scrollToBottom(element) {
    window.requestAnimationFrame(() => {
        element.scrollTop = element.scrollHeight;
    });
}

export function initializeAutoScroll(element) {
    destroyAutoScroll(element);

    const observer = new MutationObserver((mutations) => {
        const hasConsoleMutation = mutations.some((mutation) => {
            const target = mutation.target.nodeType === Node.ELEMENT_NODE
                ? mutation.target
                : mutation.target.parentElement;

            return !target?.closest('[data-slot="console-actions"]');
        });

        if (hasConsoleMutation) {
            scrollToBottom(element);
        }
    });

    observer.observe(element, {
        childList: true,
        subtree: true,
        characterData: true
    });

    autoScrollObservers.set(element, observer);
    scrollToBottom(element);
}

export function destroyAutoScroll(element) {
    const observer = autoScrollObservers.get(element);

    if (!observer) {
        return;
    }

    observer.disconnect();
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
