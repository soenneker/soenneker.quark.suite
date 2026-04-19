using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void H3_matches_shadcn_base_classes()
    {
        var h3 = Render<H3>(parameters => parameters.Add(p => p.ChildContent, "The joke tax"));
        h3.Find("[data-slot='h3']").GetAttribute("class")!.Should().ContainAll("text-2xl", "font-semibold", "tracking-tight", "scroll-m-20");
    }
}
