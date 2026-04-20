using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void ThemeToggleButton_matches_shadcn_docs_button_shell()
    {
        var cut = Render<ThemeToggleButton>(parameters => parameters
            .Add(p => p.AriaLabel, "Toggle dark mode"));

        var classes = cut.Find("[data-slot='button']").GetAttribute("class")!;

        classes.Should().Contain("group/button");
        classes.Should().Contain("size-8");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("hover:text-foreground");
        classes.Should().Contain("hidden");
        classes.Should().Contain("lg:flex");
        classes.Should().NotContain("size-10");
        classes.Should().NotContain("hover:bg-accent");
    }
}
