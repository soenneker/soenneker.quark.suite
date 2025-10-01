using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents an icon component for displaying Font Awesome icons.
/// </summary>
public interface IIcon : IElement
{
    /// <summary>
    /// Gets or sets the name of the icon (e.g., "camera" or "fa-camera").
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the Font Awesome icon style (e.g., regular, solid, light, brands).
    /// </summary>
    IconStyle? IconStyle { get; set; }

    /// <summary>
    /// Gets or sets the Font Awesome icon family (e.g., Classic, Sharp, Jelly).
    /// </summary>
    IconFamily? Family { get; set; }

    /// <summary>
    /// Gets or sets the Font Awesome size helper (e.g., xs, sm, lg, 2x).
    /// </summary>
    IconSize? IconSize { get; set; }

    /// <summary>
    /// Gets or sets the size of the icon using CSS builder.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether the icon should spin in reverse.
    /// </summary>
    bool SpinReverse { get; set; }

    /// <summary>
    /// Gets or sets the rotation angle for the icon.
    /// </summary>
    IconRotate? Rotate { get; set; }

    /// <summary>
    /// Gets or sets the custom rotation angle in degrees.
    /// </summary>
    int? RotateByDegrees { get; set; }

    /// <summary>
    /// Gets or sets the flip direction for the icon.
    /// </summary>
    IconFlip? Flip { get; set; }

    /// <summary>
    /// Gets or sets the animation for the icon.
    /// </summary>
    IconAnimation? IconAnimation { get; set; }
}

