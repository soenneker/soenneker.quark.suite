namespace Soenneker.Quark;

/// <summary>
/// Static utility for accent color. Tailwind: accent-*.
/// </summary>
public static class AccentColor
{
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public static AccentColorBuilder Auto => new("auto");
    /// <summary>
    /// `accent-primary` — uses your theme primary (shadcn maps this to CSS variables).
    /// </summary>
    public static AccentColorBuilder Primary => new("primary");
    /// <summary>
    /// Fully transparent color (`transparent`).
    /// </summary>
    public static AccentColorBuilder Transparent => new("transparent");
    /// <summary>
    /// `currentColor` — uses the element’s computed `color` (common for icons and rings).
    /// </summary>
    public static AccentColorBuilder Current => new("current");

    /// <summary>
    /// Creates an accent color builder from a Tailwind color token suffix such as <c>blue-500</c> or <c>[var(--brand)]</c>.
    /// </summary>
    public static AccentColorBuilder Token(string token) => new(token);

    /// <summary>
    /// Passes through a fully-prefixed Tailwind utility such as <c>accent-blue-500</c>.
    /// </summary>
    public static AccentColorBuilder Utility(string utility) => new(utility, isUtility: true);
}
