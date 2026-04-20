using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void SidebarInput_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarInput>();

        string classes = cut.Find("[data-slot='sidebar-input']").GetAttribute("class")!;

        classes.Should().Contain("h-8");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("border-input");
        classes.Should().NotContain("q-sidebar-input");
    }
}
