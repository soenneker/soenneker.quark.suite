using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating text wrap builders with predefined values.
/// </summary>
public static class TextWrap
{
    /// <summary>
    /// Gets a text wrap builder with wrap value (text wraps to multiple lines).
    /// </summary>
    public static TextWrapBuilder Wrap => new(TextWrapKeyword.WrapValue);

    /// <summary>
    /// Gets a text wrap builder with inherit keyword.
    /// </summary>
    public static TextWrapBuilder Inherit => new(GlobalKeyword.Inherit);
    /// <summary>
    /// Gets a text wrap builder with initial keyword.
    /// </summary>
    public static TextWrapBuilder Initial => new(GlobalKeyword.Initial);
    /// <summary>
    /// Gets a text wrap builder with revert keyword.
    /// </summary>
    public static TextWrapBuilder Revert => new(GlobalKeyword.Revert);
    /// <summary>
    /// Gets a text wrap builder with revert-layer keyword.
    /// </summary>
    public static TextWrapBuilder RevertLayer => new(GlobalKeyword.RevertLayer);
    /// <summary>
    /// Gets a text wrap builder with unset keyword.
    /// </summary>
    public static TextWrapBuilder Unset => new(GlobalKeyword.Unset);
}
