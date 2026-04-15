using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISonnerInterop"/>
public sealed class SonnerInterop : ISonnerInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _stylePath = "_content/Soenneker.Quark.Suite/css/sonner.css";
    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/sonnerinterop.js";

    public SonnerInterop(IModuleImportUtil moduleImportUtil, IResourceLoader resourceLoader)
    {
        _moduleImportUtil = moduleImportUtil;
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private async ValueTask InitializeResources(CancellationToken token)
    {
        await _resourceLoader.LoadStyle(_stylePath, cancellationToken: token);
        await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
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
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<Dictionary<string, double>>("measureToastHeights", linked, section);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
