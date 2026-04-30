using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void SidebarGroupLabel_matches_shadcn_default_component_contract()
    {
        var cut = Render<SidebarGroupLabel>(parameters => parameters
            .Add(p => p.ChildContent, "Platform"));

        var classes = cut.Find("[data-slot='sidebar-group-label']").GetAttribute("class")!;

        classes.Should().Contain("text-sidebar-foreground/70");
        classes.Should().Contain("h-8");
        classes.Should().Contain("text-xs");
        classes.Should().NotContain("text-muted-foreground");
        classes.Should().NotContain("q-sidebar-group-label");
    }
}
