using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// An enumeration for Quark, representing step rendering modes.
/// </summary>
[Intellenum<string>]
public sealed partial class StepRenderMode
{
    /// <summary>
    /// Always renders the steps html content to the DOM.
    /// </summary>
    public static readonly StepRenderMode Default = new("default");

    /// <summary>
    /// Lazy loads steps, meaning each step will only be rendered/loaded the first time it is visited.
    /// </summary>
    public static readonly StepRenderMode LazyLoad = new("lazy-load");

    /// <summary>
    /// Lazy loads steps everytime, meaning only the active step will have it's html rendered to the DOM.
    /// </summary>
    public static readonly StepRenderMode LazyReload = new("lazy-reload");
}
