namespace Soenneker.Quark;

/// <summary>
/// Border color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class BorderColor
{
    /// <summary>
    /// Gets a border color builder with primary theme color.
    /// </summary>
    public static BorderColorBuilder Primary => new("primary");
    /// <summary>
    /// Gets a border color builder with secondary theme color.
    /// </summary>
    public static BorderColorBuilder Secondary => new("secondary");
    /// <summary>
    /// Gets a border color builder with success theme color.
    /// </summary>
    public static BorderColorBuilder Success => new("success");
    /// <summary>
    /// Gets a border color builder with danger theme color.
    /// </summary>
    public static BorderColorBuilder Danger => new("danger");
    /// <summary>
    /// Gets a border color builder with warning theme color.
    /// </summary>
    public static BorderColorBuilder Warning => new("warning");
    /// <summary>
    /// Gets a border color builder with info theme color.
    /// </summary>
    public static BorderColorBuilder Info => new("info");
    /// <summary>
    /// Gets a border color builder with light theme color.
    /// </summary>
    public static BorderColorBuilder Light => new("light");
    /// <summary>
    /// Gets a border color builder with dark theme color.
    /// </summary>
    public static BorderColorBuilder Dark => new("dark");
    /// <summary>
    /// Gets a border color builder with muted color.
    /// </summary>
    public static BorderColorBuilder Muted => new("muted");
    /// <summary>
    /// Gets a border color builder with white color.
    /// </summary>
    public static BorderColorBuilder White => new("white");
    /// <summary>
    /// Gets a border color builder with black color.
    /// </summary>
    public static BorderColorBuilder Black => new("black");
    /// <summary>
    /// Gets a border color builder with body color.
    /// </summary>
    public static BorderColorBuilder Body => new("body");
    /// <summary>
    /// Gets a border color builder with body secondary color.
    /// </summary>
    public static BorderColorBuilder BodySecondary => new("body-secondary");
    /// <summary>
    /// Gets a border color builder with body tertiary color.
    /// </summary>
    public static BorderColorBuilder BodyTertiary => new("body-tertiary");
    /// <summary>
    /// Gets a border color builder with body emphasis color.
    /// </summary>
    public static BorderColorBuilder BodyEmphasis => new("body-emphasis");
    /// <summary>
    /// Gets a border color builder with body highlight color.
    /// </summary>
    public static BorderColorBuilder BodyHighlight => new("body-highlight");
    /// <summary>
    /// Gets a border color builder with body muted color.
    /// </summary>
    public static BorderColorBuilder BodyMuted => new("body-muted");
    /// <summary>
    /// Gets a border color builder with body reset color.
    /// </summary>
    public static BorderColorBuilder BodyReset => new("body-reset");
    /// <summary>
    /// Gets a border color builder with body inverse color.
    /// </summary>
    public static BorderColorBuilder BodyInverse => new("body-inverse");
    /// <summary>
    /// Gets a border color builder with body inverse secondary color.
    /// </summary>
    public static BorderColorBuilder BodyInverseSecondary => new("body-inverse-secondary");
    /// <summary>
    /// Gets a border color builder with body inverse tertiary color.
    /// </summary>
    public static BorderColorBuilder BodyInverseTertiary => new("body-inverse-tertiary");
    /// <summary>
    /// Gets a border color builder with body inverse emphasis color.
    /// </summary>
    public static BorderColorBuilder BodyInverseEmphasis => new("body-inverse-emphasis");
    /// <summary>
    /// Gets a border color builder with body inverse highlight color.
    /// </summary>
    public static BorderColorBuilder BodyInverseHighlight => new("body-inverse-highlight");
    /// <summary>
    /// Gets a border color builder with body inverse muted color.
    /// </summary>
    public static BorderColorBuilder BodyInverseMuted => new("body-inverse-muted");
    /// <summary>
    /// Gets a border color builder with body inverse reset color.
    /// </summary>
    public static BorderColorBuilder BodyInverseReset => new("body-inverse-reset");
}
