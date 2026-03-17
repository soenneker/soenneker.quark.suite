using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IOverlayInterop"/>
public sealed class OverlayInterop : IOverlayInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "Soenneker.Quark.Suite/js/overlayinterop.js";
    private const string _moduleName = "OverlayInterop";

    public OverlayInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private ValueTask InitializeResources(CancellationToken token)
    {
        return _resourceLoader.ImportModuleAndWaitUntilAvailable(_modulePath, _moduleName, 100, token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(linked);
    }

    public async ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true,
        string? initialFocusSelector = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            await _jsRuntime.InvokeVoidAsync("OverlayInterop.activate", linked, overlayId, container, trapFocus, lockScroll, initialFocusSelector);
        }
    }

    public async ValueTask Deactivate(string overlayId, bool unlockScroll = true, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            await _jsRuntime.InvokeVoidAsync("OverlayInterop.deactivate", linked, overlayId, unlockScroll);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_modulePath);
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
