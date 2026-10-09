using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class FakeSidebarInterop : ISidebarInterop
{
    public int Registrations { get; private set; }
    public int Unregistrations { get; private set; }
    public ValueTask InitializeSidebar(DotNetObjectReference<SidebarProvider> componentRef, string shortcutKey, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask<bool?> GetSidebarState(string cookieKey, CancellationToken cancellationToken = default) => ValueTask.FromResult<bool?>(null);
    public ValueTask SaveSidebarState(string cookieKey, bool value, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask Cleanup(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    public ValueTask RegisterResizeHandle(ElementReference handle, DotNetObjectReference<SidebarResizeHandle> componentRef, double minWidth, double maxWidth, bool rightSide, CancellationToken cancellationToken = default)
    {
        Registrations++;
        return ValueTask.CompletedTask;
    }
    public ValueTask UnregisterResizeHandle(ElementReference handle, CancellationToken cancellationToken = default)
    {
        Unregistrations++;
        return ValueTask.CompletedTask;
    }
}
