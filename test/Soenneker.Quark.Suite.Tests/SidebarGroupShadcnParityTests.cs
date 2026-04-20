using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarGroup_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarGroup>(parameters => parameters
            .Add(p => p.ChildContent, "Group"));

        var classes = cut.Find("[data-slot='sidebar-group']").GetAttribute("class")!;

        classes.Should().Contain("relative");
        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().NotContain("q-sidebar-group");
    }
}
