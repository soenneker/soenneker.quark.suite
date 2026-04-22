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
        rootClasses.Should().Contain("flex");
        rootClasses.Should().Contain("h-1");
        rootClasses.Should().Contain("items-center");
        rootClasses.Should().Contain("overflow-x-hidden");
        rootClasses.Should().Contain("rounded-full");
        rootClasses.Should().Contain("bg-muted");
        rootClasses.Should().NotContain("w-full");

        root.GetAttribute("data-max").Should().Be("100");
        root.GetAttribute("data-state").Should().Be("indeterminate");

        indicatorClasses.Should().Contain("size-full");
        indicatorClasses.Should().Contain("flex-1");
        indicatorClasses.Should().Contain("bg-primary");
        indicatorClasses.Should().Contain("transition-all");
    }
}
