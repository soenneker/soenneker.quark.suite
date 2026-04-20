using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Separator_matches_shadcn_base_classes()
    {
        var cut = Render<Hr>(parameters => parameters
            .Add(p => p.Orientation, SeparatorOrientation.Horizontal));

        string classes = cut.Find("[data-slot='separator']").GetAttribute("class")!;

        classes.Should().Contain("bg-border");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("data-horizontal:h-px");
        classes.Should().Contain("data-horizontal:w-full");
        classes.Should().Contain("data-vertical:w-px");
        classes.Should().Contain("data-vertical:self-stretch");
        classes.Should().NotContain("q-hr");
    }
}
