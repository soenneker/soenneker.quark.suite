using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISliderInterop"/>
public sealed class SliderInterop : ISliderInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "/_content/Soenneker.Quark.Suite/js/sliderinterop.js";
    private IJSObjectReference? _module;

    public SliderInterop(IJSRuntime jsRuntime)
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
        {
            await _initializer.Init(linked);
        }
    }

    public async ValueTask<double> GetValueFromPointer(ElementReference track, double clientX, double clientY, double min, double max, string orientation,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            return await module.InvokeAsync<double>("getValueFromPointer", linked, track, clientX, clientY, min, max, orientation);
        }
    }

    public async ValueTask<double> StartDrag(ElementReference track, long pointerId, double clientX, double clientY, double min, double max, string orientation,
        DotNetObjectReference<Slider> callbackReference, int thumbIndex, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            return await module.InvokeAsync<double>("startDrag", linked, track, pointerId, clientX, clientY, min, max, orientation, callbackReference, thumbIndex);
        }
    }

    public async ValueTask StopDrag(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await GetModule(linked);
            await module.InvokeVoidAsync("stopDrag", linked);
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
