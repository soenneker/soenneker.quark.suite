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

        spinner.GetAttribute("aria-label").Should().Be("Loading");
        classes.Should().Contain("size-4");
        classes.Should().Contain("animate-spin");
    }
}
