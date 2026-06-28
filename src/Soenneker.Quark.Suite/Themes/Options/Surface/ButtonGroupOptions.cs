using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the button group options.
/// </summary>
public sealed class ButtonGroupOptions : ComponentOptions
{
    public ButtonGroupOptions()
    {
        Selector = "[data-slot='button-group']";
    }

    /// <summary>
    /// Gets or sets button styling scoped to the button group.
    /// </summary>
    public ButtonOptions? Buttons { get; set; }

    /// <summary>
    /// Gets or sets input styling scoped to the button group.
    /// </summary>
    public InputOptions? Inputs { get; set; }

    /// <summary>
    /// Gets or sets select trigger styling scoped to the button group.
    /// </summary>
    public SelectTriggerOptions? SelectTriggers { get; set; }

    /// <summary>
    /// Gets or sets button group separator styling scoped to the button group.
    /// </summary>
    public ButtonGroupSeparatorOptions? Separators { get; set; }

    /// <summary>
    /// Gets or sets button group text styling scoped to the button group.
    /// </summary>
    public ButtonGroupTextOptions? Texts { get; set; }

    private protected override void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddChildCssRules(buffer, Buttons, "[data-slot='button']", "[data-slot='button']", baseSelector);
        AddChildCssRules(buffer, Inputs, "[data-slot='input']", "[data-slot='input']", baseSelector);
        AddChildCssRules(buffer, SelectTriggers, "[data-slot='select-trigger']", "[data-slot='select-trigger']", baseSelector);
        AddChildCssRules(buffer, Separators, "[data-slot='button-group-separator']", "[data-slot='button-group-separator']", baseSelector);
        AddChildCssRules(buffer, Texts, "[data-slot='button-group-text']", "[data-slot='button-group-text']", baseSelector);
    }
}
