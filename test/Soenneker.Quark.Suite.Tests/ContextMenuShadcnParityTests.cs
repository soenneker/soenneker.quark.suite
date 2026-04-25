using AwesomeAssertions;
using Bunit;
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
