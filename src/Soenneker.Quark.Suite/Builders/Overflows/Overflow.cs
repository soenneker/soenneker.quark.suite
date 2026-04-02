namespace Soenneker.Quark;

/// <summary>
/// Simplified overflow utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class Overflow
{
    /// <summary>
    /// Overflow auto.
    /// </summary>
    public static OverflowBuilder Auto => new(OverflowKeyword.AutoValue);

    /// <summary>
    /// Overflow hidden.
    /// </summary>
    public static OverflowBuilder Hidden => new(OverflowKeyword.HiddenValue);

    /// <summary>
    /// Overflow visible.
    /// </summary>
    public static OverflowBuilder Visible => new(OverflowKeyword.VisibleValue);

    /// <summary>
    /// Overflow scroll.
    /// </summary>
    public static OverflowBuilder Scroll => new(OverflowKeyword.ScrollValue);

    // ----- Axis builders -----
    /// <summary>
    /// Overflow X axis.
    /// </summary>
    public static OverflowBuilder X => OverflowBuilder.Create().X;

    /// <summary>
    /// Overflow Y axis.
    /// </summary>
    public static OverflowBuilder Y => OverflowBuilder.Create().Y;

    /// <summary>
    /// Overflow all axes.
    /// </summary>
    public static OverflowBuilder All => OverflowBuilder.Create().All;
}
