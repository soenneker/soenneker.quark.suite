using System.Collections.Generic;
using System;

namespace Soenneker.Quark;

/// <summary>A named set of values rendered by a <see cref="Chart"/>.</summary>
public sealed class ChartSeries
{
    public ChartSeries(string name, IReadOnlyList<double?> values)
    {
        Name = name;
        Values = values;
    }

    public ChartSeries(string name, IReadOnlyList<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        Name = name;
        var snapshot = values.Count == 0 ? Array.Empty<double?>() : new double?[values.Count];
        for (var index = 0; index < snapshot.Length; index++)
            snapshot[index] = values[index];
        Values = snapshot;
    }

    /// <summary>Gets the series name used in legends and tooltips.</summary>
    public string Name { get; }

    /// <summary>Gets the values. Null values create gaps in line and area series.</summary>
    public IReadOnlyList<double?> Values { get; }

    /// <summary>Gets or sets a per-series rendering type. Null uses <see cref="Chart.Type"/>.</summary>
    public ChartType? Type { get; init; }

    /// <summary>Gets or sets the series color. CSS variables and any browser color syntax are supported.</summary>
    public string? Color { get; init; }

    /// <summary>Gets or sets the optional fill gradient.</summary>
    public ChartGradient? Gradient { get; init; }

    /// <summary>
    /// Gets or sets optional per-slice gradients for pie and donut charts.
    /// </summary>
    /// <remarks>
    /// Entries correspond to value indexes. A null or missing entry uses the matching color from <see cref="ChartOptions.Palette"/>.
    /// This property is ignored by Cartesian chart types.
    /// </remarks>
    public IReadOnlyList<ChartGradient?>? SliceGradients { get; init; }

    /// <summary>Gets or sets the line width in view-box units.</summary>
    public double StrokeWidth { get; init; } = 2.5;

    /// <summary>Gets or sets the fill opacity for bars, areas, and radial slices.</summary>
    public double FillOpacity { get; init; } = 1;

    /// <summary>Gets or sets whether markers are rendered for this series.</summary>
    public bool? ShowPoints { get; init; }

    /// <summary>Gets or sets whether this series starts hidden.</summary>
    public bool Hidden { get; init; }
}
