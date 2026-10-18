using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>Configures DataTable records, columns, editing and optional server loading without changing its markup API.</summary>
public sealed class DataTableData<TItem> : DataTableData
{
    /// <summary>Local records. Replace this collection after edits or external updates.</summary>
    public IReadOnlyList<TItem> Items { get; set; } = [];
    /// <summary>Column definitions with unique keys.</summary>
    public required IReadOnlyList<DataTableColumn<TItem>> Columns { get; init; }
    /// <summary>Stable unique identity of a record.</summary>
    public required Func<TItem, string> RowKey { get; init; }
    /// <summary>Optional content beneath an expanded row.</summary>
    public RenderFragment<TItem>? DetailTemplate { get; set; }
    /// <summary>Persists an edit. Throw to keep the draft open. Update Items after success.</summary>
    public Func<DataTableEditContext<TItem>, CancellationToken, ValueTask>? SaveEdit { get; set; }
    /// <summary>Optional server provider owning search, filters and ordering. Virtual mode appends pages as the user scrolls.</summary>
    public Func<DataTableRequest, CancellationToken, ValueTask<DataTableResult<TItem>>>? LoadData { get; set; }
    /// <summary>Accessible table name.</summary>
    public string Label { get; set; } = "Data";
    internal override int ColumnIndex(string key) { for (var i = 0; i < Columns.Count; i++) if (Columns[i].Key == key) return i; return -1; }
    internal override string? ColumnKey(int index) => index >= 0 && index < Columns.Count ? Columns[index].Key : null;
    internal override RenderFragment Render(DataTable owner) => builder =>
    {
        builder.OpenComponent<Internal.DataTableItems<TItem>>(0);
        builder.AddAttribute(1, "Items", Items);
        builder.AddAttribute(2, "Columns", Columns);
        builder.AddAttribute(3, "RowKey", RowKey);
        builder.AddAttribute(4, "DetailTemplate", DetailTemplate);
        builder.AddAttribute(5, "SaveEdit", SaveEdit);
        builder.AddAttribute(6, "LoadData", LoadData);
        builder.AddAttribute(7, "Label", Label);
        builder.AddAttribute(8, "State", owner.State);
        builder.AddAttribute(9, "StateChanged", EventCallback.Factory.Create<QuarkViewState>(owner, owner.AcceptViewState));
        builder.AddAttribute(10, "Virtualize", owner.Virtualize);
        builder.AddAttribute(11, "ViewportHeight", (object?)owner.ViewportHeight);
        builder.AddAttribute(12, "SelectionEnabled", owner.SelectionEnabled);
        builder.AddAttribute(13, "SelectedKeys", owner.SelectedKeys);
        builder.AddAttribute(14, "SelectedKeysChanged", EventCallback.Factory.Create<IReadOnlyCollection<string>>(owner, owner.AcceptDataSelection));
        builder.AddAttribute(15, "ToolbarContent", owner.TopContent);
        builder.AddAttribute(16, "SearchDebounceMilliseconds", owner.SearchDebounceMilliseconds);
        builder.AddAttribute(17, "RootAttributes", owner.DataAttributes());
        builder.AddAttribute(18, "Owner", owner);
        builder.CloseComponent();
    };
}
