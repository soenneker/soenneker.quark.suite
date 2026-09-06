using System;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class RealtimeChartTests : BunitContext
{
    public RealtimeChartTests() => Services.AddDefaultQuarkOptionsAsScoped();

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
