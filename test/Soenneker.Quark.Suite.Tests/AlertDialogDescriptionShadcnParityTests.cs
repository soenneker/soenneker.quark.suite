using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void AlertDialogDescription_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        string classes = cut.Find("[data-slot='alert-dialog-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-balance");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("md:text-pretty");
        classes.Should().Contain("*:[a]:underline");
        classes.Should().Contain("*:[a]:underline-offset-3");
        classes.Should().Contain("*:[a]:hover:text-foreground");
    }
}
