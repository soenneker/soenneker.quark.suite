using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap column auto utilities.
/// </summary>
public readonly struct ColumnAuto
{
    /// <summary>
    /// Gets the column auto type.
    /// </summary>
    public readonly ColumnAutoType Type;
    /// <summary>
    /// Gets the column auto breakpoint.
    /// </summary>
    public readonly ColumnAutoBreakpoint Breakpoint;

    /// <summary>
    /// Initializes a new instance of the ColumnAuto struct.
    /// </summary>
    /// <param name="type">The column auto type.</param>
    /// <param name="breakpoint">The optional column auto breakpoint. If null, defaults to None.</param>
    public ColumnAuto(ColumnAutoType type, ColumnAutoBreakpoint? breakpoint = null)
    {
        Type = type;
        Breakpoint = breakpoint ?? ColumnAutoBreakpoint.None;
    }

    /// <summary>
    /// Gets a column auto with auto type and no breakpoint.
    /// </summary>
    public static ColumnAuto Auto => new(ColumnAutoType.Auto);
    /// <summary>
    /// Gets a column auto with auto type and small breakpoint.
    /// </summary>
    public static ColumnAuto AutoSmall => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Sm);
    /// <summary>
    /// Gets a column auto with auto type and medium breakpoint.
    /// </summary>
    public static ColumnAuto AutoMedium => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Md);
    /// <summary>
    /// Gets a column auto with auto type and large breakpoint.
    /// </summary>
    public static ColumnAuto AutoLarge => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Lg);
    /// <summary>
    /// Gets a column auto with auto type and extra large breakpoint.
    /// </summary>
    public static ColumnAuto AutoExtraLarge => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Xl);
    /// <summary>
    /// Gets a column auto with auto type and extra extra large breakpoint.
    /// </summary>
    public static ColumnAuto AutoExtraExtraLarge => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Xxl);
}