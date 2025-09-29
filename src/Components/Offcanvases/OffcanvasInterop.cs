using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

/// <summary>
/// JS interop wrapper for Offcanvas overlay management.
/// </summary>
public sealed class OffcanvasInterop : IOffcanvasInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncSingleton _scriptInitializer;

    private const string _module = "Soenneker.Quark.Offcanvases/js/offcanvasinterop.js";
    private const string _moduleName = "OffcanvasInterop";

    public OffcanvasInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton(async (token, _) =>
        {
            await _resourceLoader.LoadStyle("_content/Soenneker.Quark.Offcanvases/css/offcanvas.css", cancellationToken: token);

            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_module, _moduleName, 100, token);

            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _scriptInitializer.Init(cancellationToken);

    public async ValueTask Create(string id, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.create", cancellationToken, id);
    }

    public async ValueTask Open(string id, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.open", cancellationToken, id);
    }

    public async ValueTask Close(string id, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.close", cancellationToken, id);
    }

    public ValueTask Destroy(string id, CancellationToken cancellationToken = default) =>
        _jsRuntime.InvokeVoidAsync($"{_moduleName}.dispose", cancellationToken, id);

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_module);
        await _scriptInitializer.DisposeAsync();
    }
}
