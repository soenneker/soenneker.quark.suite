namespace Soenneker.Quark;

/// <summary>
/// Describes one visible series value in a chart tooltip.
/// </summary>
/// <param name="Series">The display name of the series.</param>
/// <param name="Value">The unformatted numeric value.</param>
/// <param name="Color">The resolved CSS color used by the series or radial slice.</param>
/// <param name="SeriesIndex">The zero-based index of the series in <see cref="Chart.Series"/>.</param>
public sealed record ChartTooltipRow(string Series, double Value, string Color, int SeriesIndex);
