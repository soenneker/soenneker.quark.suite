using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="IResizableInterop"/>
public sealed class ResizableInterop : IResizableInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/resizableinterop.js";

    public ResizableInterop(IModuleImportUtil moduleImportUtil)
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
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("initialize", linked);
        }
    }

    public async ValueTask RegisterHandle(ElementReference handle, ElementReference group, string orientation,
        DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await GetModule(linked);
            await module.InvokeVoidAsync("registerHandle", linked, handle, group, orientation, callbackReference, handleIndex);
        }
    }

    public async ValueTask UnregisterHandle(ElementReference handle, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await GetModule(linked);
            await module.InvokeVoidAsync("unregisterHandle", linked, handle);
        }
    }

    public async ValueTask StartDrag(ElementReference group, long pointerId, double clientX, double clientY, string orientation,
        DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await GetModule(linked);
            await module.InvokeVoidAsync("startDrag", linked, group, pointerId, clientX, clientY, orientation, callbackReference, handleIndex);
        }
    }

    public async ValueTask StopDrag(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await GetModule(linked);
            await module.InvokeVoidAsync("stopDrag", linked);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
