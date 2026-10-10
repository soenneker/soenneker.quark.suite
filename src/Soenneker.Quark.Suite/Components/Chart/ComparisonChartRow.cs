using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>A named entry with values in the same order as the comparison chart's metrics.</summary>
public sealed class ComparisonChartRow
{
    /// <summary>Gets the entry's display name.</summary>
    public required string Name { get; init; }

    /// <summary>Gets optional secondary text displayed below the name.</summary>
    public string? Description { get; init; }

    /// <summary>Gets the metric values. Missing entries, null, and nonfinite values are displayed as unavailable.</summary>
    public required IReadOnlyList<double?> Values { get; init; }

    /// <summary>Gets whether this entry uses the chart's accent color and stronger label.</summary>
    public bool Highlighted { get; init; }

    /// <summary>Gets whether this entry is the reference, rendered in the foreground color.</summary>
    public bool Reference { get; init; }

    /// <summary>Gets optional upper values, such as p95 latency, rendered as a striped extension of each bar.</summary>
    public IReadOnlyList<double?>? UpperValues { get; init; }

    /// <summary>Gets an optional CSS color overriding the default mark color.</summary>
    public string? Color { get; init; }
}
