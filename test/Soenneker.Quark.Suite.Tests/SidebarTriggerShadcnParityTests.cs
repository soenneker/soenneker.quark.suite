using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarTrigger_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarTrigger>();

        var classes = cut.Find("[data-slot='sidebar-trigger']").GetAttribute("class")!;

        classes.Should().Contain("inline-flex");
        classes.Should().Contain("rounded-md");
        classes.Should().Contain("size-7");
        classes.Should().Contain("hover:bg-accent");
        classes.Should().Contain("hover:text-accent-foreground");
        classes.Should().Contain("dark:hover:bg-accent/50");
        classes.Should().NotContain("group/button");
        classes.Should().NotContain("size-8");
        classes.Should().NotContain("hover:bg-muted");
        classes.Should().NotContain("q-sidebar-trigger");

        cut.Find("[data-slot='sidebar-trigger'] svg")
            .GetAttribute("class")!
            .Should()
            .Contain("rtl:rotate-180");
    }
}
