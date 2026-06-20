
namespace Soenneker.Quark;

/// <summary>
/// Represents the data table datatable-pagination options.
/// </summary>
public sealed class DataTablePaginationOptions : ComponentOptions
{
    public DataTablePaginationOptions()
    {
        Selector = "[data-slot='datatable-pagination']";
    }
}
