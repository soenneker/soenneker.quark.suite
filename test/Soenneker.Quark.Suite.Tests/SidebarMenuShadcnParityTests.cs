using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarMenu_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarMenu>(parameters => parameters
            .Add(p => p.ChildContent, "Menu"));

        string classes = cut.Find("[data-slot='sidebar-menu']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("w-full");
        classes.Should().Contain("flex-col");
        classes.Should().NotContain("q-sidebar-menu");
    }
}
