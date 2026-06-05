
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for blockquote component styling.
/// </summary>
public sealed class BlockquoteOptions : ComponentOptions
{
    public BlockquoteOptions()
    {
        Selector = "[data-slot='blockquote']";
    }
}
