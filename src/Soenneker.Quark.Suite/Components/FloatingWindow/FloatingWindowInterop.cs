using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using Soenneker.Utils.Json;

namespace Soenneker.Quark;

/// <inheritdoc cref="IFloatingWindowInterop"/>
public sealed class FloatingWindowInterop : IFloatingWindowInterop
{
    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/floatingwindowinterop.js";

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly AsyncInitializer<bool> _initializer;
    private readonly CancellationScope _cancellationScope = new();

    public FloatingWindowInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
        _initializer = new AsyncInitializer<bool>(InitializeResources);
    }

    private async ValueTask InitializeResources(bool useCdn, CancellationToken token)
    {
        await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
    }

    public async ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(useCdn, linked);
    }

    public async ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(options.UseCdn, linked);
            var json = JsonUtil.Serialize(options)!;
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("create", linked, id, json);
        }
    }

    public async ValueTask UpdateOptions(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var json = JsonUtil.Serialize(options)!;
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("updateOptions", linked, id, json);
        }
    }

    public async ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef, CancellationToken cancellationToken = default)
    {
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidAsync("setCallbacks", cancellationToken, id, dotNetRef);
    }

    public async ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("destroy", linked, id);
        }
    }

    public async ValueTask Show(string id, CancellationToken cancellationToken = default) => await Invoke(id, "show", cancellationToken);

    public async ValueTask Hide(string id, CancellationToken cancellationToken = default) => await Invoke(id, "hide", cancellationToken);

    public async ValueTask Toggle(string id, CancellationToken cancellationToken = default) => await Invoke(id, "toggle", cancellationToken);

    public async ValueTask Close(string id, CancellationToken cancellationToken = default) => await Invoke(id, "close", cancellationToken);

    public async ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<(int x, int y)>("getPosition", linked, id);
        }
    }

    public async ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setPosition", linked, id, x, y);
        }
    }

    public async ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<FloatingWindowSize>("getSize", linked, id);
        }
    }

    public async ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setSize", linked, id, width, height);
        }
    }

    public async ValueTask BringToFront(string id, CancellationToken cancellationToken = default) => await Invoke(id, "bringToFront", cancellationToken);

    public async ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<FloatingWindowSize>("getViewportSize", linked);
        }
    }

    public async ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default) => await Invoke(id, "centerInViewport", cancellationToken);

    private async ValueTask Invoke(string id, string identifier, CancellationToken cancellationToken)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync(identifier, linked, id);
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
