namespace Soenneker.Quark;

/// <summary>
/// Represents the breadcrumb ellipsis options.
/// </summary>
public sealed class BreadcrumbEllipsisOptions : ComponentOptions
{
    public BreadcrumbEllipsisOptions()
    {
        Selector = "[data-slot='breadcrumb-ellipsis']";
    }
}
