namespace Soenneker.Quark;

/// <summary>
/// Represents a pagination component for navigating through multiple pages.
/// </summary>
public interface IPagination : IElement
{

    /// <summary>
    /// Gets or sets whether the pagination should be displayed vertically.
    /// </summary>
    bool Vertical { get; set; }
}
