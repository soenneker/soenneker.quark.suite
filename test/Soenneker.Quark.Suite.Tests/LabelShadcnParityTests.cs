using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Label_matches_shadcn_base_classes()
    {
        var cut = Render<Label>(parameters => parameters
            .Add(p => p.For, "terms")
            .Add(p => p.ChildContent, "Accept"));

        var classes = cut.Find("[data-slot='label']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("leading-none");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("select-none");
        classes.Should().Contain("peer-disabled:cursor-not-allowed");
        classes.Should().NotContain("q-label");
    }
}
