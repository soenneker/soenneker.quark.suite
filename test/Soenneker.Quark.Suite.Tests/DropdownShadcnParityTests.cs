using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Dropdown_leaf_slots_match_shadcn_base_classes()
    {
        var sub = Render<DropdownSub>(parameters => parameters.Add(p => p.ChildContent, "Sub"));
        var subClasses = sub.Find("[data-slot='dropdown-menu-sub']").GetAttribute("class")!;
        subClasses.Should().Contain("relative");
        subClasses.Should().NotContain("q-dropdown-sub");
    }

    [Test]
    public void DropdownItem_matches_shadcn_v4_item_classes()
    {
        var item = RenderOpenDropdownWithMenu(builder =>
        {
            builder.OpenComponent<DropdownItem>(0);
            builder.AddAttribute(1, nameof(DropdownItem.Indented), true);
            builder.AddAttribute(2, nameof(DropdownItem.Variant), "destructive");
            builder.AddAttribute(3, nameof(DropdownItem.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Delete")));
            builder.CloseComponent();
        });

        var classes = item.Find("[data-slot='dropdown-menu-item']").GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("flex");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("rounded-sm");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("outline-hidden");
        classes.Should().Contain("focus:bg-accent");
        classes.Should().Contain("data-[disabled]:pointer-events-none");
        classes.Should().Contain("data-[disabled]:opacity-50");
        classes.Should().Contain("data-[inset]:pl-8");
        classes.Should().Contain("data-[variant=destructive]:text-destructive");
        classes.Should().Contain("[&_svg:not([class*='text-'])]:text-muted-foreground");
        classes.Should().NotContain("data-disabled:");
        classes.Should().NotContain("data-inset:pl-7");
    }

    [Test]
    public void DropdownItem_forwards_radix_close_on_select_option()
    {
        var source = ReadDropdownSource("DropdownItem.razor");

        source.Should().Contain("CloseOnSelect=\"@CloseOnSelect\"");
        source.Should().Contain("public bool CloseOnSelect { get; set; } = true;");
    }

    [Test]
    public void DropdownCheckboxItem_matches_shadcn_v4_indicator_and_item_classes()
    {
        var item = RenderOpenDropdownWithMenu(builder =>
        {
            builder.OpenComponent<DropdownCheckboxItem>(0);
            builder.AddAttribute(1, nameof(DropdownCheckboxItem.Checked), true);
            builder.AddAttribute(2, nameof(DropdownCheckboxItem.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Status bar")));
            builder.CloseComponent();
        });

        var classes = item.Find("[data-slot='dropdown-menu-checkbox-item']").GetAttribute("class")!;
        var indicator = item.Find("[data-slot='dropdown-menu-checkbox-item-indicator']");
        var indicatorClasses = indicator.GetAttribute("class")!;
        var iconClasses = indicator.QuerySelector("svg")!.GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("flex");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("rounded-sm");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("pr-2");
        classes.Should().Contain("pl-8");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("outline-hidden");
        classes.Should().Contain("focus:bg-accent");
        classes.Should().Contain("data-[disabled]:pointer-events-none");
        classes.Should().NotContain("data-disabled:");

        indicatorClasses.Should().Contain("absolute");
        indicatorClasses.Should().Contain("left-2");
        indicatorClasses.Should().Contain("size-3.5");
        indicatorClasses.Should().NotContain("right-2");
        iconClasses.Should().Contain("size-4");
    }

    [Test]
    public void DropdownRadioItem_matches_shadcn_v4_indicator_and_item_classes()
    {
        var item = RenderOpenDropdownWithMenu(builder =>
        {
            builder.OpenComponent<DropdownRadioItem>(0);
            builder.AddAttribute(1, nameof(DropdownRadioItem.Selected), true);
            builder.AddAttribute(2, nameof(DropdownRadioItem.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Top")));
            builder.CloseComponent();
        });

        var classes = item.Find("[data-slot='dropdown-menu-radio-item']").GetAttribute("class")!;
        var indicator = item.Find("[data-slot='dropdown-menu-radio-item-indicator']");
        var indicatorClasses = indicator.GetAttribute("class")!;
        var iconClasses = indicator.QuerySelector("svg")!.GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("flex");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("rounded-sm");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("pr-2");
        classes.Should().Contain("pl-8");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("outline-hidden");
        classes.Should().Contain("focus:bg-accent");
        classes.Should().Contain("data-[disabled]:pointer-events-none");
        classes.Should().NotContain("data-disabled:");

        indicatorClasses.Should().Contain("absolute");
        indicatorClasses.Should().Contain("left-2");
        indicatorClasses.Should().Contain("size-3.5");
        indicatorClasses.Should().NotContain("right-2");
        iconClasses.Should().Contain("size-2");
        iconClasses.Should().Contain("fill-current");
    }

    [Test]
    public void DropdownSubTrigger_matches_shadcn_v4_state_and_icon_classes()
    {
        var trigger = RenderOpenDropdownWithMenu(builder =>
        {
            builder.OpenComponent<DropdownSub>(0);
            builder.AddAttribute(1, nameof(DropdownSub.Open), true);
            builder.AddAttribute(2, nameof(DropdownSub.DefaultOpen), true);
            builder.AddAttribute(3, nameof(DropdownSub.ChildContent), (RenderFragment)(subBuilder =>
            {
                subBuilder.OpenComponent<DropdownSubTrigger>(0);
                subBuilder.AddAttribute(1, nameof(DropdownSubTrigger.Indented), true);
                subBuilder.AddAttribute(2, nameof(DropdownSubTrigger.Open), true);
                subBuilder.AddAttribute(3, nameof(DropdownSubTrigger.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "More Tools")));
                subBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        });

        var classes = trigger.Find("[data-slot='dropdown-menu-sub-trigger']").GetAttribute("class")!;
        var iconClasses = trigger.Find("svg").GetAttribute("class")!;

        classes.Should().Contain("rounded-sm");
        classes.Should().Contain("px-2");
        classes.Should().Contain("py-1.5");
        classes.Should().Contain("data-[inset]:pl-8");
        classes.Should().Contain("data-[state=open]:bg-accent");
        classes.Should().Contain("data-[state=open]:text-accent-foreground");
        classes.Should().Contain("[&_svg:not([class*='text-'])]:text-muted-foreground");
        classes.Should().NotContain("data-inset:pl-7");

        iconClasses.Should().Contain("ml-auto");
        iconClasses.Should().Contain("size-4");
        iconClasses.Should().NotContain("cn-rtl-flip");
    }

    [Test]
    public void DropdownSubContent_matches_shadcn_v4_positioning_and_animation_classes()
    {
        var content = RenderOpenDropdownWithMenu(builder =>
        {
            builder.OpenComponent<DropdownSub>(0);
            builder.AddAttribute(1, nameof(DropdownSub.ChildContent), (RenderFragment)(subBuilder =>
            {
                subBuilder.OpenComponent<DropdownSubTrigger>(0);
                subBuilder.AddAttribute(1, nameof(DropdownSubTrigger.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "More Tools")));
                subBuilder.CloseComponent();
                subBuilder.OpenComponent<DropdownSubContent>(2);
                subBuilder.AddAttribute(3, nameof(DropdownSubContent.ForceMount), true);
                subBuilder.AddAttribute(4, nameof(DropdownSubContent.ChildContent), (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Submenu")));
                subBuilder.CloseComponent();
            }));
            builder.CloseComponent();
        });

        var classes = content.Find("[data-slot='dropdown-menu-sub-content']").GetAttribute("class")!;

        classes.Should().Contain("z-50");
        classes.Should().Contain("min-w-[8rem]");
        classes.Should().Contain("origin-(--radix-dropdown-menu-content-transform-origin)");
        classes.Should().Contain("shadow-lg");
        classes.Should().Contain("data-[state=closed]:animate-out");
        classes.Should().Contain("data-[state=open]:animate-in");
        classes.Should().Contain("data-[side=right]:slide-in-from-left-2");
        classes.Should().NotContain("min-w-[96px]");
        classes.Should().NotContain("ring-1");
        classes.Should().NotContain("data-open:");
        classes.Should().NotContain("data-closed:");
    }

    [Test]
    public void DropdownMenu_content_exposes_radix_popper_collision_options()
    {
        var cut = Render<DropdownMenu>(parameters => parameters
            .Add(p => p.CollisionBoundarySelector, "#dropdown-boundary-a")
            .Add(p => p.CollisionBoundarySelectors, new[] { "#dropdown-boundary-b", "#dropdown-boundary-a" })
            .Add(p => p.Sticky, "always")
            .Add(p => p.HideWhenDetached, true)
            .Add(p => p.ChildContent, "Body"));

        cut.Instance.CollisionBoundarySelector.Should().Be("#dropdown-boundary-a");
        cut.Instance.CollisionBoundarySelectors.Should().BeEquivalentTo(["#dropdown-boundary-b", "#dropdown-boundary-a"]);
        cut.Instance.Sticky.Should().Be("always");
        cut.Instance.HideWhenDetached.Should().BeTrue();
    }

    [Test]
    public void DropdownSubContent_exposes_radix_popper_collision_options()
    {
        var source = ReadDropdownSource("DropdownSubContent.razor");

        source.Should().Contain("CollisionBoundarySelector=\"@CollisionBoundarySelector\"");
        source.Should().Contain("CollisionBoundarySelectors=\"@CollisionBoundarySelectors\"");
        source.Should().Contain("Sticky=\"@Sticky\"");
        source.Should().Contain("HideWhenDetached=\"@HideWhenDetached\"");
    }

    private IRenderedComponent<Dropdown> RenderOpenDropdownWithMenu(RenderFragment menuContent)
    {
        return Render<Dropdown>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<DropdownToggle>(0);
                builder.AddAttribute(1, nameof(DropdownToggle.ChildContent), (RenderFragment)(toggleBuilder => toggleBuilder.AddContent(0, "Open")));
                builder.CloseComponent();

                builder.OpenComponent<DropdownMenu>(2);
                builder.AddAttribute(3, nameof(DropdownMenu.ChildContent), menuContent);
                builder.CloseComponent();
            })));
    }

    private static string ReadDropdownSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForDropdown(), "src", "Soenneker.Quark.Suite", "Components", "Dropdown", fileName));
    }

    private static string GetSuiteRootForDropdown()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }

}
