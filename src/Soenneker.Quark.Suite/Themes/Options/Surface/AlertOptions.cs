
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the alert options.
/// </summary>
public sealed class AlertOptions : ComponentOptions
{
    public AlertOptions()
    {
        Selector = "[data-slot='alert']";
    }

    /// <summary>
    /// Gets or sets alert action styling scoped to the alert.
    /// </summary>
    public AlertActionOptions? Actions { get; set; }

    /// <summary>
    /// Gets or sets alert description styling scoped to the alert.
    /// </summary>
    public AlertDescriptionOptions? Descriptions { get; set; }

    /// <summary>
    /// Gets or sets icon styling scoped to the alert.
    /// </summary>
    public IconOptions? Icons { get; set; }

    /// <summary>
    /// Gets or sets alert title styling scoped to the alert.
    /// </summary>
    public AlertTitleOptions? Titles { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Actions, "[data-slot='alert-action']", "[data-slot='alert-action']", baseSelector);
        AddChildCssRules(buffer, Descriptions, "[data-slot='alert-description']", "[data-slot='alert-description']", baseSelector);
        AddChildCssRules(buffer, Icons, "[data-slot='icon']", "[data-slot='icon']", baseSelector);
        AddChildCssRules(buffer, Titles, "[data-slot='alert-title']", "[data-slot='alert-title']", baseSelector);
    }
}
