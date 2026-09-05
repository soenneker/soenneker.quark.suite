using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ChartRenderTests : BunitContext
{
    private static readonly string[] Labels = ["Jan", "Feb", "Mar"];

    public ChartRenderTests()
    {
        Services.AddDefaultQuarkOptionsAsScoped();
    }

    [Test]
    public void Repeated_hover_and_clear_skip_noop_renders()
    {
        var cut = Render<Chart>(p => p.Add(c => c.Labels, Labels)
            .Add(c => c.Series, new ChartSeries[] { new("Revenue", new double[] { 10, 20, 30 }) }));
        var hit = cut.FindAll(".quark-chart-hit-area")[1];
        hit.TriggerEvent("onpointerenter", new PointerEventArgs());
        int renders = cut.RenderCount;
        hit.TriggerEvent("onpointerdown", new PointerEventArgs());
        cut.RenderCount.Should().Be(renders);
        cut.Find(".quark-chart-tooltip-label").TextContent.Should().Be("Feb");
        cut.Find("[data-slot=chart]").TriggerEvent("onpointerleave", new PointerEventArgs());
        cut.FindAll(".quark-chart-tooltip").Should().BeEmpty();
        renders = cut.RenderCount;
        cut.Find("[data-slot=chart]").TriggerEvent("onpointerleave", new PointerEventArgs());
        cut.RenderCount.Should().Be(renders);
    }

    [Test]
    public void Chart_rerenders_when_series_parameters_change()
    {
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, new[] { "Jan", "Feb" })
            .Add(component => component.Series, new ChartSeries[] { new("Revenue", new[] { 10d, 20d }) }));

        cut.Markup.Should().Contain("20");

        cut.Render(parameters => parameters
            .Add(component => component.Labels, new[] { "Mar", "Apr" })
            .Add(component => component.Series, new ChartSeries[] { new("Revenue", new[] { 30d, 40d }) }));

        cut.Markup.Should().Contain("Apr");
        cut.Markup.Should().Contain("40");
        cut.Markup.Should().NotContain("Feb");
    }

    [Test]
    public void Stable_chart_inputs_do_not_recalculate_geometry()
    {
        var values = new CountingReadOnlyList<double?>([10, 20, 30]);
        ChartSeries[] series = [new("Revenue", values)];
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series));
        var readsAfterFirstRender = values.ReadCount;

        cut.Render(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series));

        values.ReadCount.Should().Be(readsAfterFirstRender);
    }

    [Test]
    public void Hover_renders_interaction_content_without_rebuilding_static_geometry()
    {
        var formattedLabels = 0;
        var labels = Enumerable.Range(1, 100).Select(index => $"Point {index}").ToArray();
        ChartSeries[] series = [new("Revenue", Enumerable.Range(1, 100).Select(static value => (double)value).ToArray())];
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, labels)
            .Add(component => component.Series, series)
            .Add(component => component.Options, new ChartOptions
            {
                LabelFormatter = value =>
                {
                    formattedLabels++;
                    return value;
                }
            }));
        var formatsAfterFirstRender = formattedLabels;

        cut.FindAll(".quark-chart-hit-area")[50].TriggerEvent("onpointerenter", new PointerEventArgs());

        (formattedLabels - formatsAfterFirstRender).Should().BeLessThanOrEqualTo(2);
        cut.Find(".quark-chart-tooltip-label").TextContent.Should().Be("Point 51");
        cut.FindAll(".quark-chart-point-active").Should().ContainSingle();
    }

    [Test]
    public void Line_chart_renders_responsive_svg_axes_legend_and_accessible_data()
    {
        ChartSeries[] series =
        [
            new("Revenue", new double[] { 12, 18, 15 }) { Color = "#7c5cff" },
            new("Cost", new double[] { 8, 11, 10 }) { Color = "#00bfa6" }
        ];

        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series)
            .Add(component => component.AriaLabel, "Monthly performance"));

        cut.Find("[data-slot='chart']").GetAttribute("class").Should().Contain("quark-chart");
        cut.Find("svg").GetAttribute("viewBox").Should().Be("0 0 800 320");
        cut.Find("svg").GetAttribute("aria-label").Should().Be("Monthly performance");
        cut.FindAll("[data-slot='chart-line']").Should().HaveCount(2);
        cut.FindAll(".quark-chart-point").Should().HaveCount(6);
        cut.FindAll(".quark-chart-legend-item").Should().HaveCount(2);
        cut.FindAll(".quark-chart-sr-only tbody tr").Should().HaveCount(3);
    }

    [Test]
    public void Area_chart_emits_gradient_and_handles_null_gaps()
    {
        ChartSeries[] series =
        [
            new("Traffic", new double?[] { 12, null, 24 })
            {
                Color = "#8b5cf6",
                Gradient = new ChartGradient("#8b5cf6", "transparent")
            }
        ];

        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Type, ChartType.Area)
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series));

        cut.FindAll("linearGradient").Should().ContainSingle();
        cut.Find("[data-slot='chart-area']").GetAttribute("fill").Should().StartWith("url(#quark-chart-");
        var path = cut.Find("[data-slot='chart-line']").GetAttribute("d")!;
        System.Linq.Enumerable.Count(path, static character => character == 'M').Should().Be(2);
    }

    [Test]
    public void Bar_chart_supports_grouped_and_stacked_series()
    {
        ChartSeries[] series =
        [
            new("Desktop", new double[] { 10, 14, 18 }),
            new("Mobile", new double[] { 8, 12, 15 })
        ];

        var grouped = Render<Chart>(parameters => parameters
            .Add(component => component.Type, ChartType.Bar)
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series));
        var stacked = Render<Chart>(parameters => parameters
            .Add(component => component.Type, ChartType.Bar)
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series)
            .Add(component => component.Options, new ChartOptions { Stacked = true }));

        grouped.FindAll("[data-slot='chart-bar']").Should().HaveCount(6);
        stacked.FindAll("[data-slot='chart-bar']").Should().HaveCount(6);
        grouped.FindAll("[data-slot='chart-bar']")[0].GetAttribute("width")
            .Should().NotBe(stacked.FindAll("[data-slot='chart-bar']")[0].GetAttribute("width"));
    }

    [Test]
    public void Pie_and_donut_render_slices_and_center_content()
    {
        ChartSeries[] series = [new("Visitors", new double[] { 40, 35, 25 })];

        var pie = Render<Chart>(parameters => parameters
            .Add(component => component.Type, ChartType.Pie)
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series));
        var donut = Render<Chart>(parameters => parameters
            .Add(component => component.Type, ChartType.Donut)
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series)
            .Add(component => component.DonutContent, "100 visitors"));

        pie.FindAll("[data-slot='chart-slice']").Should().HaveCount(3);
        donut.FindAll("[data-slot='chart-slice']").Should().HaveCount(3);
        donut.Find(".quark-chart-donut-content").TextContent.Should().Contain("100 visitors");
        donut.Find(".quark-chart-donut-content > div").GetAttribute("style").Should().Contain("align-items:center").And.Contain("justify-content:center");
    }

    [Test]
    public void Hovering_a_category_shows_the_shared_tooltip()
    {
        ChartSeries[] series = [new("Revenue", new double[] { 12, 18, 15 })];
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series));

        cut.FindAll(".quark-chart-hit-area")[1].TriggerEvent("onpointerenter", new PointerEventArgs());

        cut.Find(".quark-chart-tooltip-label").TextContent.Should().Be("Feb");
        cut.Find(".quark-chart-tooltip-row").TextContent.Should().Contain("Revenue").And.Contain("18");
        cut.Find(".quark-chart-tooltip").GetAttribute("class").Should().Contain("min-w-48").And.Contain("w-max");
        cut.Find(".quark-chart-tooltip").GetAttribute("style").Should().Contain("transform:translate(12px,12px)");
        cut.Find(".quark-chart-tooltip-row").GetAttribute("class").Should().Contain("minmax(max-content,1fr)");
        cut.FindAll(".quark-chart-cursor").Should().ContainSingle().Which.GetAttribute("class").Should().Contain("opacity-70");
    }

    [Test]
    public void Line_chart_shows_its_terminal_marker_when_regular_points_are_hidden()
    {
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, new ChartSeries[] { new("Execution time", new double[] { 5.2, 5.1, 5.0 }) })
            .Add(component => component.Options, new ChartOptions { ShowPoints = false }));

        var marker = cut.Find(".quark-chart-point");
        double.Parse(marker.GetAttribute("cx")!, CultureInfo.InvariantCulture).Should().BeGreaterThan(600);
        marker.GetAttribute("aria-label").Should().Contain("Mar").And.Contain("5");
    }

    [Test]
    public void Smooth_curve_does_not_overshoot_the_plot_bounds()
    {
        string[] labels = ["One", "Two", "Three", "Four", "Five"];
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, labels)
            .Add(component => component.Series, new ChartSeries[] { new("Calls", new double[] { 0, 100, 0, 0, 100 }) })
            .Add(component => component.Options, new ChartOptions { Curve = ChartCurve.Smooth }));

        var path = cut.Find("[data-slot='chart-line']").GetAttribute("d")!;
        var yCoordinates = Regex.Matches(path, @",(-?\d+(?:\.\d+)?)")
            .Select(match => double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));

        yCoordinates.Should().AllSatisfy(y => y.Should().BeInRange(16, 282));
    }

    [Test]
    public void Sparkline_can_render_only_terminal_markers_without_a_cursor()
    {
        ChartSeries[] series =
        [
            new("Revenue", new double?[] { 12, 18, null }),
            new("Cost", new double[] { 8, 11, 10 })
        ];
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series)
            .Add(component => component.Options, new ChartOptions
            {
                ShowPoints = false,
                ShowEndPoints = true,
                ShowCursor = false,
                PointRadius = 4,
                PointFill = "#202020"
            }));

        cut.FindAll(".quark-chart-point").Should().HaveCount(2);
        cut.FindAll(".quark-chart-point")[0].GetAttribute("r").Should().Be("4");
        cut.FindAll(".quark-chart-point")[0].GetAttribute("fill").Should().Be("#202020");

        cut.FindAll(".quark-chart-hit-area")[1].TriggerEvent("onpointerenter", new PointerEventArgs());
        cut.FindAll(".quark-chart-cursor").Should().BeEmpty();
        cut.FindAll(".quark-chart-tooltip").Should().ContainSingle();
        cut.FindAll(".quark-chart-point-active").Should().HaveCount(2);
        cut.FindAll(".quark-chart-point-active")[0].GetAttribute("cx").Should().Be(
            cut.FindAll(".quark-chart-point-active")[1].GetAttribute("cx"));
    }

    [Test]
    public void Arbitrary_header_overlay_and_footer_content_can_be_composed_around_the_plot()
    {
        ChartSeries[] series = [new("Revenue", new double[] { 12, 18, 15 })];
        RenderFragment header = builder =>
        {
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "href", "/usage");
            builder.AddContent(2, "Usage details");
            builder.CloseElement();
        };
        RenderFragment<ChartOverlayContext> overlay = context => builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "class", "pointer-events-auto absolute right-0 top-0");
            builder.AddAttribute(2, "style", context.Style(1, 18));
            builder.AddContent(3, "Inspect");
            builder.CloseElement();
        };

        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series)
            .Add(component => component.HeaderContent, header)
            .Add(component => component.OverlayContent, overlay)
            .Add(component => component.FooterContent, "Source: internal analytics"));

        cut.Find("[data-slot='chart-header'] a").GetAttribute("href").Should().Be("/usage");
        cut.Find("[data-slot='chart-overlay']").GetAttribute("class").Should().Contain("pointer-events-none");
        cut.Find("[data-slot='chart-overlay'] button").GetAttribute("class").Should().Contain("pointer-events-auto");
        cut.Find("[data-slot='chart-overlay'] button").GetAttribute("style").Should().Contain("left:").And.Contain("top:");
        cut.Find("[data-slot='chart-footer']").TextContent.Should().Contain("Source: internal analytics");
    }

    [Test]
    public void Custom_tooltip_receives_every_visible_series_row()
    {
        ChartTooltipContext? captured = null;
        RenderFragment<ChartTooltipContext> template = context => builder =>
        {
            captured = context;
            builder.AddContent(0, $"{context.Label}: {context.Rows.Count} rows");
        };
        ChartSeries[] series =
        [
            new("Revenue", new double[] { 12, 18, 15 }),
            new("Cost", new double[] { 8, 11, 10 })
        ];

        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series)
            .Add(component => component.TooltipTemplate, template));

        cut.FindAll(".quark-chart-hit-area")[1].TriggerEvent("onpointerenter", new PointerEventArgs());

        captured.Should().NotBeNull();
        captured!.Label.Should().Be("Feb");
        captured.Rows.Select(row => row.Series).Should().Equal("Revenue", "Cost");
        cut.Find(".quark-chart-tooltip").TextContent.Should().Contain("2 rows");
    }

    [Test]
    public void Continuous_x_values_control_spacing_and_labels_are_thinned_and_rotated()
    {
        string[] labels = Enumerable.Range(1, 20).Select(index => $"Day {index}").ToArray();
        var values = Enumerable.Range(1, 20).Select(index => (double)index).ToArray();
        var xValues = Enumerable.Range(0, 20).Select(index => index == 0 ? 0d : Math.Pow(index, 2)).ToArray();
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, labels)
            .Add(component => component.XValues, xValues)
            .Add(component => component.Series, new ChartSeries[] { new("Traffic", values) })
            .Add(component => component.Options, new ChartOptions { MaximumXAxisLabels = 5, XAxisLabelRotation = -30 }));

        var points = cut.FindAll(".quark-chart-point");
        var x0 = double.Parse(points[0].GetAttribute("cx")!, CultureInfo.InvariantCulture);
        var x1 = double.Parse(points[1].GetAttribute("cx")!, CultureInfo.InvariantCulture);
        var x2 = double.Parse(points[2].GetAttribute("cx")!, CultureInfo.InvariantCulture);
        (x1 - x0).Should().BeLessThan(x2 - x1);
        cut.FindAll("g[transform*='rotate(-30)']").Count.Should().BeLessThanOrEqualTo(5);
        cut.FindAll("g[transform*='rotate(-30)']").Count.Should().BeGreaterThanOrEqualTo(2);
    }

    [Test]
    public void Monotone_curve_nice_ticks_and_invalid_values_are_supported()
    {
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, new ChartSeries[] { new("Revenue", new[] { 12d, double.NaN, 18d }) })
            .Add(component => component.Options, new ChartOptions { Curve = ChartCurve.Monotone }));

        var path = cut.Find("[data-slot='chart-line']").GetAttribute("d")!;
        path.Should().NotContain("NaN");
        System.Linq.Enumerable.Count(path, static character => character == 'M').Should().Be(2);

        var smooth = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, new ChartSeries[] { new("Revenue", new[] { 12d, 18d, 15d }) })
            .Add(component => component.Options, new ChartOptions { Curve = ChartCurve.Monotone }));
        smooth.Find("[data-slot='chart-line']").GetAttribute("d").Should().Contain("C");
        smooth.FindAll(".quark-chart-axis-label").Select(element => element.TextContent).Should().Contain("20");
    }

    [Test]
    public void Radial_gradients_use_semantic_legend_items_and_non_positive_data_is_empty()
    {
        ChartSeries[] series =
        [
            new("Visitors", new double[] { 40, 35, 25 })
            {
                SliceGradients = [new ChartGradient("#8b5cf6", "#4f46e5") { EndOpacity = 1 }, null, null]
            }
        ];
        var pie = Render<Chart>(parameters => parameters
            .Add(component => component.Type, ChartType.Pie)
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series));

        pie.FindAll("linearGradient").Should().ContainSingle();
        pie.FindAll("[data-slot='chart-slice']")[0].GetAttribute("fill").Should().StartWith("url(#quark-chart-");
        pie.FindAll(".quark-chart-legend-item").Should().AllSatisfy(item => item.TagName.Should().Be("SPAN"));

        var empty = Render<Chart>(parameters => parameters
            .Add(component => component.Type, ChartType.Donut)
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, new ChartSeries[] { new("Invalid", new double[] { 0, -1, double.NaN }) }));
        empty.FindAll(".quark-chart-empty").Should().ContainSingle();
        empty.FindAll("[data-slot='chart-slice']").Should().BeEmpty();
    }

    [Test]
    public void Keyboard_can_navigate_and_select_chart_marks()
    {
        ChartSelection? selection = null;
        ChartSeries[] series = [new("Revenue", new double[] { 12, 18, 15 })];
        var cut = Render<Chart>(parameters => parameters
            .Add(component => component.Labels, Labels)
            .Add(component => component.Series, series)
            .Add(component => component.OnSelect, selected => selection = selected));

        var first = cut.FindAll(".quark-chart-point")[0];
        first.KeyDown(new KeyboardEventArgs { Key = "ArrowRight" });
        cut.Find(".quark-chart-tooltip-label").TextContent.Should().Be("Feb");
        cut.FindAll(".quark-chart-point")[0].KeyDown(new KeyboardEventArgs { Key = "Enter" });

        selection.Should().NotBeNull();
        selection!.Index.Should().Be(1);
        selection.SeriesIndex.Should().Be(0);
        selection.Value.Should().Be(18);
    }
}

public sealed class CountingReadOnlyList<T>(IReadOnlyList<T> values) : IReadOnlyList<T>
{
    public int ReadCount { get; private set; }

    public T this[int index]
    {
        get
        {
            ReadCount++;
            return values[index];
        }
    }

    public int Count => values.Count;

    public IEnumerator<T> GetEnumerator()
    {
        for (var index = 0; index < values.Count; index++)
            yield return this[index];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
