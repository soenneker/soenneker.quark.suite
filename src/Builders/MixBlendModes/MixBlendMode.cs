namespace Soenneker.Quark;

/// <summary>
/// Static utility for mix-blend-mode. Tailwind: mix-blend-*.
/// </summary>
public static class MixBlendMode
{
    public static MixBlendModeBuilder Normal => new("normal");
    public static MixBlendModeBuilder Multiply => new("multiply");
    public static MixBlendModeBuilder Screen => new("screen");
    public static MixBlendModeBuilder Overlay => new("overlay");
    public static MixBlendModeBuilder Darken => new("darken");
    public static MixBlendModeBuilder Lighten => new("lighten");
    public static MixBlendModeBuilder ColorDodge => new("color-dodge");
    public static MixBlendModeBuilder ColorBurn => new("color-burn");
    public static MixBlendModeBuilder HardLight => new("hard-light");
    public static MixBlendModeBuilder SoftLight => new("soft-light");
    public static MixBlendModeBuilder Difference => new("difference");
    public static MixBlendModeBuilder Exclusion => new("exclusion");
    public static MixBlendModeBuilder Hue => new("hue");
    public static MixBlendModeBuilder Saturation => new("saturation");
    public static MixBlendModeBuilder Color => new("color");
    public static MixBlendModeBuilder Luminosity => new("luminosity");
    public static MixBlendModeBuilder PlusDarker => new("plus-darker");
    public static MixBlendModeBuilder PlusLighter => new("plus-lighter");
}
