namespace Soenneker.Quark;

/// <summary>
/// Backwards-compatible facade for older demo code that still references <c>ColumnSize</c>.
/// </summary>
public static class ColumnSize
{
    public static LegacyColumnSize Is1 => ColumnSizeType.Is1;
    public static LegacyColumnSize Is2 => ColumnSizeType.Is2;
    public static LegacyColumnSize Is3 => ColumnSizeType.Is3;
    public static LegacyColumnSize Is4 => ColumnSizeType.Is4;
    public static LegacyColumnSize Is5 => ColumnSizeType.Is5;
    public static LegacyColumnSize Is6 => ColumnSizeType.Is6;
    public static LegacyColumnSize Is7 => ColumnSizeType.Is7;
    public static LegacyColumnSize Is8 => ColumnSizeType.Is8;
    public static LegacyColumnSize Is9 => ColumnSizeType.Is9;
    public static LegacyColumnSize Is10 => ColumnSizeType.Is10;
    public static LegacyColumnSize Is11 => ColumnSizeType.Is11;
    public static LegacyColumnSize Is12 => ColumnSizeType.Is12;
    public static LegacyColumnSize Auto => ColumnSizeType.Auto;
}

/// <summary>
/// Legacy wrapper that preserves older breakpoint suffix syntax like <c>ColumnSize.Is6.OnLg</c>.
/// </summary>
public readonly struct LegacyColumnSize
{
    private readonly ColumnSizeType columnSize;

    public LegacyColumnSize(ColumnSizeType columnSize)
    {
        this.columnSize = columnSize;
    }

    public ColumnSizeType OnBase => columnSize;
    public ColumnSizeType OnSm => columnSize;
    public ColumnSizeType OnMd => columnSize;
    public ColumnSizeType OnLg => columnSize;
    public ColumnSizeType OnXl => columnSize;
    public ColumnSizeType On2xl => columnSize;

    public static implicit operator LegacyColumnSize(ColumnSizeType columnSize) => new(columnSize);
    public static implicit operator ColumnSizeType(LegacyColumnSize columnSize) => columnSize.columnSize;
}
