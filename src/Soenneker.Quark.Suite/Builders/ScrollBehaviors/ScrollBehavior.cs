
namespace Soenneker.Quark;

/// <summary>
/// Simplified scroll behavior utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class ScrollBehavior
{
    /// <summary>
    /// Auto scroll behavior (auto).
    /// </summary>
    public static ScrollBehaviorBuilder Auto => new("auto");

    /// <summary>
    /// Smooth scroll behavior (smooth).
    /// </summary>
    public static ScrollBehaviorBuilder Smooth => new("smooth");
}
