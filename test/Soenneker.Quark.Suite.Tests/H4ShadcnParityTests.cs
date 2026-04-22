using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void H4_matches_shadcn_base_classes()
    {
        var h4 = Render<H4>(parameters => parameters.Add(p => p.ChildContent, "People stopped telling jokes"));
        h4.Find("[data-slot='h4']").GetAttribute("class")!.Should().ContainAll("text-xl", "font-semibold", "tracking-tight", "scroll-m-20");
    }
}
