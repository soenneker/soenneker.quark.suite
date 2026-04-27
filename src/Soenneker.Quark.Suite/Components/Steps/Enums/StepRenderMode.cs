using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class StepsRenderMode
{
    /// <summary>
    /// Default render mode - always renders step content.
    /// </summary>
    public static readonly StepsRenderMode Default = new(0);

    /// <summary>
    /// Lazy load mode - renders step content only on first visit.
    /// </summary>
    public static readonly StepsRenderMode LazyLoad = new(1);

    /// <summary>
    /// Lazy reload mode - only renders the active step content.
    /// </summary>
    public static readonly StepsRenderMode LazyReload = new(2);
}