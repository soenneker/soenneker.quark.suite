namespace Soenneker.Quark;

/// <summary>
/// Represents a grid row component (Tailwind CSS grid).
/// </summary>
public interface IRow : IElement
{
    /// <summary>
    /// Gets or sets the gutter configuration for the row (gap utilities).
    /// </summary>
    CssValue<GutterBuilder>? Gutter { get; set; }

    /// <summary>
    /// Number of columns in the grid (default 12). Renders as grid-cols-1 through grid-cols-12.
    /// </summary>
    int Columns { get; set; }
}