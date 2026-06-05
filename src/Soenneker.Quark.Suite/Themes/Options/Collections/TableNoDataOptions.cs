
namespace Soenneker.Quark;

/// <summary>
/// Represents the table no data options.
/// </summary>
public sealed class TableNoDataOptions : ComponentOptions
{
    public TableNoDataOptions()
    {
        Selector = "[data-slot='table-empty']";
    }
}
