using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Progress_matches_shadcn_default_component_contract()
    {
        var cut = Render<Progress>(parameters => parameters
            .Add(p => p.Indeterminate, true));

        var root = cut.Find("[data-slot='progress']");
        var indicator = cut.Find("[data-slot='progress-indicator']");
        var rootClasses = root.GetAttribute("class")!;
        var indicatorClasses = indicator.GetAttribute("class")!;

        rootClasses.Should().Contain("relative");
        rootClasses.Should().Contain("h-2");
        rootClasses.Should().Contain("w-full");
        rootClasses.Should().Contain("overflow-hidden");
        rootClasses.Should().Contain("rounded-full");
        rootClasses.Should().Contain("bg-primary/20");
        rootClasses.Should().NotContain("h-1");
        rootClasses.Should().NotContain("overflow-x-hidden");
        rootClasses.Should().NotContain("bg-muted");

        root.GetAttribute("data-max").Should().Be("100");
        root.GetAttribute("data-state").Should().Be("indeterminate");

        indicatorClasses.Should().Contain("h-full");
        indicatorClasses.Should().Contain("w-full");
        indicatorClasses.Should().Contain("flex-1");
        indicatorClasses.Should().Contain("bg-primary");
        indicatorClasses.Should().Contain("transition-all");
    }

    [Test]
    public void Progress_clamps_visual_and_aria_state_together()
    {
        var cut = Render<Progress>(parameters => parameters
            .Add(p => p.ProgressValue, 150));

        var root = cut.Find("[data-slot='progress']");
        var indicator = cut.Find("[data-slot='progress-indicator']");

        root.GetAttribute("aria-valuenow").Should().Be("100");
        root.GetAttribute("data-value").Should().Be("100");
        root.GetAttribute("data-state").Should().Be("complete");
        indicator.GetAttribute("data-state").Should().Be("complete");
        indicator.GetAttribute("style").Should().Contain("translateX(-0%)");
    }

    [Test]
    public void Progress_invalid_max_uses_radix_default_for_visual_and_aria_state()
    {
        var cut = Render<Progress>(parameters => parameters
            .Add(p => p.ProgressValue, 50)
            .Add(p => p.Max, 0));

        var root = cut.Find("[data-slot='progress']");
        var indicator = cut.Find("[data-slot='progress-indicator']");

        root.GetAttribute("aria-valuemax").Should().Be("100");
        root.GetAttribute("aria-valuenow").Should().Be("50");
        root.GetAttribute("data-state").Should().Be("loading");
        indicator.GetAttribute("style").Should().Contain("translateX(-50%)");
    }
}
