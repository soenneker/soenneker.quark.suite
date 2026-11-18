using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Specifies how a chart series is rendered.</summary>
[EnumValue]
public sealed partial class ChartType
{
    public static readonly ChartType Line = new(0);
    public static readonly ChartType Area = new(1);
    public static readonly ChartType Bar = new(2);
    public static readonly ChartType Pie = new(3);
    public static readonly ChartType Donut = new(4);
    public static readonly ChartType Scatter = new(5);
    public static readonly ChartType Bubble = new(6);
    public static readonly ChartType Radar = new(7);
    public static readonly ChartType Heatmap = new(8);
    public static readonly ChartType Treemap = new(9);
    public static readonly ChartType Sankey = new(10);
    public static readonly ChartType Funnel = new(11);
    public static readonly ChartType Candlestick = new(12);
    public static readonly ChartType BoxPlot = new(13);
    public static readonly ChartType Waterfall = new(14);
}
