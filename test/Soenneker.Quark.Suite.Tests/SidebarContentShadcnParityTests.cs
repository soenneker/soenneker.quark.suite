using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarContent_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarContent>(parameters => parameters
            .Add(p => p.ChildContent, "Content"));

        string classes = cut.Find("[data-slot='sidebar-content']").GetAttribute("class")!;

        classes.Should().Contain("no-scrollbar");
        classes.Should().Contain("flex-1");
        classes.Should().Contain("overflow-y-auto");
        classes.Should().NotContain("q-sidebar-content");
    }
}
