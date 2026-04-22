using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ThemeToggleButton_matches_shadcn_docs_button_shell()
    {
        var cut = Render<ThemeToggleButton>(parameters => parameters
            .Add(p => p.AriaLabel, "Toggle dark mode"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Contain("group/button");
        classes.Should().Contain("size-8");
        classes.Should().Contain("group/toggle");
        classes.Should().Contain("extend-touch-target");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("rounded-md");
        classes.Should().NotContain("size-10");
        classes.Should().NotContain("hover:bg-muted");
    }
}
