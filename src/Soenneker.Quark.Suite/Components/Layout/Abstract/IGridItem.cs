namespace Soenneker.Quark;

/// <summary>
/// Represents a grid child element that can span columns and rows.
/// </summary>
public interface IGridItem : IElement
{
    /// <summary>
    /// Gets or sets the column span classes to apply.
    /// </summary>
    CssValue<ColumnSpanBuilder>? ColumnSpan { get; set; }

}
