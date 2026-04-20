using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void HoverCard_trigger_and_content_match_shadcn_default_component_contract()
    {
        var cut = Render<HoverCard>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<HoverCardTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Hover")));
                builder.CloseComponent();

                builder.OpenComponent<HoverCardContent>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Content")));
                builder.CloseComponent();
            })));

        var trigger = cut.Find("[data-slot='hover-card-trigger']");
        var content = cut.Find("[data-slot='hover-card-content']");

        var triggerClasses = trigger.GetAttribute("class")!;
        var contentClasses = content.GetAttribute("class")!;

        triggerClasses.Should().Contain("group/button");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("bg-clip-padding");
        triggerClasses.Should().Contain("focus-visible:ring-3");
        triggerClasses.Should().Contain("focus-visible:ring-ring/50");
        triggerClasses.Should().Contain("border-border");
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("h-8");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("px-2.5");

        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("w-64");
        contentClasses.Should().Contain("origin-(--radix-hover-card-content-transform-origin)");
        contentClasses.Should().Contain("rounded-lg");
        contentClasses.Should().Contain("bg-popover");
        contentClasses.Should().Contain("p-2.5");
        contentClasses.Should().Contain("text-sm");
        contentClasses.Should().Contain("text-popover-foreground");
        contentClasses.Should().Contain("shadow-md");
        contentClasses.Should().Contain("ring-1");
        contentClasses.Should().Contain("ring-foreground/10");
        contentClasses.Should().Contain("outline-hidden");
        contentClasses.Should().Contain("duration-100");
        contentClasses.Should().Contain("data-open:animate-in");
        contentClasses.Should().Contain("data-open:fade-in-0");
        contentClasses.Should().Contain("data-open:zoom-in-95");
        contentClasses.Should().Contain("data-closed:animate-out");
        contentClasses.Should().Contain("data-closed:fade-out-0");
        contentClasses.Should().Contain("data-closed:zoom-out-95");
        contentClasses.Should().NotContain("z-[80]");
        contentClasses.Should().NotContain("p-4");
        contentClasses.Should().NotContain("data-[state=open]:animate-in");
        contentClasses.Should().NotContain("data-[state=closed]:hidden");
    }
}
