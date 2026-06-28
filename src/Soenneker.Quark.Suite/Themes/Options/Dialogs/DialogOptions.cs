
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the dialog options.
/// </summary>
public sealed class DialogOptions : ComponentOptions
{
    public DialogOptions()
    {
        Selector = "[data-slot='dialog']";
    }

    /// <summary>
    /// Gets or sets body styling scoped to the dialog content.
    /// </summary>
    public DialogBodyOptions? Bodies { get; set; }

    /// <summary>
    /// Gets or sets close button styling scoped to the dialog content.
    /// </summary>
    public DialogCloseButtonOptions? CloseButtons { get; set; }

    /// <summary>
    /// Gets or sets content styling for the dialog.
    /// </summary>
    public DialogContentOptions? Contents { get; set; }

    /// <summary>
    /// Gets or sets description styling scoped to the dialog content.
    /// </summary>
    public DialogDescriptionOptions? Descriptions { get; set; }

    /// <summary>
    /// Gets or sets footer styling scoped to the dialog content.
    /// </summary>
    public DialogFooterOptions? Footers { get; set; }

    /// <summary>
    /// Gets or sets header styling scoped to the dialog content.
    /// </summary>
    public DialogHeaderOptions? Headers { get; set; }

    /// <summary>
    /// Gets or sets title styling scoped to the dialog content.
    /// </summary>
    public DialogTitleOptions? Titles { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        string dialogScope = baseSelector == "[data-slot='dialog']" ? string.Empty : baseSelector;

        AddChildCssRules(buffer, Contents, "[data-slot='dialog-content']", "[data-slot='dialog-content']", dialogScope);
        AddChildCssRules(buffer, Bodies, "[data-slot='dialog-body']", "[data-slot='dialog-body']", dialogScope);
        AddChildCssRules(buffer, CloseButtons, "[data-slot='dialog-close']", "[data-slot='dialog-close']", dialogScope);
        AddChildCssRules(buffer, Descriptions, "[data-slot='dialog-description']", "[data-slot='dialog-description']", dialogScope);
        AddChildCssRules(buffer, Footers, "[data-slot='dialog-footer']", "[data-slot='dialog-footer']", dialogScope);
        AddChildCssRules(buffer, Headers, "[data-slot='dialog-header']", "[data-slot='dialog-header']", dialogScope);
        AddChildCssRules(buffer, Titles, "[data-slot='dialog-title']", "[data-slot='dialog-title']", dialogScope);
    }
}
