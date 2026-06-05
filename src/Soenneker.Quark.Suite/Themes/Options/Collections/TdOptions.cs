
namespace Soenneker.Quark;

/// <summary>
/// Represents the td options.
/// </summary>
public sealed class TdOptions : ComponentOptions
{
    public TdOptions()
    {
        Selector = "[data-slot='table-cell']";
    }
}
