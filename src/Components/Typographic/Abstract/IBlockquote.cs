namespace Soenneker.Quark;

/// <summary>
/// Represents a blockquote element for quotations from another source.
/// </summary>
public interface IBlockquote : IElement
{
    /// <summary>
    /// Gets or sets the citation URL for the source of the quotation.
    /// </summary>
    string? Cite { get; set; }
}

