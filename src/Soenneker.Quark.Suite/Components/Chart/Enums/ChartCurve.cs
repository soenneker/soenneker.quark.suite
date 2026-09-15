using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Specifies the interpolation used between cartesian data points.</summary>
[EnumValue]
public sealed partial class ChartCurve
{
    /// <summary>Connects adjacent values with straight segments.</summary>
    public static readonly ChartCurve Linear = new(0);

    /// <summary>Connects values with a general-purpose smooth cubic curve.</summary>
    public static readonly ChartCurve Smooth = new(1);

    /// <summary>Connects values with horizontal and vertical step segments.</summary>
    public static readonly ChartCurve Step = new(2);

    /// <summary>
    /// Connects values with a monotone cubic curve that avoids introducing extrema between adjacent data points.
    /// </summary>
    public static readonly ChartCurve Monotone = new(3);
}
