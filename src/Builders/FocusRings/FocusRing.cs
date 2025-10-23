namespace Soenneker.Quark;

public static class FocusRing
{
    public static FocusRingBuilder Primary => new("primary");
    public static FocusRingBuilder Secondary => new("secondary");
    public static FocusRingBuilder Success => new("success");
    public static FocusRingBuilder Info => new("info");
    public static FocusRingBuilder Warning => new("warning");
    public static FocusRingBuilder Danger => new("danger");
    public static FocusRingBuilder Light => new("light");
    public static FocusRingBuilder Dark => new("dark");
}





