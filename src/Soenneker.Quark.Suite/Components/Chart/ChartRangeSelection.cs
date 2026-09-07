namespace Soenneker.Quark;

/// <summary>Identifies a contiguous range of chart categories selected by the user.</summary>
/// <param name="StartLabel">The label at the beginning of the normalized range.</param>
/// <param name="EndLabel">The label at the end of the normalized range.</param>
/// <param name="StartIndex">The zero-based index at the beginning of the normalized range.</param>
/// <param name="EndIndex">The zero-based index at the end of the normalized range.</param>
/// <param name="StartXValue">The numeric x coordinate at the beginning of the range, when the chart has numeric x coordinates.</param>
/// <param name="EndXValue">The numeric x coordinate at the end of the range, when the chart has numeric x coordinates.</param>
public sealed record ChartRangeSelection(string StartLabel, string EndLabel, int StartIndex, int EndIndex, double? StartXValue = null, double? EndXValue = null);
