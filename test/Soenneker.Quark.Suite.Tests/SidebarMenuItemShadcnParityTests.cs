using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarMenuItem_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarMenuItem>(parameters => parameters
            .Add(p => p.ChildContent, "Item"));

        var classes = cut.Find("[data-slot='sidebar-menu-item']").GetAttribute("class")!;

        classes.Should().Contain("group/menu-item");
        classes.Should().Contain("relative");
        classes.Should().NotContain("q-sidebar-menu-item");
    }
}
