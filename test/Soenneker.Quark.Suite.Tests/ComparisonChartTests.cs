using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ComparisonChartTests : BunitContext
{
    public ComparisonChartTests()
    {
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddLogging();
        Services.AddQuarkComparisonChartAsScoped();
        var resources = JSInterop.SetupModule("./_content/Soenneker.Blazor.Utils.ResourceLoader/js/resourceloader.js");
        resources.SetupVoid("loadStyle", _ => true).SetVoidResult();
        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/comparisonchartinterop.js");
        module.SetupVoid("initialize", _ => true).SetVoidResult();
        module.SetupVoid("destroy", _ => true).SetVoidResult();
    }

    [Test]
    public void Sorting_respects_metric_direction_keeps_missing_last_and_preserves_input()
    {
        ComparisonChartRow[] rows =
        [
            new() { Name = "Slow", Values = [400] },
            new() { Name = "Missing", Values = [null] },
            new() { Name = "Fast", Values = [20] }
        ];
        var cut = Render<ComparisonChart>(p => p.Add(c => c.Rows, rows)
            .Add(c => c.Metrics, [new() { Name = "Latency", Maximum = 500, LowerIsBetter = true }]));

        cut.Find(".comparison-sort button").Click();
        cut.FindAll("tbody th button").Select(e => e.TextContent).Should().Equal("Fast", "Slow", "Missing");
        cut.Find("th[aria-sort]").GetAttribute("aria-sort").Should().Be("ascending");
        cut.Find(".comparison-sort button").Click();
        cut.FindAll("tbody th button").Select(e => e.TextContent).Should().Equal("Slow", "Fast", "Missing");
        rows.Select(r => r.Name).Should().Equal("Slow", "Missing", "Fast");
    }

    [Test]
    public void Logarithmic_geometry_clamps_marks_but_preserves_real_values_and_missing_data()
    {
        var cut = Render<ComparisonChart>(p => p
            .Add(c => c.Metrics, [new() { Name = "Cost", Minimum = 1, Maximum = 100, Logarithmic = true }])
            .Add(c => c.Rows, [new() { Name = "Middle", Values = [10] }, new() { Name = "Above", Values = [200] },
                new() { Name = "Invalid", Values = [double.NaN] }, new() { Name = "Absent", Values = [] }])
            .Add(c => c.MirrorFirstMetric, false));

        cut.FindAll(".comparison-value")[0].GetAttribute("style").Should().Contain("50%");
        cut.FindAll(".comparison-value")[1].GetAttribute("style").Should().Contain("100%");
        cut.FindAll(".comparison-number")[1].TextContent.Should().Be("200");
        cut.FindAll(".comparison-missing").Should().HaveCount(2);
        cut.FindAll("[data-mirrored=true]").Should().BeEmpty();
    }

    [Test]
    public void Selection_exposes_details_and_callback_and_animation_can_be_disabled()
    {
        var row = new ComparisonChartRow { Name = "Example", Values = [85, null], Highlighted = true };
        ComparisonChartRow? selected = null;
        var cut = Render<ComparisonChart>(p => p.Add(c => c.Rows, [row])
            .Add(c => c.Metrics, [new() { Name = "Score", ShowMarker = true }, new() { Name = "Speed" }])
            .Add(c => c.Animate, false).Add(c => c.OnSelect, value => selected = value));

        cut.Find("tbody button").Click();
        selected.Should().BeSameAs(row);
        cut.Find("tbody button").GetAttribute("aria-pressed").Should().Be("true");
        cut.Find(".comparison-detail").TextContent.Should().Contain("Score: 85").And.Contain("Speed: Not measured");
        cut.Find(".comparison").GetAttribute("data-animated").Should().Be("false");
        cut.FindAll(".comparison-marker").Should().ContainSingle();
    }

    [Test]
    public void Legend_emphasizes_groups_and_upper_values_render_striped_extensions()
    {
        var cut = Render<ComparisonChart>(p => p
            .Add(c => c.Rows, [new() { Name = "Primary", Values = [20], UpperValues = [30], Highlighted = true },
                new() { Name = "Reference", Values = [80], Reference = true }])
            .Add(c => c.Metrics, [new() { Name = "Latency", Ticks = [0, 50, 100] }]));

        cut.Find(".comparison-range").ParentElement!.GetAttribute("style").Should().Contain("--comparison-position:20%").And.Contain("--comparison-upper:30%");
        cut.FindAll(".comparison-axis > span").Should().HaveCount(3);
        cut.FindAll(".comparison-legend button")[0].Click();
        cut.FindAll("tbody tr[data-dimmed=true]").Should().ContainSingle();
        cut.Find("tbody tr[data-dimmed=true] button").TextContent.Trim().Should().Be("Reference");
        cut.FindAll(".comparison-legend button")[0].Click();
        cut.FindAll("tbody tr[data-dimmed=true]").Should().BeEmpty();
    }

    [Test]
    public async System.Threading.Tasks.Task Reveal_observers_survive_interaction_and_are_removed_when_animation_is_disabled()
    {
        var cut = Render<ComparisonChart>(p => p
            .Add(c => c.Rows, [new() { Name = "Primary", Values = [50], Highlighted = true }])
            .Add(c => c.Metrics, [new() { Name = "Score" }]));

        JSInterop.Invocations["initialize"].Should().ContainSingle();
        cut.Find("tbody button").Click();
        JSInterop.Invocations["initialize"].Should().ContainSingle();
        JSInterop.Invocations["destroy"].Should().BeEmpty();

        cut.Render(p => p.Add(c => c.Animate, false));
        JSInterop.Invocations["destroy"].Should().ContainSingle();
        cut.Render(p => p.Add(c => c.Animate, true));
        JSInterop.Invocations["initialize"].Should().HaveCount(2);
        await cut.Instance.DisposeAsync();
        JSInterop.Invocations["destroy"].Should().HaveCount(2);
    }

    [Test]
    public void Invalid_logarithmic_bounds_are_rejected()
    {
        Action render = () => Render<ComparisonChart>(p => p.Add(c => c.Metrics,
            [new() { Name = "Invalid", Logarithmic = true, Minimum = 0 }]));
        render.Should().Throw<ArgumentException>();
    }

    [Test]
    public async Task Tick_cache_reuses_styles_and_observes_in_place_custom_tick_changes()
    {
        double[] ticks = [0, 50, 100];
        var cut = Render<ComparisonChart>(p => p
            .Add(c => c.Rows, [new() { Name = "Example", Values = [40] }])
            .Add(c => c.Metrics, [new() { Name = "Score", Ticks = ticks }]));
        var caches = (IList)typeof(ComparisonChart).GetField("_tickCaches", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(cut.Instance)!;
        object cache = caches[0]!;
        var tickProperty = cache.GetType().GetProperty("Ticks")!;
        var original = (Array)tickProperty.GetValue(cache)!;
        object middle = original.GetValue(1)!;
        var styleProperty = middle.GetType().GetProperty("Style")!;
        string originalStyle = (string)styleProperty.GetValue(middle)!;

        await cut.InvokeAsync(cut.Instance.Refresh);
        tickProperty.GetValue(cache).Should().BeSameAs(original);
        styleProperty.GetValue(original.GetValue(1)).Should().BeSameAs(originalStyle);

        ticks[1] = 25;
        await cut.InvokeAsync(cut.Instance.Refresh);
        cut.FindAll(".comparison-grid i")[1].GetAttribute("style").Should().Be("--tick:25%");
        cut.FindAll(".comparison-axis > span")[1].TextContent.Should().Be("25");
        cut.FindAll(".comparison-axis > span")[1].GetAttribute("style").Should().Be("--tick:25%");
    }

    [Test]
    public async Task Cached_sort_keeps_equal_rows_stable_and_observes_in_place_value_changes()
    {
        double?[] firstValues = [10];
        var rows = new List<ComparisonChartRow>
        {
            new() { Name = "First", Values = firstValues },
            new() { Name = "Second", Values = [10] },
            new() { Name = "Missing", Values = [null] }
        };
        var cut = Render<ComparisonChart>(p => p.Add(c => c.Rows, rows)
            .Add(c => c.Metrics, [new() { Name = "Score", LowerIsBetter = true }]));
        cut.Find(".comparison-sort button").Click();
        cut.FindAll("tbody th button").Select(e => e.TextContent).Should().Equal("First", "Second", "Missing");
        cut.Find(".comparison-sort button").Click();
        cut.FindAll("tbody th button").Select(e => e.TextContent).Should().Equal("First", "Second", "Missing");

        firstValues[0] = 5;
        await cut.InvokeAsync(cut.Instance.Refresh);
        cut.FindAll("tbody th button").Select(e => e.TextContent).Should().Equal("Second", "First", "Missing");
        rows.RemoveAt(1);
        await cut.InvokeAsync(cut.Instance.Refresh);
        cut.FindAll("tbody th button").Select(e => e.TextContent).Should().Equal("First", "Missing");
    }

    [Test]
    public void Metric_replacement_updates_layout_and_tick_geometry_without_changing_collection_identity()
    {
        ComparisonChartMetric[] metrics = [new() { Name = "Score" }];
        var cut = Render<ComparisonChart>(p => p.Add(c => c.Rows, [new() { Name = "Example", Values = [10] }])
            .Add(c => c.Metrics, metrics));
        cut.FindAll(".comparison-grid i").Should().HaveCount(5);
        metrics[0] = new() { Name = "Log score", ShowMarker = true, Minimum = 1, Maximum = 100, Logarithmic = true };
        cut.Render(p => p.Add(c => c.Metrics, metrics).Add(c => c.MirrorFirstMetric, false));
        cut.FindAll(".comparison-grid i").Should().HaveCount(3);
        cut.FindAll(".comparison-grid i")[1].GetAttribute("style").Should().Be("--tick:50%");
        cut.Find(".comparison").GetAttribute("style").Should().Be("--comparison-columns:minmax(105px, 220px) minmax(100px, .42fr)");
        cut.Find("thead th").TextContent.Should().Be("Entry");
    }

    [Test]
    public async Task Cached_geometry_observes_changed_values_and_keeps_custom_formatters_live()
    {
        double?[] values = [20], upperValues = [30];
        string suffix = " old";
        var cut = Render<ComparisonChart>(p => p
            .Add(c => c.Rows, [new() { Name = "Example", Values = values, UpperValues = upperValues }])
            .Add(c => c.Metrics, [new() { Name = "Score", ValueFormatter = value => value + suffix }]));
        cut.Find(".comparison-value").GetAttribute("style").Should().Be("--comparison-position:20%;--comparison-upper:30%;");
        suffix = " new";
        values[0] = 40;
        upperValues[0] = 60;
        await cut.InvokeAsync(cut.Instance.Refresh);
        cut.Find(".comparison-value").GetAttribute("style").Should().Be("--comparison-position:40%;--comparison-upper:60%;");
        cut.Find(".comparison-number").TextContent.Should().Contain("40 new").And.Contain("60 new");
        cut.FindAll(".comparison-axis > span")[0].TextContent.Should().Be("0 new");
    }
}
