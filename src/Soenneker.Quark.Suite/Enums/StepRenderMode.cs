using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class StepsRenderMode
{
    /// <summary>
    /// Default render mode - always renders step content.
    /// </summary>
    public static readonly StepsRenderMode Default = new("default");

    /// <summary>
    /// Lazy load mode - renders step content only on first visit.
    /// </summary>
    public static readonly StepsRenderMode LazyLoad = new("lazy-load");

    /// <summary>
    /// Lazy reload mode - only renders the active step content.
    /// </summary>
    public static readonly StepsRenderMode LazyReload = new("lazy-reload");
}