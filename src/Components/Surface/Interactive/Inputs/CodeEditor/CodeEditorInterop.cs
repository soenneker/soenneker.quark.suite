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

    private const string _monacoBaseCdn = "https://cdn.jsdelivr.net/npm/monaco-editor@0.52.0";
    private const string _monacoLoaderJs = _monacoBaseCdn + "/min/vs/loader.js";
    private const string _monacoEditorCss = _monacoBaseCdn + "/min/vs/editor/editor.main.css";

    public CodeEditorInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;

        _initializer = new AsyncSingleton(async (token, _) =>
        {
            // Load Monaco CSS (ensures fonts and base styles are available)
            await _resourceLoader.LoadStyle(_monacoEditorCss, cancellationToken: token);
            // Load AMD loader for Monaco
            await _resourceLoader.LoadScript(_monacoLoaderJs, cancellationToken: token);

            // Load our bootstrapper interop script (registered in _content path)
            await _resourceLoader.LoadScript("_content/Soenneker.Quark.Suite/js/monaco-interop.js", cancellationToken: token);

            // Initialize the AMD config for monaco if needed by our JS glue
            await _jsRuntime.InvokeVoidAsync("QuarkMonaco.ensureConfigured", token, _monacoBaseCdn + "/min");
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public async ValueTask CreateEditor(ElementReference container, string optionsJson, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync("QuarkMonaco.createEditor", cancellationToken, container, optionsJson);
    }

    public async ValueTask SetValue(ElementReference container, string value, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync("QuarkMonaco.setValue", cancellationToken, container, value);
    }

    public async ValueTask<string?> GetValue(ElementReference container, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        return await _jsRuntime.InvokeAsync<string?>("QuarkMonaco.getValue", cancellationToken, container);
    }

    public async ValueTask SetLanguage(ElementReference container, string language, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync("QuarkMonaco.setLanguage", cancellationToken, container, language);
    }

    public async ValueTask SetTheme(string theme, CancellationToken cancellationToken = default)
    {
        await _initializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync("QuarkMonaco.setTheme", cancellationToken, theme);
    }

    public async ValueTask DisposeEditor(ElementReference container, CancellationToken cancellationToken = default)
    {
        await _jsRuntime.InvokeVoidAsync("QuarkMonaco.disposeEditor", cancellationToken, container);
    }

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}


