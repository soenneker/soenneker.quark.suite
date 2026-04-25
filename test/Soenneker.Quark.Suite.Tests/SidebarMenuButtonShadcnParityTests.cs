using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarMenuButton_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarMenuButton>(parameters => parameters
            .Add(p => p.Active, true)
            .Add(p => p.ChildContent, "Dashboard"));

        var classes = cut.Find("[data-slot='sidebar-menu-button']").GetAttribute("class")!;

        classes.Should().Contain("peer/menu-button");
        classes.Should().Contain("w-full");
        classes.Should().Contain("overflow-hidden");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("p-2");
        classes.Should().Contain("text-left");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("ring-sidebar-ring");
        classes.Should().Contain("transition-[width,height,padding]");
        classes.Should().Contain("hover:bg-sidebar-accent");
        classes.Should().Contain("hover:text-sidebar-accent-foreground");
        classes.Should().Contain("focus-visible:ring-2");
        classes.Should().Contain("data-[active=true]:bg-sidebar-accent");
        classes.Should().Contain("data-[active=true]:font-medium");
        classes.Should().NotContain("w-fit");
        classes.Should().NotContain("overflow-visible");
        classes.Should().NotContain("3xl:fixed:w-full");
        classes.Should().NotContain("q-sidebar-menu-button");
    }
}
