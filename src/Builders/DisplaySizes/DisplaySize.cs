namespace Soenneker.Quark;

/// <summary>
/// Simplified display size utility with fluent API and Bootstrap-first approach for display headings.
/// </summary>
public static class DisplaySize
{
    /// <summary>
    /// Display heading size 1 (largest). Generates 'display-1' class.
    /// </summary>
    public static DisplaySizeBuilder Is1 => new("1");

    /// <summary>
    /// Display heading size 2. Generates 'display-2' class.
    /// </summary>
    public static DisplaySizeBuilder Is2 => new("2");

    /// <summary>
    /// Display heading size 3. Generates 'display-3' class.
    /// </summary>
    public static DisplaySizeBuilder Is3 => new("3");

    /// <summary>
    /// Display heading size 4. Generates 'display-4' class.
    /// </summary>
    public static DisplaySizeBuilder Is4 => new("4");

    /// <summary>
    /// Display heading size 5. Generates 'display-5' class.
    /// </summary>
    public static DisplaySizeBuilder Is5 => new("5");

    /// <summary>
    /// Display heading size 6 (smallest). Generates 'display-6' class.
    /// </summary>
    public static DisplaySizeBuilder Is6 => new("6");
}

