namespace Soenneker.Quark;

/// <summary>
/// Represents the breadcrumb page options.
/// </summary>
public sealed class BreadcrumbPageOptions : ComponentOptions
{
    public BreadcrumbPageOptions()
    {
        Selector = "[data-slot='breadcrumb-page']";
    }
}
