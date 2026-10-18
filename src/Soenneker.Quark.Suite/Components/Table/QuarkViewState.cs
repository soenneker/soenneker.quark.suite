using System.Collections.Generic;
namespace Soenneker.Quark;
/// <summary>Serializable application view state, reusable by tables, boards, and search screens.</summary>
public sealed record QuarkViewState
{
    /// <summary>Schema version. Version zero is accepted as legacy state.</summary>
    public int Version { get; init; } = 1;
    public FilterGroup Filters { get; init; } = new();
    public IReadOnlyList<DataTableColumnState> Columns { get; init; } = [];
    public IReadOnlyList<DataTableSort> Sorts { get; init; } = [];
    public string Search { get; init; } = "";
    public int PageSize { get; init; } = 25;
}
