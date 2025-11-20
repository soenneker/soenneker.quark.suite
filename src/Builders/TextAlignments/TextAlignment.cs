using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating text alignment builders with predefined values.
/// </summary>
public static class TextAlignment
{
	/// <summary>
	/// Gets a text alignment builder with start value (left in LTR, right in RTL).
	/// </summary>
	public static TextAlignmentBuilder Start => new(TextAlignKeyword.StartValue);

	/// <summary>
	/// Gets a text alignment builder with center value (text is centered).
	/// </summary>
	public static TextAlignmentBuilder Center => new(TextAlignKeyword.CenterValue);

	/// <summary>
	/// Gets a text alignment builder with end value (right in LTR, left in RTL).
	/// </summary>
	public static TextAlignmentBuilder End => new(TextAlignKeyword.EndValue);

	/// <summary>
	/// Gets a text alignment builder with inherit keyword.
	/// </summary>
	public static TextAlignmentBuilder Inherit => new(GlobalKeyword.InheritValue);
	/// <summary>
	/// Gets a text alignment builder with initial keyword.
	/// </summary>
	public static TextAlignmentBuilder Initial => new(GlobalKeyword.InitialValue);
	/// <summary>
	/// Gets a text alignment builder with revert keyword.
	/// </summary>
	public static TextAlignmentBuilder Revert => new(GlobalKeyword.RevertValue);
	/// <summary>
	/// Gets a text alignment builder with revert-layer keyword.
	/// </summary>
	public static TextAlignmentBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Gets a text alignment builder with unset keyword.
	/// </summary>
	public static TextAlignmentBuilder Unset => new(GlobalKeyword.UnsetValue);
}
