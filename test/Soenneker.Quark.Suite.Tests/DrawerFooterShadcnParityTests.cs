using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DrawerFooter_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerFooter>(parameters => parameters.Add(p => p.ChildContent, "Actions"));

        var classes = cut.Find("[data-slot='drawer-footer']").GetAttribute("class")!;

        classes.Should().Contain("mt-auto");
        classes.Should().Contain("flex");
        classes.Should().Contain("flex-col");
        classes.Should().Contain("gap-2");
        classes.Should().Contain("p-4");
    }
}
