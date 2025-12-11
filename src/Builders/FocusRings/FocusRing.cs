namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating focus ring builders with predefined theme colors.
/// </summary>
public static class FocusRing
{
    /// <summary>
    /// Gets a focus ring builder with primary theme color.
    /// </summary>
    public static FocusRingBuilder Primary => new("primary");
    /// <summary>
    /// Gets a focus ring builder with secondary theme color.
    /// </summary>
    public static FocusRingBuilder Secondary => new("secondary");
    /// <summary>
    /// Gets a focus ring builder with success theme color.
    /// </summary>
    public static FocusRingBuilder Success => new("success");
    /// <summary>
    /// Gets a focus ring builder with info theme color.
    /// </summary>
    public static FocusRingBuilder Info => new("info");
    /// <summary>
    /// Gets a focus ring builder with warning theme color.
    /// </summary>
    public static FocusRingBuilder Warning => new("warning");
    /// <summary>
    /// Gets a focus ring builder with danger theme color.
    /// </summary>
    public static FocusRingBuilder Danger => new("danger");
    /// <summary>
    /// Gets a focus ring builder with light theme color.
    /// </summary>
    public static FocusRingBuilder Light => new("light");
    /// <summary>
    /// Gets a focus ring builder with dark theme color.
    /// </summary>
    public static FocusRingBuilder Dark => new("dark");
}







