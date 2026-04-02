namespace Soenneker.Quark;

/// <summary>
/// Simplified display utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class Display
{
    /// <summary>
    /// Display none (hidden).
    /// </summary>
    public static DisplayBuilder None => new(DisplayKeyword.NoneValue);

    /// <summary>
    /// Display inline.
    /// </summary>
    public static DisplayBuilder Inline => new(DisplayKeyword.InlineValue);

    /// <summary>
    /// Display inline-block.
    /// </summary>
    public static DisplayBuilder InlineBlock => new(DisplayKeyword.InlineBlockValue);

    /// <summary>
    /// Display block.
    /// </summary>
    public static DisplayBuilder Block => new(DisplayKeyword.BlockValue);

    /// <summary>
    /// Display flex.
    /// </summary>
    public static DisplayBuilder Flex => new(DisplayKeyword.FlexValue);

    /// <summary>
    /// Display inline-flex.
    /// </summary>
    public static DisplayBuilder InlineFlex => new(DisplayKeyword.InlineFlexValue);

    /// <summary>
    /// Display grid.
    /// </summary>
    public static DisplayBuilder Grid => new(DisplayKeyword.GridValue);

    /// <summary>
    /// Display inline-grid.
    /// </summary>
    public static DisplayBuilder InlineGrid => new(DisplayKeyword.InlineGridValue);

    /// <summary>
    /// Display table.
    /// </summary>
    public static DisplayBuilder Table => new(DisplayKeyword.TableValue);

    /// <summary>
    /// Display table-cell.
    /// </summary>
    public static DisplayBuilder TableCell => new(DisplayKeyword.TableCellValue);

    /// <summary>
    /// Display table-row.
    /// </summary>
    public static DisplayBuilder TableRow => new(DisplayKeyword.TableRowValue);
}
