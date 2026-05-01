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
        classes.Should().NotContain("bg-accent");
        classes.Should().NotContain("q-skeleton");
    }

    [Test]
    public void Skeleton_explicit_radius_overrides_default_radius()
    {
        var cut = Render<Skeleton>(parameters => parameters.Add(p => p.Class, "h-12 w-12 rounded-full"));

        var classes = cut.Find("[data-slot='skeleton']").GetAttribute("class")!;

        classes.Should().Contain("rounded-full");
        classes.Should().NotContain("rounded-md");
    }
}
