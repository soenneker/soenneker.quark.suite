
namespace Soenneker.Quark;

/// <summary>
/// Represents the table loader options.
/// </summary>
public sealed class TableLoaderOptions : ComponentOptions
{
    public TableLoaderOptions()
    {
        Selector = "[data-slot='table-loader']";
    }
}
