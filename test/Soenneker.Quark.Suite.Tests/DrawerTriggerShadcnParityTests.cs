using AwesomeAssertions;
using Bunit;
using Soenneker.Quark;
using Xunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Fact]
    public void DrawerTrigger_matches_shadcn_default_component_contract()
    {
        var cut = Render<DrawerTrigger>(parameters => parameters.Add(p => p.ChildContent, "Open Drawer"));

        string classes = cut.Find("[data-slot='drawer-trigger']").GetAttribute("class")!;

        classes.Should().Contain("group/button");
        classes.Should().Contain("inline-flex");
        classes.Should().Contain("shrink-0");
        classes.Should().Contain("items-center");
        classes.Should().Contain("justify-center");
        classes.Should().Contain("rounded-lg");
        classes.Should().Contain("border");
        classes.Should().Contain("text-sm");
        classes.Should().Contain("font-medium");
        classes.Should().Contain("whitespace-nowrap");
        classes.Should().Contain("transition-all");
        classes.Should().Contain("outline-none");
        classes.Should().Contain("select-none");
        classes.Should().Contain("focus-visible:border-ring");
        classes.Should().Contain("focus-visible:ring-3");
        classes.Should().Contain("border-border");
        classes.Should().Contain("bg-background");
        classes.Should().Contain("hover:bg-muted");
        classes.Should().Contain("aria-expanded:bg-muted");
        classes.Should().Contain("h-8");
        classes.Should().Contain("gap-1.5");
        classes.Should().Contain("px-2.5");
    }
}
