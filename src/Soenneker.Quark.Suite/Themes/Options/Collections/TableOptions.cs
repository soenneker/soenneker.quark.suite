
namespace Soenneker.Quark;

/// <summary>
/// Represents the table options.
/// </summary>
public sealed class TableOptions : ComponentOptions
{
    public TableOptions()
    {
        Selector = "[data-slot='table']";
    }
}
