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
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("shadow-xs");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("dark:border-input");
        classes.Should().Contain("dark:bg-input/30");
        classes.Should().Contain("dark:hover:bg-input/50");
        classes.Should().Contain("h-9");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("px-4");
        classes.Should().Contain("py-2");
        classes.Should().Contain("focus-visible:ring-[3px]");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("bg-clip-padding");
        classes.Should().NotContain("hover:bg-muted");
        classes.Should().NotContain("aria-expanded:bg-muted");
        classes.Should().NotContain("focus-visible:ring-3");
        classes.Should().NotContain("q-button");
    }
}
