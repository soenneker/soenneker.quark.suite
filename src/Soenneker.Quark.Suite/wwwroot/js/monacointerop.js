const editors = new WeakMap();
/** Containers whose editor was disposed before require() finished — skip late create (nav away race). */
const disposeBeforeCreate = new WeakSet();
let configured = false;

function getEditor(container) {
    const editor = editors.get(container);

    if (!editor) {
        throw new Error('Editor not found for container');
    }

    return editor;
}

export function ensureConfigured(basePath) {
    if (configured) return;
    if (typeof require !== 'function') return;

    require.config({ paths: { 'vs': basePath + '/vs' } });
    configured = true;
}

export function createEditor(container, optionsJson) {
    const options = optionsJson ? JSON.parse(optionsJson) : {};

    if (!configured) {
        throw new Error('Monaco not configured. Call ensureConfigured first.');
    }

    return new Promise((resolve, reject) => {
        require(['vs/editor/editor.main'], () => {
            try {
                if (disposeBeforeCreate.has(container)) {
                    disposeBeforeCreate.delete(container);
                    resolve();
                    return;
                }

                const existing = editors.get(container);

                if (existing) {
                    existing.dispose();
                    editors.delete(container);
                }

                const editor = monaco.editor.create(container, options);
                editors.set(container, editor);
                resolve();
            } catch (error) {
                reject(error);
            }
        });
    });
}

export function setValue(container, value) {
    getEditor(container).setValue(value || '');
}

export function getValue(container) {
    return getEditor(container).getValue();
}

export function setLanguage(container, language) {
    const editor = getEditor(container);
    const model = editor.getModel();

    if (model) {
        monaco.editor.setModelLanguage(model, language);
    }
}

export function setTheme(theme) {
    monaco.editor.setTheme(theme);
}

/** Applies Monaco editor options without recreating the editor (readOnly, wordWrap, minimap, etc.). */
export function updateOptions(container, optionsJson) {
    const editor = editors.get(container);

    if (!editor) {
        return;
    }

    const options = optionsJson ? JSON.parse(optionsJson) : {};
    editor.updateOptions(options);
}

export function disposeEditor(container) {
    const editor = editors.get(container);

    if (editor) {
        editor.dispose();
        editors.delete(container);
    }
    else {
        disposeBeforeCreate.add(container);
    }
}

// Re-measures the editor after the container becomes visible or changes size (e.g. parent was display:none).
export function layoutEditor(container) {
    const editor = editors.get(container);

    if (!editor) return;

    editor.layout();
}

export function updateContentHeight(container, minLines, maxLines) {
    const editor = getEditor(container);
    const model = editor.getModel();

    if (!model) return;

    const lineCount = model.getLineCount();
    const lineHeight = editor.getOption(monaco.editor.EditorOption.lineHeight);
    const effectiveLines = Math.max(minLines || 1, Math.min(maxLines || lineCount, lineCount));
    const contentHeight = effectiveLines * lineHeight;
    const totalHeight = contentHeight + 20;

    container.style.height = totalHeight + 'px';
    editor.layout();
}

export function addContentChangeListener(container, dotNetRef, minLines, maxLines) {
    const editor = getEditor(container);
    const model = editor.getModel();

    if (!model) return;

    updateContentHeight(container, minLines, maxLines);

    model.onDidChangeContent(() => {
        updateContentHeight(container, minLines, maxLines);
    });
}


