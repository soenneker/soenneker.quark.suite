using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarMenu_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarMenu>(parameters => parameters
            .Add(p => p.ChildContent, "Menu"));

        var classes = cut.Find("[data-slot='sidebar-menu']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("w-full");
        classes.Should().Contain("flex-col");
        classes.Should().NotContain("q-sidebar-menu");
    }
}
