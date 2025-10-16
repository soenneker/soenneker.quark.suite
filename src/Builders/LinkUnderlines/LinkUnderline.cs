namespace Soenneker.Quark;

public static class LinkUnderline
{
    public static LinkUnderlineBuilder Default => new("", "base");
    
    // Opacity
    public static LinkUnderlineBuilder Opacity0 => new("0", "opacity");
    public static LinkUnderlineBuilder Opacity10 => new("10", "opacity");
    public static LinkUnderlineBuilder Opacity25 => new("25", "opacity");
    public static LinkUnderlineBuilder Opacity50 => new("50", "opacity");
    public static LinkUnderlineBuilder Opacity75 => new("75", "opacity");
    public static LinkUnderlineBuilder Opacity100 => new("100", "opacity");
    
    // Colors
    public static LinkUnderlineBuilder Primary => new("primary", "color");
    public static LinkUnderlineBuilder Secondary => new("secondary", "color");
    public static LinkUnderlineBuilder Success => new("success", "color");
    public static LinkUnderlineBuilder Info => new("info", "color");
    public static LinkUnderlineBuilder Warning => new("warning", "color");
    public static LinkUnderlineBuilder Danger => new("danger", "color");
    public static LinkUnderlineBuilder Light => new("light", "color");
    public static LinkUnderlineBuilder Dark => new("dark", "color");
}


