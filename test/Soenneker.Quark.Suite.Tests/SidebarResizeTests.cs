using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Quark.Dtos;

namespace Soenneker.Quark.Suite.Tests;

public sealed class SidebarResizeTests : BunitContext
{
    private readonly FakeSidebarInterop _interop = new();
    private readonly FakeSidebarLocalStorageUtil _storage = new();

    public SidebarResizeTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddSingleton<ISidebarInterop>(_interop);
        Services.AddSingleton<Soenneker.Blazor.Utils.LocalStorage.Abstract.ILocalStorageUtil>(_storage);
    }

    [Test]
    [Arguments("320", 320d)]
    [Arguments("900", 480d)]
    [Arguments("bad", null)]
    [Arguments(null, null)]
    public void Synchronous_storage_restores_width_in_the_first_render(string? saved, double? expectedWidth)
    {
        _interop.SupportsSynchronousStorage = true;
        _interop.StoredWidth = saved;
        double? changed = null;
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ResizeStorageKey, "sidebar-a")
            .Add(c => c.ExpandedWidthChanged, value => changed = value));
        cut.Instance.ExpandedWidth.Should().Be(expectedWidth);
        changed.Should().Be(expectedWidth);
        cut.RenderCount.Should().Be(1);
        cut.FindAll("[data-sidebar-resize-root]").Count.Should().Be(1);
        _storage.Reads.Should().Be(0);
    }

    [Test]
    [Arguments("320", "320px")]
    [Arguments(null, null)]
    [Arguments("bad", null)]
    public async Task Async_storage_does_not_render_a_temporary_sidebar(string? saved, string? expectedWidth)
    {
        _storage.PendingRead = new TaskCompletionSource<string?>();
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ResizeStorageKey, "sidebar-a"));
        cut.FindAll("[data-sidebar-resize-root]").Should().BeEmpty();
        _interop.Registrations.Should().Be(0);

        await cut.InvokeAsync(() => _storage.PendingRead.SetResult(saved));
        cut.WaitForAssertion(() => cut.FindAll("[data-sidebar-resize-root]").Count.Should().Be(1));
        if (expectedWidth is not null)
            cut.Find("[data-sidebar-resize-root]").GetAttribute("style").Should().Contain(expectedWidth);
        cut.FindAll("[data-sidebar='resize-handle']").Count.Should().Be(1);
    }

    [Test]
    public async Task Failed_async_storage_reveals_the_default_sidebar()
    {
        _storage.PendingRead = new TaskCompletionSource<string?>();
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ResizeStorageKey, "sidebar-a"));
        cut.FindAll("[data-sidebar-resize-root]").Should().BeEmpty();
        await cut.InvokeAsync(() => _storage.PendingRead.SetException(new Microsoft.JSInterop.JSException("Storage is blocked")));
        cut.WaitForAssertion(() => cut.FindAll("[data-sidebar='resize-handle']").Count.Should().Be(1));
    }

    [Test]
    public void Resizing_is_opt_in()
    {
        var cut = Render<Sidebar>();
        cut.FindAll("[data-sidebar='resize-handle']").Should().BeEmpty();
        _interop.Registrations.Should().Be(0);
    }

    [Test]
    public async Task Width_callback_updates_local_width_and_clamps_to_limits()
    {
        double? changed = null;
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.ExpandedWidthChanged, value => changed = value));
        var handle = cut.FindComponent<SidebarResizeHandle>();
        await cut.InvokeAsync(() => handle.Instance.OnWidthChanged(900));
        changed.Should().Be(480);
        cut.Find("[data-sidebar-resize-root]").GetAttribute("style").Should().Contain("--sidebar-width: 480px");
        _interop.Registrations.Should().Be(1);
        await cut.InvokeAsync(() => handle.Instance.OnWidthChanged(double.NaN));
        changed.Should().Be(480);
    }

    [Test]
    public void Collapsed_sidebar_has_no_resize_handle_and_retains_expanded_width()
    {
        var state = new SidebarContextState { Open = false, OpenMobile = false, IsMobile = false };
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.ExpandedWidth, 320d).AddCascadingValue("SidebarContextState", state));
        cut.FindAll("[data-sidebar='resize-handle']").Should().BeEmpty();
        cut.Find("[data-sidebar-resize-root]").GetAttribute("style").Should().Contain("320px");
    }

    [Test]
    public void Static_sidebar_can_resize_even_when_provider_is_collapsed()
    {
        var state = new SidebarContextState { Open = false, OpenMobile = false, IsMobile = false };
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.Collapsible, SidebarCollapsible.None).AddCascadingValue("SidebarContextState", state));
        cut.Find("aside [data-sidebar='resize-handle']").GetAttribute("role").Should().Be("separator");
    }

    [Test]
    public void Mobile_static_sidebar_does_not_resize()
    {
        var state = new SidebarContextState { Open = true, OpenMobile = false, IsMobile = true };
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.Collapsible, SidebarCollapsible.None).AddCascadingValue("SidebarContextState", state));
        cut.FindAll("[data-sidebar='resize-handle']").Should().BeEmpty();
    }

    [Test]
    public void Disabling_resizing_cleans_up_handle_and_retains_width()
    {
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ExpandedWidth, 320d));
        cut.Render(p => p.Add(c => c.Resizable, false));
        cut.FindAll("[data-sidebar='resize-handle']").Should().BeEmpty();
        _interop.Unregistrations.Should().Be(1);
        cut.Find("[data-sidebar-resize-root]").GetAttribute("style").Should().Contain("320px");
    }

    [Test]
    public async Task Storage_restores_clamps_and_persists_through_the_utility()
    {
        _storage.Values["sidebar-a"] = "900";
        double? changed = null;
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.ResizeStorageKey, "sidebar-a").Add(c => c.ExpandedWidthChanged, value => changed = value));
        cut.WaitForAssertion(() => changed.Should().Be(480));
        cut.WaitForAssertion(() => cut.Find("[data-sidebar-resize-root]").GetAttribute("style").Should().Contain("480px"));
        var handle = cut.FindComponent<SidebarResizeHandle>();
        await cut.InvokeAsync(() => handle.Instance.OnWidthChanged(300));
        _storage.Values["sidebar-a"].Should().Be("300");
        _storage.Reads.Should().Be(1);
        var next = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ResizeStorageKey, "sidebar-a"));
        next.WaitForAssertion(() => next.Instance.ExpandedWidth.Should().Be(300));
    }

    [Test]
    public void Changing_storage_keys_restores_the_matching_preference()
    {
        _storage.Values["sidebar-a"] = "300";
        _storage.Values["sidebar-b"] = "400";
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ResizeStorageKey, "sidebar-a"));
        cut.WaitForAssertion(() => cut.Instance.ExpandedWidth.Should().Be(300));
        cut.Render(p => p.Add(c => c.ResizeStorageKey, "sidebar-b"));
        cut.WaitForAssertion(() => cut.Instance.ExpandedWidth.Should().Be(400));
        _storage.Reads.Should().Be(2);
    }

    [Test]
    [Arguments("")]
    [Arguments("bad")]
    [Arguments("NaN")]
    [Arguments("Infinity")]
    [Arguments("-1")]
    [Arguments("0")]
    public void Invalid_stored_width_is_ignored(string value)
    {
        _storage.Values["sidebar-a"] = value;
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ResizeStorageKey, "sidebar-a"));
        cut.WaitForAssertion(() => _storage.Reads.Should().Be(1));
        cut.Instance.ExpandedWidth.Should().BeNull();
    }

    [Test]
    public async Task Storage_failures_do_not_break_resizing_or_callbacks()
    {
        _storage.Unavailable = true;
        double? changed = null;
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.ResizeStorageKey, "sidebar-a").Add(c => c.ExpandedWidthChanged, value => changed = value));
        await cut.InvokeAsync(() => cut.FindComponent<SidebarResizeHandle>().Instance.OnWidthChanged(320));
        changed.Should().Be(320);
        cut.Instance.ExpandedWidth.Should().Be(320);
    }

    [Test]
    public async Task No_key_does_not_access_storage()
    {
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true));
        await cut.InvokeAsync(() => cut.FindComponent<SidebarResizeHandle>().Instance.OnWidthChanged(320));
        _storage.Reads.Should().Be(0);
        _storage.Writes.Should().Be(0);
    }

    [Test]
    public void Invalid_limits_are_rejected()
    {
        Action render = () => Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.MinResizeWidth, 500).Add(c => c.MaxResizeWidth, 200));
        render.Should().Throw<ArgumentOutOfRangeException>();
    }
}
