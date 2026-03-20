using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISonnerInterop"/>
public sealed class SonnerInterop : ISonnerInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _stylePath = "/_content/Soenneker.Quark.Suite/css/sonner.css";
    private const string _modulePath = "Soenneker.Quark.Suite/js/sonnerinterop.js";
    private const string _moduleName = "SonnerInterop";

    public SonnerInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private ValueTask InitializeResources(CancellationToken token)
    {
        return InitializeResourcesInternal(token);
    }

    private async ValueTask InitializeResourcesInternal(CancellationToken token)
    {
        await _resourceLoader.LoadStyle(_stylePath, cancellationToken: token);
        await _resourceLoader.ImportModuleAndWaitUntilAvailable(_modulePath, _moduleName, 100, token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(linked);
    }

    public async ValueTask<Dictionary<string, double>> MeasureToastHeights(ElementReference section, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            return await _jsRuntime.InvokeAsync<Dictionary<string, double>>("SonnerInterop.measureToastHeights", linked, section);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_modulePath);
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
