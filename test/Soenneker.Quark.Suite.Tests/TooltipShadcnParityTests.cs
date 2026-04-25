using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.IO;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Tooltip_content_matches_shadcn_default_component_contract()
    {
        var cut = Render<Tooltip>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<TooltipTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Hover")));
                builder.CloseComponent();

                builder.OpenComponent<TooltipContent>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Add to library")));
                builder.CloseComponent();
            })));

        var content = cut.Find("[data-slot='tooltip-content']");
        var contentClasses = content.GetAttribute("class")!;

        contentClasses.Should().Contain("w-fit");
        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("origin-(--radix-tooltip-content-transform-origin)");
        contentClasses.Should().Contain("animate-in");
        contentClasses.Should().Contain("rounded-md");
        contentClasses.Should().Contain("bg-foreground");
        contentClasses.Should().Contain("px-3");
        contentClasses.Should().Contain("py-1.5");
        contentClasses.Should().Contain("text-xs");
        contentClasses.Should().Contain("text-balance");
        contentClasses.Should().Contain("text-background");
        contentClasses.Should().Contain("fade-in-0");
        contentClasses.Should().Contain("zoom-in-95");
        contentClasses.Should().Contain("data-[state=closed]:animate-out");
        contentClasses.Should().Contain("data-[state=closed]:fade-out-0");
        contentClasses.Should().Contain("data-[state=closed]:zoom-out-95");
        contentClasses.Should().NotContain("inline-flex");
        contentClasses.Should().NotContain("max-w-xs");
        contentClasses.Should().NotContain("has-data-[slot=kbd]:pr-1.5");
        contentClasses.Should().NotContain("data-open:");
        contentClasses.Should().NotContain("data-closed:");
        contentClasses.Should().NotContain("z-[80]");
        contentClasses.Should().NotContain("bg-primary");
        contentClasses.Should().NotContain("text-primary-foreground");

        cut.Markup.Should().Contain("bg-foreground fill-foreground");
    }

    [Test]
    public void Tooltip_source_matches_shadcn_v4_contract()
    {
        var root = ReadTooltipSource("Tooltip.razor");
        var provider = ReadTooltipSource("TooltipProvider.razor");
        var content = ReadTooltipSource("TooltipContent.razor");

        root.Should().Contain("data-slot\"] = \"tooltip\"");
        root.Should().NotContain("Position ??= Quark.Position.Relative");
        root.Should().NotContain("group/tooltip");

        provider.Should().Contain("public int DelayDuration { get; set; }");
        provider.Should().Contain("public bool DisableHoverableContent { get; set; }");
        provider.Should().NotContain("DisableHoverableContent { get; set; } = true");

        content.Should().Contain("ForceMount=\"@ForceMount\"");
        content.Should().Contain("public bool ForceMount { get; set; }");
        content.Should().Contain("SideOffset=\"@SideOffset\"");
        content.Should().Contain("CollisionBoundarySelector=\"@CollisionBoundarySelector\"");
        content.Should().Contain("Sticky=\"@Sticky\"");
        content.Should().Contain("z-50");
    }

    [Test]
    public void Tooltip_content_exposes_radix_popper_collision_options()
    {
        var cut = Render<Tooltip>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<TooltipTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Hover")));
                builder.CloseComponent();

                builder.OpenComponent<TooltipContent>(2);
                builder.AddAttribute(3, nameof(TooltipContent.CollisionBoundarySelector), "#tooltip-boundary-a");
                builder.AddAttribute(4, nameof(TooltipContent.CollisionBoundarySelectors), new[] { "#tooltip-boundary-b", "#tooltip-boundary-a" });
                builder.AddAttribute(5, nameof(TooltipContent.Sticky), "always");
                builder.AddAttribute(6, nameof(TooltipContent.HideWhenDetached), true);
                builder.AddAttribute(7, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Body")));
                builder.CloseComponent();
            })));

        var content = cut.FindComponent<TooltipContent>().Instance;

        content.CollisionBoundarySelector.Should().Be("#tooltip-boundary-a");
        content.CollisionBoundarySelectors.Should().BeEquivalentTo(["#tooltip-boundary-b", "#tooltip-boundary-a"]);
        content.Sticky.Should().Be("always");
        content.HideWhenDetached.Should().BeTrue();
    }

    private static string ReadTooltipSource(string fileName)
    {
        return File.ReadAllText(Path.Combine(GetSuiteRootForTooltip(), "src", "Soenneker.Quark.Suite", "Components", "Tooltip", fileName));
    }

    private static string GetSuiteRootForTooltip()
    {
        var directory = AppContext.BaseDirectory;

        for (var i = 0; i < 6; i++)
        {
            directory = Directory.GetParent(directory)!.FullName;
        }

        return directory;
    }
}
