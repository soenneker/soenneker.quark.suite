using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap gutter utilities for spacing between grid columns.
/// </summary>
public readonly struct Gutter
{
    public readonly int Value;
    public readonly GutterType Type;
    public readonly GutterBreakpoint Breakpoint;

    public Gutter(int value, GutterType? type = null, GutterBreakpoint? breakpoint = null)
    {
        Value = value;
        Type = type ?? GutterType.All;
        Breakpoint = breakpoint ?? GutterBreakpoint.None;
    }

    public static Gutter None => new(0, GutterType.All);
    public static Gutter Xs => new(1, GutterType.All);
    public static Gutter Sm => new(1, GutterType.All, GutterBreakpoint.Sm);
    public static Gutter Md => new(1, GutterType.All, GutterBreakpoint.Md);
    public static Gutter Lg => new(1, GutterType.All, GutterBreakpoint.Lg);
    public static Gutter Xl => new(1, GutterType.All, GutterBreakpoint.Xl);
    public static Gutter Xxl => new(1, GutterType.All, GutterBreakpoint.Xxl);

    public static Gutter X(int value) => new(value, GutterType.X);
    public static Gutter Y(int value) => new(value, GutterType.Y);
    public static Gutter All(int value) => new(value, GutterType.All);

    public static Gutter XSm(int value) => new(value, GutterType.X, GutterBreakpoint.Sm);
    public static Gutter YSm(int value) => new(value, GutterType.Y, GutterBreakpoint.Sm);
    public static Gutter AllSm(int value) => new(value, GutterType.All, GutterBreakpoint.Sm);

    public static Gutter XMd(int value) => new(value, GutterType.X, GutterBreakpoint.Md);
    public static Gutter YMd(int value) => new(value, GutterType.Y, GutterBreakpoint.Md);
    public static Gutter AllMd(int value) => new(value, GutterType.All, GutterBreakpoint.Md);

    public static Gutter XLg(int value) => new(value, GutterType.X, GutterBreakpoint.Lg);
    public static Gutter YLg(int value) => new(value, GutterType.Y, GutterBreakpoint.Lg);
    public static Gutter AllLg(int value) => new(value, GutterType.All, GutterBreakpoint.Lg);

    public static Gutter XXl(int value) => new(value, GutterType.X, GutterBreakpoint.Xl);
    public static Gutter YXl(int value) => new(value, GutterType.Y, GutterBreakpoint.Xl);
    public static Gutter AllXl(int value) => new(value, GutterType.All, GutterBreakpoint.Xl);

    public static Gutter XXxl(int value) => new(value, GutterType.X, GutterBreakpoint.Xxl);
    public static Gutter YXxl(int value) => new(value, GutterType.Y, GutterBreakpoint.Xxl);
    public static Gutter AllXxl(int value) => new(value, GutterType.All, GutterBreakpoint.Xxl);
}
