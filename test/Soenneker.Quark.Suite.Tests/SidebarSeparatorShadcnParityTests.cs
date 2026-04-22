using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarSeparator_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarSeparator>();

        var classes = cut.Find("[data-slot='sidebar-separator']").GetAttribute("class")!;

        classes.Should().Contain("bg-sidebar-border");
        classes.Should().Contain("h-px");
        classes.Should().Contain("mx-2");
        classes.Should().NotContain("q-sidebar-separator");
    }
}
