using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
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
    private const string _cdnLoaderPath = "/min/vs/loader.js";
    private const string _cdnBasePath = "/min";
    
    private const string _localCssPath = "/css/monaco-editor/editor.main.css";
    private const string _localLoaderPath = "/js/monaco-editor/loader.js";
    private const string _localBasePath = "/js/monaco-editor/min";
    
    private const string _cssIntegrity = "sha256-22wc1geUOoYbR8dmDQ7wLvZPcXMZTXxLi85cmIFW5fg=";
    private const string _loaderIntegrity = "sha256-NbWd+AtBqLc78c6xWbRPBAVnwqtT9Od6PQAtncOdVdE=";

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
        string cssUrl;
        string loaderUrl;
        string monacoBaseUrl;
        bool useIntegrity;

        if (_quarkOptions.CodeEditorUseCdn)
        {
            cssUrl = $"{_cdnBaseUrl}{_cdnCssPath}";
            loaderUrl = $"{_cdnBaseUrl}{_cdnLoaderPath}";
            monacoBaseUrl = $"{_cdnBaseUrl}{_cdnBasePath}";
            useIntegrity = true;
        }
        else
        {
            cssUrl = _localCssPath;
            loaderUrl = _localLoaderPath;
            monacoBaseUrl = _localBasePath;
            useIntegrity = false;
        }

        if (useIntegrity)
        {
            await _resourceLoader.LoadStyle(cssUrl, integrity: _cssIntegrity, cancellationToken: token);
            await _resourceLoader.LoadScript(loaderUrl, integrity: _loaderIntegrity, cancellationToken: token);
        }
        else
        {
            await _resourceLoader.LoadStyle(cssUrl, cancellationToken: token);
            await _resourceLoader.LoadScript(loaderUrl, cancellationToken: token);
        }

        IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
        await module.InvokeVoidAsync("ensureConfigured", token, monacoBaseUrl);
    }

    /// <summary>
    /// Initializes the code editor by loading required resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(linked);
    }

    /// <summary>
    /// Creates a new Monaco editor instance in the specified container.
    /// </summary>
    /// <param name="container">The element reference to the container where the editor will be created.</param>
    /// <param name="optionsJson">JSON string containing the editor options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the editor creation operation.</returns>
    public async ValueTask CreateEditor(ElementReference container, string optionsJson, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("createEditor", linked, container, optionsJson);
        }
    }

    public async ValueTask Layout(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("layoutEditor", linked, container);
        }
    }

    /// <summary>
    /// Sets the value of the editor content.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="value">The new value to set.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the set value operation.</returns>
    public async ValueTask SetValue(ElementReference container, string value, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setValue", linked, container, value);
        }
    }

    /// <summary>
    /// Gets the current value of the editor content.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task that contains the editor content, or null if not available.</returns>
    public async ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<string?>("getValue", linked, container);
        }
    }

    /// <summary>
    /// Sets the programming language for syntax highlighting.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="language">The language identifier (e.g., "csharp", "javascript", "typescript").</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the set language operation.</returns>
    public async ValueTask SetLanguage(ElementReference container, string language, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setLanguage", linked, container, language);
        }
    }

    /// <summary>
    /// Sets the editor theme (e.g., "vs-dark", "vs-light").
    /// </summary>
    /// <param name="theme">The theme identifier.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the set theme operation.</returns>
    public async ValueTask SetTheme(string theme, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setTheme", linked, theme);
        }
    }

    /// <summary>
    /// Disposes the editor instance in the specified container.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the dispose operation.</returns>
    public async ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("disposeEditor", linked, container);
        }
    }

    /// <summary>
    /// Updates the content height of the editor based on the number of lines.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="minLines">The minimum number of lines to display.</param>
    /// <param name="maxLines">The maximum number of lines to display.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the update content height operation.</returns>
    public async ValueTask UpdateContentHeight(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("updateContentHeight", linked, container, minLines, maxLines);
        }
    }

    /// <summary>
    /// Adds a content change listener that automatically adjusts the editor height based on content.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="minLines">The minimum number of lines to display.</param>
    /// <param name="maxLines">The maximum number of lines to display.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the add content change listener operation.</returns>
    public async ValueTask AddContentChangeListener(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("addContentChangeListener", linked, container, minLines, maxLines);
        }
    }

    public async ValueTask RegisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef, CancellationToken cancellationToken = default)
        where T : class
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference themeModule = await _moduleImportUtil.GetContentModuleReference(_themeModulePath, linked);
            await themeModule.InvokeVoidAsync("registerThemeChangedCallback", linked, dotNetRef);
        }
    }

    public async ValueTask UnregisterThemeChangedCallback<T>(DotNetObjectReference<T> dotNetRef)
        where T : class
    {
        try
        {
            IJSObjectReference themeModule = await _moduleImportUtil.GetContentModuleReference(_themeModulePath);
            await themeModule.InvokeVoidAsync("unregisterThemeChangedCallback", dotNetRef);
        }
        catch
        {
            // Ignore if JS or theme interop is no longer available
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _moduleImportUtil.DisposeContentModule(_themeModulePath);
    }
}


