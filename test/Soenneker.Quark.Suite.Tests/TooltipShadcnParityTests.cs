using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
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
        string contentClasses = content.GetAttribute("class")!;

        contentClasses.Should().Contain("inline-flex");
        contentClasses.Should().Contain("w-fit");
        contentClasses.Should().Contain("max-w-xs");
        contentClasses.Should().Contain("items-center");
        contentClasses.Should().Contain("gap-1.5");
        contentClasses.Should().Contain("rounded-md");
        contentClasses.Should().Contain("bg-foreground");
        contentClasses.Should().Contain("px-3");
        contentClasses.Should().Contain("py-1.5");
        contentClasses.Should().Contain("text-xs");
        contentClasses.Should().Contain("text-background");
        contentClasses.Should().Contain("has-data-[slot=kbd]:pr-1.5");
        contentClasses.Should().Contain("data-open:animate-in");
        contentClasses.Should().Contain("data-open:fade-in-0");
        contentClasses.Should().Contain("data-open:zoom-in-95");
        contentClasses.Should().Contain("data-closed:animate-out");
        contentClasses.Should().NotContain("bg-primary");
        contentClasses.Should().NotContain("text-primary-foreground");

        cut.Markup.Should().Contain("bg-foreground fill-foreground");
    }
}
