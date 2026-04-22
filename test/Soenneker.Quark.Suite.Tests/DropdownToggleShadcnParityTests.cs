using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DropdownToggle_default_mode_matches_current_outline_trigger_shell()
    {
        var cut = Render<DropdownToggle>(parameters => parameters
            .Add(p => p.ChildContent, "Open"));

        var classes = cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("class")!;

        classes.Should().Contain("group/button");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("border-border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("hover:text-foreground");
        classes.Should().Contain("aria-expanded:bg-muted");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
        classes.Should().Contain("transition-all");
        classes.Should().NotContain("bg-secondary");
        classes.Should().NotContain("h-9");
        classes.Should().NotContain("px-4");
        classes.Should().NotContain("py-2");
    }

    [Test]
    public void DropdownToggle_split_mode_matches_current_button_shell()
    {
        var cut = Render<DropdownToggle>(parameters => parameters
            .Add(p => p.IsSplit, true)
            .Add(p => p.ChildContent, "More"));

        var classes = cut.Find("[data-slot='dropdown-menu-trigger']").GetAttribute("class")!;

        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("border-input");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("hover:text-foreground");
        classes.Should().Contain("aria-expanded:bg-muted");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("transition-all");
        classes.Should().NotContain("shadow-xs");
        classes.Should().NotContain("hover:bg-accent");
    }
}
