
namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating text break builders with predefined values.
/// </summary>
public static class TextBreak
{
    /// <summary>
    /// Gets a text break builder that enables word breaking.
    /// </summary>
    public static TextBreakBuilder Enable => new(true);
    /// <summary>
    /// Gets a text break builder that disables word breaking.
    /// </summary>
    public static TextBreakBuilder Disable => new(false);
}
