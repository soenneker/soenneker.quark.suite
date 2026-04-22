using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Toggle_matches_shadcn_base_classes()
    {
        var toggle = Render<Toggle>(parameters => parameters.Add(p => p.ChildContent, "Italic"));

        var toggleClasses = toggle.Find("[data-slot='toggle']").GetAttribute("class")!;

        toggleClasses.Should().Contain("group/toggle");
        toggleClasses.Should().Contain("rounded-lg");
        toggleClasses.Should().Contain("hover:bg-muted");
        toggleClasses.Should().Contain("aria-pressed:bg-muted");
        toggleClasses.Should().Contain("data-[state=on]:bg-muted");
        toggleClasses.Should().NotContain("q-toggle");
    }
}
