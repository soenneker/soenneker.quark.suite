
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the table options.
/// </summary>
public sealed class TableOptions : ComponentOptions
{
    public TableOptions()
    {
        Selector = "[data-slot='table']";
    }

    /// <summary>
    /// Gets or sets table caption styling scoped to the table.
    /// </summary>
    public TableCaptionOptions? Captions { get; set; }

    /// <summary>
    /// Gets or sets table container styling scoped to the table wrapper.
    /// </summary>
    public TableContainerOptions? Containers { get; set; }

    /// <summary>
    /// Gets or sets table footer styling scoped to the table.
    /// </summary>
    public TableFooterOptions? Footers { get; set; }

    /// <summary>
    /// Gets or sets table body styling scoped to the table.
    /// </summary>
    public TbodyOptions? Tbodys { get; set; }

    /// <summary>
    /// Gets or sets table cell styling scoped to the table.
    /// </summary>
    public TdOptions? Tds { get; set; }

    /// <summary>
    /// Gets or sets table header group styling scoped to the table.
    /// </summary>
    public TheadOptions? Theads { get; set; }

    /// <summary>
    /// Gets or sets table header cell styling scoped to the table.
    /// </summary>
    public ThOptions? Ths { get; set; }

    /// <summary>
    /// Gets or sets table row styling scoped to the table.
    /// </summary>
    public TrOptions? Trs { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        string containerScope = baseSelector == "[data-slot='table']" ? string.Empty : baseSelector;

        AddChildCssRules(buffer, Containers, "[data-slot='table-container']", "[data-slot='table-container']", containerScope);
        AddChildCssRules(buffer, Captions, "[data-slot='table-caption']", "[data-slot='table-caption']", baseSelector);
        AddChildCssRules(buffer, Footers, "[data-slot='table-footer']", "[data-slot='table-footer']", baseSelector);
        AddChildCssRules(buffer, Tbodys, "[data-slot='table-body']", "[data-slot='table-body']", baseSelector);
        AddChildCssRules(buffer, Tds, "[data-slot='table-cell']", "[data-slot='table-cell']", baseSelector);
        AddChildCssRules(buffer, Theads, "[data-slot='table-header']", "[data-slot='table-header']", baseSelector);
        AddChildCssRules(buffer, Ths, "[data-slot='table-head']", "[data-slot='table-head']", baseSelector);
        AddChildCssRules(buffer, Trs, "[data-slot='table-row']", "[data-slot='table-row']", baseSelector);
    }
}
