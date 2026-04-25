using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogDescription_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogDescription>(parameters => parameters
            .Add(p => p.ChildContent, "Body"));

        var classes = cut.Find("[data-slot='alert-dialog-description']").GetAttribute("class")!;

        classes.Should().Contain("text-sm");
        classes.Should().Contain("text-muted-foreground");
        classes.Should().NotContain("text-balance");
        classes.Should().NotContain("md:text-pretty");
        classes.Should().NotContain("*:[a]:underline");
    }
}
