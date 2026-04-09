using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISliderInterop"/>
public sealed class SliderInterop : ISliderInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/sliderinterop.js";

    public SliderInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    private async ValueTask<IJSObjectReference> GetModule(CancellationToken cancellationToken)
    {
        return await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
        }
    }

    public async ValueTask<double> GetValueFromPointer(ElementReference track, double clientX, double clientY, double min, double max, string orientation,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
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
            IJSObjectReference module = await GetModule(linked);
            return await module.InvokeAsync<double>("startDrag", linked, track, pointerId, clientX, clientY, min, max, orientation, callbackReference, thumbIndex);
        }
    }

    public async ValueTask<bool> IsDirectionRtl(ElementReference track, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await GetModule(linked);
            return await module.InvokeAsync<bool>("isDirectionRtl", linked, track);
        }
    }

    public async ValueTask StopDrag(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await GetModule(linked);
            await module.InvokeVoidAsync("stopDrag", linked);
        }
    }

    public async ValueTask AttachKeyboardGuard(ElementReference sliderRoot, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await GetModule(linked);
            await module.InvokeVoidAsync("attachSliderKeyboardGuard", linked, sliderRoot);
        }
    }

    public async ValueTask DetachKeyboardGuard(ElementReference sliderRoot, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await GetModule(linked);
            await module.InvokeVoidAsync("detachSliderKeyboardGuard", linked, sliderRoot);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
