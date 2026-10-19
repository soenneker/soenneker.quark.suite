const editors = new WeakMap();
const allowed = new Set(["P", "DIV", "BR", "STRONG", "B", "EM", "I", "U", "UL", "OL", "LI", "BLOCKQUOTE", "A", "SPAN"]);
const discard = new Set(["SCRIPT", "STYLE", "IFRAME", "OBJECT", "EMBED", "SVG", "MATH", "TEMPLATE", "FORM", "INPUT", "BUTTON"]);
export function safeUrl(value) {
    if (!value || /[\u0000-\u001f\u007f\\]/.test(value) || value.trim().startsWith("//")) return false;
    const text = value.trim();
    if (!text) return false;
    return /^(https?:|mailto:)/i.test(text) || !text.includes(":");
}
export function sanitize(html) {
    const source = document.createElement("template"); source.innerHTML = html;
    const output = document.createElement("div");
    function copy(node, target) {
        if (node.nodeType === Node.TEXT_NODE) { target.append(document.createTextNode(node.textContent)); return; }
        if (node.nodeType !== Node.ELEMENT_NODE || discard.has(node.tagName)) return;
        let next = target;
        if (allowed.has(node.tagName)) {
            next = document.createElement(node.tagName.toLowerCase());
            if (node.tagName === "A" && safeUrl(node.getAttribute("href"))) {
                next.setAttribute("href", node.getAttribute("href").trim()); next.setAttribute("rel", "noopener noreferrer");
            }
            if (node.tagName === "SPAN" && node.hasAttribute("data-mention")) next.setAttribute("data-mention", node.getAttribute("data-mention"));
            target.append(next);
        }
        for (const child of node.childNodes) copy(child, next);
    }
    for (const child of source.content.childNodes) copy(child, output);
    return output.innerHTML;
}
export function mount(element, callback) {
    unmount(element);
    const state = { callback, range: null, alive: true, last: "", dirty: false, sending: false, composing: false, format: "" };
    const format = () => {
        if (state.composing || !state.alive) return;
        const node = state.range?.startContainer;
        const link = (node?.nodeType === Node.ELEMENT_NODE ? node : node?.parentElement)?.closest("a");
        const value = { bold: document.queryCommandState("bold"), italic: document.queryCommandState("italic"), underline: document.queryCommandState("underline"),
            bullets: document.queryCommandState("insertUnorderedList"), numbered: document.queryCommandState("insertOrderedList"),
            linkUrl: link && element.contains(link) ? link.getAttribute("href") : null, linkText: link && element.contains(link) ? link.textContent : null };
        const serialized = JSON.stringify(value);
        if (state.format === serialized) return;
        state.format = serialized;
        void callback.invokeMethodAsync("OnFormatChanged", value).catch(() => {});
    };
    const remember = () => {
        const selection = window.getSelection();
        if (selection?.rangeCount && element.contains(selection.anchorNode) && element.contains(selection.focusNode)) { state.range = selection.getRangeAt(0).cloneRange(); format(); }
    };
    const emit = async () => {
        if (state.composing) return;
        state.dirty = true;
        if (state.sending) return;
        state.sending = true;
        try {
            while (state.dirty && state.alive && !state.composing) {
                state.dirty = false;
                const html = sanitize(element.innerHTML);
                if (html === state.last) continue;
                state.last = html;
                await callback.invokeMethodAsync("OnContentChanged", html);
            }
        } catch { /* A disposed circuit cannot receive editor updates. */ }
        finally { state.sending = false; }
    };
    const paste = event => {
        if (!element.isContentEditable) return;
        event.preventDefault();
        const html = event.clipboardData?.getData("text/html");
        if (html) document.execCommand("insertHTML", false, sanitize(html));
        else document.execCommand("insertText", false, event.clipboardData?.getData("text/plain") || "");
        remember(); void emit();
    };
    const drop = event => { event.preventDefault(); }; // Files go through the application-owned upload callback.
    const keydown = event => {
        if (event.isComposing || state.composing) return;
        if ((event.ctrlKey || event.metaKey) && ["b", "i", "u"].includes(event.key.toLowerCase())) {
            event.preventDefault(); execute(element, { b: "bold", i: "italic", u: "underline" }[event.key.toLowerCase()]);
        }
    };
    const compositionStart = () => { state.composing = true; };
    const compositionEnd = () => { state.composing = false; remember(); void emit(); };
    const toolbar = element.parentElement.querySelector('[role="toolbar"]');
    const toolbarKeys = event => {
        if (event.isComposing || !["ArrowLeft", "ArrowRight", "Home", "End"].includes(event.key) || event.target.tagName !== "BUTTON") return;
        const buttons = [...toolbar.querySelectorAll("button:not(:disabled)")];
        let index = buttons.indexOf(event.target);
        if (index < 0) return;
        event.preventDefault();
        const direction = getComputedStyle(toolbar).direction === "rtl" ? -1 : 1;
        index = event.key === "Home" ? 0 : event.key === "End" ? buttons.length - 1 : (index + (event.key === "ArrowLeft" ? -1 : 1) * direction + buttons.length) % buttons.length;
        buttons[index].focus();
    };
    toolbar?.addEventListener("keydown", toolbarKeys);
    element.addEventListener("compositionstart", compositionStart); element.addEventListener("compositionend", compositionEnd);
    state.emit = emit; state.remember = remember;
    state.cleanup = () => {
        state.alive = false;
        toolbar?.removeEventListener("keydown", toolbarKeys);
        element.removeEventListener("compositionstart", compositionStart); element.removeEventListener("compositionend", compositionEnd);
        document.removeEventListener("selectionchange", remember);
        element.removeEventListener("input", emit); element.removeEventListener("paste", paste);
        element.removeEventListener("drop", drop); element.removeEventListener("keydown", keydown);
    };
    document.addEventListener("selectionchange", remember);
    element.addEventListener("input", emit); element.addEventListener("paste", paste);
    element.addEventListener("drop", drop); element.addEventListener("keydown", keydown);
    editors.set(element, state);
}
export function setContent(element, html, readOnly) {
    const state = editors.get(element); if (!state) return;
    element.contentEditable = readOnly ? "false" : "true";
    const clean = sanitize(html);
    if (state.composing || state.sending) return;
    if (clean !== state.last && clean !== sanitize(element.innerHTML)) { element.innerHTML = clean; state.range = null; }
    state.last = clean;
}
export function execute(element, command, value, label) {
    const state = editors.get(element); if (!state || state.composing || !element.isContentEditable) return;
    element.focus();
    const selection = window.getSelection();
    if (state.range && element.contains(state.range.commonAncestorContainer)) { selection.removeAllRanges(); selection.addRange(state.range); }
    else { const range = document.createRange(); range.selectNodeContents(element); range.collapse(false); selection.removeAllRanges(); selection.addRange(range); }
    if (["bold", "italic", "underline", "insertUnorderedList", "insertOrderedList", "undo", "redo"].includes(command)) document.execCommand(command, false);
    else if (command === "unlink") {
        const node = selection.anchorNode;
        const link = (node?.nodeType === Node.ELEMENT_NODE ? node : node?.parentElement)?.closest("a");
        if (link && element.contains(link)) { const range = document.createRange(); range.selectNode(link); selection.removeAllRanges(); selection.addRange(range); }
        document.execCommand("unlink", false);
    }
    else if (command === "mention" || command === "link" || command === "attachment") {
        if (command !== "mention" && !safeUrl(value)) return;
        if (command === "link") {
            const anchor = selection.anchorNode;
            const existing = (anchor?.nodeType === Node.ELEMENT_NODE ? anchor : anchor?.parentElement)?.closest("a");
            if (existing && element.contains(existing)) { const range = document.createRange(); range.selectNode(existing); selection.removeAllRanges(); selection.addRange(range); }
        }
        const node = document.createElement(command === "mention" ? "span" : "a");
        node.textContent = label || selection.toString() || value;
        if (command === "mention") node.setAttribute("data-mention", value);
        else { node.setAttribute("href", value.trim()); node.setAttribute("rel", "noopener noreferrer"); }
        document.execCommand("insertHTML", false, node.outerHTML);
    }
    state.remember(); void state.emit();
}
export function unmount(element) { const state = editors.get(element); if (state) { state.cleanup(); editors.delete(element); } }
