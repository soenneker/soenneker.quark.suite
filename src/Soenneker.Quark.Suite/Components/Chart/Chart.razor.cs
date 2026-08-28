namespace Soenneker.Quark;

/// <summary>
/// Renders responsive, accessible SVG charts from strongly typed series and options.
/// </summary>
/// <remarks>
/// <para>
/// The component supports line, area, grouped or stacked bar, pie, donut, and mixed Cartesian charts without a JavaScript charting dependency.
/// It includes keyboard-focusable data marks, an assistive-technology data table, shared hover tooltips, legend interaction, null-value gaps,
/// negative Cartesian values, CSS colors, and SVG gradients.
/// </para>
/// <para>
/// Supply values through <see cref="Series"/>, category names through <see cref="Labels"/>, and visual or interaction behavior through
/// <see cref="Options"/>. Use <see cref="HeaderContent"/>, <see cref="OverlayContent"/>, and <see cref="FooterContent"/> to compose arbitrary
/// application UI around or over the plot without requiring chart-specific title, link, annotation, or action components. The SVG preserves
/// the configured view-box aspect ratio and scales to the available container width. Supply <see cref="XValues"/> when numeric or time-based
/// data requires proportional horizontal spacing instead of categorical spacing.
/// </para>
/// </remarks>
public partial class Chart
{
}
