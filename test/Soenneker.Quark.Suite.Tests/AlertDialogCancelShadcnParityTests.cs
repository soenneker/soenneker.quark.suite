using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogCancel_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogCancel>(parameters => parameters
            .Add(p => p.ChildContent, "Cancel"));

        var classes = cut.Find("[data-slot='alert-dialog-cancel']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border-border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
        classes.Should().NotContain("q-button");
    }
}
