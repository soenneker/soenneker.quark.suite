using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// Legacy Quark step render mode. Replaced by <see cref="StepsRenderMode"/>.
/// </summary>
[Intellenum<string>]
public sealed partial class StepRenderMode
{
    /// <summary>
    /// Default render mode - always renders step content.
    /// </summary>
    public static readonly StepRenderMode Default = new("default");

    /// <summary>
    /// Lazy load mode - renders step content only on first visit.
    /// </summary>
    public static readonly StepRenderMode LazyLoad = new("lazy-load");

    /// <summary>
    /// Lazy reload mode - only renders the active step content.
    /// </summary>
    public static readonly StepRenderMode LazyReload = new("lazy-reload");
}
