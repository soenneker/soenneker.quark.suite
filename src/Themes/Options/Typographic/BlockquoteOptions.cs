
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for blockquote component styling.
/// </summary>
public sealed class BlockquoteOptions : ComponentOptions
{
    /// <summary>
    /// Initializes a new instance of the BlockquoteOptions class.
    /// </summary>
    public BlockquoteOptions()
    {
        Selector = ".blockquote, blockquote";
    }
}

