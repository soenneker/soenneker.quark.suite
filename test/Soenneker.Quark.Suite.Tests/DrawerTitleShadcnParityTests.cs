using AwesomeAssertions;
using Bunit;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DrawerTitle_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerTitle>(parameters => parameters.Add(p => p.ChildContent, "Edit profile"));

        var classes = cut.Find("[data-slot='drawer-title']").GetAttribute("class")!;

        classes.Should().Contain("font-semibold");
        classes.Should().Contain("leading-none");
    }
}
