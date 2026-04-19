using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Muted_matches_shadcn_base_classes()
    {
        var muted = Render<Muted>(parameters => parameters.Add(p => p.ChildContent, "Muted copy"));
        muted.Find("[data-slot='muted']").GetAttribute("class")!.Should().ContainAll("text-sm", "text-muted-foreground");
    }
}
