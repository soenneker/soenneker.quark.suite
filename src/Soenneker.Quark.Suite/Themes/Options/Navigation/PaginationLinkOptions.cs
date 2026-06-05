
namespace Soenneker.Quark;

/// <summary>
/// Represents the pagination link options.
/// </summary>
public sealed class PaginationLinkOptions : ComponentOptions
{
    public PaginationLinkOptions()
    {
        Selector = "[data-slot='pagination-link']";
    }
}
