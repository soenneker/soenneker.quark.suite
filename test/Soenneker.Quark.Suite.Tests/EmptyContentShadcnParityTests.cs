using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void EmptyContent_matches_shadcn_base_classes()
    {
        var cut = Render<EmptyContent>(parameters => parameters
            .Add(p => p.ChildContent, "Content"));

        var classes = cut.Find("[data-slot='empty-content']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("w-full");
        classes.Should().Contain("max-w-sm");
        classes.Should().Contain("min-w-0");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("items-center");
        classes.Should().NotContain("justify-center");
        classes.Should().Contain("gap-4");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-balance");
        classes.Should().NotContain("flex-row");
        classes.Should().NotContain("gap-2.5");
    }
}
