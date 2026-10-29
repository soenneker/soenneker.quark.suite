using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class CodeEditorInterop : ICodeEditorInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private readonly string _modulePath;
    private readonly string _themeModulePath;
    
    private const string _cdnBaseUrl = "https://cdn.jsdelivr.net/npm/monaco-editor@0.55.1";
    private const string _cdnCssPath = "/min/vs/editor/editor.main.css";
    
    private const string _localContentPath = "_content/Soenneker.Quark.Suite";
    private const string _localCssPath = $"{_localContentPath}/js/monaco-editor/monaco.editor.main.esm.css";
    private const string _localModulePath = $"{_localContentPath}/js/monaco-editor/monaco.editor.main.esm.js";
    
    private const string _cssIntegrity = "sha256-22wc1geUOoYbR8dmDQ7wLvZPcXMZTXxLi85cmIFW5fg=";

    private static readonly IReadOnlyDictionary<string, string> _localWorkerUrls = new Dictionary<string, string>
    {
        ["editor"] = $"{_localContentPath}/js/monaco-editor/workers/editor.worker.esm.js",
        ["json"] = $"{_localContentPath}/js/monaco-editor/workers/json.worker.esm.js",
        ["css"] = $"{_localContentPath}/js/monaco-editor/workers/css.worker.esm.js",
        ["html"] = $"{_localContentPath}/js/monaco-editor/workers/html.worker.esm.js",
        ["typescript"] = $"{_localContentPath}/js/monaco-editor/workers/ts.worker.esm.js"
    };

    private readonly QuarkOptions _quarkOptions;

    public CodeEditorInterop(IModuleImportUtil moduleImportUtil, IResourceLoader resourceLoader, QuarkOptions quarkOptions)
    {
        _modulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/monacointerop.js", quarkOptions);
        _themeModulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/themeinterop.js", quarkOptions);
        _moduleImportUtil = moduleImportUtil;
        _resourceLoader = resourceLoader;
        _quarkOptions = quarkOptions;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private async ValueTask InitializeResources(CancellationToken token)
    {
        var cssUrl = _quarkOptions.CodeEditorUseCdn ? $"{_cdnBaseUrl}{_cdnCssPath}" : _localCssPath;

        if (_quarkOptions.CodeEditorUseCdn)
        {
            await _resourceLoader.LoadStyle(cssUrl, integrity: _cssIntegrity, cancellationToken: token);
        }
        else
        {
            await _resourceLoader.LoadStyle(cssUrl, cancellationToken: token);
        }

        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
        await module.InvokeVoidWithContextAsync("ensureConfigured", token, _localModulePath, JsonSerializer.SerializeToElement(_localWorkerUrls, QuarkInteropJsonContext.Default.IReadOnlyDictionaryStringString));
    }

    public async ValueTask ObserveVisibility(ElementReference container, DotNetObjectReference<CodeEditor> receiver, CancellationToken cancellationToken = default)
    {
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidWithContextAsync("observeVisibility", cancellationToken, container, receiver);
    }

    public async ValueTask UnobserveVisibility(ElementReference container, CancellationToken cancellationToken = default)
    {
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidWithContextAsync("unobserveVisibility", cancellationToken, container);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(linked);
    }

    public async ValueTask CreateEditor(ElementReference container, string optionsJson, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("createEditor", linked, container, optionsJson);
        }
    }

    public async ValueTask Layout(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("layoutEditor", linked, container);
        }
    }

    public async ValueTask UpdateOptions(ElementReference container, bool readOnly, bool wordWrap, bool minimap, bool autoHeight,
        int? minLines, int? maxLines, string? ariaLabel, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("updateEditorOptions", linked, container, readOnly, wordWrap, minimap, autoHeight, minLines, maxLines, ariaLabel);
        }
    }

    public async ValueTask SetValue(ElementReference container, string value, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("setValue", linked, container, value);
        }
    }

    public async ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeWithContextAsync<string?>("getValue", linked, container);
        }
    }

    public async ValueTask SetLanguage(ElementReference container, string language, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("setLanguage", linked, container, language);
        }
    }

    public async ValueTask SetTheme(string theme, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("setTheme", linked, theme);
        }
    }

    public async ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("disposeEditor", linked, container);
        }
    }

    public async ValueTask UpdateContentHeight(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("updateContentHeight", linked, container, minLines, maxLines);
        }
    }

    public async ValueTask AddContentChangeListener(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("addContentChangeListener", linked, container, minLines, maxLines);
        }
    }

    public async ValueTask RegisterContentChangedCallback(ElementReference container, DotNetObjectReference<CodeEditor> dotNetRef,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("registerContentChangedCallback", linked, container, dotNetRef);
        }
    }

    public async ValueTask ConfigureFileDrop(ElementReference container, ElementReference dropZone, string inputId,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("configureFileDrop", linked, container, dropZone, inputId);
        }
    }

    public async ValueTask RemoveFileDrop(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("removeFileDrop", linked, container);
        }
    }

    public async ValueTask InsertTextAtDropPosition(ElementReference container, string text, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidWithContextAsync("insertTextAtDropPosition", linked, container, text);
        }
    }

    public async ValueTask RegisterThemeChangedCallback(DotNetObjectReference<CodeEditor> dotNetRef, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var themeModule = await _moduleImportUtil.GetContentModuleReference(_themeModulePath, linked);
            await themeModule.InvokeVoidWithContextAsync("registerThemeChangedCallback", linked, dotNetRef);
        }
    }

    public async ValueTask UnregisterThemeChangedCallback(DotNetObjectReference<CodeEditor> dotNetRef)
    {
        try
        {
            var themeModule = await _moduleImportUtil.GetContentModuleReference(_themeModulePath);
            await themeModule.InvokeVoidWithContextAsync("unregisterThemeChangedCallback", dotNetRef);
        }
        catch
        {
            // Ignore if JS or theme interop is no longer available
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _moduleImportUtil.DisposeContentModule(_themeModulePath);
    }
}
