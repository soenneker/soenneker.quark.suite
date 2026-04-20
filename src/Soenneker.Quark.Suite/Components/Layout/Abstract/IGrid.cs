namespace Soenneker.Quark;

/// <summary>
/// Represents a layout container that renders with CSS grid defaults.
/// </summary>
public interface IGrid : IElement
{
    /// <summary>
    /// Gets or sets the grid column classes to apply.
    /// </summary>
    CssValue<GridColsBuilder>? Columns { get; set; }

    /// <summary>
    /// Gets or sets the grid row classes to apply.
    /// </summary>
    CssValue<GridRowsBuilder>? Rows { get; set; }

    /// <summary>
    /// Gets or sets whether the grid should render as inline-grid.
    /// </summary>
    bool Inline { get; set; }
}
