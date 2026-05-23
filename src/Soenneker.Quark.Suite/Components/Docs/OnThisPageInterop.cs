using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="IOnThisPageInterop"/>
public sealed class OnThisPageInterop : IOnThisPageInterop, IAsyncDisposable
{
    private const string ModulePath = "./_content/Soenneker.Quark.Suite/js/docsonthispageinterop.js";

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public OnThisPageInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask<OnThisPageTocItem[]> GetItems(object options, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await GetModule(linked);
            return await module.InvokeAsync<OnThisPageTocItem[]>("getItems", linked, options);
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
