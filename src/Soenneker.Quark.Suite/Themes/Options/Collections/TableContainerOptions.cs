namespace Soenneker.Quark;

/// <summary>
/// Represents the table container options.
/// </summary>
public sealed class TableContainerOptions : ComponentOptions
{
    public TableContainerOptions()
    {
        Selector = "[data-slot='table-container']";
    }
}
