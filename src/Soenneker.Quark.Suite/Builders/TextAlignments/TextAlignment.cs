namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating text alignment builders with predefined values.
/// </summary>
public static class TextAlignment
{
	/// <summary>
	/// Gets a text alignment builder with start value (left in LTR, right in RTL).
	/// </summary>
	public static TextAlignmentBuilder Start => new("start");

	/// <summary>
	/// Gets a text alignment builder with center value (text is centered).
	/// </summary>
	public static TextAlignmentBuilder Center => new(TextAlignKeyword.CenterValue);

	/// <summary>
	/// Gets a text alignment builder with end value (right in LTR, left in RTL).
	/// </summary>
	public static TextAlignmentBuilder End => new("end");
}
