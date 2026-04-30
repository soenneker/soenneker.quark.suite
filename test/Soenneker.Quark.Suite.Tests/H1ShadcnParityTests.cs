using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void H1_matches_shadcn_base_classes()
    {
        var h1 = Render<H1>(parameters => parameters.Add(p => p.ChildContent, "Taxing Laughter"));
        var classes = h1.Find("[data-slot='h1']").GetAttribute("class")!;

        classes.Should().ContainAll("text-center", "text-4xl", "font-extrabold", "tracking-tight", "scroll-m-20", "text-balance");
        classes.Should().NotContain("lg:text-5xl");
    }
}
