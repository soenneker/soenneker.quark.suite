
namespace Soenneker.Quark;

/// <summary>
/// Represents the table pagination options.
/// </summary>
public sealed class TablePaginationOptions : ComponentOptions
{
    public TablePaginationOptions()
    {
        Selector = "[data-slot='pagination']";
    }
}
