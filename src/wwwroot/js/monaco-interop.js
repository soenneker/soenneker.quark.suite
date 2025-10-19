// Minimal AMD/Monaco bootstrap and editor lifecycle helpers under QuarkMonaco namespace
window.QuarkMonaco = window.QuarkMonaco || (function () {
    const editors = new WeakMap();
    let configured = false;

    function ensureConfigured(basePath) {
        if (configured) return;
        if (typeof require !== 'function') return; // loader.js not ready yet
        require.config({ paths: { 'vs': basePath + '/vs' } });
        configured = true;
    }

    function createEditor(container, optionsJson) {
        const options = optionsJson ? JSON.parse(optionsJson) : {};
        if (!configured) throw new Error('Monaco not configured. Call ensureConfigured first.');

        require(['vs/editor/editor.main'], function () {
            const editor = monaco.editor.create(container, options);
            editors.set(container, editor);
        });
    }

    function getEditor(container) {
        const editor = editors.get(container);
        if (!editor) throw new Error('Editor not found for container');
        return editor;
    }

    function setValue(container, value) {
        const editor = getEditor(container);
        editor.setValue(value || '');
    }

    function getValue(container) {
        const editor = getEditor(container);
        return editor.getValue();
    }

    function setLanguage(container, language) {
        const editor = getEditor(container);
        const model = editor.getModel();
        if (model) monaco.editor.setModelLanguage(model, language);
    }

    function setTheme(theme) {
        monaco.editor.setTheme(theme);
    }

    function disposeEditor(container) {
        const editor = editors.get(container);
        if (editor) {
            editor.dispose();
            editors.delete(container);
        }
    }

    return {
        ensureConfigured,
        createEditor,
        setValue,
        getValue,
        setLanguage,
        setTheme,
        disposeEditor
    };
})();


