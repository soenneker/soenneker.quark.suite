using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents an accordion component for organizing content into collapsible panels.
/// </summary>
public interface IAccordion : IElement
{
    /// <summary>
    /// Gets or sets whether multiple accordion items can be expanded at the same time.
    /// </summary>
    bool AllowMultiple { get; set; }

    /// <summary>
    /// Gets or sets the currently expanded accordion item names.
    /// </summary>
    string[] ExpandedItems { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked When the expanded items change.
    /// </summary>
    EventCallback<string[]> OnExpandedItemsChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the accordion should use flush styling (no borders/background).
    /// </summary>
    bool Flush { get; set; }
}
