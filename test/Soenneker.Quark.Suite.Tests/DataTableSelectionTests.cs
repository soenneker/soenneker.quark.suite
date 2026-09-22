using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DataTable_live_refresh_does_not_bypass_retained_row_boundary()
    {
        var cut = Render<DataTable>(p => p.Add(c => c.TableContent, builder =>
        {
            builder.OpenComponent<RetainedTableRow>(0);
            builder.CloseComponent();
        }));
        var row = cut.FindComponent<Tr>();
        int renders = row.RenderCount;

        cut.Render(p => p.Add(c => c.TotalRecords, 100));
        cut.Render(p => p.Add(c => c.IsLoading, true));
        cut.Render(p => p.Add(c => c.IsLoading, false));

        row.RenderCount.Should().Be(renders);
        cut.Find("tr").TextContent.Should().Be("Retained job");
    }

    private sealed class RetainedTableRow : ComponentBase
    {
        protected override bool ShouldRender() => false;
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<Tr>(0);
            builder.AddAttribute(1, nameof(Tr.ChildContent), (RenderFragment)(cell =>
            {
                cell.OpenComponent<Td>(0);
                cell.AddAttribute(1, nameof(Td.ChildContent), (RenderFragment)(text => text.AddContent(0, "Retained job")));
                cell.CloseComponent();
            }));
            builder.CloseComponent();
        }
    }

    [Test]
    public async Task DataTable_selection_updates_checkboxes_toolbar_and_preserves_other_pages()
    {
        IReadOnlyCollection<string> original = new[] { "other-page" };
        IReadOnlyCollection<string>? changed = null;
        var cut = Render<DataTable>(p => p
            .Add(c => c.SelectionEnabled, true)
            .Add(c => c.SelectableRowKeys, new[] { "a", "b" })
            .Add(c => c.SelectedKeys, original)
            .Add(c => c.SelectedKeysChanged, value => changed = value)
            .Add(c => c.TableContent, SelectionTableContent));

        await cut.Find("[aria-label='Select a']").ClickAsync();
        cut.Find("[aria-label='Select all rows on this page']").GetAttribute("aria-checked").Should().Be("mixed");
        cut.Find("tr[aria-selected='true']").TextContent.Should().Contain("a");
        changed.Should().BeEquivalentTo(new[] { "other-page", "a" });
        original.Should().BeEquivalentTo(new[] { "other-page" });
        cut.Find("[data-slot='datatable-selection-toolbar']").TextContent.Should().Contain("2 selected");

        await cut.Find("[aria-label='Select all rows on this page']").ClickAsync();
        changed.Should().BeEquivalentTo(new[] { "other-page", "a", "b" });
        cut.Find("[aria-label='Select all rows on this page']").GetAttribute("aria-checked").Should().Be("true");
        await cut.Find("[aria-label='Select all rows on this page']").ClickAsync();
        changed.Should().BeEquivalentTo(new[] { "other-page" });
        cut.Find("[aria-label='Select all rows on this page']").GetAttribute("aria-checked").Should().Be("false");

        // The synthetic selection header must not shift server-side data-column indices.
        await cut.InvokeAsync(() => cut.Instance.HandleColumnSort(0).AsTask());
        cut.Instance.SortBy.Should().Be("name");
        await cut.Find("[data-slot='datatable-selection-toolbar'] button").ClickAsync();
        changed.Should().BeEmpty();
        cut.FindAll("[data-slot='datatable-selection-toolbar']").Should().BeEmpty();
    }

    [Test]
    public async Task DataTable_selection_is_opt_in_and_reacts_to_page_and_loading_changes()
    {
        var cut = Render<DataTable>(p => p.Add(c => c.TableContent, SelectionTableContent));
        cut.FindAll("[role='checkbox']").Should().BeEmpty();
        cut.Render(p => p.Add(c => c.SelectionEnabled, true));
        cut.Find("[aria-label='Select all rows on this page']").HasAttribute("disabled").Should().BeTrue();
        cut.Render(p => p.Add(c => c.SelectableRowKeys, new[] { "a" }).Add(c => c.SelectedKeys, new[] { "a" }));
        cut.Find("[aria-label='Select b']").HasAttribute("disabled").Should().BeTrue();
        cut.Find("[aria-label='Select all rows on this page']").GetAttribute("aria-checked").Should().Be("true");

        cut.Render(p => p.Add(c => c.SelectableRowKeys, new[] { "b" }));
        cut.Find("[aria-label='Select all rows on this page']").GetAttribute("aria-checked").Should().Be("false");
        cut.Find("[aria-label='Select b']").HasAttribute("disabled").Should().BeFalse();
        cut.Render(p => p.Add(c => c.IsLoading, true));
        await cut.InvokeAsync(() => cut.Instance.SelectAllRows(true));
        cut.Instance.SelectedKeys.Should().BeEquivalentTo(new[] { "a" });
        cut.Find("[aria-label='Select b']").HasAttribute("disabled").Should().BeTrue();
        cut.Render(p => p.Add(c => c.SelectionEnabled, false));
        cut.FindAll("[role='checkbox']").Should().BeEmpty();
        cut.FindAll("tr[aria-selected]").Should().BeEmpty();
    }

    private static readonly RenderFragment SelectionTableContent = builder =>
    {
        builder.OpenElement(0, "thead");
        builder.OpenComponent<Tr>(1);
        builder.AddAttribute(2, nameof(Tr.SelectionHeader), true);
        builder.AddAttribute(3, nameof(Tr.ChildContent), (RenderFragment)(header =>
        {
            header.OpenComponent<Th>(0);
            header.AddAttribute(1, nameof(Th.Sortable), true);
            header.AddAttribute(2, nameof(Th.Data), "name");
            header.AddAttribute(3, nameof(Th.ChildContent), (RenderFragment)(text => text.AddContent(0, "Name")));
            header.CloseComponent();
        }));
        builder.CloseComponent();
        builder.CloseElement();
        builder.OpenElement(4, "tbody");
        foreach (var key in new[] { "a", "b" })
        {
            builder.OpenComponent<Tr>(5);
            builder.SetKey(key);
            builder.AddAttribute(6, nameof(Tr.SelectionKey), key);
            builder.AddAttribute(7, nameof(Tr.SelectionLabel), $"Select {key}");
            builder.AddAttribute(8, nameof(Tr.ChildContent), (RenderFragment)(cell =>
            {
                cell.OpenComponent<Td>(0);
                cell.AddAttribute(1, nameof(Td.ChildContent), (RenderFragment)(text => text.AddContent(0, key)));
                cell.CloseComponent();
            }));
            builder.CloseComponent();
        }
        builder.CloseElement();
    };
}
