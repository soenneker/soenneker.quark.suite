using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void AlertDescription_matches_shadcn_base_classes()
    {
        var cut = Render<AlertDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        string classes = cut.Find("[data-slot='alert-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-balance");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("md:text-pretty");
        classes.Should().Contain("[&_a]:underline-offset-3");
        classes.Should().NotContain("q-alert-description");
        classes.Should().NotContain("col-start-2");
    }
}
