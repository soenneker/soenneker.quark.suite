
namespace Soenneker.Quark;

/// <summary>
/// Represents the pagination item options.
/// </summary>
public sealed class PaginationItemOptions : ComponentOptions
{
    public PaginationItemOptions()
    {
        Selector = "[data-slot='pagination-item']";
    }
}
