using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Specifies where a chart legend is rendered.</summary>
[EnumValue]
public sealed partial class ChartLegendPosition
{
    public static readonly ChartLegendPosition Top = new(0);
    public static readonly ChartLegendPosition Bottom = new(1);
    public static readonly ChartLegendPosition None = new(2);
}
