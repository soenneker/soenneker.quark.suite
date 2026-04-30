using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarContent_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarContent>(parameters => parameters
            .Add(p => p.ChildContent, "Content"));

        var classes = cut.Find("[data-slot='sidebar-content']").GetAttribute("class")!;

        classes.Should().Contain("min-h-0");
        classes.Should().Contain("flex-1");
        classes.Should().Contain("overflow-auto");
        classes.Should().Contain("group-data-[collapsible=icon]:overflow-hidden");
        classes.Should().NotContain("no-scrollbar");
        classes.Should().NotContain("overflow-y-auto");
        classes.Should().NotContain("overflow-x-hidden");
        classes.Should().NotContain("q-sidebar-content");
    }
}
