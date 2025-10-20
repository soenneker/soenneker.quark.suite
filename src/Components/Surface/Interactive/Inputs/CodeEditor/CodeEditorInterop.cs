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

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public async ValueTask CreateEditor(ElementReference container, string optionsJson, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.createEditor", cancellationToken, container, optionsJson);
    }

    public async ValueTask SetValue(ElementReference container, string value, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.setValue", cancellationToken, container, value);
    }

    public async ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        return await _jsRuntime.InvokeAsync<string?>($"{_moduleName}.getValue", cancellationToken, container);
    }

    public async ValueTask SetLanguage(ElementReference container, string language, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.setLanguage", cancellationToken, container, language);
    }

    public async ValueTask SetTheme(string theme, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.setTheme", cancellationToken, theme);
    }

    public async ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default)
    {
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.disposeEditor", cancellationToken, container);
    }

    public async ValueTask UpdateContentHeight(ElementReference container, int? minLines = null, int? maxLines = null, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.updateContentHeight", cancellationToken, container, minLines, maxLines);
    }

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


