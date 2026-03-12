namespace Soenneker.Quark;

public static class Grid
{
    public static GridBuilder Display => new("display", "grid");
    public static GridBuilder Inline => new("display", "inline-grid");

    public static GridBuilder Cols(int value) => new("grid-cols", value.ToString());
    public static GridBuilder Rows(int value) => new("grid-rows", value.ToString());

    public static GridBuilder ColSpan(int value) => new("col-span", value.ToString());
    public static GridBuilder RowSpan(int value) => new("row-span", value.ToString());
    public static GridBuilder ColStart(int value) => new("col-start", value.ToString());
    public static GridBuilder ColEnd(int value) => new("col-end", value.ToString());
    public static GridBuilder RowStart(int value) => new("row-start", value.ToString());
    public static GridBuilder RowEnd(int value) => new("row-end", value.ToString());

    public static GridBuilder AutoCols(string value) => new("auto-cols", value);
    public static GridBuilder AutoRows(string value) => new("auto-rows", value);

    public static GridBuilder PlaceItems(string value) => new("place-items", value);
    public static GridBuilder PlaceContent(string value) => new("place-content", value);
    public static GridBuilder PlaceSelf(string value) => new("place-self", value);
}
