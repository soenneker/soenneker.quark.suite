
namespace Soenneker.Quark;

/// <summary>
/// Represents the data table no data options.
/// </summary>
public sealed class DataTableNoDataOptions : ComponentOptions
{
    public DataTableNoDataOptions()
    {
        Selector = "[data-slot='datatable-empty']";
    }
}
