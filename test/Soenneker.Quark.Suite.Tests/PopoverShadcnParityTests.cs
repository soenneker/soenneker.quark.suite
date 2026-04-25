using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Popover_slots_match_shadcn_default_component_contract()
    {
        var cut = Render<Popover>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PopoverTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open popover")));
                builder.CloseComponent();

                builder.OpenComponent<PopoverContent>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<PopoverHeader>(0);
                    contentBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(headerBuilder =>
                    {
                        headerBuilder.OpenComponent<PopoverTitle>(0);
                        headerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Dimensions")));
                        headerBuilder.CloseComponent();

                        headerBuilder.OpenComponent<PopoverDescription>(2);
                        headerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Set the dimensions for the layer.")));
                        headerBuilder.CloseComponent();
                    }));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        var popoverClasses = cut.Find("[data-slot='popover']").GetAttribute("class")!;
        var triggerClasses = cut.Find("[data-slot='popover-trigger']").GetAttribute("class")!;
        var contentClasses = cut.Find("[data-slot='popover-content']").GetAttribute("class")!;
        var headerClasses = cut.Find("[data-slot='popover-header']").GetAttribute("class")!;
        var titleClasses = cut.Find("[data-slot='popover-title']").GetAttribute("class")!;
        var descriptionClasses = cut.Find("[data-slot='popover-description']").GetAttribute("class")!;

        popoverClasses.Should().Contain("relative");

        triggerClasses.Should().Contain("group/button");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("shrink-0");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("justify-center");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("whitespace-nowrap");
        triggerClasses.Should().Contain("transition-all");
        triggerClasses.Should().Contain("outline-none");
        triggerClasses.Should().Contain("select-none");
        triggerClasses.Should().Contain("focus-visible:border-ring");
        triggerClasses.Should().Contain("focus-visible:ring-3");
        triggerClasses.Should().Contain("border-border");
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("aria-expanded:bg-muted");
        triggerClasses.Should().Contain("h-8");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("px-2.5");

        contentClasses.Should().Contain("z-50");
        contentClasses.Should().Contain("w-72");
        contentClasses.Should().Contain("origin-(--radix-popover-content-transform-origin)");
        contentClasses.Should().Contain("rounded-md");
        contentClasses.Should().Contain("border");
        contentClasses.Should().Contain("bg-popover");
        contentClasses.Should().Contain("p-4");
        contentClasses.Should().Contain("text-popover-foreground");
        contentClasses.Should().Contain("shadow-md");
        contentClasses.Should().Contain("outline-hidden");
        contentClasses.Should().Contain("data-[side=bottom]:slide-in-from-top-2");
        contentClasses.Should().Contain("data-[state=closed]:animate-out");
        contentClasses.Should().Contain("data-[state=open]:animate-in");
        contentClasses.Should().NotContain("z-[80]");

        headerClasses.Should().Contain("flex");
        headerClasses.Should().Contain("flex-col");
        headerClasses.Should().Contain("gap-1");
        headerClasses.Should().Contain("text-sm");
        headerClasses.Should().NotContain("grid");

        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().NotContain("leading-none");

        descriptionClasses.Should().Contain("text-muted-foreground");
        descriptionClasses.Should().NotContain("text-sm");
    }

    [Test]
    public void Popover_content_exposes_radix_popper_collision_options()
    {
        var cut = Render<Popover>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PopoverTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open popover")));
                builder.CloseComponent();

                builder.OpenComponent<PopoverContent>(2);
                builder.AddAttribute(3, nameof(PopoverContent.CollisionBoundarySelector), "#popover-boundary-a");
                builder.AddAttribute(4, nameof(PopoverContent.CollisionBoundarySelectors), new[] { "#popover-boundary-b", "#popover-boundary-a" });
                builder.AddAttribute(5, nameof(PopoverContent.Sticky), "always");
                builder.AddAttribute(6, nameof(PopoverContent.HideWhenDetached), true);
                builder.AddAttribute(7, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Body")));
                builder.CloseComponent();
            })));

        var content = cut.FindComponent<PopoverContent>().Instance;

        content.CollisionBoundarySelector.Should().Be("#popover-boundary-a");
        content.CollisionBoundarySelectors.Should().BeEquivalentTo(["#popover-boundary-b", "#popover-boundary-a"]);
        content.Sticky.Should().Be("always");
        content.HideWhenDetached.Should().BeTrue();
    }
}
