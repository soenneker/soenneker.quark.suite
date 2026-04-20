using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Popover_slots_match_shadcn_default_component_contract()
    {
        var cut = Render<Popover>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<PopoverTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open popover")));
                builder.CloseComponent();

                builder.OpenComponent<PopoverHeader>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(headerBuilder =>
                {
                    headerBuilder.OpenComponent<PopoverTitle>(0);
                    headerBuilder.AddAttribute(1, "ChildContent", (RenderFragment)(titleBuilder => titleBuilder.AddContent(0, "Dimensions")));
                    headerBuilder.CloseComponent();

                    headerBuilder.OpenComponent<PopoverDescription>(2);
                    headerBuilder.AddAttribute(3, "ChildContent", (RenderFragment)(descriptionBuilder => descriptionBuilder.AddContent(0, "Set the dimensions for the layer.")));
                    headerBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            })));

        string popoverClasses = cut.Find("[data-slot='popover']").GetAttribute("class")!;
        string triggerClasses = cut.Find("[data-slot='popover-trigger']").GetAttribute("class")!;
        string headerClasses = cut.Find("[data-slot='popover-header']").GetAttribute("class")!;
        string titleClasses = cut.Find("[data-slot='popover-title']").GetAttribute("class")!;
        string descriptionClasses = cut.Find("[data-slot='popover-description']").GetAttribute("class")!;

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

        headerClasses.Should().Contain("grid");
        headerClasses.Should().Contain("gap-1.5");

        titleClasses.Should().Contain("font-medium");
        titleClasses.Should().Contain("leading-none");

        descriptionClasses.Should().Contain("text-sm");
        descriptionClasses.Should().Contain("text-muted-foreground");
    }
}
