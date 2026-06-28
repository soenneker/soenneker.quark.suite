namespace Soenneker.Quark;

/// <summary>
/// Represents the breadcrumb list options.
/// </summary>
public sealed class BreadcrumbListOptions : ComponentOptions
{
    public BreadcrumbListOptions()
    {
        Selector = "[data-slot='breadcrumb-list']";
    }
}
