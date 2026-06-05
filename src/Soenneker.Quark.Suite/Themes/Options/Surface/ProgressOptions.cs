
namespace Soenneker.Quark;

/// <summary>
/// Represents the progress options.
/// </summary>
public sealed class ProgressOptions : ComponentOptions
{
    public ProgressOptions()
    {
        Selector = "[data-slot='progress']";
    }
}
