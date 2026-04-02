using Soenneker.Quark.Enums;


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

    /// <summary>
    /// Gets an overflow builder with inherit keyword.
    /// </summary>
    public static OverflowBuilder Inherit => new(GlobalKeyword.Inherit);
    /// <summary>
    /// Gets an overflow builder with initial keyword.
    /// </summary>
    public static OverflowBuilder Initial => new(GlobalKeyword.Initial);
    /// <summary>
    /// Gets an overflow builder with revert keyword.
    /// </summary>
    public static OverflowBuilder Revert => new(GlobalKeyword.Revert);
    /// <summary>
    /// Gets an overflow builder with revert-layer keyword.
    /// </summary>
    public static OverflowBuilder RevertLayer => new(GlobalKeyword.RevertLayer);
    /// <summary>
    /// Gets an overflow builder with unset keyword.
    /// </summary>
    public static OverflowBuilder Unset => new(GlobalKeyword.Unset);

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
