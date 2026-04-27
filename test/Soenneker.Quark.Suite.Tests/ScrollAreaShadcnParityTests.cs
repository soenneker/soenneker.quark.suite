using AwesomeAssertions;
using Bunit;
using System.Linq;


namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void ScrollArea_matches_shadcn_default_component_contract()
    {
        var area = Render<ScrollArea>(parameters => parameters
            .Add(p => p.ForceMountScrollBars, true)
            .Add(p => p.ChildContent, "Content"));

        var root = area.Find("[data-slot='scroll-area']");
        var viewport = area.Find("[data-slot='scroll-area-viewport']");
        var scrollbar = area.Find("[data-slot='scroll-area-scrollbar']");
        var thumb = area.Find("[data-slot='scroll-area-thumb']");

        var rootClasses = root.GetAttribute("class")!;
        var viewportClasses = viewport.GetAttribute("class")!;
        var scrollbarClasses = scrollbar.GetAttribute("class")!;
        var thumbClasses = thumb.GetAttribute("class")!;

        rootClasses.Should().Contain("relative");

        viewportClasses.Should().Contain("size-full");
        viewportClasses.Should().Contain("rounded-[inherit]");
        viewportClasses.Should().Contain("transition-[color,shadow]");
        viewportClasses.Should().Contain("outline-none");
        viewportClasses.Should().Contain("focus-visible:ring-[3px]");
        viewportClasses.Should().Contain("focus-visible:ring-ring/50");
        viewportClasses.Should().Contain("focus-visible:outline-1");
        viewportClasses.Should().NotContain("overflow-auto");
        viewportClasses.Should().NotContain("[&::-webkit-scrollbar]:hidden");

        scrollbar.GetAttribute("data-orientation")!.Should().Be("vertical");
        scrollbarClasses.Should().Contain("flex");
        scrollbarClasses.Should().Contain("touch-none");
        scrollbarClasses.Should().Contain("p-px");
        scrollbarClasses.Should().Contain("transition-colors");
        scrollbarClasses.Should().Contain("select-none");
        scrollbarClasses.Should().Contain("data-[orientation=vertical]:h-full");
        scrollbarClasses.Should().Contain("data-[orientation=vertical]:w-2.5");
        scrollbarClasses.Should().Contain("data-[orientation=vertical]:border-l");

        thumbClasses.Should().Contain("relative");
        thumbClasses.Should().Contain("flex-1");
        thumbClasses.Should().Contain("rounded-full");
        thumbClasses.Should().Contain("bg-border");
    }

    [Test]
    public void ScrollArea_horizontal_scrollbar_matches_shadcn_orientation_classes()
    {
        var area = Render<ScrollArea>(parameters => parameters
            .Add(p => p.ShowHorizontalScrollBar, true)
            .Add(p => p.ForceMountScrollBars, true)
            .Add(p => p.ChildContent, "Content"));

        var horizontalScrollbar = area.FindAll("[data-slot='scroll-area-scrollbar']")
                                      .Single(element => element.GetAttribute("data-orientation") == "horizontal");
        var classes = horizontalScrollbar.GetAttribute("class")!;

        classes.Should().Contain("data-[orientation=horizontal]:h-2.5");
        classes.Should().Contain("data-[orientation=horizontal]:flex-col");
        classes.Should().Contain("data-[orientation=horizontal]:border-t");
        classes.Should().Contain("data-[orientation=horizontal]:border-t-transparent");
    }
}
