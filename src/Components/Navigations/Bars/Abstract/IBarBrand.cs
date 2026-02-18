namespace Soenneker.Quark;

/// <summary>
/// Represents a brand/logo component within a navigation bar.
/// </summary>
public interface IBarBrand : IElement
{
    /// <summary>
    /// Gets or sets the URL to navigate to When the brand is clicked.
    /// </summary>
    string? To { get; set; }
}

