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

        var triggerClasses = cut.Find("[data-slot='popover-trigger']").GetAttribute("class") ?? string.Empty;
        var contentClasses = cut.Find("[data-slot='popover-content']").GetAttribute("class")!;
        var headerClasses = cut.Find("[data-slot='popover-header']").GetAttribute("class")!;
        var titleClasses = cut.Find("[data-slot='popover-title']").GetAttribute("class")!;
        var descriptionClasses = cut.Find("[data-slot='popover-description']").GetAttribute("class")!;

        cut.Markup.Should().NotContain("data-slot=\"popover\"");
        cut.Markup.Should().NotContain("class=\"relative\"");

        triggerClasses.Should().NotContain("group/button");
        triggerClasses.Should().NotContain("inline-flex");
        triggerClasses.Should().NotContain("rounded-lg");
        triggerClasses.Should().NotContain("border-border");
        triggerClasses.Should().NotContain("bg-background");

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
        contentClasses.Should().NotContain("rounded-lg");
        contentClasses.Should().NotContain("p-2.5");
        contentClasses.Should().NotContain("ring-1");
        contentClasses.Should().NotContain("duration-100");
        contentClasses.Should().NotContain("data-closed:animate-out");
        contentClasses.Should().NotContain("data-open:animate-in");
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
