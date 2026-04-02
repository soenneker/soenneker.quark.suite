namespace Soenneker.Quark;

/// <summary>
/// Static utility for mix-blend-mode. Tailwind: mix-blend-*.
/// </summary>
public static class MixBlendMode
{
    /// <summary>
    /// Fluent step for `Normal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Normal => new("normal");
    /// <summary>
    /// Fluent step for `Multiply` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Multiply => new("multiply");
    /// <summary>
    /// Viewport-relative size (e.g. `w-screen`, `min-h-screen`) tied to the viewport width/height.
    /// </summary>
    public static MixBlendModeBuilder Screen => new("screen");
    /// <summary>
    /// Fluent step for `Overlay` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Overlay => new("overlay");
    /// <summary>
    /// Fluent step for `Darken` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Darken => new("darken");
    /// <summary>
    /// Fluent step for `Lighten` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Lighten => new("lighten");
    /// <summary>
    /// Fluent step for `Color Dodge` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder ColorDodge => new("color-dodge");
    /// <summary>
    /// Fluent step for `Color Burn` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder ColorBurn => new("color-burn");
    /// <summary>
    /// Fluent step for `Hard Light` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder HardLight => new("hard-light");
    /// <summary>
    /// Fluent step for `Soft Light` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder SoftLight => new("soft-light");
    /// <summary>
    /// Fluent step for `Difference` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Difference => new("difference");
    /// <summary>
    /// Fluent step for `Exclusion` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Exclusion => new("exclusion");
    /// <summary>
    /// Fluent step for `Hue` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Hue => new("hue");
    /// <summary>
    /// Fluent step for `Saturation` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Saturation => new("saturation");
    /// <summary>
    /// Fluent step for `Color` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Color => new("color");
    /// <summary>
    /// Fluent step for `Luminosity` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder Luminosity => new("luminosity");
    /// <summary>
    /// Fluent step for `Plus Darker` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder PlusDarker => new("plus-darker");
    /// <summary>
    /// Fluent step for `Plus Lighter` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static MixBlendModeBuilder PlusLighter => new("plus-lighter");
}
