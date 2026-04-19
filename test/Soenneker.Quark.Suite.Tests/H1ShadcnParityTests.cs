using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void H1_matches_shadcn_base_classes()
    {
        var h1 = Render<H1>(parameters => parameters.Add(p => p.ChildContent, "Taxing Laughter"));
        h1.Find("[data-slot='h1']").GetAttribute("class")!.Should().ContainAll("text-4xl", "font-extrabold", "tracking-tight", "scroll-m-20", "text-balance", "lg:text-5xl");
    }
}
