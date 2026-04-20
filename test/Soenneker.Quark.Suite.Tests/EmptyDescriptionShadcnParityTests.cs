using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void EmptyDescription_matches_shadcn_base_classes()
    {
        var cut = Render<EmptyDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        var classes = cut.Find("[data-slot='empty-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("leading-relaxed");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("[&>a]:underline-offset-4");
    }
}
