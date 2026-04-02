namespace Soenneker.Quark;

public static class Stroke
{
    /// <summary>
    /// Tailwind token segment (spacing scale step, arbitrary value like `[17rem]`, or theme key). Builds the matching utility class for this builder.
    /// </summary>
    /// <param name="value">Suffix/token after the utility prefix (see Tailwind docs for this family).</param>
    public static StrokeBuilder Token(string value) => new("stroke", value);
    public static StrokeBuilder Width(int value) => new("stroke", value.ToString());
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public static StrokeBuilder None => new("stroke", "none");
}
