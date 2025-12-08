
namespace Soenneker.Quark;

/// <summary>
/// Configuration options for paragraph component styling.
/// </summary>
public sealed class ParagraphOptions : ComponentOptions
{
    /// <summary>
    /// Initializes a new instance of the ParagraphOptions class.
    /// </summary>
    public ParagraphOptions()
    {
        Selector = ".lead, p";
    }
}
