using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DialogDescription_matches_shadcn_default_component_contract()
    {
        var cut = Render<DialogDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        var classes = cut.Find("[data-slot='dialog-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("*:[a]:underline");
        classes.Should().Contain("*:[a]:underline-offset-3");
        classes.Should().Contain("*:[a]:hover:text-foreground");
    }
}
