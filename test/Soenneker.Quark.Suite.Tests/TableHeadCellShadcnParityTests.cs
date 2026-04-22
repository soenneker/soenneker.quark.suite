using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Th_matches_shadcn_base_classes()
    {
        var th = Render<Th>(parameters => parameters.Add(p => p.ChildContent, "Header"));

        var thClasses = th.Find("[data-slot='table-head']").GetAttribute("class")!;

        thClasses.Should().Contain("h-10");
        thClasses.Should().Contain("px-2");
        thClasses.Should().Contain("font-medium");
        thClasses.Should().NotContain("q-table-th");
    }
}
