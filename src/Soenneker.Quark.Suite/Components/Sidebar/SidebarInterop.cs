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
    private readonly string _modulePath;

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly CancellationScope _cancellationScope = new();

    public SidebarInterop(IModuleImportUtil moduleImportUtil, QuarkOptions? quarkOptions = null)
    {
        _modulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/sidebarinterop.js", quarkOptions);
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask InitializeSidebar(DotNetObjectReference<SidebarProvider> componentRef, string shortcutKey, CancellationToken cancellationToken = default)
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

    public async ValueTask RegisterResizeHandle(ElementReference handle, DotNetObjectReference<SidebarResizeHandle> componentRef, double minWidth, double maxWidth, bool rightSide, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);
        using (source)
        {
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("registerResizeHandle", linked, handle, componentRef, minWidth, maxWidth, rightSide);
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
