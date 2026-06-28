namespace Soenneker.Quark;

/// <summary>
/// Represents the table caption options.
/// </summary>
public sealed class TableCaptionOptions : ComponentOptions
{
    public TableCaptionOptions()
    {
        Selector = "[data-slot='table-caption']";
    }
}
