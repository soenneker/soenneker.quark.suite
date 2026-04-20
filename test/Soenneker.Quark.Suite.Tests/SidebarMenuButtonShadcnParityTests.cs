using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarMenuButton_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarMenuButton>(parameters => parameters
            .Add(p => p.Variant, SidebarMenuButtonVariant.Navigation)
            .Add(p => p.Active, true)
            .Add(p => p.ChildContent, "Dashboard"));

        string classes = cut.Find("[data-slot='sidebar-menu-button']").GetAttribute("class")!;

        classes.Should().Contain("peer/menu-button");
        classes.Should().Contain("w-fit");
        classes.Should().Contain("overflow-visible");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("after:absolute");
        classes.Should().Contain("3xl:fixed:w-full");
        classes.Should().Contain("data-[active=true]:bg-sidebar-accent");
        classes.Should().NotContain("q-sidebar-menu-button");
    }
}
