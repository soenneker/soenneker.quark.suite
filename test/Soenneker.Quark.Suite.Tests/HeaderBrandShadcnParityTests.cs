using AwesomeAssertions;
using Bunit;
using HeaderBrandComponent = Soenneker.Quark.Header.HeaderBrand;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void HeaderBrand_matches_docs_shell_link_contract()
    {
        var cut = Render<HeaderBrandComponent>(parameters => parameters
            .Add(p => p.Title, "Soenneker Quark Suite")
            .Add(p => p.Subtitle, "Blazor components for .NET"));

        var classes = cut.Find("a").GetAttribute("class")!;

        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("hover:text-foreground");
        classes.Should().NotContain("rounded-md");
        classes.Should().NotContain("transition-colors");
        classes.Should().NotContain("hover:bg-accent");
    }
}
