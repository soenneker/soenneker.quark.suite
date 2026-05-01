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
        thClasses.Should().Contain("text-left");
        thClasses.Should().Contain("align-middle");
        thClasses.Should().Contain("font-medium");
        thClasses.Should().Contain("whitespace-nowrap");
        thClasses.Should().Contain("text-foreground");
        thClasses.Should().Contain("[&:has([role=checkbox])]:pr-0");
        thClasses.Should().NotContain("[&>[role=checkbox]]:translate-y-[2px]");
        th.Find("[data-slot='table-head']").HasAttribute("scope").Should().BeFalse();
        thClasses.Should().NotContain("q-table-th");
    }
}
