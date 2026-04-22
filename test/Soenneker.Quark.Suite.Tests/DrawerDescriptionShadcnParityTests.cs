using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DrawerDescription_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerDescription>(parameters => parameters.Add(p => p.ChildContent, "Make changes to your profile here."));

        var classes = cut.Find("[data-slot='drawer-description']").GetAttribute("class")!;

        classes.Should().Contain("text-muted-foreground");
        classes.Should().Contain("text-sm");
    }
}
