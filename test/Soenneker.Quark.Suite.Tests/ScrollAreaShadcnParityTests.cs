using AwesomeAssertions;
using Bunit;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ScrollArea_matches_shadcn_default_component_contract()
    {
        var area = Render<ScrollArea>(parameters => parameters
            .Add(p => p.ChildContent, "Content"));

        var root = area.Find("[data-slot='scroll-area']");
        var viewport = area.Find("[data-slot='scroll-area-viewport']");

        var rootClasses = root.GetAttribute("class")!;
        var viewportClasses = viewport.GetAttribute("class")!;

        rootClasses.Should().Contain("relative");

        viewportClasses.Should().Contain("size-full");
        viewportClasses.Should().Contain("rounded-[inherit]");
        viewportClasses.Should().Contain("transition-[color,box-shadow]");
        viewportClasses.Should().Contain("outline-none");
        viewportClasses.Should().Contain("focus-visible:ring-[3px]");
        viewportClasses.Should().Contain("focus-visible:ring-ring/50");
        viewportClasses.Should().Contain("focus-visible:outline-1");
        viewportClasses.Should().NotContain("overflow-auto");
        viewportClasses.Should().NotContain("[&::-webkit-scrollbar]:hidden");
    }
}
