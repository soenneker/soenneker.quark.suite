using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Spinner_matches_shadcn_base_classes()
    {
        var cut = Render<Spinner>();

        var spinner = cut.Find("svg[role='status']");
        var classes = spinner.GetAttribute("class")!;

        spinner.HasAttribute("data-slot").Should().BeFalse();
        spinner.GetAttribute("aria-label").Should().Be("Loading");
        classes.Should().Contain("lucide");
        classes.Should().Contain("lucide-loader-circle");
        classes.Should().Contain("size-4");
        classes.Should().Contain("animate-spin");
    }
}
