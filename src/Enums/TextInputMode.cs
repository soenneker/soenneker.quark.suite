using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Hints at the type of data that might be entered by the user while editing the TextEdit component.
/// </summary>
[EnumValue<string>]
public sealed partial class TextInputMode
{
    /// <summary>
    /// Standard keyboard for text input.
    /// </summary>
    public static readonly TextInputMode Text = new("text");

    /// <summary>
    /// Numeric keypad for decimal numbers.
    /// </summary>
    public static readonly TextInputMode Decimal = new("decimal");

    /// <summary>
    /// Numeric keypad for integers.
    /// </summary>
    public static readonly TextInputMode Numeric = new("numeric");

    /// <summary>
    /// Numeric keypad for telephone numbers.
    /// </summary>
    public static readonly TextInputMode Tel = new("tel");

    /// <summary>
    /// Email keyboard layout.
    /// </summary>
    public static readonly TextInputMode Email = new("email");

    /// <summary>
    /// URL keyboard layout.
    /// </summary>
    public static readonly TextInputMode Url = new("url");

    /// <summary>
    /// Search keyboard layout.
    /// </summary>
    public static readonly TextInputMode Search = new("search");

    /// <summary>
    /// Password keyboard layout.
    /// </summary>
    public static readonly TextInputMode Password = new("password");
}
