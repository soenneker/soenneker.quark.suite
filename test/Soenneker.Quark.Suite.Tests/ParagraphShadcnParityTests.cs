using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Paragraph_matches_shadcn_base_classes()
    {
        var paragraph = Render<Paragraph>(parameters => parameters.Add(p => p.ChildContent, "Paragraph"));
        paragraph.Find("[data-slot='paragraph']").GetAttribute("class")!.Should().ContainAll("leading-7", "[&:not(:first-child)]:mt-6");
    }
}
