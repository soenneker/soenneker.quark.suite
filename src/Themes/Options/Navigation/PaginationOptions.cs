
namespace Soenneker.Quark;

public sealed class PaginationOptions : ComponentOptions
{
    public PaginationOptions()
    {
        Selector = ".pagination, .pagination .page-item, .pagination .page-link";
    }
}
