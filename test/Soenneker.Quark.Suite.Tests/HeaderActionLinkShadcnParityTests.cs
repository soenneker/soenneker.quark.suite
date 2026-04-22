using AwesomeAssertions;
using Bunit;
using HeaderActionLinkComponent = Soenneker.Quark.Header.HeaderActionLink;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void HeaderActionLink_matches_docs_header_button_contract()
    {
        var cut = Render<HeaderActionLinkComponent>(parameters => parameters
            .Add(p => p.To, "https://example.com")
            .Add(p => p.Label, "GitHub")
            .Add(p => p.AriaLabel, "View GitHub"));

        var classes = cut.Find("a").GetAttribute("class")!;

        classes.Should().Contain("border");
        classes.Should().Contain("border-transparent");
        classes.Should().Contain("bg-clip-padding");
        classes.Should().Contain("select-none");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("dark:hover:bg-accent/50");
        classes.Should().NotContain("hover:bg-muted");
    }
}
