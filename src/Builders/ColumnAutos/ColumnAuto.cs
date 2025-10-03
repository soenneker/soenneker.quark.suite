using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap column auto utilities.
/// </summary>
public readonly struct ColumnAuto
{
    public readonly ColumnAutoType Type;
    public readonly ColumnAutoBreakpoint Breakpoint;

    public ColumnAuto(ColumnAutoType type, ColumnAutoBreakpoint? breakpoint = null)
    {
        Type = type;
        Breakpoint = breakpoint ?? ColumnAutoBreakpoint.None;
    }

    public static ColumnAuto Auto => new(ColumnAutoType.Auto);
    public static ColumnAuto AutoSmall => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Sm);
    public static ColumnAuto AutoMedium => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Md);
    public static ColumnAuto AutoLarge => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Lg);
    public static ColumnAuto AutoExtraLarge => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Xl);
    public static ColumnAuto AutoExtraExtraLarge => new(ColumnAutoType.Auto, ColumnAutoBreakpoint.Xxl);
}