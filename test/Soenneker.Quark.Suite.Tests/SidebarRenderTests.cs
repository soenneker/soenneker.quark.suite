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

    [Test]
    public void Sidebar_rail_hit_target_stays_outside_sidebar_content()
    {
        var cut = Render<SidebarRail>();

        var rail = cut.Find("[data-sidebar='rail']");
        var cls = rail.GetAttribute("class");

        cls.Should().Contain("group-data-[side=left]:-right-4");
        cls.Should().Contain("group-data-[side=right]:-left-4");
        cls.Should().NotContain("-translate-x-1/2");
    }
    [Test]
    [Arguments(false, "normal")]
    [Arguments(true, "normal")]
    [Arguments(false, "opt-out")]
    [Arguments(true, "opt-out")]
    [Arguments(false, "ctrl")]
    [Arguments(true, "ctrl")]
    [Arguments(false, "meta")]
    [Arguments(true, "meta")]
    [Arguments(false, "shift")]
    [Arguments(true, "shift")]
    [Arguments(false, "alt")]
    [Arguments(true, "alt")]
    [Arguments(false, "middle")]
    [Arguments(true, "middle")]
    [Arguments(false, "blank")]
    [Arguments(true, "blank")]
    [Arguments(false, "self")]
    [Arguments(true, "self")]
    public async System.Threading.Tasks.Task Sidebar_navigation_click_respects_dismissal_intent(bool submenu, string scenario)
    {
        var clicks = 0;
        var closes = 0;
        var cut = Render<SidebarProvider>(parameters => parameters
            .Add(component => component.IsMobile, true)
            .Add(component => component.OpenMobile, true)
            .Add(component => component.PersistState, false)
            .Add(component => component.CloseMobileOnNavigation, scenario != "opt-out")
            .Add(component => component.OpenMobileChanged, value => { if (!value) closes++; })
            .Add(component => component.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent(0, submenu ? typeof(SidebarMenuSubButton) : typeof(SidebarMenuButton));
                builder.AddAttribute(1, "Href", "/destination");
                if (scenario is "blank" or "self")
                    builder.AddAttribute(2, "Target", (object)(scenario == "blank" ? Target.Blank : Target.Self));
                builder.AddAttribute(3, "OnClick", EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, _ => clicks++));
                builder.AddAttribute(4, "ChildContent", (RenderFragment)(content => content.AddContent(0, "Navigate")));
                builder.CloseComponent();
            })));

        await cut.Find("a").ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs
        {
            Button = scenario == "middle" ? 1 : 0,
            CtrlKey = scenario == "ctrl",
            MetaKey = scenario == "meta",
            ShiftKey = scenario == "shift",
            AltKey = scenario == "alt"
        });

        closes.Should().Be(scenario is "normal" or "self" ? 1 : 0);
        clicks.Should().Be(1);
    }
    [Test]
    [Arguments("default")]
    [Arguments("sm")]
    [Arguments("lg")]
    public void Sidebar_menu_size_has_one_collapsed_padding_rule(string size)
    {
        var cut = Render<SidebarMenuButton>(parameters => parameters.Add(component => component.ButtonSize,
            size == "lg" ? SidebarMenuButtonSize.Lg : size == "sm" ? SidebarMenuButtonSize.Sm : SidebarMenuButtonSize.Default));

        var classes = cut.Find("button").ClassList;
        classes.Should().Contain(size == "lg" ? "group-data-[collapsible=icon]:p-0!" : "group-data-[collapsible=icon]:p-2!");
        classes.Should().NotContain(size == "lg" ? "group-data-[collapsible=icon]:p-2!" : "group-data-[collapsible=icon]:p-0!");
    }

    [Test]
    [Arguments("default")]
    [Arguments("outline")]
    [Arguments("navigation")]
    public void Sidebar_menu_variant_defaults_are_applied(string variant)
    {
        var cut = Render<SidebarMenuButton>(parameters => parameters.Add(component => component.Variant,
            variant == "outline" ? SidebarMenuButtonVariant.Outline : variant == "navigation" ? SidebarMenuButtonVariant.Navigation : SidebarMenuButtonVariant.Default));

        var classes = cut.Find("button").ClassList;
        classes.Should().Contain(variant == "outline" ? "bg-background" : "bg-transparent");
        classes.Should().Contain(variant == "navigation" ? "w-fit" : "w-full");
        classes.Should().Contain(variant == "navigation" ? "overflow-visible" : "overflow-hidden");
    }

    [Test]
    [Arguments("outline")]
    [Arguments("navigation")]
    public void Sidebar_menu_variant_preserves_explicit_styles(string variant)
    {
        var cut = Render<SidebarMenuButton>(parameters => parameters
            .Add(component => component.Variant, variant == "outline" ? SidebarMenuButtonVariant.Outline : SidebarMenuButtonVariant.Navigation)
            .Add(component => component.Width, Width.IsFull)
            .Add(component => component.Overflow, Overflow.Hidden)
            .Add(component => component.BackgroundColor, BackgroundColor.Muted));

        var classes = cut.Find("button").ClassList;
        classes.Should().Contain("w-full");
        classes.Should().Contain("overflow-hidden");
        classes.Should().Contain("bg-muted");
        classes.Should().NotContain("w-fit");
        classes.Should().NotContain("overflow-visible");
        classes.Should().NotContain("bg-background");
    }
}
