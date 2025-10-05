namespace Soenneker.Quark;

/// <summary>
/// Border color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class BorderColor
{
    public static BorderColorBuilder Primary => new("primary");
    public static BorderColorBuilder Secondary => new("secondary");
    public static BorderColorBuilder Success => new("success");
    public static BorderColorBuilder Danger => new("danger");
    public static BorderColorBuilder Warning => new("warning");
    public static BorderColorBuilder Info => new("info");
    public static BorderColorBuilder Light => new("light");
    public static BorderColorBuilder Dark => new("dark");
    public static BorderColorBuilder Muted => new("muted");
    public static BorderColorBuilder White => new("white");
    public static BorderColorBuilder Black => new("black");
    public static BorderColorBuilder Body => new("body");
    public static BorderColorBuilder BodySecondary => new("body-secondary");
    public static BorderColorBuilder BodyTertiary => new("body-tertiary");
    public static BorderColorBuilder BodyEmphasis => new("body-emphasis");
    public static BorderColorBuilder BodyHighlight => new("body-highlight");
    public static BorderColorBuilder BodyMuted => new("body-muted");
    public static BorderColorBuilder BodyReset => new("body-reset");
    public static BorderColorBuilder BodyInverse => new("body-inverse");
    public static BorderColorBuilder BodyInverseSecondary => new("body-inverse-secondary");
    public static BorderColorBuilder BodyInverseTertiary => new("body-inverse-tertiary");
    public static BorderColorBuilder BodyInverseEmphasis => new("body-inverse-emphasis");
    public static BorderColorBuilder BodyInverseHighlight => new("body-inverse-highlight");
    public static BorderColorBuilder BodyInverseMuted => new("body-inverse-muted");
    public static BorderColorBuilder BodyInverseReset => new("body-inverse-reset");
}
