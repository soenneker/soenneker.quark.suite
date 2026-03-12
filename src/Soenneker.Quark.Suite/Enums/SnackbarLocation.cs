using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the snackbar location.
/// </summary>
[EnumValue<string>]
public sealed partial class SnackbarLocation
{
    /// <summary>
    /// Show the snackbar on the bottom side of the screen.
    /// </summary>
    public static readonly SnackbarLocation Bottom = new("bottom");

    /// <summary>
    /// Show the snackbar on the bottom-start side of the screen.
    /// </summary>
    public static readonly SnackbarLocation BottomStart = new("bottom-start");

    /// <summary>
    /// Show the snackbar on the bottom-end side of the screen.
    /// </summary>
    public static readonly SnackbarLocation BottomEnd = new("bottom-end");

    /// <summary>
    /// Show the snackbar on the top side of the screen.
    /// </summary>
    public static readonly SnackbarLocation Top = new("top");

    /// <summary>
    /// Show the snackbar on the top-start side of the screen.
    /// </summary>
    public static readonly SnackbarLocation TopStart = new("top-start");

    /// <summary>
    /// Show the snackbar on the top-end side of the screen.
    /// </summary>
    public static readonly SnackbarLocation TopEnd = new("top-end");
}
