using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void Small_matches_shadcn_base_classes()
    {
        var small = Render<Small>(parameters => parameters.Add(p => p.ChildContent, "Small print"));
        small.Find("[data-slot='small']").GetAttribute("class")!.Should().ContainAll("text-sm", "font-medium", "leading-none");
    }
}
