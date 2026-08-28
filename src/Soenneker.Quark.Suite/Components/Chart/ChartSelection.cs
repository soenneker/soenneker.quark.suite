namespace Soenneker.Quark;

/// <summary>Identifies a chart value selected by the user.</summary>
/// <param name="Label">The category or radial-slice label.</param>
/// <param name="Series">The selected series name.</param>
/// <param name="Value">The selected unformatted numeric value.</param>
/// <param name="Index">The zero-based category or slice index.</param>
/// <param name="SeriesIndex">The zero-based series index. Radial selections use <c>0</c>.</param>
public sealed record ChartSelection(string Label, string Series, double Value, int Index, int SeriesIndex = 0);
