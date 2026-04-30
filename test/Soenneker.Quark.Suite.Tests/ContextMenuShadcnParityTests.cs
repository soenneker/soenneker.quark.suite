using AwesomeAssertions;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ContextMenuItem_matches_shadcn_v4_item_classes()
    {
        var source = ReadContextMenuSource("ContextMenuItem.razor");

        source.Should().Contain("Rounded.Sm");
        source.Should().Contain("Padding.Is2.OnX.Token(\"1.5\").OnY");
        source.Should().Contain("data-[disabled]:pointer-events-none");
        source.Should().Contain("data-[disabled]:opacity-50");
        source.Should().Contain("data-[inset]:pl-8");
        source.Should().Contain("data-[variant=destructive]:text-destructive");
        source.Should().Contain("[&_svg:not([class*='text-'])]:text-muted-foreground");
        source.Should().NotContain("data-disabled:");
        source.Should().NotContain("data-inset:pl-7");
    }

    [Test]
    public void ContextMenuCheckboxItem_matches_shadcn_v4_indicator_and_item_classes()
    {
        var source = ReadContextMenuSource("ContextMenuCheckboxItem.razor");

        source.Should().Contain("Rounded.Sm");
        source.Should().Contain("Padding.Is2.FromRight.Token(\"8\").FromLeft.Token(\"1.5\").OnY");
        source.Should().Contain("data-[disabled]:pointer-events-none");
        source.Should().Contain("data-slot=\"context-menu-checkbox-item-indicator\"");
        source.Should().Contain("absolute left-2 flex size-3.5 items-center justify-center");
        source.Should().Contain("Class=\"size-4\"");
        source.Should().NotContain("right-2");
        source.Should().NotContain("data-disabled:");
    }

    [Test]
    public void ContextMenuRadioItem_matches_shadcn_v4_indicator_and_item_classes()
    {
        var source = ReadContextMenuSource("ContextMenuRadioItem.razor");

        source.Should().Contain("Rounded.Sm");
        source.Should().Contain("Padding.Is2.FromRight.Token(\"8\").FromLeft.Token(\"1.5\").OnY");
        source.Should().Contain("data-[disabled]:pointer-events-none");
        source.Should().Contain("data-slot=\"context-menu-radio-item-indicator\"");
        source.Should().Contain("absolute left-2 flex size-3.5 items-center justify-center");
        source.Should().Contain("LucideIcon.Circle");
        source.Should().Contain("Class=\"size-2 fill-current\"");
        source.Should().NotContain("right-2");
        source.Should().NotContain("data-disabled:");
    }

    [Test]
    public void ContextMenuContent_exposes_radix_popper_collision_options()
    {
        var cut = Render<ContextMenuContent>(parameters => parameters
            .Add(p => p.CollisionBoundarySelector, "#context-menu-boundary-a")
            .Add(p => p.CollisionBoundarySelectors, new[] { "#context-menu-boundary-b", "#context-menu-boundary-a" })
            .Add(p => p.Sticky, "always")
            .Add(p => p.HideWhenDetached, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Body"))));

        cut.Instance.CollisionBoundarySelector.Should().Be("#context-menu-boundary-a");
        cut.Instance.CollisionBoundarySelectors.Should().BeEquivalentTo(["#context-menu-boundary-b", "#context-menu-boundary-a"]);
        cut.Instance.Sticky.Should().Be("always");
        cut.Instance.HideWhenDetached.Should().BeTrue();
    }

    [Test]
    public void ContextMenu_demo_matches_current_shadcn_docs_examples()
    {
        var source = File.ReadAllText(Path.Combine(GetSuiteRootForContextMenu(), "test", "Soenneker.Quark.Suite.Demo", "Pages", "Components", "ContextMenus.razor"));

        source.Should().Contain("Displays a menu of actions triggered by a right click.");
        source.Should().Contain("A simple context menu with a few actions.");
        source.Should().Contain("Use `ContextMenuSub` to nest secondary actions.");
        source.Should().Contain("Add `ContextMenuShortcut` to show keyboard hints.");
        source.Should().Contain("Group related actions and separate them with dividers.");
        source.Should().Contain("Combine icons with labels for quick scanning.");
        source.Should().Contain("Use `ContextMenuCheckboxItem` for toggles.");
        source.Should().Contain("Use `ContextMenuRadioItem` for exclusive choices.");
        source.Should().Contain("Use `variant=&quot;destructive&quot;` to style the menu item as destructive.");
        source.Should().Contain("To enable RTL support in shadcn/ui, see the RTL configuration guide.");
        source.Should().Contain("<ContextMenuItem>Profile</ContextMenuItem>");
        source.Should().Contain("<ContextMenuItem>Subscription</ContextMenuItem>");
        source.Should().Contain("LucideIcon.Pencil");
        source.Should().Contain("LucideIcon.Share");
        source.Should().Contain("LucideIcon.Trash");
        source.Should().NotContain("Shadcn-aligned Basic example.");
        source.Should().NotContain("Browser-style context menu with shortcuts");
    }

    private static string ReadContextMenuSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForContextMenu(), "src", "Soenneker.Quark.Suite", "Components", "ContextMenu", fileName));
    }

    private static string GetSuiteRootForContextMenu()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }

}
