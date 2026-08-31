using System.Collections.Generic;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Represents an indeterminate circular loading indicator whose arc grows, contracts, rotates, and can cycle through colors.
/// </summary>
public interface ISpinner : IElement
{
    /// <summary>
    /// Gets or sets a single indicator color. When set, this takes precedence over <see cref="ForegroundColors"/> and disables color cycling.
    /// </summary>
    string? ForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets colors cycled by the indicator. Up to four colors are used. When omitted, the spinner remains monochrome; use <see cref="SpinnerColorPalettes.Google"/> for the Google sequence.
    /// </summary>
    IReadOnlyList<string>? ForegroundColors { get; set; }

    /// <summary>
    /// Gets or sets the color of the stationary background track.
    /// </summary>
    string TrackColor { get; set; }

    /// <summary>
    /// Gets or sets whether the stationary background track is rendered.
    /// </summary>
    bool ShowTrack { get; set; }

    /// <summary>
    /// Gets or sets the background track opacity, clamped from <c>0</c> through <c>1</c>.
    /// </summary>
    double TrackOpacity { get; set; }

    /// <summary>
    /// Gets or sets the indicator thickness on the component's legacy 50-unit sizing scale. A value of <c>4</c> occupies eight percent of the rendered diameter.
    /// </summary>
    double StrokeWidth { get; set; }

    /// <summary>
    /// Gets or sets the background track stroke width. When omitted, <see cref="StrokeWidth"/> is used.
    /// </summary>
    double? TrackStrokeWidth { get; set; }

    /// <summary>
    /// Gets or sets the shortest visible arc as a percentage of the circumference.
    /// </summary>
    double MinimumArcLength { get; set; }

    /// <summary>
    /// Gets or sets the longest visible arc as a percentage of the circumference.
    /// </summary>
    double MaximumArcLength { get; set; }

    /// <summary>
    /// Gets or sets how far the arc offset travels during one growth cycle, as a percentage of the circumference.
    /// </summary>
    double ArcTravel { get; set; }

    /// <summary>
    /// Gets or sets a multiplier applied to all animation speeds. Values greater than <c>1</c> run faster.
    /// </summary>
    double Speed { get; set; }

    /// <summary>
    /// Gets or sets the duration, in seconds, of one full rotation before <see cref="Speed"/> is applied.
    /// Leave unset to derive Google's synchronized rotation timing from <see cref="ArcDuration"/>.
    /// </summary>
    double? RotationDuration { get; set; }

    /// <summary>
    /// Gets or sets the duration, in seconds, of one arc growth cycle before <see cref="Speed"/> is applied.
    /// </summary>
    double ArcDuration { get; set; }

    /// <summary>
    /// Gets or sets the duration, in seconds, of one complete foreground color cycle before <see cref="Speed"/> is applied.
    /// Leave unset to synchronize the color cycle to four arc cycles.
    /// </summary>
    double? ColorDuration { get; set; }

    /// <summary>
    /// Gets or sets the CSS timing function used by the grow and contract phases.
    /// </summary>
    string ArcEasing { get; set; }

    /// <summary>
    /// Gets or sets whether rotation runs counterclockwise.
    /// </summary>
    bool Reverse { get; set; }

    /// <summary>
    /// Gets or sets whether the spinner is hidden from assistive technology. Non-decorative spinners default to a loading status.
    /// </summary>
    bool Decorative { get; set; }

    /// <summary>
    /// Gets or sets an optional legacy Lucide loader icon. Leave unset to render the configurable circular spinner.
    /// </summary>
    LucideIcon? Name { get; set; }
}
