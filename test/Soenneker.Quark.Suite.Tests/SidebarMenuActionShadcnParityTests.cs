using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarMenuAction_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarMenuAction>(parameters => parameters
            .Add(p => p.ChildContent, "+"));

        string classes = cut.Find("[data-slot='sidebar-menu-action']").GetAttribute("class")!;

        classes.Should().Contain("absolute");
        classes.Should().Contain("top-1.5");
        classes.Should().Contain("right-1");
        classes.Should().Contain("rounded-md");
        classes.Should().NotContain("q-sidebar-menu-action");
    }
}
