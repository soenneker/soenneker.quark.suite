namespace Soenneker.Quark;

/// <summary>
/// Static utility for caret color. Tailwind: caret-*.
/// </summary>
public static class CaretColor
{
    /// <summary>
    /// Fluent step for `Primary` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static CaretColorBuilder Primary => new("primary");
    /// <summary>
    /// Fully transparent color (`transparent`).
    /// </summary>
    public static CaretColorBuilder Transparent => new("transparent");
    /// <summary>
    /// `currentColor` — uses the element’s computed `color` (common for icons and rings).
    /// </summary>
    public static CaretColorBuilder Current => new("current");

    /// <summary>
    /// Creates a caret color builder from a Tailwind color token suffix such as <c>blue-500</c> or <c>[var(--brand)]</c>.
    /// </summary>
    public static CaretColorBuilder Token(string token) => new(token);

    /// <summary>
    /// Passes through a fully-prefixed Tailwind utility such as <c>caret-blue-500</c>.
    /// </summary>
    public static CaretColorBuilder Utility(string utility) => new(utility, isUtility: true);
}
