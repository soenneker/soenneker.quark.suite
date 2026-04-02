namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned radius utility.
/// </summary>
public static class Radius
{
    /// <summary>
    /// Default theme radius: `rounded` with no suffix — in Tailwind’s default config typically `0.25rem` (maps to shadcn `--radius` usage when you align tokens).
    /// </summary>
    public static RadiusBuilder Default => new RadiusBuilder().Default;
    /// <summary>
    /// <c>rounded-none</c> — <c>border-radius: 0</c> on the selected corners (fully square corners).
    /// </summary>
    public static RadiusBuilder None => new RadiusBuilder().None;
    /// <summary>
    /// `rounded-sm` — small radius (default theme `0.125rem`).
    /// </summary>
    public static RadiusBuilder Sm => new RadiusBuilder().Sm;
    /// <summary>
    /// `rounded-md` — medium radius (default theme `0.375rem`); common for cards and inputs in shadcn.
    /// </summary>
    public static RadiusBuilder Md => new RadiusBuilder().Md;
    /// <summary>
    /// `rounded-lg` — large radius (default theme `0.5rem`).
    /// </summary>
    public static RadiusBuilder Lg => new RadiusBuilder().Lg;
    /// <summary>
    /// `rounded-xl` — extra-large radius (default theme `0.75rem`).
    /// </summary>
    public static RadiusBuilder Xl => new RadiusBuilder().Xl;
    /// <summary>
    /// `rounded-2xl` — 2× XL radius (default theme `1rem`).
    /// </summary>
    public static RadiusBuilder TwoXl => new RadiusBuilder().TwoXl;
    /// <summary>
    /// `rounded-3xl` — very large radius (default theme `1.5rem`).
    /// </summary>
    public static RadiusBuilder ThreeXl => new RadiusBuilder().ThreeXl;
    /// <summary>
    /// “Full” extremum for this utility. For border radius this is `rounded-full` (`border-radius: 9999px`), producing pills/circles; for width/height often `100%` (`w-full` / `h-full`).
    /// </summary>
    public static RadiusBuilder Full => new RadiusBuilder().Full;

    /// <summary>
    /// Targets all corners (`rounded-*` with no corner suffix).
    /// </summary>
    public static RadiusBuilder All => new RadiusBuilder().All;
    /// <summary>
    /// Top corners only (`rounded-t-*`).
    /// </summary>
    public static RadiusBuilder Top => new RadiusBuilder().Top;
    /// <summary>
    /// Bottom corners only (`rounded-b-*`).
    /// </summary>
    public static RadiusBuilder Bottom => new RadiusBuilder().Bottom;
    /// <summary>
    /// Left corners only (`rounded-l-*`).
    /// </summary>
    public static RadiusBuilder Left => new RadiusBuilder().Left;
    /// <summary>
    /// Right corners only (`rounded-r-*`).
    /// </summary>
    public static RadiusBuilder Right => new RadiusBuilder().Right;

    /// <summary>
    /// Top-left corner only (`rounded-tl-*`).
    /// </summary>
    public static RadiusBuilder TopLeft => new RadiusBuilder().TopLeft;
    /// <summary>
    /// Top-right corner only (`rounded-tr-*`).
    /// </summary>
    public static RadiusBuilder TopRight => new RadiusBuilder().TopRight;
    /// <summary>
    /// Bottom-left corner only (`rounded-bl-*`).
    /// </summary>
    public static RadiusBuilder BottomLeft => new RadiusBuilder().BottomLeft;
    /// <summary>
    /// Bottom-right corner only (`rounded-br-*`).
    /// </summary>
    public static RadiusBuilder BottomRight => new RadiusBuilder().BottomRight;

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public static RadiusBuilder OnBase => new RadiusBuilder().OnBase;
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public static RadiusBuilder OnSm => new RadiusBuilder().OnSm;
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public static RadiusBuilder OnMd => new RadiusBuilder().OnMd;
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public static RadiusBuilder OnLg => new RadiusBuilder().OnLg;
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public static RadiusBuilder OnXl => new RadiusBuilder().OnXl;
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public static RadiusBuilder On2xl => new RadiusBuilder().On2xl;

    /// <summary>
    /// Custom <c>rounded-*</c> suffix: theme scale key, arbitrary length (for example <c>[2vw]</c>), or CSS variable reference aligned with shadcn’s <c>--radius</c> pattern.
    /// </summary>
    /// <param name="value">The segment after <c>rounded-</c> (and any corner prefix such as <c>tl-</c>).</param>
    public static RadiusBuilder Token(string value) => new RadiusBuilder().Token(value);
}