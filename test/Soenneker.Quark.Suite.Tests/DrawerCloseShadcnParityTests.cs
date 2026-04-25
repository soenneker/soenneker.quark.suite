using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DrawerClose_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerClose>();

        var buttonClasses = cut.Find("[data-slot='drawer-close']").GetAttribute("class")!;

        buttonClasses.Should().NotContain("q-button");
        buttonClasses.Should().Contain("size-9");
        buttonClasses.Should().Contain("hover:bg-accent");
        buttonClasses.Should().Contain("hover:text-accent-foreground");
    }
}
