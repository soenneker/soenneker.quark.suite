
namespace Soenneker.Quark;

/// <summary>
/// Represents the breadcrumb options.
/// </summary>
public sealed class BreadcrumbOptions : ComponentOptions
{
    public BreadcrumbOptions()
    {
        Selector = "[data-slot='breadcrumb']";
    }
}
