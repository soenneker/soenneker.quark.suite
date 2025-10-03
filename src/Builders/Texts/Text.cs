using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap text utilities.
/// </summary>
public readonly struct TextStyle
{
    public readonly TextType Type;
    public readonly TextBreakpoint? Breakpoint;

    public TextStyle(TextType type, TextBreakpoint? breakpoint = null)
    {
        Type = type;
        Breakpoint = breakpoint;
    }

    public static TextStyle Start => new(TextType.Start);
    public static TextStyle Center => new(TextType.Center);
    public static TextStyle End => new(TextType.End);
    public static TextStyle Justify => new(TextType.Justify);
    public static TextStyle Wrap => new(TextType.Wrap);
    public static TextStyle Nowrap => new(TextType.Nowrap);
    public static TextStyle Truncate => new(TextType.Truncate);
    public static TextStyle Lowercase => new(TextType.Lowercase);
    public static TextStyle Uppercase => new(TextType.Uppercase);
    public static TextStyle Capitalize => new(TextType.Capitalize);
    public static TextStyle Reset => new(TextType.Reset);
    public static TextStyle Muted => new(TextType.Muted);
    public static TextStyle Emphasis => new(TextType.Emphasis);
}