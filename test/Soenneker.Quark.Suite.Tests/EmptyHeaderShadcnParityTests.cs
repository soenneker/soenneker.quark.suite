using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void EmptyHeader_matches_shadcn_base_classes()
    {
        var cut = Render<EmptyHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        var classes = cut.Find("[data-slot='empty-header']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("max-w-sm");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("text-center");
        classes.Should().NotContain("q-empty-header");
    }
}
