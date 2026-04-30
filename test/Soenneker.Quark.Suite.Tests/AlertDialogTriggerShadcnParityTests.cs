using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogTrigger_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogTrigger>(parameters => parameters
            .Add(p => p.ChildContent, "Open"));

        var classes = cut.Find("[data-slot='alert-dialog-trigger']").GetAttribute("class")!;

        classes.Should().BeNullOrEmpty();

        var trigger = cut.Find("[data-slot='alert-dialog-trigger']");
        trigger.GetAttribute("data-slot").Should().Be("alert-dialog-trigger");
        trigger.GetAttribute("class").Should().BeNullOrEmpty("shadcn AlertDialogTrigger is an unstyled Radix trigger; use Button asChild for styling");
    }

    [Test]
    public void AlertDialogTrigger_aschild_preserves_alert_dialog_trigger_slot_on_child_button()
    {
        var cut = Render<AlertDialogTrigger>(parameters => parameters
            .Add(p => p.AsChild, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open")));
                builder.CloseComponent();
            })));

        cut.Find("button[data-slot='alert-dialog-trigger']");
        cut.FindAll("button[data-slot='button']").Should().BeEmpty();
    }
}
