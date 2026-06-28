namespace Soenneker.Quark;

/// <summary>
/// Represents the table footer options.
/// </summary>
public sealed class TableFooterOptions : ComponentOptions
{
    public TableFooterOptions()
    {
        Selector = "[data-slot='table-footer']";
    }
}
