namespace Soenneker.Quark;

/// <summary>
/// Represents a layout container that renders with CSS grid defaults.
/// </summary>
public interface IGrid : IElement
{
    /// <summary>Gets or sets the GridColumns utilities, including responsive and state variants.</summary>
    CssValue<GridColsBuilder>? GridColumns { get; set; }

    /// <summary>Gets or sets the GridRows utilities, including responsive and state variants.</summary>
    CssValue<GridRowsBuilder>? GridRows { get; set; }

    /// <summary>Gets or sets the AutoCols utilities, including responsive and state variants.</summary>
    CssValue<AutoColsBuilder>? AutoCols { get; set; }

    /// <summary>Gets or sets the AutoRows utilities, including responsive and state variants.</summary>
    CssValue<AutoRowsBuilder>? AutoRows { get; set; }

    /// <summary>Gets or sets the GridAutoFlow utilities, including responsive and state variants.</summary>
    CssValue<GridAutoFlowBuilder>? GridAutoFlow { get; set; }

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
