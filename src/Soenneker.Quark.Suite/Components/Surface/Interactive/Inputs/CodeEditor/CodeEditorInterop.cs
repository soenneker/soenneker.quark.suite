using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="ICodeEditorInterop"/>
public sealed class CodeEditorInterop : ICodeEditorInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/monacointerop.js";
    private const string _themeModulePath = "./_content/Soenneker.Quark.Suite/js/themeinterop.js";
    
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
        await module.InvokeVoidAsync("ensureConfigured", token, _localModulePath, _localWorkerUrls);
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
            await module.InvokeVoidAsync("createEditor", linked, container, optionsJson);
        }
    }

    public async ValueTask Layout(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("layoutEditor", linked, container);
        }
    }

    public async ValueTask SetValue(ElementReference container, string value, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setValue", linked, container, value);
        }
    }

    public async ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<string?>("getValue", linked, container);
        }
    }

    public async ValueTask SetLanguage(ElementReference container, string language, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setLanguage", linked, container, language);
        }
    }

    public async ValueTask SetTheme(string theme, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setTheme", linked, theme);
        }
    }

    public async ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("disposeEditor", linked, container);
        }
    }

    public async ValueTask UpdateContentHeight(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("updateContentHeight", linked, container, minLines, maxLines);
        }
    }

    public async ValueTask AddContentChangeListener(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("addContentChangeListener", linked, container, minLines, maxLines);
        }
    }

    public async ValueTask RegisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef, CancellationToken cancellationToken = default)
        where T : class
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var themeModule = await _moduleImportUtil.GetContentModuleReference(_themeModulePath, linked);
            await themeModule.InvokeVoidAsync("registerThemeChangedCallback", linked, dotNetRef);
        }
    }

    public async ValueTask UnregisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef)
        where T : class
    {
        try
        {
            var themeModule = await _moduleImportUtil.GetContentModuleReference(_themeModulePath);
            await themeModule.InvokeVoidAsync("unregisterThemeChangedCallback", dotNetRef);
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


