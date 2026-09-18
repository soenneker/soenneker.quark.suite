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

    public SidebarResizeTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddSingleton<ISidebarInterop>(_interop);
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
    public void Storage_key_is_forwarded_and_changing_it_reconfigures_the_handle()
    {
        var cut = Render<Sidebar>(p => p.Add(c => c.Resizable, true).Add(c => c.ResizeStorageKey, "sidebar-a"));
        _interop.StorageKey.Should().Be("sidebar-a");
        cut.Render(p => p.Add(c => c.ResizeStorageKey, "sidebar-b"));
        _interop.StorageKey.Should().Be("sidebar-b");
        _interop.Registrations.Should().Be(2);
        cut.Render(p => p.Add(c => c.ResizeStorageKey, (string?) null));
        _interop.StorageKey.Should().BeNull();
    }

    [Test]
    public void Invalid_limits_are_rejected()
    {
        Action render = () => Render<Sidebar>(p => p.Add(c => c.Resizable, true)
            .Add(c => c.MinResizeWidth, 500).Add(c => c.MaxResizeWidth, 200));
        render.Should().Throw<ArgumentOutOfRangeException>();
    }
}
