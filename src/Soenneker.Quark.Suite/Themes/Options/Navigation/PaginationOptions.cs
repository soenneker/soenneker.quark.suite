
namespace Soenneker.Quark;

/// <summary>
/// Represents the pagination options.
/// </summary>
public sealed class PaginationOptions : ComponentOptions
{
    public PaginationOptions()
    {
        Selector = "[data-slot='pagination']";
    }
}
