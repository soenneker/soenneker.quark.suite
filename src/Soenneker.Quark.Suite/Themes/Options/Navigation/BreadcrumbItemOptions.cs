
namespace Soenneker.Quark;

/// <summary>
/// Represents the breadcrumb item options.
/// </summary>
public sealed class BreadcrumbItemOptions : ComponentOptions
{
    public BreadcrumbItemOptions()
    {
        Selector = "[data-slot='breadcrumb-item']";
    }
}
