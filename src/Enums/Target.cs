using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Target attribute enumeration for anchor elements.
/// </summary>
[EnumValue<string>]
public sealed partial class Target
{
    /// <summary>
    /// Opens the link in the current window/tab (default behavior).
    /// </summary>
    public static readonly Target Self = new("_self");

    /// <summary>
    /// Opens the link in a new window/tab.
    /// </summary>
    public static readonly Target Blank = new("_blank");

    /// <summary>
    /// Opens the link in the parent frame.
    /// </summary>
    public static readonly Target Parent = new("_parent");

    /// <summary>
    /// Opens the link in the full body of the window.
    /// </summary>
    public static readonly Target Top = new("_top");
}
