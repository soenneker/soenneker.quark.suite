using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Sheet_trigger_matches_shadcn_default_component_contract()
    {
        var cut = Render<SheetTrigger>(parameters => parameters
            .Add(p => p.ChildContent, "Open"));

        var trigger = cut.Find("[data-slot='sheet-trigger']");
        var triggerClasses = trigger.GetAttribute("class");

        triggerClasses.Should().BeNullOrEmpty();
        trigger.GetAttribute("data-state").Should().Be("closed");
        trigger.GetAttribute("aria-haspopup").Should().Be("dialog");
    }

    [Test]
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
