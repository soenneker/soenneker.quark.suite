using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void EmptyHeader_matches_shadcn_base_classes()
    {
        var cut = Render<EmptyHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Header"));

        string classes = cut.Find("[data-slot='empty-header']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("max-w-sm");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("items-center");
        classes.Should().Contain("gap-2");
        classes.Should().NotContain("q-empty-header");
    }
}
