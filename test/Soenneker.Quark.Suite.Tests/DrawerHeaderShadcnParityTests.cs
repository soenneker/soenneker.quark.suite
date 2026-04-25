using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DrawerHeader_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerHeader>(parameters => parameters.Add(p => p.ChildContent, "Header"));

        var classes = cut.Find("[data-slot='drawer-header']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("gap-0.5");
        classes.Should().Contain("p-4");
        classes.Should().Contain("group-data-[vaul-drawer-direction=bottom]/drawer-content:text-center");
        classes.Should().Contain("group-data-[vaul-drawer-direction=top]/drawer-content:text-center");
        classes.Should().Contain("md:text-left");
        classes.Should().NotContain("group-data-[direction=bottom]/drawer-content:text-center");
    }
}
