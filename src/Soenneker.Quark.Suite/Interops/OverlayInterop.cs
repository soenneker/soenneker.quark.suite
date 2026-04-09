using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IOverlayInterop"/>
public sealed class OverlayInterop : IOverlayInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/overlayinterop.js";

    public OverlayInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
    }

    public async ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true,
        string? initialFocusSelector = null, DotNetObjectReference<OverlayElement>? escapeInvoker = null, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("activate", linked, overlayId, container, trapFocus, lockScroll, initialFocusSelector, escapeInvoker);
        }
    }

    public async ValueTask Deactivate(string overlayId, bool unlockScroll = true, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("deactivate", linked, overlayId, unlockScroll);
        }
    }

    public async ValueTask RegisterTooltipEscapeAsync(DotNetObjectReference<TooltipProvider> dotNetRef, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("registerTooltipEscape", linked, dotNetRef);
        }
    }

    public async ValueTask UnregisterTooltipEscapeAsync(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("unregisterTooltipEscape", linked);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
