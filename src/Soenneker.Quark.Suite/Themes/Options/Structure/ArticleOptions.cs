
namespace Soenneker.Quark;

/// <summary>
/// Represents the article options.
/// </summary>
public sealed class ArticleOptions : ComponentOptions
{
    public ArticleOptions()
    {
        Selector = "[data-slot='article']";
    }
}
