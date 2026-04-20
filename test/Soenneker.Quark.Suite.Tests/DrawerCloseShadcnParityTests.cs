using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DrawerClose_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerClose>();

        var buttonClasses = cut.Find("[data-slot='drawer-close']").GetAttribute("class")!;

        buttonClasses.Should().NotContain("q-button");
        buttonClasses.Should().Contain("size-8");
    }
}
