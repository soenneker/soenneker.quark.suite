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
        var button = cut.Find("[data-slot='button']");
        var iconClasses = cut.Find("svg").GetAttribute("class")!;

        button.GetAttribute("type").Should().Be("button");
        button.GetAttribute("data-variant").Should().Be("ghost");
        button.GetAttribute("data-size").Should().Be("icon");
        button.GetAttribute("aria-label").Should().Be("Toggle dark mode");
        cut.Find(".sr-only").TextContent.Should().Be("Toggle dark mode");

        classes.Should().Contain("size-8");
        classes.Should().Contain("group/toggle");
        classes.Should().Contain("extend-touch-target");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("rounded-md");
        iconClasses.Should().Contain("size-4.5");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("size-10");
        classes.Should().NotContain("hover:bg-muted");
    }
}
