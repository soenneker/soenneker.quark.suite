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
}
