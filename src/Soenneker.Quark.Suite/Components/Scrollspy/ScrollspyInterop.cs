using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="IScrollspyInterop"/>
public sealed class ScrollspyInterop : IScrollspyInterop, IAsyncDisposable
{
    private const string ModulePath = "./_content/Soenneker.Quark.Suite/js/scrollspyinterop.js";

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public ScrollspyInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Initialize(ElementReference element, object options, DotNetObjectReference<Scrollspy> callbackReference,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await GetModule(linked);
            await module.InvokeVoidAsync("initialize", linked, element, options, callbackReference);
        }
    }

    public async ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await GetModule(linked);
            await module.InvokeVoidAsync("destroy", linked, element);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(ModulePath);
    }

    private async ValueTask<IJSObjectReference> GetModule(CancellationToken cancellationToken)
    {
        return await _moduleImportUtil.GetContentModuleReference(ModulePath, cancellationToken);
    }
}
