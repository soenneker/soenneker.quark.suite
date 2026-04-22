using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Small_matches_shadcn_base_classes()
    {
        var small = Render<Small>(parameters => parameters.Add(p => p.ChildContent, "Small print"));
        small.Find("[data-slot='small']").GetAttribute("class")!.Should().ContainAll("text-sm", "font-medium", "leading-none");
    }
}
