namespace Soenneker.Quark;

/// <summary>
/// Configuration options for fieldset component styling.
/// </summary>
public sealed class FieldsetOptions : ComponentOptions
{
    public FieldsetOptions()
    {
        Selector = "[data-slot='field-set']";
    }
}
