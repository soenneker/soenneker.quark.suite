namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating shadcn-style focus ring builders.
/// </summary>
public static class FocusRing
{
    public static FocusRingBuilder Ring => new("ring");
    public static FocusRingBuilder Primary => new("primary");
    public static FocusRingBuilder Secondary => new("secondary");
    public static FocusRingBuilder Destructive => new("destructive");
    public static FocusRingBuilder Muted => new("muted");
    public static FocusRingBuilder Accent => new("accent");

    public static FocusRingBuilder Success => new("success");
    public static FocusRingBuilder Info => new("info");
    public static FocusRingBuilder Warning => new("warning");
    public static FocusRingBuilder Danger => new("danger");
    public static FocusRingBuilder Light => new("light");
    public static FocusRingBuilder Dark => new("dark");
}
