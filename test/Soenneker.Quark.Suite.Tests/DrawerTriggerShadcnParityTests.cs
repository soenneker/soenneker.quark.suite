using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DrawerTrigger_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerTrigger>(parameters => parameters.Add(p => p.ChildContent, "Open Drawer"));

        var classes = cut.Find("[data-slot='drawer-trigger']").GetAttribute("class")!;

        classes.Should().BeNullOrEmpty();
        cut.Find("[data-slot='drawer-trigger']").GetAttribute("aria-haspopup").Should().Be("dialog");
        cut.Find("[data-slot='drawer-trigger']").GetAttribute("data-state").Should().Be("closed");
    }
}
