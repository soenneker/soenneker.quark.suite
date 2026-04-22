using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Skeleton_matches_shadcn_default_component_contract()
    {
        var cut = Render<Skeleton>();

        var root = cut.Find("[data-slot='skeleton']");
        var classes = root.GetAttribute("class")!;

        classes.Should().Contain("animate-pulse");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("bg-muted");
        classes.Should().NotContain("q-skeleton");
    }
}
