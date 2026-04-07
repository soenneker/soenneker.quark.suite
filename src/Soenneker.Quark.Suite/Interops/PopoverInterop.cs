using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IPopoverInterop"/>
public sealed class PopoverInterop : IPopoverInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "/_content/Soenneker.Quark.Suite/js/popoverinterop.js";
    private IJSObjectReference? _module;

    public PopoverInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private async ValueTask InitializeResources(CancellationToken token)
    {
        _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", token, _modulePath);
    }

    private async ValueTask<IJSObjectReference> GetModule(CancellationToken cancellationToken)
    {
        _module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken, _modulePath);
        return _module;
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _initializer.Init(linked);
    }

    public async ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content, string side, string align, int sideOffset = 4,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            await module.InvokeVoidAsync("observePosition", linked, popoverId, trigger, content, side, align, sideOffset);
        }
    }

    public async ValueTask StopObserving(string popoverId, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            await module.InvokeVoidAsync("stopObserving", linked, popoverId);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
            await _module.DisposeAsync();

        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
