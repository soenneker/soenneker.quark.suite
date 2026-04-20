using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Blockquote_matches_shadcn_base_classes()
    {
        var blockquote = Render<Blockquote>(parameters => parameters.Add(p => p.ChildContent, "Blockquote"));
        blockquote.Find("[data-slot='blockquote']").GetAttribute("class")!.Should().ContainAll("mt-6", "border-l-2", "pl-6", "italic");
    }
}
