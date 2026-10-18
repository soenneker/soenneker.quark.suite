using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>Typed data configuration consumed by DataTable. Use DataTableData&lt;TItem&gt; to configure records and columns.</summary>
public abstract class DataTableData
{
    internal abstract int ColumnIndex(string key);
    internal abstract string? ColumnKey(int index);
    internal abstract RenderFragment Render(DataTable owner);
}
