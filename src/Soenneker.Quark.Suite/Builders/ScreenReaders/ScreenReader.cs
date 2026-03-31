
namespace Soenneker.Quark;

/// <summary>
/// Simplified screen reader utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class ScreenReader
{
    /// <summary>
    /// Screen reader only (sr-only).
    /// </summary>
    public static ScreenReaderBuilder Only => new("only");

    /// <summary>
    /// Screen reader only focusable (sr-only-focusable).
    /// </summary>
    public static ScreenReaderBuilder OnlyFocusable => new("only-focusable");
}
