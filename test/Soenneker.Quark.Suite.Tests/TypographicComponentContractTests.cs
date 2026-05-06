using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Anchor_preserves_aria_current()
    {
        var cut = Render<Anchor>(parameters => parameters
            .Add(p => p.Href, "/docs")
            .Add(p => p.AriaCurrent, "page")
            .Add(p => p.ChildContent, "Docs"));

        var anchor = cut.Find("a");

        anchor.GetAttribute("href").Should().Be("/docs");
        anchor.GetAttribute("aria-current").Should().Be("page");
    }
}
