
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for paragraph component styling.
/// </summary>
public sealed class ParagraphOptions : ComponentOptions
{
    public ParagraphOptions()
    {
        Selector = "[data-slot='paragraph']";
    }
}
