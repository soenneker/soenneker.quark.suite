using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarMenuBadge_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarMenuBadge>(parameters => parameters
            .Add(p => p.ChildContent, "4"));

        string classes = cut.Find("[data-slot='sidebar-menu-badge']").GetAttribute("class")!;

        classes.Should().Contain("absolute");
        classes.Should().Contain("right-1");
        classes.Should().Contain("text-xs");
        classes.Should().NotContain("q-sidebar-menu-badge");
    }
}
