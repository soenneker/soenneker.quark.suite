// Monaco editor interop module
export class MonacoInterop {
    constructor() {
        this.editors = new WeakMap();
        this.configured = false;
    }

    ensureConfigured(basePath) {
        if (this.configured) return;
        if (typeof require !== 'function') return; // loader.js not ready yet
        require.config({ paths: { 'vs': basePath + '/vs' } });
        this.configured = true;
    }

    createEditor(container, optionsJson) {
        const options = optionsJson ? JSON.parse(optionsJson) : {};
        if (!this.configured) throw new Error('Monaco not configured. Call ensureConfigured first.');

        return new Promise((resolve, reject) => {
            require(['vs/editor/editor.main'], () => {
                try {
                    const editor = monaco.editor.create(container, options);
                    this.editors.set(container, editor);
                    resolve();
                } catch (error) {
                    reject(error);
                }
            });
        });
    }

    getEditor(container) {
        const editor = this.editors.get(container);
        if (!editor) throw new Error('Editor not found for container');
        return editor;
    }

    setValue(container, value) {
        const editor = this.getEditor(container);
        editor.setValue(value || '');
    }

    getValue(container) {
        const editor = this.getEditor(container);
        return editor.getValue();
    }

    setLanguage(container, language) {
        const editor = this.getEditor(container);
        const model = editor.getModel();
        if (model) monaco.editor.setModelLanguage(model, language);
    }

    setTheme(theme) {
        monaco.editor.setTheme(theme);
    }

    disposeEditor(container) {
        const editor = this.editors.get(container);
        if (editor) {
            editor.dispose();
            this.editors.delete(container);
        }
    }

    updateContentHeight(container, minLines, maxLines) {
        const editor = this.getEditor(container);
        const model = editor.getModel();
        if (!model) return;

        const lineCount = model.getLineCount();
        const lineHeight = editor.getOption(monaco.editor.EditorOption.lineHeight);
        
        // Apply min/max constraints
        const effectiveLines = Math.max(minLines || 1, Math.min(maxLines || lineCount, lineCount));
        const contentHeight = effectiveLines * lineHeight;
        
        // Add padding for scrollbars and chrome (approximately 10px top + 10px bottom)
        const totalHeight = contentHeight + 20;
        
        container.style.height = totalHeight + 'px';
        editor.layout();
    }

    addContentChangeListener(container, dotNetRef, minLines, maxLines) {
        const editor = this.getEditor(container);
        const model = editor.getModel();
        if (!model) return;

        // Initial height adjustment
        this.updateContentHeight(container, minLines, maxLines);

        // Listen for content changes
        model.onDidChangeContent(() => {
            this.updateContentHeight(container, minLines, maxLines);
        });
    }
}

window.MonacoInterop = new MonacoInterop();


