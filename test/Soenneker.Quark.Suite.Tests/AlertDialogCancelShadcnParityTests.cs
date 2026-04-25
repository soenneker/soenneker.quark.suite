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
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border-border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("hover:text-foreground");
        classes.Should().Contain("aria-expanded:bg-muted");
        classes.Should().Contain("aria-expanded:text-foreground");
        classes.Should().Contain("dark:border-input");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().Contain("dark:hover:bg-input/50");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
        classes.Should().NotContain("shadow-xs");
        classes.Should().NotContain("hover:bg-accent");
        classes.Should().NotContain("hover:text-accent-foreground");
        classes.Should().NotContain("h-9");
        classes.Should().NotContain("px-4");
        classes.Should().NotContain("py-2");
        classes.Should().NotContain("q-button");
    }
}
