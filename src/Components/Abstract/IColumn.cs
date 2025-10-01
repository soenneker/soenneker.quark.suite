namespace Soenneker.Quark;

/// <summary>
/// Represents a Bootstrap grid column component.
/// </summary>
public interface IColumn : IElement
{
    /// <summary>
    /// Gets or sets the size configuration for the column.
    /// </summary>
    CssValue<ColumnSizeBuilder>? ColumnSize { get; set; }

    /// <summary>
    /// Gets or sets the offset configuration for the column.
    /// </summary>
    CssValue<ColumnOffsetBuilder>? ColumnOffset { get; set; }

    /// <summary>
    /// Gets or sets the order configuration for the column.
    /// </summary>
    CssValue<ColumnOrderBuilder>? ColumnOrder { get; set; }
}