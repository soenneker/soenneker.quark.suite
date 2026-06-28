namespace Soenneker.Quark;

/// <summary>
/// Represents the pagination ellipsis options.
/// </summary>
public sealed class PaginationEllipsisOptions : ComponentOptions
{
    public PaginationEllipsisOptions()
    {
        Selector = "[data-slot='pagination-ellipsis']";
    }
}
