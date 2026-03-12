namespace Soenneker.Quark;

/// <summary>
/// Static utility for background-blend-mode. Tailwind: bg-blend-*.
/// </summary>
public static class BackgroundBlendMode
{
    public static BackgroundBlendModeBuilder Normal => new("normal");
    public static BackgroundBlendModeBuilder Multiply => new("multiply");
    public static BackgroundBlendModeBuilder Screen => new("screen");
    public static BackgroundBlendModeBuilder Overlay => new("overlay");
    public static BackgroundBlendModeBuilder Darken => new("darken");
    public static BackgroundBlendModeBuilder Lighten => new("lighten");
    public static BackgroundBlendModeBuilder ColorDodge => new("color-dodge");
    public static BackgroundBlendModeBuilder ColorBurn => new("color-burn");
    public static BackgroundBlendModeBuilder HardLight => new("hard-light");
    public static BackgroundBlendModeBuilder SoftLight => new("soft-light");
    public static BackgroundBlendModeBuilder Difference => new("difference");
    public static BackgroundBlendModeBuilder Exclusion => new("exclusion");
    public static BackgroundBlendModeBuilder Hue => new("hue");
    public static BackgroundBlendModeBuilder Saturation => new("saturation");
    public static BackgroundBlendModeBuilder Color => new("color");
    public static BackgroundBlendModeBuilder Luminosity => new("luminosity");
}
