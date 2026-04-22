using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void TooltipTrigger_matches_shadcn_default_component_contract()
    {
        var trigger = Render<TooltipTrigger>(parameters => parameters
            .Add(p => p.ChildContent, "Hover"));

        var triggerElement = trigger.Find("[data-slot='tooltip-trigger']");
        var triggerClasses = triggerElement.GetAttribute("class") ?? string.Empty;

        triggerElement.HasAttribute("data-slot").Should().BeTrue();
        triggerClasses.Should().NotContain("transition-[color,box-shadow]");
        triggerClasses.Should().NotContain("outline-none");
        triggerClasses.Should().NotContain("focus-visible:ring-[3px]");
        triggerClasses.Should().NotContain("focus-visible:ring-ring/50");
        triggerClasses.Should().NotContain("disabled:pointer-events-none");
        triggerClasses.Should().NotContain("disabled:opacity-50");
    }
}
