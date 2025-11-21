using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;


namespace Soenneker.Quark;

/// <inheritdoc cref="ICodeEditorInterop"/>
public sealed class CodeEditorInterop : ICodeEditorInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncSingleton _initializer;


    private const string _monacoBaseCdn = "https://cdn.jsdelivr.net/npm/monaco-editor@0.54.0";
    private const string _modulePath = "Soenneker.Quark.Suite/js/monacointerop.js";
    private const string _moduleName = "MonacoInterop";

    public CodeEditorInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;

        _initializer = new AsyncSingleton(async (token, _) =>
        {
            await _resourceLoader.LoadStyle("https://cdn.jsdelivr.net/npm/monaco-editor@0.54.0/min/vs/editor/editor.main.css", integrity: "sha256-KPxKE5YDTnMFYh7t0uKscHO8puZlkZPttjPyUGqo2Lo=", cancellationToken: token);
            await _resourceLoader.LoadScript("https://cdn.jsdelivr.net/npm/monaco-editor@0.54.0/min/vs/loader.js", integrity: "sha256-NbWd+AtBqLc78c6xWbRPBAVnwqtT9Od6PQAtncOdVdE=", cancellationToken: token);

            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_modulePath, _moduleName, 100, token);

            // Initialize the AMD config for monaco
            await _jsRuntime.InvokeVoidAsync($"{_moduleName}.ensureConfigured", token, _monacoBaseCdn + "/min");
            return new object();
        });
    }

    /// <summary>
    /// Initializes the code editor by loading required resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    /// <summary>
    /// Creates a new Monaco editor instance in the specified container.
    /// </summary>
    /// <param name="container">The element reference to the container where the editor will be created.</param>
    /// <param name="optionsJson">JSON string containing the editor options.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the editor creation operation.</returns>
    public async ValueTask CreateEditor(ElementReference container, string optionsJson, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.createEditor", cancellationToken, container, optionsJson);
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
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.setValue", cancellationToken, container, value);
    }

    /// <summary>
    /// Gets the current value of the editor content.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task that contains the editor content, or null if not available.</returns>
    public async ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        return await _jsRuntime.InvokeAsync<string?>($"{_moduleName}.getValue", cancellationToken, container);
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
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.setLanguage", cancellationToken, container, language);
    }

    /// <summary>
    /// Sets the editor theme (e.g., "vs-dark", "vs-light").
    /// </summary>
    /// <param name="theme">The theme identifier.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the set theme operation.</returns>
    public async ValueTask SetTheme(string theme, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.setTheme", cancellationToken, theme);
    }

    /// <summary>
    /// Disposes the editor instance in the specified container.
    /// </summary>
    /// <param name="container">The element reference to the editor container.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the dispose operation.</returns>
    public async ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default)
    {
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.disposeEditor", cancellationToken, container);
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
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.updateContentHeight", cancellationToken, container, minLines, maxLines);
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
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.addContentChangeListener", cancellationToken, container, null, minLines, maxLines);
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_modulePath);
        await _initializer.DisposeAsync();
    }
}


