using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IPopoverInterop"/>
public sealed class PopoverInterop : IPopoverInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/popoverinterop.js";

    public PopoverInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
    }

    public async ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content,
        string side, string align, int sideOffset = 4, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("observePosition", linked, popoverId, trigger, content, null, side, align, sideOffset);
        }
    }

    public async ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content,
        DotNetObjectReference<PopoverOutsideCloseProxy> callbackReference, string side, string align, int sideOffset = 4,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("observePosition", linked, popoverId, trigger, content, callbackReference, side, align, sideOffset);
        }
    }

    public async ValueTask StopObserving(string popoverId, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("stopObserving", linked, popoverId);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
