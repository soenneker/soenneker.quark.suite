namespace Soenneker.Quark;
/// <summary>A draft cell value. Save callbacks validate and persist it before the editor closes; cancel leaves the item unchanged.</summary>
public sealed class DataTableEditContext<TItem>
{
    public required TItem Item { get; init; }
    public required string ColumnKey { get; init; }
    public string? Value { get; set; }
}
