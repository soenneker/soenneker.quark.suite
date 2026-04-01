
namespace Soenneker.Quark;

/// <summary>
/// Tailwind text break utility entry points.
/// </summary>
public static class TextBreak
{
    /// <summary>
    /// Uses normal line breaking.
    /// </summary>
    public static TextBreakBuilder Normal => new("normal");
    /// <summary>
    /// Breaks words when needed.
    /// </summary>
    public static TextBreakBuilder Words => new("words");
    /// <summary>
    /// Breaks at any character.
    /// </summary>
    public static TextBreakBuilder All => new("all");
    /// <summary>
    /// Prevents breaks in CJK text.
    /// </summary>
    public static TextBreakBuilder Keep => new("keep");
}
