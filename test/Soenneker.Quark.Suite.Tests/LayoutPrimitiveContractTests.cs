using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Container_uses_centered_shadcn_style_wrapper_defaults()
    {
        var cut = Render<Container>(parameters => parameters.Add(p => p.ChildContent, "Content"));
        var element = cut.Find("[data-slot='container']");
        var classes = element.GetAttribute("class")!;

        classes.Should().Contain("mx-auto");
        classes.Should().Contain("w-full");
        classes.Should().Contain("max-w-7xl");
        classes.Should().Contain("px-4");
        classes.Should().Contain("md:px-6");
        classes.Should().Contain("lg:px-8");
    }

    [Test]
    public void Grid_and_grid_item_emit_builder_backed_layout_classes()
    {
        var grid = Render<Grid>(parameters => parameters
            .Add(p => p.Columns, GridCols.Is1.OnMd.Is3)
            .Add(p => p.Rows, GridRows.Is2)
            .Add(p => p.Gap, Gap.Is4)
            .Add(p => p.ChildContent, "Grid"));
        var item = Render<GridItem>(parameters => parameters
            .Add(p => p.ColumnSpan, ColumnSpan.Is1.OnMd.Is2)
            .Add(p => p.RowSpan, RowSpan.Is2)
            .Add(p => p.ChildContent, "Item"));

        var gridClasses = grid.Find("[data-slot='grid']").GetAttribute("class")!;
        var itemElement = item.Find("[data-slot='grid-item']");
        var itemClasses = itemElement.GetAttribute("class")!;

        gridClasses.Should().Contain("grid");
        gridClasses.Should().Contain("grid-cols-1");
        gridClasses.Should().Contain("md:grid-cols-3");
        gridClasses.Should().Contain("grid-rows-2");
        gridClasses.Should().Contain("gap-4");
        itemClasses.Should().Contain("col-span-1");
        itemClasses.Should().Contain("md:col-span-2");
        itemClasses.Should().Contain("row-span-2");
    }

    [Test]
    public void Stack_uses_builder_backed_flex_direction_and_wrap_overrides()
    {
        var vertical = Render<Stack>(parameters => parameters
            .Add(p => p.Gap, Gap.Is3)
            .Add(p => p.ChildContent, "Vertical"));
        var horizontal = Render<Stack>(parameters => parameters
            .Add(p => p.Orientation, StackOrientation.Horizontal)
            .Add(p => p.Inline, true)
            .Add(p => p.FlexWrap, FlexWrap.Wrap)
            .Add(p => p.ChildContent, "Horizontal"));

        var verticalClasses = vertical.Find("[data-slot='stack']").GetAttribute("class")!;
        var horizontalClasses = horizontal.Find("[data-slot='stack']").GetAttribute("class")!;

        verticalClasses.Should().Contain("flex");
        verticalClasses.Should().Contain("flex-col");
        verticalClasses.Should().Contain("gap-3");
        horizontalClasses.Should().Contain("inline-flex");
        horizontalClasses.Should().Contain("flex-row");
        horizontalClasses.Should().Contain("flex-wrap");
    }

    [Test]
    public void DirectionProvider_and_overlay_container_emit_expected_layout_contract()
    {
        var direction = Render<DirectionProvider>(parameters => parameters
            .Add(p => p.Dir, "RTL")
            .Add(p => p.ChildContent, "RTL"));
        var overlay = Render<OverlayContainer>(parameters => parameters.Add(p => p.ChildContent, "Overlay"));

        var directionElement = direction.Find("[data-slot='direction-provider']");
        var overlayClasses = overlay.Find("[data-slot='overlay-container']").GetAttribute("class")!;

        directionElement.GetAttribute("dir").Should().Be("rtl");
        overlayClasses.Should().Contain("relative");
    }
}
