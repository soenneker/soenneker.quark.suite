namespace Soenneker.Quark;

/// <summary>
/// Represents the pagination content options.
/// </summary>
public sealed class PaginationContentOptions : ComponentOptions
{
    public PaginationContentOptions()
    {
        Selector = "[data-slot='pagination-content']";
    }
}
