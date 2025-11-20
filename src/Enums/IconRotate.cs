using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents rotation transformations for Font Awesome icons.
/// </summary>
[Intellenum<string>]
public sealed partial class IconRotate
{
    /// <summary>
    /// No rotation applied.
    /// </summary>
    public static readonly IconRotate None = new("");

    /// <summary>
    /// Rotate the icon 90 degrees clockwise.
    /// </summary>
    public static readonly IconRotate R90 = new("fa-rotate-90");

    /// <summary>
    /// Rotate the icon 180 degrees.
    /// </summary>
    public static readonly IconRotate R180 = new("fa-rotate-180");

    /// <summary>
    /// Rotate the icon 270 degrees clockwise (or 90 degrees counter-clockwise).
    /// </summary>
    public static readonly IconRotate R270 = new("fa-rotate-270");
}