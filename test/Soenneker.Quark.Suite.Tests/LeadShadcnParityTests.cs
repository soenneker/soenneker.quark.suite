using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Lead_matches_shadcn_base_classes()
    {
        var lead = Render<Lead>(parameters => parameters.Add(p => p.ChildContent, "A modal dialog"));
        lead.Find("[data-slot='lead']").GetAttribute("class")!.Should().ContainAll("text-xl", "text-muted-foreground");
    }
}
