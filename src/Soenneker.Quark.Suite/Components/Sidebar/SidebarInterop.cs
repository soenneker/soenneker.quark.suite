using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

public sealed class SidebarInterop : ISidebarInterop
{
    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/sidebarinterop.js";

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public SidebarInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask InitializeSidebar<T>(DotNetObjectReference<T> componentRef, string shortcutKey, CancellationToken cancellationToken = default) where T : class
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("initializeSidebar", linked, componentRef, shortcutKey);
        }
    }

    public async ValueTask<bool?> GetSidebarState(string cookieKey, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<bool?>("getSidebarState", linked, cookieKey);
        }
    }

    public async ValueTask SaveSidebarState(string cookieKey, bool value, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("saveSidebarState", linked, cookieKey, value);
        }
    }

    public async ValueTask RegisterResizeHandle<T>(ElementReference handle, DotNetObjectReference<T> componentRef, double minWidth, double maxWidth, bool rightSide, string? storageKey = null, CancellationToken cancellationToken = default) where T : class
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("registerResizeHandle", linked, handle, componentRef, minWidth, maxWidth, rightSide, storageKey);
        }
    }

    public async ValueTask UnregisterResizeHandle(ElementReference handle, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("unregisterResizeHandle", linked, handle);
        }
    }

    public async ValueTask Cleanup(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("cleanup", linked);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
