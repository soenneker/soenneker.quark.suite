using System;
using System.Globalization;

namespace Soenneker.Quark;

/// <summary>
/// Converts chart data coordinates into positions for <see cref="Chart.OverlayContent"/>.
/// </summary>
public sealed class ChartOverlayContext
{
    private readonly Func<int, double> _categoryX;
    private readonly Func<double, double> _valueY;

    internal ChartOverlayContext(double width, double height, double plotLeft, double plotTop, double plotRight, double plotBottom,
        Func<int, double> categoryX, Func<double, double> valueY)
    {
        Width = width;
        Height = height;
        PlotLeft = plotLeft;
        PlotTop = plotTop;
        PlotRight = plotRight;
        PlotBottom = plotBottom;
        _categoryX = categoryX;
        _valueY = valueY;
    }

    /// <summary>Gets the intrinsic SVG width used by the chart.</summary>
    public double Width { get; }

    /// <summary>Gets the intrinsic SVG height used by the chart.</summary>
    public double Height { get; }

    /// <summary>Gets the left edge of the Cartesian plot area in SVG view-box units.</summary>
    public double PlotLeft { get; }

    /// <summary>Gets the top edge of the Cartesian plot area in SVG view-box units.</summary>
    public double PlotTop { get; }

    /// <summary>Gets the right edge of the Cartesian plot area in SVG view-box units.</summary>
    public double PlotRight { get; }

    /// <summary>Gets the bottom edge of the Cartesian plot area in SVG view-box units.</summary>
    public double PlotBottom { get; }

    /// <summary>
    /// Converts a category index and y value into SVG and CSS percentage coordinates.
    /// </summary>
    public ChartOverlayPoint Point(int index, double value)
    {
        var x = _categoryX(index);
        var y = _valueY(value);
        return new ChartOverlayPoint(x, y, x / Width * 100, y / Height * 100);
    }

    /// <summary>
    /// Builds an inline CSS position for an absolutely positioned overlay element anchored to a datum.
    /// </summary>
    /// <param name="index">The zero-based category index.</param>
    /// <param name="value">The y value to anchor.</param>
    /// <param name="transform">An optional CSS transform. The default centers the element on the datum.</param>
    public string Style(int index, double value, string transform = "translate(-50%,-50%)")
    {
        var point = Point(index, value);
        return string.Create(CultureInfo.InvariantCulture,
            $"position:absolute;left:{point.LeftPercent:0.###}%;top:{point.TopPercent:0.###}%;transform:{transform}");
    }
}

/// <summary>
/// Represents a data position in SVG units and in percentages relative to the full chart stage.
/// </summary>
public readonly record struct ChartOverlayPoint(double X, double Y, double LeftPercent, double TopPercent);
