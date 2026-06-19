
namespace Soenneker.Quark;

/// <summary>
/// Represents the data table loader options.
/// </summary>
public sealed class DataTableLoaderOptions : ComponentOptions
{
    public DataTableLoaderOptions()
    {
        Selector = "[data-slot='data-table-loader']";
    }
}
