using System;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>A typed column with a stable key. Accessors avoid reflection and work in AOT applications.</summary>
public sealed class DataTableColumn<TItem>
{
    public required string Key { get; init; }
    public required string Title { get; init; }
    public required Func<TItem, object?> Value { get; init; }
    public RenderFragment<TItem>? Template { get; init; }
    public RenderFragment<DataTableEditContext<TItem>>? EditTemplate { get; init; }
    public bool Sortable { get; init; } = true;
    public bool Editable { get; init; }
    public int Width { get; init; } = 180;
    public FilterFieldType FilterType { get; init; }
    public System.Collections.Generic.IReadOnlyList<SelectOption>? Choices { get; init; }
}
