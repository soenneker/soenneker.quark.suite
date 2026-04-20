using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Collapsible_slots_match_shadcn_base_classes()
    {
        var cut = Render<Collapsible>(parameters => parameters
            .Add(p => p.DefaultOpen, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<CollapsibleTrigger>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Toggle")));
                builder.CloseComponent();

                builder.OpenComponent<CollapsibleContent>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Hidden content")));
                builder.CloseComponent();
            })));

        var root = cut.Find("[data-slot='collapsible']");
        string triggerClasses = cut.Find("[data-slot='collapsible-trigger']").GetAttribute("class")!;
        string contentClasses = cut.Find("[data-slot='collapsible-content']").GetAttribute("class")!;

        root.HasAttribute("data-slot").Should().BeTrue();
        triggerClasses.Should().Contain("flex");
        triggerClasses.Should().Contain("w-full");
        triggerClasses.Should().Contain("items-center");
        triggerClasses.Should().Contain("justify-between");
        triggerClasses.Should().Contain("gap-2");
        triggerClasses.Should().Contain("rounded-md");
        triggerClasses.Should().Contain("text-sm");
        triggerClasses.Should().Contain("font-medium");
        triggerClasses.Should().Contain("transition-colors");
        triggerClasses.Should().Contain("focus-visible:ring-[3px]");
        triggerClasses.Should().Contain("focus-visible:ring-ring/50");
        triggerClasses.Should().NotContain("q-collapsible-trigger");

        contentClasses.Should().Contain("overflow-hidden");
        contentClasses.Should().NotContain("q-collapsible-content");
    }
}
