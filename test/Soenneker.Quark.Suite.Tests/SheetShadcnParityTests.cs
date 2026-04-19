using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Sheet_trigger_matches_shadcn_default_component_contract()
    {
        var cut = Render<SheetTrigger>(parameters => parameters
            .Add(p => p.ChildContent, "Open"));

        var trigger = cut.Find("[data-slot='sheet-trigger']");
        string triggerClasses = trigger.GetAttribute("class")!;

        triggerClasses.Should().Contain("group/button");
        triggerClasses.Should().Contain("inline-flex");
        triggerClasses.Should().Contain("rounded-lg");
        triggerClasses.Should().Contain("border");
        triggerClasses.Should().Contain("bg-background");
        triggerClasses.Should().Contain("hover:bg-muted");
        triggerClasses.Should().Contain("aria-expanded:bg-muted");
        triggerClasses.Should().Contain("h-8");
        triggerClasses.Should().Contain("gap-1.5");
        triggerClasses.Should().Contain("px-2.5");
    }

    [Fact]
    public void Sheet_trigger_aschild_preserves_sheet_trigger_slot_on_child_button()
    {
        var cut = Render<SheetTrigger>(parameters => parameters
            .Add(p => p.AsChild, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open")));
                builder.CloseComponent();
            })));

        cut.Find("button[data-slot='sheet-trigger']");
        cut.FindAll("button[data-slot='button']").Should().BeEmpty();
    }

}
