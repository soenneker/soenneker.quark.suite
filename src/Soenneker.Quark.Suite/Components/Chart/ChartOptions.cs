using System;
using System.Collections.Generic;
using System.Globalization;

namespace Soenneker.Quark;

/// <summary>
/// Configures the layout, appearance, formatting, and interactive behavior of a <see cref="Chart"/>.
/// </summary>
/// <remarks>
/// <para>
/// Instances are intended to be created once and reused. Dimensions, padding, radii, gaps, and stroke-related values are expressed in SVG view-box units;
/// the chart scales those units responsively to the width of its container.
/// </para>
/// <para>
/// Options that only apply to a particular chart family, such as <see cref="Stacked"/> for bars or <see cref="DonutThickness"/> for donuts,
/// are safely ignored by other chart types.
/// </para>
/// </remarks>
public sealed class ChartOptions
{
    /// <summary>
    /// Gets the intrinsic SVG width used for geometry and aspect-ratio calculations. The default is <c>800</c>.
    /// </summary>
    /// <remarks>The rendered chart remains responsive; this value does not impose a fixed CSS width.</remarks>
    public double Width { get; init; } = 800;

    /// <summary>
    /// Gets the intrinsic SVG height used for geometry and aspect-ratio calculations. The default is <c>320</c>.
    /// </summary>
    /// <remarks>Use a smaller value for compact sparklines and metric cards.</remarks>
    public double Height { get; init; } = 320;

    /// <summary>Gets the space reserved between the top of the SVG and the plot area. The default is <c>16</c>.</summary>
    public double PaddingTop { get; init; } = 16;

    /// <summary>Gets the space reserved between the right edge of the plot area and the SVG. The default is <c>18</c>.</summary>
    public double PaddingRight { get; init; } = 18;

    /// <summary>
    /// Gets the space reserved below the plot area. The default is <c>38</c>, which leaves room for x-axis labels.
    /// </summary>
    /// <remarks>This space is not removed automatically when <see cref="ShowXAxis"/> is <see langword="false"/>.</remarks>
    public double PaddingBottom { get; init; } = 38;

    /// <summary>
    /// Gets the space reserved to the left of the plot area. The default is <c>54</c>, which leaves room for y-axis labels.
    /// </summary>
    /// <remarks>This space is not removed automatically when <see cref="ShowYAxis"/> is <see langword="false"/>.</remarks>
    public double PaddingLeft { get; init; } = 54;

    /// <summary>Gets whether horizontal y-value grid lines are rendered for Cartesian charts. The default is <see langword="true"/>.</summary>
    public bool ShowGrid { get; init; } = true;

    /// <summary>Gets whether category labels are rendered along the x-axis of Cartesian charts. The default is <see langword="true"/>.</summary>
    public bool ShowXAxis { get; init; } = true;

    /// <summary>Gets whether formatted numeric labels are rendered along the y-axis of Cartesian charts. The default is <see langword="true"/>.</summary>
    public bool ShowYAxis { get; init; } = true;

    /// <summary>Gets whether pointing at, touching, or focusing chart data displays a tooltip. The default is <see langword="true"/>.</summary>
    public bool ShowTooltip { get; init; } = true;

    /// <summary>
    /// Gets whether Cartesian tooltips include every visible series at the active category. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>When disabled, directly hovering or focusing a mark exposes only that mark's series; category hit areas remain shared.</remarks>
    public bool SharedTooltip { get; init; } = true;

    /// <summary>
    /// Gets whether every non-null value in line and area series receives an interactive point marker. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>A series-level <see cref="ChartSeries.ShowPoints"/> value overrides this setting for that series.</remarks>
    public bool ShowPoints { get; init; } = true;

    /// <summary>
    /// Gets whether the last non-null value in each line or area series receives a point marker. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>
    /// This is useful for sparklines. It remains effective when <see cref="ShowPoints"/> or a series-level point setting is disabled.
    /// When all points are visible, enabling this option does not create a duplicate marker.
    /// </remarks>
    public bool ShowEndPoints { get; init; } = true;

    /// <summary>
    /// Gets whether a vertical guide follows the hovered category in Cartesian charts. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>This does not disable hover detection or tooltips; use <see cref="ShowTooltip"/> to control tooltip visibility.</remarks>
    public bool ShowCursor { get; init; } = true;

    /// <summary>
    /// Gets whether the automatically calculated y-axis domain must include zero. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>
    /// Disable this to emphasize variation in line or area data. When enabled, zero may expand a bound supplied through
    /// <see cref="Minimum"/> or <see cref="Maximum"/> so the resulting domain still contains zero.
    /// </remarks>
    public bool IncludeZero { get; init; } = true;

    /// <summary>Gets whether bar series are stacked by category instead of grouped side by side. The default is <see langword="false"/>.</summary>
    public bool Stacked { get; init; }

    /// <summary>
    /// Gets whether chart marks use the built-in entrance transition. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>User preferences for reduced motion are respected regardless of this value.</remarks>
    public bool Animate { get; init; } = true;

    /// <summary>
    /// Gets whether selecting a Cartesian legend item toggles that series. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>Radial legend items identify slices and do not toggle individual values.</remarks>
    public bool ToggleSeriesFromLegend { get; init; } = true;

    /// <summary>
    /// Gets whether automatically derived y-axis bounds and intervals are expanded to human-friendly round values. The default is <see langword="true"/>.
    /// </summary>
    /// <remarks>Explicit <see cref="Minimum"/> or <see cref="Maximum"/> bounds are never rounded.</remarks>
    public bool NiceYAxis { get; init; } = true;

    /// <summary>
    /// Gets the requested number of y-axis tick positions, including the domain endpoints. The default is <c>5</c> and the effective minimum is <c>2</c>.
    /// </summary>
    public int YAxisTicks { get; init; } = 5;

    /// <summary>
    /// Gets the maximum number of x-axis labels rendered before labels are automatically thinned. The default is <c>12</c>.
    /// </summary>
    /// <remarks>The first and last labels remain visible. Values less than <c>2</c> are treated as <c>2</c>.</remarks>
    public int MaximumXAxisLabels { get; init; } = 12;

    /// <summary>
    /// Gets the clockwise rotation of x-axis labels in degrees. The default is <c>0</c>.
    /// </summary>
    /// <remarks>Negative values rotate labels counterclockwise. Rotation is applied around each label's axis position.</remarks>
    public double XAxisLabelRotation { get; init; }

    /// <summary>Gets the gap between adjacent grouped bars, in view-box units. The default is <c>4</c>.</summary>
    public double BarGap { get; init; } = 4;

    /// <summary>
    /// Gets the corner radius applied to bars, in view-box units. The default is <c>4</c>.
    /// </summary>
    /// <remarks>The rendered radius is limited to half the computed bar width.</remarks>
    public double BarRadius { get; init; } = 4;

    /// <summary>
    /// Gets the fraction of the radial radius occupied by a donut ring. The default is <c>0.38</c>.
    /// </summary>
    /// <remarks>Values are constrained to the inclusive range <c>0.08</c> through <c>0.92</c> when rendered.</remarks>
    public double DonutThickness { get; init; } = 0.38;

    /// <summary>Gets the radius of an inactive line or area point marker, in view-box units. The default is <c>3</c>.</summary>
    public double PointRadius { get; init; } = 3;

    /// <summary>Gets the radius of a hovered or keyboard-focused point marker, in view-box units. The default is <c>5</c>.</summary>
    public double ActivePointRadius { get; init; } = 5;

    /// <summary>
    /// Gets an optional explicit lower bound for the Cartesian y-axis domain. A <see langword="null"/> value derives the bound from visible data.
    /// </summary>
    public double? Minimum { get; init; }

    /// <summary>
    /// Gets an optional explicit upper bound for the Cartesian y-axis domain. A <see langword="null"/> value derives the bound from visible data.
    /// </summary>
    public double? Maximum { get; init; }

    /// <summary>Gets the interpolation used between points in line and area series. The default is <see cref="ChartCurve.Smooth"/>.</summary>
    /// <remarks>Use <see cref="ChartCurve.Monotone"/> when a curve must not visually introduce extrema between adjacent values.</remarks>
    public ChartCurve Curve { get; init; } = ChartCurve.Smooth;

    /// <summary>Gets where the legend is rendered, or whether it is omitted. The default is <see cref="ChartLegendPosition.Bottom"/>.</summary>
    public ChartLegendPosition Legend { get; init; } = ChartLegendPosition.Bottom;

    /// <summary>Gets the status message rendered when no series contains a non-null value. The default is <c>"No chart data"</c>.</summary>
    public string EmptyText { get; init; } = "No chart data";

    /// <summary>
    /// Gets an optional suffix appended by the default numeric formatter, such as <c>"%"</c>, <c>"ms"</c>, or <c>"k"</c>.
    /// </summary>
    /// <remarks>This value is ignored when <see cref="ValueFormatter"/> is supplied.</remarks>
    public string? Unit { get; init; }

    /// <summary>
    /// Gets the CSS color used inside line and area point markers. The default is <c>var(--background)</c>.
    /// </summary>
    /// <remarks>Use the surrounding card color to create outlined endpoint markers on custom surfaces.</remarks>
    public string PointFill { get; init; } = "var(--background)";

    /// <summary>
    /// Gets an optional function that formats numeric values in axes, tooltips, and radial accessibility labels.
    /// </summary>
    /// <remarks>The function should be fast and deterministic because it may run multiple times during rendering.</remarks>
    public Func<double, string>? ValueFormatter { get; init; }

    /// <summary>Gets an optional function that formats category labels displayed on axes and in tooltips.</summary>
    public Func<string, string>? LabelFormatter { get; init; }

    /// <summary>
    /// Gets the ordered CSS color palette used when a series does not define <see cref="ChartSeries.Color"/> and for radial slices.
    /// </summary>
    /// <remarks>
    /// Any browser-supported CSS color, including custom properties, hexadecimal, RGB, HSL, and OKLCH values, is accepted.
    /// Colors repeat when the number of series or slices exceeds the palette count. An empty palette falls back to <c>var(--series-1)</c>.
    /// </remarks>
    public IReadOnlyList<string> Palette { get; init; } =
    [
        "var(--series-1)",
        "var(--series-2)",
        "var(--series-3)",
        "var(--series-4)",
        "var(--series-5)"
    ];

    internal string FormatValue(double value) => ValueFormatter?.Invoke(value) ?? string.Create(CultureInfo.CurrentCulture, $"{value:0.##}{Unit}");
    internal string FormatLabel(string value) => LabelFormatter?.Invoke(value) ?? value;
}
