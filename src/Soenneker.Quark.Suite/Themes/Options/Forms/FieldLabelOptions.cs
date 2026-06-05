namespace Soenneker.Quark;

/// <summary>
/// Configuration options for field label component styling.
/// </summary>
public sealed class FieldLabelOptions : ComponentOptions
{
    public FieldLabelOptions()
    {
        Selector = "[data-slot='field-label']";
    }
}
