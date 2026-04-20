using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Dialog_trigger_matches_shadcn_default_component_contract()
    {
        var cut = Render<DialogTrigger>(parameters => parameters
            .Add(p => p.ChildContent, "Open"));

        var classes = cut.Find("[data-slot='dialog-trigger']").GetAttribute("class")!;

        classes.Should().Contain("group/button");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("aria-expanded:bg-muted");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
    }

    [Fact]
    public void Dialog_trigger_aschild_preserves_dialog_trigger_slot_on_child_button()
    {
        var cut = Render<DialogTrigger>(parameters => parameters
            .Add(p => p.AsChild, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(contentBuilder => contentBuilder.AddContent(0, "Open")));
                builder.CloseComponent();
            })));

        cut.Find("button[data-slot='dialog-trigger']");
        cut.FindAll("button[data-slot='button']").Should().BeEmpty();
    }
}
