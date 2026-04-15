const editors = new WeakMap();
const activeEditors = new Set();
let configured = false;
let domObserver = null;

function removeEditorState(state) {
    if (!state) {
        return;
    }

    activeEditors.delete(state);

    if (state.container) {
        editors.delete(state.container);
    }

    state.container = null;
}

function getEditorState(container) {
    const state = editors.get(container);

    if (!state) {
        throw new Error('Editor not found for container');
    }

    return state;
}

function getEditor(container) {
    return getEditorState(container).editor;
}

function disposeEditorState(state) {
    if (!state) {
        return;
    }

    if (state.contentChangeDisposable) {
        state.contentChangeDisposable.dispose();
        state.contentChangeDisposable = null;
    }

    if (state.editor) {
        const model = state.editor.getModel();
        state.editor.dispose();
        state.editor = null;

        if (state.ownsModel && model) {
            model.dispose();
        }
    }
}

function cleanupDetachedEditors() {
    for (const state of activeEditors) {
        if (!state.container || !state.container.isConnected) {
            disposeEditorState(state);
            removeEditorState(state);
        }
    }
}

function ensureDomObserver() {
    if (domObserver || typeof MutationObserver === 'undefined' || !document?.body) {
        return;
    }

    domObserver = new MutationObserver(() => {
        cleanupDetachedEditors();
    });

    domObserver.observe(document.body, {
        childList: true,
        subtree: true
    });
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
                cleanupDetachedEditors();

                if (!container || !container.isConnected) {
                    resolve();
                    return;
                }

                const existingState = editors.get(container);

                if (existingState) {
                    disposeEditorState(existingState);
                    removeEditorState(existingState);
                }

                const editor = monaco.editor.create(container, options);
                const state = {
                    container,
                    editor,
                    contentChangeDisposable: null,
                    ownsModel: !options.model
                };

                editors.set(container, state);
                activeEditors.add(state);
                ensureDomObserver();
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

export function disposeEditor(container) {
    const state = editors.get(container);

    if (state) {
        disposeEditorState(state);
        removeEditorState(state);
    }
}

// Re-measures the editor after the container becomes visible or changes size (e.g. parent was display:none).
export function layoutEditor(container) {
    const state = editors.get(container);

    if (!state?.editor) return;

    state.editor.layout();
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

export function addContentChangeListener(container, minLines, maxLines) {
    const state = getEditorState(container);
    const editor = state.editor;
    const model = editor.getModel();

    if (!model) return;

    if (state.contentChangeDisposable) {
        state.contentChangeDisposable.dispose();
    }

    updateContentHeight(container, minLines, maxLines);

    state.contentChangeDisposable = model.onDidChangeContent(() => {
        updateContentHeight(container, minLines, maxLines);
    });
}


