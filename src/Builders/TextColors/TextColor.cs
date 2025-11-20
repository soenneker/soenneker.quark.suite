namespace Soenneker.Quark;

/// <summary>
/// Text color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class TextColor
{
    /// <summary>
    /// Gets a text color builder with primary theme color.
    /// </summary>
    public static TextColorBuilder Primary => new("primary");
    /// <summary>
    /// Gets a text color builder with secondary theme color.
    /// </summary>
    public static TextColorBuilder Secondary => new("secondary");
    /// <summary>
    /// Gets a text color builder with success theme color.
    /// </summary>
    public static TextColorBuilder Success => new("success");
    /// <summary>
    /// Gets a text color builder with danger theme color.
    /// </summary>
    public static TextColorBuilder Danger => new("danger");
    /// <summary>
    /// Gets a text color builder with warning theme color.
    /// </summary>
    public static TextColorBuilder Warning => new("warning");
    /// <summary>
    /// Gets a text color builder with info theme color.
    /// </summary>
    public static TextColorBuilder Info => new("info");
    /// <summary>
    /// Gets a text color builder with light theme color.
    /// </summary>
    public static TextColorBuilder Light => new("light");
    /// <summary>
    /// Gets a text color builder with dark theme color.
    /// </summary>
    public static TextColorBuilder Dark => new("dark");
    /// <summary>
    /// Gets a text color builder with muted color.
    /// </summary>
    public static TextColorBuilder Muted => new("muted");
    /// <summary>
    /// Gets a text color builder with white color.
    /// </summary>
    public static TextColorBuilder White => new("white");
    /// <summary>
    /// Gets a text color builder with black color.
    /// </summary>
    public static TextColorBuilder Black => new("black");
    /// <summary>
    /// Gets a text color builder with body color.
    /// </summary>
    public static TextColorBuilder Body => new("body");
    /// <summary>
    /// Gets a text color builder with body secondary color.
    /// </summary>
    public static TextColorBuilder BodySecondary => new("body-secondary");
    /// <summary>
    /// Gets a text color builder with body tertiary color.
    /// </summary>
    public static TextColorBuilder BodyTertiary => new("body-tertiary");
    /// <summary>
    /// Gets a text color builder with body emphasis color.
    /// </summary>
    public static TextColorBuilder BodyEmphasis => new("body-emphasis");
    /// <summary>
    /// Gets a text color builder with body highlight color.
    /// </summary>
    public static TextColorBuilder BodyHighlight => new("body-highlight");
    /// <summary>
    /// Gets a text color builder with body muted color.
    /// </summary>
    public static TextColorBuilder BodyMuted => new("body-muted");
    /// <summary>
    /// Gets a text color builder with body reset color.
    /// </summary>
    public static TextColorBuilder BodyReset => new("body-reset");
    /// <summary>
    /// Gets a text color builder with body inverse color.
    /// </summary>
    public static TextColorBuilder BodyInverse => new("body-inverse");
    /// <summary>
    /// Gets a text color builder with body inverse secondary color.
    /// </summary>
    public static TextColorBuilder BodyInverseSecondary => new("body-inverse-secondary");
    /// <summary>
    /// Gets a text color builder with body inverse tertiary color.
    /// </summary>
    public static TextColorBuilder BodyInverseTertiary => new("body-inverse-tertiary");
    /// <summary>
    /// Gets a text color builder with body inverse emphasis color.
    /// </summary>
    public static TextColorBuilder BodyInverseEmphasis => new("body-inverse-emphasis");
    /// <summary>
    /// Gets a text color builder with body inverse highlight color.
    /// </summary>
    public static TextColorBuilder BodyInverseHighlight => new("body-inverse-highlight");
    /// <summary>
    /// Gets a text color builder with body inverse muted color.
    /// </summary>
    public static TextColorBuilder BodyInverseMuted => new("body-inverse-muted");
    /// <summary>
    /// Gets a text color builder with body inverse reset color.
    /// </summary>
    public static TextColorBuilder BodyInverseReset => new("body-inverse-reset");
}
