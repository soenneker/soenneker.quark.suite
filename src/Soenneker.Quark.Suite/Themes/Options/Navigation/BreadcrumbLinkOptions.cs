namespace Soenneker.Quark;

/// <summary>
/// Represents the breadcrumb link options.
/// </summary>
public sealed class BreadcrumbLinkOptions : ComponentOptions
{
    public BreadcrumbLinkOptions()
    {
        Selector = "[data-slot='breadcrumb-link']";
    }
}
