using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// Legacy Quark step render mode. Replaced by <see cref="StepsRenderMode"/>.
/// </summary>
[Intellenum<string>]
public sealed partial class StepRenderMode
{
    public static readonly StepRenderMode Default = new("default");
    public static readonly StepRenderMode LazyLoad = new("lazy-load");
    public static readonly StepRenderMode LazyReload = new("lazy-reload");
}
