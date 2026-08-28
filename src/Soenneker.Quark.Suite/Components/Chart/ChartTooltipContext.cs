using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Provides the active category and visible series values to a custom chart tooltip.
/// </summary>
/// <param name="Label">The active category or radial-slice label.</param>
/// <param name="Series">The first visible row's series name, retained as a convenience for single-series templates.</param>
/// <param name="Value">The first visible row's unformatted value.</param>
/// <param name="Index">The zero-based active category or slice index.</param>
/// <param name="Color">The first visible row's resolved CSS color.</param>
/// <remarks>
/// Use <see cref="Rows"/> for shared tooltips. The singular <see cref="Series"/>, <see cref="Value"/>, and <see cref="Color"/>
/// properties preserve the concise API used by single-series charts.
/// </remarks>
public sealed record ChartTooltipContext(string Label, string Series, double Value, int Index, string Color)
{
    /// <summary>
    /// Gets every visible, non-null series value at the active category. Radial charts expose one row for the active slice.
    /// </summary>
    public IReadOnlyList<ChartTooltipRow> Rows { get; init; } = [];
}
