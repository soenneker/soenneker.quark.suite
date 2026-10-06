namespace Soenneker.Quark;

/// <summary>
/// Represents the pagination options.
/// </summary>
public sealed class PaginationOptions : ComponentOptions
{
    public PaginationOptions()
    {
        Selector = "[data-slot='pagination']";
    }

    /// <summary>
    /// Gets or sets pagination content/list styling scoped to the pagination.
    /// </summary>
    public PaginationContentOptions? Contents { get; set; }

    /// <summary>
    /// Gets or sets pagination ellipsis styling scoped to the pagination.
    /// </summary>
    public PaginationEllipsisOptions? Ellipses { get; set; }

    /// <summary>
    /// Gets or sets icon styling scoped to the pagination.
    /// </summary>
    public IconOptions? Icons { get; set; }

    /// <summary>
    /// Gets or sets pagination item styling scoped to the pagination.
    /// </summary>
    public PaginationItemOptions? Items { get; set; }

    /// <summary>
    /// Gets or sets pagination link styling scoped to the pagination.
    /// </summary>
    public PaginationLinkOptions? Links { get; set; }

    /// <summary>
    /// Gets or sets span styling scoped to the pagination.
    /// </summary>
    public SpanOptions? Spans { get; set; }

    private protected override void CollectChildCssRules(ref ComponentCssRuleCollector buffer, string baseSelector)
    {
        AddChildCssRules(ref buffer, Contents, "[data-slot='pagination-content']", "[data-slot='pagination-content']", baseSelector);
        AddChildCssRules(ref buffer, Ellipses, "[data-slot='pagination-ellipsis']", "[data-slot='pagination-ellipsis']", baseSelector);
        AddChildCssRules(ref buffer, Icons, "[data-slot='icon']", "[data-slot='icon']", baseSelector);
        AddChildCssRules(ref buffer, Items, "[data-slot='pagination-item']", "[data-slot='pagination-item']", baseSelector);
        AddChildCssRules(ref buffer, Links, "[data-slot='pagination-link']", "[data-slot='pagination-link']", baseSelector);
        AddChildCssRules(ref buffer, Spans, "[data-slot='span']", "[data-slot='span']", baseSelector);
    }
}
