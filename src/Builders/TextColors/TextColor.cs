namespace Soenneker.Quark;

/// <summary>
/// Text color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class TextColor
{
    public static TextColorBuilder Primary => new("primary");
    public static TextColorBuilder Secondary => new("secondary");
    public static TextColorBuilder Success => new("success");
    public static TextColorBuilder Danger => new("danger");
    public static TextColorBuilder Warning => new("warning");
    public static TextColorBuilder Info => new("info");
    public static TextColorBuilder Light => new("light");
    public static TextColorBuilder Dark => new("dark");
    public static TextColorBuilder Muted => new("muted");
    public static TextColorBuilder White => new("white");
    public static TextColorBuilder Black => new("black");
    public static TextColorBuilder Body => new("body");
    public static TextColorBuilder BodySecondary => new("body-secondary");
    public static TextColorBuilder BodyTertiary => new("body-tertiary");
    public static TextColorBuilder BodyEmphasis => new("body-emphasis");
    public static TextColorBuilder BodyHighlight => new("body-highlight");
    public static TextColorBuilder BodyMuted => new("body-muted");
    public static TextColorBuilder BodyReset => new("body-reset");
    public static TextColorBuilder BodyInverse => new("body-inverse");
    public static TextColorBuilder BodyInverseSecondary => new("body-inverse-secondary");
    public static TextColorBuilder BodyInverseTertiary => new("body-inverse-tertiary");
    public static TextColorBuilder BodyInverseEmphasis => new("body-inverse-emphasis");
    public static TextColorBuilder BodyInverseHighlight => new("body-inverse-highlight");
    public static TextColorBuilder BodyInverseMuted => new("body-inverse-muted");
    public static TextColorBuilder BodyInverseReset => new("body-inverse-reset");
}
