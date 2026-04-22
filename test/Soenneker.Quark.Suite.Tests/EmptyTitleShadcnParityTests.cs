using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void EmptyTitle_matches_shadcn_base_classes()
    {
        var cut = Render<EmptyTitle>(parameters => parameters
            .Add(p => p.ChildContent, "Title"));

        var classes = cut.Find("[data-slot='empty-title']").GetAttribute("class")!;

        classes.Should().Contain("cn-font-heading");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("tracking-tight");
    }
}
