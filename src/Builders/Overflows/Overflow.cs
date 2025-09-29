using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Simplified overflow utility with fluent API and Bootstrap-first approach.
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

    public static OverflowBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static OverflowBuilder Initial => new(GlobalKeyword.InitialValue);
    public static OverflowBuilder Revert => new(GlobalKeyword.RevertValue);
    public static OverflowBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static OverflowBuilder Unset => new(GlobalKeyword.UnsetValue);

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
