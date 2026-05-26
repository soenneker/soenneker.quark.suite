const editors = new WeakMap();
const activeEditors = new Set();
let configured = false;
let configuration = null;
let monacoModule = null;
let monacoPromise = null;
let domObserver = null;
let themesDefined = false;
const workerObjectUrls = new Map();

function toAbsoluteUrl(url, parameterName) {
    if (typeof url !== 'string' || url.trim().length === 0) {
        throw new Error(`${parameterName} must be a non-empty string.`);
    }

    return new URL(url, document.baseURI).href;
}

function normalizeWorkerUrls(workerUrls) {
    const urls = workerUrls || {};
    const editor = toAbsoluteUrl(urls.editor, 'workerUrls.editor');

    return {
        editor,
        json: toAbsoluteUrl(urls.json || editor, 'workerUrls.json'),
        css: toAbsoluteUrl(urls.css || editor, 'workerUrls.css'),
        html: toAbsoluteUrl(urls.html || editor, 'workerUrls.html'),
        typescript: toAbsoluteUrl(urls.typescript || editor, 'workerUrls.typescript')
    };
}

function getWorkerKey(label) {
    switch ((label || '').toLowerCase()) {
        case 'json':
            return 'json';
        case 'css':
        case 'less':
        case 'scss':
            return 'css';
        case 'handlebars':
        case 'html':
        case 'razor':
            return 'html';
        case 'javascript':
        case 'typescript':
            return 'typescript';
        default:
            return 'editor';
    }
}

function createWorkerBootstrapUrl(workerUrl) {
    const url = toAbsoluteUrl(workerUrl, 'workerUrl');

    if (globalThis.location) {
        const worker = new URL(url);

        if (worker.origin === globalThis.location.origin) {
            return url;
        }
    }

    let objectUrl = workerObjectUrls.get(url);

    if (!objectUrl) {
        const source = `import ${JSON.stringify(url)};`;
        objectUrl = URL.createObjectURL(new Blob([source], { type: 'text/javascript' }));
        workerObjectUrls.set(url, objectUrl);
    }

    return objectUrl;
}

function configureWorkers(workerUrls) {
    const normalizedWorkerUrls = normalizeWorkerUrls(workerUrls);
    const previousEnvironment = globalThis.MonacoEnvironment || {};

    globalThis.MonacoEnvironment = {
        ...previousEnvironment,
        globalAPI: true,
        getWorker: undefined,
        getWorkerUrl: (_moduleId, label) => {
            const key = getWorkerKey(label);
            return createWorkerBootstrapUrl(normalizedWorkerUrls[key] || normalizedWorkerUrls.editor);
        }
    };

    return normalizedWorkerUrls;
}

function rewriteCodiconFontUrl(style, codiconUrl) {
    if (!codiconUrl || style?.tagName !== 'STYLE' || !style.textContent?.includes('codicon.ttf')) {
        return;
    }

    const fontUrl = toAbsoluteUrl(codiconUrl, 'codiconUrl').replace(/"/g, '%22');
    style.textContent = style.textContent.replace(/url\((['"]?)\.\/codicon\.ttf\1\)/g, `url("${fontUrl}")`);
}

async function importWithCodiconFontRewrite(moduleUrl, codiconUrl) {
    if (!codiconUrl || typeof Node === 'undefined') {
        return await import(moduleUrl);
    }

    const originalAppendChild = Node.prototype.appendChild;
    const patchedAppendChild = function (child) {
        rewriteCodiconFontUrl(child, codiconUrl);
        return originalAppendChild.call(this, child);
    };

    Node.prototype.appendChild = patchedAppendChild;

    try {
        return await import(moduleUrl);
    } finally {
        if (Node.prototype.appendChild === patchedAppendChild) {
            Node.prototype.appendChild = originalAppendChild;
        }
    }
}

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

function ensureThemesDefined() {
    const monaco = monacoModule || globalThis.monaco;

    if (themesDefined || !monaco?.editor) {
        return;
    }

    const lightThemeName = String.fromCharCode(113, 117, 97, 114, 107, 76, 105, 103, 104, 116);

    monaco.editor.defineTheme(lightThemeName, {
        base: 'vs',
        inherit: true,
        rules: [],
        colors: {
            'editor.background': '#f5f5f5',
            'editorGutter.background': '#f5f5f5',
            'editorLineNumber.foreground': '#737373',
            'editorLineNumber.activeForeground': '#171717',
            'editorIndentGuide.background1': '#d4d4d4',
            'editorIndentGuide.activeBackground1': '#a3a3a3'
        }
    });

    themesDefined = true;
}

async function ensureMonacoLoaded() {
    if (!configured || !configuration) {
        throw new Error('Monaco not configured. Call ensureConfigured first.');
    }

    if (!monacoPromise) {
        monacoPromise = importWithCodiconFontRewrite(configuration.moduleUrl, configuration.codiconUrl)
            .then((module) => {
                monacoModule = globalThis.monaco?.editor ? globalThis.monaco : module;
                globalThis.monaco = monacoModule;
                ensureThemesDefined();
                return monacoModule;
            })
            .catch((error) => {
                monacoPromise = null;
                throw error;
            });
    }

    return monacoPromise;
}

export function ensureConfigured(moduleUrl, workerUrls, codiconUrl) {
    const normalizedModuleUrl = toAbsoluteUrl(moduleUrl, 'moduleUrl');

    if (configured) {
        if (configuration?.moduleUrl !== normalizedModuleUrl) {
            throw new Error('Monaco is already configured with a different module URL.');
        }

        return;
    }

    configuration = {
        moduleUrl: normalizedModuleUrl,
        codiconUrl,
        workerUrls: configureWorkers(workerUrls)
    };
    configured = true;
}

export async function createEditor(container, optionsJson) {
    const options = optionsJson ? JSON.parse(optionsJson) : {};

    if (!configured) {
        throw new Error('Monaco not configured. Call ensureConfigured first.');
    }

    if (!container || !container.isConnected) {
        return;
    }

    const monaco = await ensureMonacoLoaded();
    ensureThemesDefined();
    cleanupDetachedEditors();

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
}

export function setValue(container, value) {
    getEditor(container).setValue(value || '');
}

export function getValue(container) {
    return getEditor(container).getValue();
}

export async function setLanguage(container, language) {
    const monaco = await ensureMonacoLoaded();
    const editor = getEditor(container);
    const model = editor.getModel();

    if (model) {
        monaco.editor.setModelLanguage(model, language);
    }
}

export async function setTheme(theme) {
    const monaco = await ensureMonacoLoaded();
    ensureThemesDefined();
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

export async function updateContentHeight(container, minLines, maxLines) {
    const monaco = await ensureMonacoLoaded();
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

export async function addContentChangeListener(container, minLines, maxLines) {
    const state = getEditorState(container);
    const editor = state.editor;
    const model = editor.getModel();

    if (!model) return;

    if (state.contentChangeDisposable) {
        state.contentChangeDisposable.dispose();
    }

    await updateContentHeight(container, minLines, maxLines);

    state.contentChangeDisposable = model.onDidChangeContent(() => {
        updateContentHeight(container, minLines, maxLines);
    });
}


