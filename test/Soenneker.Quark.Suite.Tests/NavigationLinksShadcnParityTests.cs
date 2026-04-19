using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void NavigationLinks_header_mode_matches_docs_button_shell()
    {
        var cut = Render<NavigationLinks>(parameters => parameters
            .Add(p => p.Mode, NavigationLinksMode.Header)
            .Add(p => p.Items, [new NavigationItem("Docs", "/docs")]));

        string classes = cut.Find("a").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("border");
        classes.Should().Contain("border-transparent");
        classes.Should().Contain("bg-clip-padding");
        classes.Should().Contain("select-none");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("dark:hover:bg-accent/50");
        classes.Should().NotContain("transition-colors");
    }
}
