using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Empty_matches_shadcn_base_classes()
    {
        var cut = Render<Empty>(parameters => parameters
            .Add(p => p.ChildContent, "Empty"));

        var classes = cut.Find("[data-slot='empty']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-1");
        classes.Should().Contain("min-w-0");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("gap-6");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border-dashed");
        classes.Should().Contain("p-6");
        classes.Should().Contain("text-center");
        classes.Should().Contain("text-balance");
        classes.Should().Contain("md:p-12");
        classes.Should().NotContain("gap-4");
        classes.Should().NotContain("rounded-xl");
        classes.Should().NotContain("q-empty");
        classes.Should().NotContain("flex-none");
        classes.Should().NotContain("w-full");
        classes.Should().NotContain("border ");
    }
}
