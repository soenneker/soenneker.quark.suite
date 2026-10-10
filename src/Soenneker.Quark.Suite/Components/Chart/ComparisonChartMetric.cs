using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>A numeric column and its independent scale in a comparison chart.</summary>
public sealed class ComparisonChartMetric
{
    /// <summary>Gets the column heading.</summary>
    public required string Name { get; init; }

    /// <summary>Gets optional explanatory text displayed below the heading.</summary>
    public string? Description { get; init; }

    /// <summary>Gets optional axis tick values. Null uses five evenly spaced ticks (three for markers).</summary>
    public IReadOnlyList<double>? Ticks { get; init; }

    /// <summary>Gets the inclusive scale minimum. Defaults to zero.</summary>
    public double Minimum { get; init; }

    /// <summary>Gets the inclusive scale maximum. Defaults to 100.</summary>
    public double Maximum { get; init; } = 100;

    /// <summary>Gets whether smaller values rank first when sorting this metric.</summary>
    public bool LowerIsBetter { get; init; }

    /// <summary>Gets whether to use a logarithmic scale. Both bounds must be positive.</summary>
    public bool Logarithmic { get; init; }

    /// <summary>Gets whether to render a point on a track instead of a filled bar.</summary>
    public bool ShowMarker { get; init; }

    /// <summary>Gets the optional value and axis-label formatter.</summary>
    public Func<double, string>? ValueFormatter { get; init; }

    /// <summary>Gets the label used for missing or nonfinite values.</summary>
    public string MissingText { get; init; } = "Not measured";
}
