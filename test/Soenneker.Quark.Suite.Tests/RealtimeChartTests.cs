using System;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class RealtimeChartTests : BunitContext
{
    public RealtimeChartTests() => Services.AddDefaultQuarkOptionsAsScoped();

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
