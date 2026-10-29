using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

public sealed class ResizableInterop : IResizableInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    private readonly string _modulePath;

    public ResizableInterop(IModuleImportUtil moduleImportUtil, QuarkOptions? quarkOptions = null)
    {
        _modulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/resizableinterop.js", quarkOptions);
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
        }
    }

    public async ValueTask RegisterHandle(ElementReference handle, ElementReference group, string orientation,
        DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeQuarkVoidAsync("registerHandle", linked, handle, group, orientation, callbackReference, handleIndex);
        }
    }

    public async ValueTask UnregisterHandle(ElementReference handle, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeQuarkVoidAsync("unregisterHandle", linked, handle);
        }
    }

    public async ValueTask StartDrag(ElementReference group, long pointerId, double clientX, double clientY, string orientation,
        DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeQuarkVoidAsync("startDrag", linked, group, pointerId, clientX, clientY, orientation, callbackReference, handleIndex);
        }
    }

    public async ValueTask StopDrag(ElementReference group, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeQuarkVoidAsync("stopDrag", linked, group);
        }
    }

    public async ValueTask StopDrag(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeQuarkVoidAsync("stopDrag", linked);
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
