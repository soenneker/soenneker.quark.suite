using System;
using System.Reflection;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ComparisonChartAllocationTests
{
    private static Func<ComparisonChartRow, ComparisonChartMetric, double, double?, string> CreateStyleCache()
    {
        Type type = typeof(ComparisonChart).Assembly.GetType("Soenneker.Quark.ComparisonChartCellStyleCache")!;
        object cache = Activator.CreateInstance(type)!;
        return type.GetMethod("GetStyle", BindingFlags.Instance | BindingFlags.NonPublic)!
            .CreateDelegate<Func<ComparisonChartRow, ComparisonChartMetric, double, double?, string>>(cache);
    }

    [Test]
    public void Unchanged_cell_styles_allocate_nothing_after_warmup()
    {
        var getStyle = CreateStyleCache();
        var row = new ComparisonChartRow { Name = "Example", Values = [20], Color = "var(--chart-1)" };
        var metric = new ComparisonChartMetric { Name = "Score" };
        string original = getStyle(row, metric, 20, 30);
        for (int index = 0; index < 100; index++)
            getStyle(row, metric, 20, 30);

        long before = GC.GetAllocatedBytesForCurrentThread();
        for (int index = 0; index < 1000; index++)
            getStyle(row, metric, 20, 30);
        long allocated = GC.GetAllocatedBytesForCurrentThread() - before;

        allocated.Should().Be(0);
        getStyle(row, metric, 20, 30).Should().BeSameAs(original);
        original.Should().Be("--comparison-position:20%;--comparison-upper:30%;--comparison-color:var(--chart-1);");
    }

    [Test]
    public void Cell_style_cache_observes_bounds_logarithms_upper_values_colors_and_signed_zero()
    {
        var getStyle = CreateStyleCache();
        var row = new ComparisonChartRow { Name = "Example", Values = [10] };
        var metric = new ComparisonChartMetric { Name = "Score" };
        getStyle(row, metric, 10, null).Should().Be("--comparison-position:10%;--comparison-upper:10%;");
        getStyle(row, metric, 10, 20).Should().Be("--comparison-position:10%;--comparison-upper:20%;");
        metric = new() { Name = "Score", Maximum = 200 };
        getStyle(row, metric, 10, 20).Should().Be("--comparison-position:5%;--comparison-upper:10%;");
        metric = new() { Name = "Score", Minimum = 1, Maximum = 100, Logarithmic = true };
        getStyle(row, metric, 10, 100).Should().Be("--comparison-position:50%;--comparison-upper:100%;");
        row = new() { Name = "Example", Values = [10], Color = "red" };
        getStyle(row, metric, 10, 100).Should().EndWith("--comparison-color:red;");
        metric = new() { Name = "Score" };
        string positiveZero = getStyle(row, metric, 0d, null);
        string negativeZero = getStyle(row, metric, -0d, null);
        negativeZero.Should().NotBeSameAs(positiveZero);
        negativeZero.Should().Be("--comparison-position:-0%;--comparison-upper:-0%;--comparison-color:red;");
    }
}
