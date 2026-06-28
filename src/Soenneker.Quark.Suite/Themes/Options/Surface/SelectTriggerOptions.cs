namespace Soenneker.Quark;

/// <summary>
/// Represents the select trigger options.
/// </summary>
public sealed class SelectTriggerOptions : ComponentOptions
{
    public SelectTriggerOptions()
    {
        Selector = "[data-slot='select-trigger']";
    }
}
