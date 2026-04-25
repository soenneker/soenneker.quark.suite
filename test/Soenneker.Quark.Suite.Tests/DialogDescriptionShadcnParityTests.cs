using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DialogDescription_matches_shadcn_default_component_contract()
    {
        var cut = Render<DialogDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Description"));

        var classes = cut.Find("[data-slot='dialog-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().NotContain("*:[a]:underline");
    }
}
