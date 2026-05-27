using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Navigation_shell_static_sidebar_is_viewport_constrained()
    {
        var cut = Render<Sidebar>(parameters => parameters
            .Add(component => component.Collapsible, SidebarCollapsible.None)
            .Add(component => component.Shell, SidebarShell.Navigation)
            .Add(component => component.ChildContent, (RenderFragment) (builder =>
            {
                builder.OpenComponent<SidebarContent>(0);
                builder.AddAttribute(1, nameof(SidebarContent.ShowFade), true);
                builder.AddAttribute(2, nameof(SidebarContent.ChildContent), (RenderFragment) (contentBuilder => contentBuilder.AddContent(0, "Navigation")));
                builder.CloseComponent();
            })));

        var sidebar = cut.Find("aside[data-sidebar='sidebar'][data-shell='navigation']");
        var cls = sidebar.GetAttribute("class");
        var style = sidebar.GetAttribute("style");

        cls.Should().Contain("sticky");
        cls.Should().Contain("self-start");
        cls.Should().Contain("overflow-hidden");
        style.Should().Contain("top: var(--header-height, 0px)");
        style.Should().Contain("height: calc(100svh - var(--header-height, 0px))");
        style.Should().Contain("max-height: calc(100svh - var(--header-height, 0px))");
    }
}
