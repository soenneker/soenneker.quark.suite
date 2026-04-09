const editors = new WeakMap();
let configured = false;

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
                const existingState = editors.get(container);

                if (existingState) {
                    disposeEditorState(existingState);
                }

                const editor = monaco.editor.create(container, options);
                editors.set(container, {
                    editor,
                    contentChangeDisposable: null,
                    ownsModel: !options.model
                });
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
        editors.delete(container);
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


