using AwesomeAssertions;
using Bunit;
using System.Collections.Generic;
using System.Linq;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void NavigationLinks_header_mode_renders_button_like_links_with_active_semantics()
    {
        var items = new List<NavigationItem>
        {
            new("Docs", "/docs", IsNew: true),
            new("Components", "/components"),
            new("Disabled")
        };

        var cut = Render<NavigationLinks>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Mode, NavigationLinksMode.Header)
            .Add(p => p.IsActive, item => item.Title == "Docs"));

        var links = cut.FindAll("a[data-slot='button']");
        links.Count.Should().Be(2);

        links[0].GetAttribute("href").Should().Be("/docs");
        links[0].GetAttribute("data-variant").Should().Be("ghost");
        links[0].GetAttribute("data-size").Should().Be("sm");
        links[0].GetAttribute("data-active").Should().Be("true");
        links[0].GetAttribute("data-new").Should().Be("true");
        links[0].GetAttribute("aria-current").Should().Be("page");
        links[0].GetAttribute("class").Should().ContainAll("inline-flex", "h-8", "rounded-md", "bg-accent", "focus-visible:ring-[3px]");

        links[1].GetAttribute("data-active").Should().Be("false");
        links[1].HasAttribute("aria-current").Should().BeFalse();

        var disabled = cut.Find("span[data-slot='button']");
        disabled.GetAttribute("data-active").Should().Be("false");
        disabled.GetAttribute("class").Should().Contain("opacity-75");
    }

    [Test]
    public void NavigationLinks_stack_mode_renders_group_title_new_marker_and_active_page()
    {
        var items = new List<NavigationItem>
        {
            new("Introduction", "/docs", IsNew: true),
            new("Disabled")
        };

        var cut = Render<NavigationLinks>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Mode, NavigationLinksMode.Stack)
            .Add(p => p.Title, "Menu")
            .Add(p => p.IsActive, item => item.Title == "Introduction"));

        cut.Markup.Should().Contain("Menu");

        var active = cut.Find("a[href='/docs']");
        active.GetAttribute("aria-current").Should().Be("page");
        active.GetAttribute("class").Should().ContainAll("inline-flex", "text-2xl", "font-medium", "text-foreground");
        active.QuerySelector("span[title='New']").Should().NotBeNull();

        var disabled = cut.FindAll("span").Single(span => span.TextContent.Trim() == "Disabled");
        disabled.GetAttribute("class").Should().Contain("opacity-70");
    }

    [Test]
    public void NavigationLinks_sidebar_mode_renders_sidebar_menu_contract()
    {
        var items = new List<NavigationItem>
        {
            new("Components", "/components", IsNew: true),
            new("Disabled")
        };

        var cut = Render<NavigationLinks>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.Mode, NavigationLinksMode.Sidebar)
            .Add(p => p.Title, "Docs")
            .Add(p => p.GroupClass, "pt-4")
            .Add(p => p.IsActive, item => item.Title == "Components"));

        cut.Find("[data-sidebar='group']").GetAttribute("class").Should().Contain("pt-4");
        cut.Find("[data-sidebar='group-label']").TextContent.Should().Be("Docs");

        var active = cut.Find("a[data-sidebar='menu-button']");
        active.GetAttribute("href").Should().Be("/components");
        active.GetAttribute("data-active").Should().Be("true");
        active.GetAttribute("aria-current").Should().Be("page");
        active.GetAttribute("class").Should().Contain("data-[active=true]:bg-sidebar-accent");
        active.QuerySelector("span[title='New']").Should().NotBeNull();

        var disabled = cut.Find("button[data-sidebar='menu-button'][aria-disabled='true']");
        disabled.GetAttribute("tabindex").Should().Be("-1");
        disabled.GetAttribute("class").Should().Contain("opacity-70");
    }
}
