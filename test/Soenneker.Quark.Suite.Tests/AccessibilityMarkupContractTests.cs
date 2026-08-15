using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.SimpleIcons.Enums.Icons;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Action_without_destination_uses_button_and_plain_anchor_uses_span()
    {
        var action = Render<Anchor>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => { }))
            .Add(p => p.ChildContent, "Action"));
        var plain = Render<Anchor>(parameters => parameters.Add(p => p.ChildContent, "Plain text"));

        action.Find("button").GetAttribute("type").Should().Be("button");
        plain.Find("span[data-slot='anchor']").TextContent.Should().Be("Plain text");
    }

    [Test]
    public void Pagination_without_destination_uses_button()
    {
        var cut = Render<PaginationLink>(parameters => parameters.Add(p => p.ChildContent, "2"));

        cut.Find("button[data-slot='pagination-link']").GetAttribute("type").Should().Be("button");
        cut.FindAll("a").Should().BeEmpty();
    }

    [Test]
    public void Button_defaults_to_non_submitting_type()
    {
        var cut = Render<Button>(parameters => parameters.Add(p => p.ChildContent, "Save"));

        cut.Find("button").GetAttribute("type").Should().Be("button");
    }

    [Test]
    public void Sortable_table_header_uses_native_button_and_sort_state()
    {
        var cut = Render<Th>(parameters => parameters
            .Add(p => p.Sortable, true)
            .Add(p => p.ChildContent, "Name"));
        var header = cut.Find("th");

        header.GetAttribute("scope").Should().Be("col");
        header.GetAttribute("aria-sort").Should().Be("none");
        cut.Find("th button[type='button']").TextContent.Should().Contain("Name");
    }

    [Test]
    public void Announcement_action_uses_native_button()
    {
        var cut = Render<Announcement>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => { }))
            .Add(p => p.ChildContent, "Read update"));

        var action = cut.Find("button[data-slot='announcement']");
        action.GetAttribute("type").Should().Be("button");
        action.HasAttribute("role").Should().BeFalse();
    }

    [Test]
    public void Decorative_and_named_icons_have_safe_accessibility_defaults()
    {
        var decorative = Render<SimpleIcon>(parameters => parameters.Add(p => p.Name, SimpleIconEnum.Github));
        var named = Render<SimpleIcon>(parameters => parameters
            .Add(p => p.Name, SimpleIconEnum.Github)
            .Add(p => p.AriaLabel, "Expand"));

        decorative.Find("svg").GetAttribute("aria-hidden").Should().Be("true");
        decorative.Find("svg").GetAttribute("focusable").Should().Be("false");
        named.Find("svg").GetAttribute("role").Should().Be("img");
        named.Find("svg").HasAttribute("aria-hidden").Should().BeFalse();
    }

    [Test]
    public void Tree_uses_roving_tab_stop_and_exposes_hierarchy()
    {
        var cut = Render<Tree>(parameters => parameters.Add(p => p.ChildContent, BuildTreeItems()));
        var items = cut.FindAll("[role='treeitem']");

        items.Should().HaveCount(2);
        System.Linq.Enumerable.Count(items, item => item.GetAttribute("tabindex") == "0").Should().Be(1);
        items[0].GetAttribute("aria-level").Should().Be("1");
        items[1].GetAttribute("aria-level").Should().Be("2");
        items[1].GetAttribute("aria-posinset").Should().Be("1");
        items[1].GetAttribute("aria-setsize").Should().Be("1");
    }

    [Test]
    public void Header_mobile_trigger_automatically_controls_sidebar_panel()
    {
        var cut = Render<SidebarProvider>(parameters => parameters
            .Add(p => p.IsMobile, true)
            .Add(p => p.OpenMobile, true)
            .Add(p => p.ChildContent, BuildHeaderAndSidebar()));

        var trigger = cut.Find("header button[data-slot='sidebar-trigger']");
        var panel = cut.Find("[data-mobile='true']");

        trigger.GetAttribute("aria-expanded").Should().Be("true");
        trigger.GetAttribute("aria-controls").Should().Be(panel.Id);
    }

    private static RenderFragment BuildTreeItems() => builder =>
    {
        builder.OpenComponent<TreeItem>(0);
        builder.AddAttribute(1, nameof(TreeItem.Level), 0);
        builder.AddAttribute(2, nameof(TreeItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Root")));
        builder.CloseComponent();

        builder.OpenComponent<TreeItem>(3);
        builder.AddAttribute(4, nameof(TreeItem.Level), 1);
        builder.AddAttribute(5, nameof(TreeItem.PositionInSet), 1);
        builder.AddAttribute(6, nameof(TreeItem.SetSize), 1);
        builder.AddAttribute(7, nameof(TreeItem.ChildContent), (RenderFragment)(content => content.AddContent(0, "Child")));
        builder.CloseComponent();
    };

    private static RenderFragment BuildHeaderAndSidebar() => builder =>
    {
        builder.OpenComponent<Soenneker.Quark.Header.Header>(0);
        builder.AddAttribute(1, nameof(Soenneker.Quark.Header.Header.ShowSidebarTrigger), true);
        builder.CloseComponent();

        builder.OpenComponent<Sidebar>(2);
        builder.AddAttribute(3, nameof(Sidebar.ChildContent), (RenderFragment)(content => content.AddContent(0, "Navigation")));
        builder.CloseComponent();
    };
}
