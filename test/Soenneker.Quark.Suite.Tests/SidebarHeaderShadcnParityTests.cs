using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarHeader_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarHeader>(parameters => parameters
            .Add(p => p.ChildContent, "Workspace"));

        string classes = cut.Find("[data-slot='sidebar-header']").GetAttribute("class")!;

        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("p-2");
        classes.Should().NotContain("q-sidebar-header");
    }
}
