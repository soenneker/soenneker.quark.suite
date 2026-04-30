using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
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

        triggerClasses.Should().BeNullOrEmpty();

        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("w-64");
        contentClasses.Should().Contain("origin-(--radix-hover-card-content-transform-origin)");
        contentClasses.Should().Contain("rounded-md");
        contentClasses.Should().Contain("border");
        contentClasses.Should().Contain("bg-popover");
        contentClasses.Should().Contain("p-4");
        contentClasses.Should().Contain("text-popover-foreground");
        contentClasses.Should().Contain("shadow-md");
        contentClasses.Should().Contain("outline-none");
        contentClasses.Should().Contain("data-[state=open]:animate-in");
        contentClasses.Should().Contain("data-[state=open]:fade-in-0");
        contentClasses.Should().Contain("data-[state=open]:zoom-in-95");
        contentClasses.Should().Contain("data-[state=closed]:animate-out");
        contentClasses.Should().Contain("data-[state=closed]:fade-out-0");
        contentClasses.Should().Contain("data-[state=closed]:zoom-out-95");
        contentClasses.Should().NotContain("z-[80]");
        contentClasses.Should().NotContain("ring-1");
        contentClasses.Should().NotContain("duration-100");
        contentClasses.Should().NotContain("data-open:");
        contentClasses.Should().NotContain("data-closed:");
        contentClasses.Should().NotContain("data-[state=closed]:hidden");
    }

    [Test]
    public void HoverCard_content_exposes_radix_popper_collision_options()
    {
        var cut = Render<HoverCard>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<HoverCardTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Hover")));
                builder.CloseComponent();

                builder.OpenComponent<HoverCardContent>(2);
                builder.AddAttribute(3, nameof(HoverCardContent.CollisionBoundarySelector), "#hover-card-boundary-a");
                builder.AddAttribute(4, nameof(HoverCardContent.CollisionBoundarySelectors), new[] { "#hover-card-boundary-b", "#hover-card-boundary-a" });
                builder.AddAttribute(5, nameof(HoverCardContent.Sticky), "always");
                builder.AddAttribute(6, nameof(HoverCardContent.HideWhenDetached), true);
                builder.AddAttribute(7, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Body")));
                builder.CloseComponent();
            })));

        var content = cut.FindComponent<HoverCardContent>().Instance;

        content.CollisionBoundarySelector.Should().Be("#hover-card-boundary-a");
        content.CollisionBoundarySelectors.Should().BeEquivalentTo(["#hover-card-boundary-b", "#hover-card-boundary-a"]);
        content.Sticky.Should().Be("always");
        content.HideWhenDetached.Should().BeTrue();
    }
}
