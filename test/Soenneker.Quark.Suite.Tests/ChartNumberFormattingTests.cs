using System;
using System.Globalization;
using System.Text;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class ChartNumberFormattingTests
{
    [Test]
    [Arguments(0d, "0")]
    [Arguments(-0.0001, "-0")]
    [Arguments(1.2345, "1.235")]
    [Arguments(-1.2345, "-1.235")]
    [Arguments(999999999.999, "999999999.999")]
    [Arguments(double.NaN, "NaN")]
    [Arguments(double.PositiveInfinity, "Infinity")]
    public void Coordinates_keep_the_existing_invariant_format(double value, string expected)
    {
        ChartNumberFormatting.Format(value).Should().Be(expected);
        new StringBuilder().AppendChartNumber(value).ToString().Should().Be(expected);
    }

    [Test]
    public void Extreme_coordinates_can_exceed_the_stack_buffer()
    {
        double value = double.MaxValue;
        string expected = value.ToString("0.###", CultureInfo.InvariantCulture);
        ChartNumberFormatting.Format(value).Should().Be(expected);
        new StringBuilder().AppendChartNumber(value).ToString().Should().Be(expected);
    }

    [Test]
    public void Appending_coordinates_has_no_temporary_string_allocations()
    {
        var builder = new StringBuilder(128);
        for (int i = 0; i < 100; i++)
            builder.Clear().AppendChartNumber(123.456);

        long before = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < 100; i++)
            builder.Clear().AppendChartNumber(123.456);

        (GC.GetAllocatedBytesForCurrentThread() - before).Should().Be(0);
        builder.ToString().Should().Be("123.456");
    }
}
