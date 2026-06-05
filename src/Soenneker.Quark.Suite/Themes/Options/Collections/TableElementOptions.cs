
namespace Soenneker.Quark;

/// <summary>
/// Represents the table element options.
/// </summary>
public sealed class TableElementOptions : ComponentOptions
{
    public TableElementOptions()
    {
        Selector = "[data-slot='table']";
    }
}
