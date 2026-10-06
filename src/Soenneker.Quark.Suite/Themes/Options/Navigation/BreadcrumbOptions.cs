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

    /// <summary>
    /// Gets or sets breadcrumb ellipsis styling scoped to the breadcrumb.
    /// </summary>
    public BreadcrumbEllipsisOptions? Ellipses { get; set; }

    /// <summary>
    /// Gets or sets icon styling scoped to the breadcrumb.
    /// </summary>
    public IconOptions? Icons { get; set; }

    /// <summary>
    /// Gets or sets breadcrumb item styling scoped to the breadcrumb.
    /// </summary>
    public BreadcrumbItemOptions? Items { get; set; }

    /// <summary>
    /// Gets or sets breadcrumb link styling scoped to the breadcrumb.
    /// </summary>
    public BreadcrumbLinkOptions? Links { get; set; }

    /// <summary>
    /// Gets or sets breadcrumb list styling scoped to the breadcrumb.
    /// </summary>
    public BreadcrumbListOptions? Lists { get; set; }

    /// <summary>
    /// Gets or sets breadcrumb page styling scoped to the breadcrumb.
    /// </summary>
    public BreadcrumbPageOptions? Pages { get; set; }

    /// <summary>
    /// Gets or sets breadcrumb separator styling scoped to the breadcrumb.
    /// </summary>
    public BreadcrumbSeparatorOptions? Separators { get; set; }

    private protected override void CollectChildCssRules(ref ComponentCssRuleCollector buffer, string baseSelector)
    {
        AddChildCssRules(ref buffer, Ellipses, "[data-slot='breadcrumb-ellipsis']", "[data-slot='breadcrumb-ellipsis']", baseSelector);
        AddChildCssRules(ref buffer, Icons, "[data-slot='icon']", "[data-slot='icon']", baseSelector);
        AddChildCssRules(ref buffer, Items, "[data-slot='breadcrumb-item']", "[data-slot='breadcrumb-item']", baseSelector);
        AddChildCssRules(ref buffer, Links, "[data-slot='breadcrumb-link']", "[data-slot='breadcrumb-link']", baseSelector);
        AddChildCssRules(ref buffer, Lists, "[data-slot='breadcrumb-list']", "[data-slot='breadcrumb-list']", baseSelector);
        AddChildCssRules(ref buffer, Pages, "[data-slot='breadcrumb-page']", "[data-slot='breadcrumb-page']", baseSelector);
        AddChildCssRules(ref buffer, Separators, "[data-slot='breadcrumb-separator']", "[data-slot='breadcrumb-separator']", baseSelector);
    }
}
