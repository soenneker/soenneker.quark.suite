using System;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class RealtimeChartTests : BunitContext
{
    public RealtimeChartTests() => Services.AddDefaultQuarkOptionsAsScoped();

    [Test]
    public void Scrolling_is_opt_in_and_owns_its_module_lifecycle()
    {
        var data = new RealtimeChartData(3, "API");
        var start = DateTimeOffset.UtcNow;
        data.Append(start, 10);
        data.Append(start.AddMilliseconds(500), 20);
        data.Append(start.AddMilliseconds(1000), 30);
        var cut = Render<Chart>(p => p.Add(c => c.Series, data.Series)
            .Add(c => c.XValues, data.XValues).Add(c => c.DataVersion, data.Version));
        JSInterop.Invocations.Should().BeEmpty();
        cut.Find("[data-slot=chart]").GetAttribute("data-scroll-enabled").Should().Be("false");

        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/chartscrollinterop.js");
        var initialize = module.SetupVoid("initialize", _ => true);
        initialize.SetVoidResult();
        var destroy = module.SetupVoid("destroy", _ => true);
        destroy.SetVoidResult();
        var options = new ChartOptions
        {
            EnableRealtimeScrolling = true,
            RealtimeScrollDuration = TimeSpan.FromMilliseconds(500),
            Minimum = 0,
            Maximum = 50
        };
        cut.Render(p => p.Add(c => c.Options, options));
        initialize.Invocations.Count.Should().Be(1);
        cut.Find("[data-slot=chart]").GetAttribute("data-scroll-duration").Should().Be("500");
        cut.Find("[data-slot=chart-plot]").ParentElement!.GetAttribute("clip-path").Should().StartWith("url(#");
        data.Append(start.AddMilliseconds(1500), 40);
        cut.Render(p => p.Add(c => c.DataVersion, data.Version).Add(c => c.ScrollPaused, true));
        initialize.Invocations.Count.Should().Be(1);
        cut.Find("[data-slot=chart]").GetAttribute("data-scroll-paused").Should().Be("true");
        cut.Render(p => p.Add(c => c.Options, new ChartOptions()));
        destroy.Invocations.Count.Should().Be(1);
        cut.Find("[data-slot=chart]").GetAttribute("data-scroll-enabled").Should().Be("false");
    }

    [Test]
    public void Scrolling_rejects_nonpositive_duration()
    {
        Action render = () => Render<Chart>(p => p.Add(c => c.Options,
            new ChartOptions { EnableRealtimeScrolling = true, RealtimeScrollDuration = TimeSpan.Zero }));
        render.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Scrolling_retains_outgoing_segments_without_changing_buffer_capacity()
    {
        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/chartscrollinterop.js");
        module.SetupVoid("initialize", _ => true).SetVoidResult();
        module.SetupVoid("destroy", _ => true).SetVoidResult();
        var data = new RealtimeChartData(5, "API");
        var start = DateTimeOffset.UtcNow;
        for (var i = 0; i < 5; i++)
            data.Append(start.AddSeconds(i), (i + 1) * 10);
        var cut = Render<Chart>(p => p.Add(c => c.Series, data.Series)
            .Add(c => c.XValues, data.XValues).Add(c => c.DataVersion, data.Version)
            .Add(c => c.Options, new ChartOptions
            {
                EnableRealtimeScrolling = true, Curve = ChartCurve.Linear, Minimum = 0, Maximum = 100
            }));
        data.Append(start.AddSeconds(5), 60);
        cut.Render(p => p.Add(c => c.DataVersion, data.Version));
        // The outgoing point is left of the final viewport but its segment remains visible during translation.
        cut.Find("[data-slot=chart-line]").GetAttribute("d").Should().StartWith("M-128,255.4L54,228.8");
        cut.Find("[data-slot=chart-scroll-point]").GetAttribute("cx").Should().Be("-128");
        cut.FindAll(".quark-chart-point").Count.Should().Be(5);
        data.Count.Should().Be(5);

        data.AppendBatch([start.AddSeconds(6), start.AddSeconds(7)], [70, 80]);
        cut.Render(p => p.Add(c => c.DataVersion, data.Version));
        cut.Find("[data-slot=chart-line]").GetAttribute("d").Should().StartWith("M-492,255.4L-310,228.8L-128,202.2L54,175.6");
        data.Count.Should().Be(5);

        data.Clear();
        data.Append(start, 90);
        data.Append(start.AddSeconds(1), 100);
        cut.Render(p => p.Add(c => c.DataVersion, data.Version));
        cut.FindAll("[data-slot=chart-scroll-point]").Should().BeEmpty();
        cut.Find("[data-slot=chart-line]").GetAttribute("d").Should().StartWith("M54,");
    }

    [Test]
    public void Outgoing_history_preserves_null_gaps()
    {
        var module = JSInterop.SetupModule("./_content/Soenneker.Quark.Suite/js/chartscrollinterop.js");
        module.SetupVoid("initialize", _ => true).SetVoidResult();
        module.SetupVoid("destroy", _ => true).SetVoidResult();
        var data = new RealtimeChartData(4, "API");
        var start = DateTimeOffset.UtcNow;
        data.AppendBatch([start, start.AddSeconds(1), start.AddSeconds(2), start.AddSeconds(3)], [10, null, 30, 40]);
        var cut = Render<Chart>(p => p.Add(c => c.Series, data.Series)
            .Add(c => c.XValues, data.XValues).Add(c => c.DataVersion, data.Version)
            .Add(c => c.Options, new ChartOptions { EnableRealtimeScrolling = true, Curve = ChartCurve.Linear, Minimum = 0, Maximum = 100 }));
        data.Append(start.AddSeconds(4), 50);
        cut.Render(p => p.Add(c => c.DataVersion, data.Version));
        var path = cut.Find("[data-slot=chart-line]").GetAttribute("d")!;
        path.Split('M').Length.Should().Be(3);
        cut.FindAll(".quark-chart-point").Count.Should().Be(3);
    }

    [Test]
    public void Radial_updates_validate_atomically_and_preserve_collection_identity()
    {
        string[] labels = ["Completed", "Failed"];
        var data = new RealtimeRadialChartData("Requests", labels);
        var series = data.Series;
        labels[0] = "Changed";
        data.Labels[0].Should().Be("Completed");
        data.Update(3, 1);
        Action invalid = () => data.Update(10, double.NaN);
        invalid.Should().Throw<ArgumentException>();
        Action negative = () => data.Update(10, -1);
        negative.Should().Throw<ArgumentException>();
        Action wrongCount = () => data.Update(10);
        wrongCount.Should().Throw<ArgumentException>();
        data.Version.Should().Be(1);
        data.Series[0].Values.Should().Equal(3d, 1d);
        data.Update((ReadOnlySpan<double>)[1, 9]);
        data.Version.Should().Be(2);
        data.Clear();
        data.Version.Should().Be(3);
        data.Series.Should().BeSameAs(series);
        data.Series[0].Values.Should().Equal(0d, 0d);
    }

    [Test]
    public void Radial_version_updates_pie_and_donut_geometry_and_recovers_from_empty()
    {
        foreach (var type in new[] { ChartType.Pie, ChartType.Donut })
        {
            var data = new RealtimeRadialChartData("Requests", "Completed", "Failed");
            data.Update(3, 1);
            var cut = Render<Chart>(p => p.Add(c => c.Type, type).Add(c => c.Series, data.Series)
                .Add(c => c.Labels, data.Labels).Add(c => c.DataVersion, data.Version));
            var before = cut.Find("[data-slot=chart-slice]").GetAttribute("d");
            data.Update(1, 9);
            cut.Render(p => p.Add(c => c.DataVersion, data.Version));
            cut.Find("[data-slot=chart-slice]").GetAttribute("d").Should().NotBe(before);
            cut.FindAll("[data-slot=chart-slice]")[1].GetAttribute("aria-label").Should().Contain("9");
            cut.Find("table").TextContent.Should().Contain("9");
            data.Update(0, 9);
            cut.Render(p => p.Add(c => c.DataVersion, data.Version));
            cut.FindAll("[data-slot=chart-slice]").Count.Should().Be(1);
            data.Clear();
            cut.Render(p => p.Add(c => c.DataVersion, data.Version));
            cut.Find("[role=status]").TextContent.Should().Be("No chart data");
            data.Update(2, 3);
            cut.Render(p => p.Add(c => c.DataVersion, data.Version));
            cut.FindAll("[data-slot=chart-slice]").Count.Should().Be(2);
        }
    }

    [Test]
    public void Batch_validates_atomically_and_formats_rolling_labels_lazily()
    {
        var data = new RealtimeChartData(2, "API", "Worker");
        var start = new DateTimeOffset(2026, 9, 4, 12, 30, 1, TimeSpan.FromHours(2));
        DateTimeOffset[] timestamps = [start, start.AddSeconds(1), start.AddSeconds(2)];
        double?[] values = [1, 2, 3, null, 5, 6];
        data.AppendBatch(timestamps, values);
        data.Version.Should().Be(1);
        data.Series[0].Values.Should().Equal(3d, 5d);
        data.Labels.Should().Equal("12:30:02.000", "12:30:03.000");
        string label = data.Labels[0];
        ReferenceEquals(label, data.Labels[0]).Should().BeTrue();
        Action invalid = () => data.AppendBatch([start.AddSeconds(3), start.AddSeconds(4)], [7, 8, double.NaN, 9]);
        invalid.Should().Throw<ArgumentException>();
        data.Version.Should().Be(1);
        data.Series[0].Values.Should().Equal(3d, 5d);
        Action invalidTimestamp = () => data.AppendBatch([start.AddSeconds(3), start.AddSeconds(2)], [7, 8, 9, 10]);
        invalidTimestamp.Should().Throw<ArgumentException>();
        data.AppendBatch([], []);
        data.Version.Should().Be(1);
        data.Append(start.AddSeconds(3), (ReadOnlySpan<double?>)[7, 8]);
        data.Labels.Should().Equal("12:30:03.000", "12:30:04.000");
        data.Clear();
        data.Append(start, (ReadOnlySpan<double?>)[9, 10]);
        data.Labels.Should().Equal("12:30:01.000");
    }

    [Test]
    public void Buffer_rolls_all_series_and_validates_before_mutation()
    {
        var data = new RealtimeChartData(2, "API", "Worker");
        var start = DateTimeOffset.UtcNow;
        data.Append(start, 1, 2);
        data.Append(start.AddSeconds(1), null, 3);
        data.Append(start.AddSeconds(3), 4, 5);
        data.Count.Should().Be(2);
        data.Series[0].Values.Should().Equal(null, 4d);
        data.Series[1].Values.Should().Equal(3d, 5d);
        data.XValues.Should().Equal((double)start.AddSeconds(1).ToUnixTimeMilliseconds(), (double)start.AddSeconds(3).ToUnixTimeMilliseconds());
        data.Labels.Count.Should().Be(2);
        Action invalidValue = () => data.Append(start.AddSeconds(4), 6, double.NaN);
        invalidValue.Should().Throw<ArgumentException>();
        Action duplicate = () => data.Append(start.AddSeconds(3), 6, 7);
        duplicate.Should().Throw<ArgumentException>();
        Action wrongCount = () => data.Append(start.AddSeconds(4), 6);
        wrongCount.Should().Throw<ArgumentException>();
        data.Version.Should().Be(3);
        data.Series[1].Values.Should().Equal(3d, 5d);
        data.Clear();
        data.Count.Should().Be(0);
        data.Series[0].Values.Should().BeEmpty();
        data.Append(start, 8, 9);
        data.Series[0].Values.Should().Equal(8d);
    }

    [Test]
    public void Version_updates_geometry_and_preserves_legend_selection()
    {
        var data = new RealtimeChartData(2, "API", "Worker");
        var start = DateTimeOffset.UtcNow;
        data.Append(start, 1, 2);
        data.Append(start.AddSeconds(1), 3, 4);
        var cut = Render<Chart>(p => p.Add(c => c.Series, data.Series)
            .Add(c => c.Labels, data.Labels).Add(c => c.XValues, data.XValues).Add(c => c.DataVersion, data.Version));
        cut.Find(".quark-chart-legend-item").Click();
        var before = cut.Find("[data-slot=chart-line]").GetAttribute("d");
        data.Append(start.AddSeconds(2), 5, 20);
        cut.Render(p => p.Add(c => c.DataVersion, data.Version));
        cut.Find(".quark-chart-legend-item").GetAttribute("data-hidden").Should().Be("true");
        cut.FindAll("[data-slot=chart-line]").Count.Should().Be(1);
        cut.Find("[data-slot=chart-line]").GetAttribute("d").Should().NotBe(before);
        cut.Find("table").TextContent.Should().Contain("20");
        data.Clear();
        cut.Render(p => p.Add(c => c.DataVersion, data.Version));
        cut.Find("[role=status]").TextContent.Should().Be("No chart data");
    }
}
