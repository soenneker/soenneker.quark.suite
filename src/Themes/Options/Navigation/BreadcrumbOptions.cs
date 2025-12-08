
namespace Soenneker.Quark;

public sealed class BreadcrumbOptions : ComponentOptions
{
    public BreadcrumbOptions()
    {
        Selector = ".breadcrumb, .breadcrumb .breadcrumb-item, .breadcrumb-item.active";
    }
}
