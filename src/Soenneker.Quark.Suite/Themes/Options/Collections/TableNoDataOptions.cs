
namespace Soenneker.Quark;

public sealed class TableNoDataOptions : ComponentOptions
{
    public TableNoDataOptions()
    {
        Selector = "[data-slot='table-empty']";
    }
}
