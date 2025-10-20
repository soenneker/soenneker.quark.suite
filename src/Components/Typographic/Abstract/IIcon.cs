using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Interface for the Icon component
/// </summary>
public interface IIcon : ITypographicElement
{
    /// <summary>
    /// Gets or sets the name of the icon.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets the icon style (solid, regular, light, etc.).
    /// </summary>
    IconStyle? IconStyle { get; set; }

    /// <summary>
    /// Gets or sets the icon family (Font Awesome, Bootstrap Icons, etc.).
    /// </summary>
    IconFamily? Family { get; set; }

    /// <summary>
    /// Gets or sets the predefined icon size.
    /// </summary>
    IconSize? IconSize { get; set; }

    /// <summary>
    /// Gets or sets the custom size of the icon.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether the icon should spin in reverse.
    /// </summary>
    bool SpinReverse { get; set; }

    /// <summary>
    /// Gets or sets the predefined rotation angle for the icon.
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
    /// Gets or sets the animation style for the icon.
    /// </summary>
    IconAnimation? IconAnimation { get; set; }
}

