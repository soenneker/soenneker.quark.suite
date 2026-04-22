using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void AlertDialogAction_matches_shadcn_default_component_contract()
    {
        var cut = Render<AlertDialogAction>(parameters => parameters
            .Add(p => p.ChildContent, "Continue"));

        var classes = cut.Find("[data-slot='alert-dialog-action']").GetAttribute("class")!;

        classes.Should().Contain("group/button");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("bg-primary");
        classes.Should().Contain("text-primary-foreground");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
    }
}
