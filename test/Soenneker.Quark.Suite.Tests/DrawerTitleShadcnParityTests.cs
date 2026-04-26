using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DrawerTitle_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerTitle>(parameters => parameters.Add(p => p.ChildContent, "Edit profile"));

        var classes = cut.Find("[data-slot='drawer-title']").GetAttribute("class")!;

        classes.Should().Contain("font-medium");
        classes.Should().Contain("cn-font-heading");
        classes.Should().Contain("text-foreground");
        classes.Should().NotContain("leading-none");
    }
}
