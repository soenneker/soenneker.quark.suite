using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents animation types for Font Awesome icons.
/// </summary>
[Intellenum<string>]
public sealed partial class IconAnimation
{
    /// <summary>
    /// No animation applied.
    /// </summary>
    public static readonly IconAnimation None = new("");

    /// <summary>
    /// Continuous spinning animation.
    /// </summary>
    public static readonly IconAnimation Spin = new("fa-spin");

    /// <summary>
    /// Continuous spinning animation in reverse direction.
    /// </summary>
    public static readonly IconAnimation SpinRev = new("fa-spin-reverse");

    /// <summary>
    /// Pulsing animation that scales the icon.
    /// </summary>
    public static readonly IconAnimation Pulse = new("fa-pulse");

    /// <summary>
    /// Bouncing animation effect.
    /// </summary>
    public static readonly IconAnimation Bounce = new("fa-bounce");

    /// <summary>
    /// Shaking animation effect.
    /// </summary>
    public static readonly IconAnimation Shake = new("fa-shake");

    /// <summary>
    /// Beating animation effect.
    /// </summary>
    public static readonly IconAnimation Beat = new("fa-beat");

    /// <summary>
    /// Fading animation effect.
    /// </summary>
    public static readonly IconAnimation Fade = new("fa-fade");
}
