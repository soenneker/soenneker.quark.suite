using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void H2_matches_shadcn_base_classes()
    {
        var h2 = Render<H2>(parameters => parameters.Add(p => p.ChildContent, "People stopped telling jokes"));
        h2.Find("[data-slot='h2']").GetAttribute("class")!.Should().ContainAll("text-3xl", "font-semibold", "tracking-tight", "scroll-m-20", "border-b", "pb-2", "first:mt-0");
    }
}
