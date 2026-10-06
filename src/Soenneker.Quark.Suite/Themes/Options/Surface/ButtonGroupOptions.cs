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

    private protected override void CollectChildCssRules(ref ComponentCssRuleCollector buffer, string baseSelector)
    {
        AddChildCssRules(ref buffer, Buttons, "[data-slot='button']", "[data-slot='button']", baseSelector);
        AddChildCssRules(ref buffer, Inputs, "[data-slot='input']", "[data-slot='input']", baseSelector);
        AddChildCssRules(ref buffer, SelectTriggers, "[data-slot='select-trigger']", "[data-slot='select-trigger']", baseSelector);
        AddChildCssRules(ref buffer, Separators, "[data-slot='button-group-separator']", "[data-slot='button-group-separator']", baseSelector);
        AddChildCssRules(ref buffer, Texts, "[data-slot='button-group-text']", "[data-slot='button-group-text']", baseSelector);
    }
}
