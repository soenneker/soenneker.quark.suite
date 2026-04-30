using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Blockquote_matches_shadcn_base_classes()
    {
        var blockquote = Render<Blockquote>(parameters => parameters.Add(p => p.ChildContent, "Blockquote"));
        var classes = blockquote.Find("[data-slot='blockquote']").GetAttribute("class")!;

        classes.Should().ContainAll("mt-6", "border-s-2", "ps-6", "italic");
        classes.Should().NotContain("border-l-2");
        classes.Should().NotContain("pl-6");
    }
}
