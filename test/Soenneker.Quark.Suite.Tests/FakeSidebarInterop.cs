using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class FakeSidebarInterop : ISidebarInterop
{
    public bool SupportsSynchronousStorage { get; set; }
    public string? StoredWidth { get; set; }
    public bool TryGetStoredWidth(string storageKey, out string? width)
    {
        width = StoredWidth;
        return SupportsSynchronousStorage;
    }

    public int Registrations { get; private set; }
    public int Unregistrations { get; private set; }
    public ValueTask InitializeSidebar<T>(DotNetObjectReference<T> componentRef, string shortcutKey, CancellationToken cancellationToken = default) where T : class => ValueTask.CompletedTask;
    public ValueTask<bool?> GetSidebarState(string cookieKey, CancellationToken cancellationToken = default) => ValueTask.FromResult<bool?>(null);
    public ValueTask SaveSidebarState(string cookieKey, bool value, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask Cleanup(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    public ValueTask RegisterResizeHandle<T>(ElementReference handle, DotNetObjectReference<T> componentRef, double minWidth, double maxWidth, bool rightSide, CancellationToken cancellationToken = default) where T : class
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
