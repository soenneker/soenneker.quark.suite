namespace Soenneker.Quark;

/// <summary>
/// Represents the breadcrumb separator options.
/// </summary>
public sealed class BreadcrumbSeparatorOptions : ComponentOptions
{
    public BreadcrumbSeparatorOptions()
    {
        Selector = "[data-slot='breadcrumb-separator']";
    }
}
