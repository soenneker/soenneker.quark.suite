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

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("bg-primary");
        classes.Should().Contain("text-primary-foreground");
        classes.Should().Contain("hover:bg-primary/90");
        classes.Should().Contain("h-9");
        classes.Should().Contain("px-4");
        classes.Should().Contain("py-2");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("[a]:hover:bg-primary/80");
        classes.Should().NotContain("focus-visible:ring-3");
    }
}
