using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
namespace Soenneker.Quark.Suite.Tests;
public sealed class WorkflowTests : BunitContext
{
    private readonly QuarkLocalizer _localizer = new();
    public WorkflowTests() { Services.AddSingleton<ICollapseCoordinator, CollapseCoordinator>();
        Services.AddSingleton<IVirtualViewportInterop, WorkflowViewportTestInterop>();
        Services.AddSingleton<ITablesInterop, WorkflowTablesTestInterop>();
        Services.AddSingleton<IWorkflowInteractionInterop, WorkflowInteractionTestInterop>();
        Services.AddLogging();
        Services.AddSingleton(new QuarkOptions());
        Services.AddSingleton<IQuarkLocalizer>(_localizer); Services.AddSingleton<IInputInterop, InputSelectionTestInterop>(); }

    [Test]
    public void Nested_filters_use_typed_values_and_roundtrip_without_reflection()
    {
        FilterField[] fields = [new("score", "Score", FilterFieldType.Number), new("name", "Name")];
        var state = new QuarkViewState { Filters = new() {
            Rules = [new() { Field = "score", Operator = FilterOperator.GreaterThan, Value = "9" }],
            Groups = [new() { Junction = FilterJunction.Any, Rules = [new() { Field = "name", Operator = FilterOperator.Contains, Value = "alex" }, new() { Field = "name", Value = "Sam" }] }]
        }, Columns = [new("name", Width: 240, Pin: DataTableColumnPin.Start)] };
        var restored = JsonSerializer.Deserialize(JsonSerializer.Serialize(state, QuarkViewJsonContext.Default.QuarkViewState), QuarkViewJsonContext.Default.QuarkViewState)!;
        FilterEvaluator.Matches(restored.Filters, fields, key => key == "score" ? 10 : "Alexander").Should().BeTrue();
        FilterEvaluator.Matches(restored.Filters, fields, key => key == "score" ? 2 : "Alexander").Should().BeFalse();
        FilterEvaluator.Matches(restored.Filters, fields, key => key == "score" ? 10 : "Jordan").Should().BeFalse();
        restored.Columns[0].Pin.Should().Be(DataTableColumnPin.Start);
        FilterEvaluator.IsValid(new() { Field = "score", Value = "invalid" }, fields[0]).Should().BeFalse();
    }

    [Test]
    public async Task Localization_falls_back_to_parent_and_refreshes_mounted_components()
    {
        var cut = Render<QueryBuilder>(p => p.Add(c => c.Fields, new[] { new FilterField("name", "Name") }));
        _localizer.SetTranslations("es", new Dictionary<string, string> { ["AddRule"] = "Añadir filtro" });
        await cut.InvokeAsync(() => _localizer.SetCulture(CultureInfo.GetCultureInfo("es-MX")));
        cut.Markup.Should().Contain("Añadir filtro");
        _localizer["AddGroup"].Should().Be("Add group");
        await cut.FindAll("button").First(x => x.TextContent == "Añadir filtro").ClickAsync();
        cut.FindAll("[data-slot='filter-rule']").Count.Should().Be(1);
    }

    [Test]
    public async Task DataTable_sorts_numbers_preserves_selection_and_applies_column_preferences()
    {
        IReadOnlyList<DataTableColumn<SelectOption>> columns = [new() { Key = "value", Title = "Number", Value = x => int.Parse(x.Value), FilterType = FilterFieldType.Number }, new() { Key = "label", Title = "Label", Value = x => x.Label }];
        var cut = Render<DataTable>(p => p.Add(c => c.Data, new DataTableData<SelectOption> { Items = [new("10", "Ten"), new("2", "Two")], Columns = columns, RowKey = x => x.Value })
            .Add(c => c.SelectionEnabled, true).Add(c => c.SelectedKeys, new[] { "elsewhere" })
            .Add(c => c.State, new QuarkViewState { Columns = [new("value", Width: 240, Pin: DataTableColumnPin.Start), new("label", false)] }));
        await cut.Find("th[scope='col'] button").ClickAsync();
        cut.FindAll("tbody tr")[0].TextContent.Should().Contain("2");
        cut.FindAll("th[scope='col']").Count.Should().Be(1);
        cut.Find("th[scope='col']").GetAttribute("style").Should().Contain("position:sticky");
        await cut.Find("input[aria-label='Select all rows']").ChangeAsync(new ChangeEventArgs { Value = true });
        cut.Instance.SelectedKeys.Should().BeEquivalentTo(new[] { "elsewhere", "2", "10" });
    }

    [Test]
    public async Task Failed_cell_save_keeps_draft_open_and_original_item_unchanged()
    {
        var item = new SelectOption("1", "Original");
        var cut = Render<DataTable>(p => p.Add(c => c.Data, new DataTableData<SelectOption> { Items = [item], RowKey = x => x.Value,
            Columns = [new() { Key = "label", Title = "Label", Value = x => x.Label, Editable = true }],
            SaveEdit = (_, _) => throw new InvalidOperationException("offline") }));
        await cut.Find("button[aria-label='Edit Label']").ClickAsync();
        await cut.Find("input[aria-label='Label']").ChangeAsync(new ChangeEventArgs { Value = "Draft" });
        await cut.FindAll("button").First(x => x.TextContent == "Save").ClickAsync();
        cut.Find("[role='alert']").TextContent.Should().Contain("Could not save");
        item.Label.Should().Be("Original");
        cut.Find("input[aria-label='Label']").GetAttribute("value").Should().Be("Draft");
    }

    [Test]
    public async Task Saved_view_is_only_added_after_persistence_succeeds()
    {
        var store = new WorkflowViewStore { Fail = true }; Services.AddSingleton<IQuarkViewStore>(store);
        var cut = Render<SavedViews>(p => p.Add(c => c.StorageKey, "test").Add(c => c.State, new QuarkViewState { Search = "Alex" }));
        await cut.Find("input").InputAsync(new ChangeEventArgs { Value = "My view" });
        await cut.FindAll("button").First(x => x.TextContent == "Save view").ClickAsync();
        cut.Instance.Views.Should().BeEmpty();
        store.Fail = false;
        await cut.FindAll("button").First(x => x.TextContent == "Save view").ClickAsync();
        store.Views.Should().ContainSingle(); store.Views[0].State.Search.Should().Be("Alex");
        await cut.Find("select").ChangeAsync(new ChangeEventArgs { Value = store.Views[0].Id });
        cut.Instance.State.Should().NotBeSameAs(store.Views[0].State);
    }

    [Test]
    public void Scheduler_rejects_DST_gaps_and_uses_earlier_overlap_instant()
    {
        var zone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
        SchedulerDates.ToInstant(new(2026, 3, 8, 2, 30, 0), zone).Should().BeNull();
        var overlap = SchedulerDates.ToInstant(new(2026, 11, 1, 1, 30, 0), zone);
        overlap!.Value.Offset.Should().Be(TimeSpan.FromHours(-5));
        SchedulerDates.WeekStart(new(2026, 10, 18), DayOfWeek.Monday).Should().Be(new DateOnly(2026, 10, 12));
    }

    [Test]
    public async Task Scheduler_failed_save_retains_existing_event()
    {
        var appointment = new SchedulerEvent("a", "Original", new(2026, 10, 18, 9, 0, 0, TimeSpan.Zero), new(2026, 10, 18, 10, 0, 0, TimeSpan.Zero));
        var cut = Render<Scheduler>(p => p.Add(c => c.Date, new DateOnly(2026, 10, 18)).Add(c => c.View, SchedulerView.Agenda)
            .Add(c => c.Events, new[] { appointment }).Add(c => c.Save, (_, _) => throw new InvalidOperationException("offline")));
        await cut.FindAll("button").First(x => x.TextContent.Contains("Original")).ClickAsync();
        await cut.Find("input:not([type])").ChangeAsync(new ChangeEventArgs { Value = "Changed" });
        await cut.FindAll("button").First(x => x.TextContent == "Save").ClickAsync();
        cut.Find("[role='alert']").TextContent.Should().Contain("Could not save");
        cut.Instance.Events[0].Title.Should().Be("Original");
    }

    [Test]
    public async Task Notification_read_failure_preserves_unread_state_and_can_retry()
    {
        var fail = true;
        var cut = Render<NotificationCenter>(p => p.Add(c => c.Items, new[] { new QuarkNotification("a", "Update", null, DateTimeOffset.UtcNow) })
            .Add(c => c.UnreadCount, 5).Add(c => c.MarkRead, (_, _) => fail ? ValueTask.FromException(new InvalidOperationException("offline")) : ValueTask.CompletedTask));
        await cut.FindAll("button").First(x => x.TextContent == "Mark read").ClickAsync();
        cut.Instance.Items[0].IsRead.Should().BeFalse(); cut.Instance.UnreadCount.Should().Be(5);
        fail = false;
        await cut.FindAll("button").First(x => x.TextContent == "Mark read").ClickAsync();
        cut.Instance.Items[0].IsRead.Should().BeTrue(); cut.Instance.UnreadCount.Should().Be(4);
    }

    [Test]
    public void Rich_text_URLs_reject_active_content_and_protocol_relative_links()
    {
        foreach (var url in new[] { "javascript:alert(1)", "data:text/html,hello", "//evil.example", "java\nscript:alert(1)", "\\evil.example" }) RichTextUrl.IsSafe(url).Should().BeFalse();
        foreach (var url in new[] { "https://example.com/a", "mailto:team@example.com", "/attachments/1", "#section" }) RichTextUrl.IsSafe(url).Should().BeTrue();
    }

    [Test]
    public void Scheduler_layout_only_shares_columns_when_events_overlap()
    {
        var start = new DateTimeOffset(2026, 10, 18, 9, 0, 0, TimeSpan.Zero);
        var layout = SchedulerLayout.Arrange([
            new("a", "A", start, start.AddHours(1)), new("b", "B", start.AddMinutes(30), start.AddHours(2)),
            new("c", "C", start.AddHours(3), start.AddHours(4))], new(2026, 10, 18), TimeZoneInfo.Utc);
        layout.Single(p => p.Event.Id == "a").ColumnCount.Should().Be(2);
        layout.Single(p => p.Event.Id == "b").Column.Should().Be(1);
        layout.Single(p => p.Event.Id == "c").ColumnCount.Should().Be(1);
    }

    [Test]
    public async Task Removing_nested_filter_chip_prunes_empty_groups_without_mutating_original()
    {
        var rule = new FilterRule { Field = "name", Value = "Alex" };
        var original = new FilterGroup { Groups = [new() { Rules = [rule] }] };
        var cut = Render<ActiveFilters>(p => p.Add(c => c.Value, original).Add(c => c.Fields, new[] { new FilterField("name", "Name") }));
        await cut.Find("button[aria-label]").ClickAsync();
        cut.Instance.Value.Groups.Should().BeEmpty();
        original.Groups[0].Rules.Should().ContainSingle();
    }

    [Test]
    public async Task Server_table_passes_query_state_and_rejects_late_pages()
    {
        var first = new TaskCompletionSource<DataTableResult<SelectOption>>();
        var second = new TaskCompletionSource<DataTableResult<SelectOption>>();
        var cut = Render<DataTable>(p => p.Add(c => c.Data, new DataTableData<SelectOption> { RowKey = x => x.Value,
            Columns = [new() { Key = "name", Title = "Name", Value = x => x.Label }],
            LoadData = (request, _) => request.State.Search.Length == 0
                ? ValueTask.FromResult(new DataTableResult<SelectOption>([], 0))
                : new ValueTask<DataTableResult<SelectOption>>(request.State.Search == "a" ? first.Task : second.Task) }));
        var a = cut.Find("input[aria-label='Search']").InputAsync(new ChangeEventArgs { Value = "a" });
        var b = cut.Find("input[aria-label='Search']").InputAsync(new ChangeEventArgs { Value = "ab" });
        second.SetResult(new([new("new", "Newest")], 1)); await b;
        first.SetResult(new([new("old", "Stale")], 1)); await a;
        cut.Find("tbody").TextContent.Should().Contain("Newest").And.NotContain("Stale");
        cut.Instance.State.Search.Should().Be("ab");
    }

    [Test]
    public void Localized_validator_uses_current_resources_on_each_validation()
    {
        var validator = new LocalizedValidator(new RequiredValidator(), _localizer, "Required");
        validator.Validate("").ErrorText.Should().Be("This field is required.");
        _localizer.SetTranslations("es", new Dictionary<string, string> { ["Required"] = "Campo obligatorio" });
        _localizer.SetCulture(CultureInfo.GetCultureInfo("es"));
        validator.Validate("").ErrorText.Should().Be("Campo obligatorio");
        validator.Validate("value").IsValid.Should().BeTrue();
    }

    [Test]
    public void Scheduler_edit_preserves_later_DST_occurrence_when_only_title_changes()
    {
        var zone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
        var original = new DateTimeOffset(2026, 11, 1, 1, 30, 0, TimeSpan.FromHours(-6));
        SchedulerDates.ResolveEdit(new(2026, 11, 1, 1, 30, 0), zone, original).Should().Be(original);
    }

    [Test]
    [Arguments(4)]
    [Arguments(3)]
    public async Task Notifications_reconcile_late_pages_with_local_reads_and_update_duplicates(int returnedCount)
    {
        var pending = new TaskCompletionSource<NotificationPage>();
        var item = new QuarkNotification("a", "Original", null, DateTimeOffset.UtcNow);
        var cut = Render<NotificationCenter>(p => p.Add(c => c.Items, new[] { item }).Add(c => c.UnreadCount, 4)
            .Add(c => c.Load, (_, _) => new ValueTask<NotificationPage>(pending.Task)));
        await cut.FindAll("button").First(x => x.TextContent == "Mark read").ClickAsync();
        pending.SetResult(new([item with { Title = "Updated" }], "next", returnedCount));
        cut.WaitForAssertion(() => cut.Instance.Items[0].Title.Should().Be("Updated"));
        cut.Instance.Items[0].IsRead.Should().BeTrue();
        cut.Instance.UnreadCount.Should().Be(3);
    }

    [Test]
    public async Task Notification_refresh_supersedes_a_pending_page()
    {
        var old = new TaskCompletionSource<NotificationPage>(); var calls = 0;
        var cut = Render<NotificationCenter>(p => p.Add(c => c.Load, (_, _) => ++calls == 1 ? new ValueTask<NotificationPage>(old.Task)
            : ValueTask.FromResult(new NotificationPage([new("new", "Fresh", null, DateTimeOffset.UtcNow)], null, 1))));
        await cut.InvokeAsync(cut.Instance.RefreshItems);
        old.SetResult(new([new("old", "Stale", null, DateTimeOffset.UtcNow)], "next", 99));
        cut.WaitForAssertion(() => cut.Instance.Items.Single().Id.Should().Be("new"));
        cut.Instance.UnreadCount.Should().Be(1);
    }

    [Test]
    public void Saved_views_migrate_schema_and_reject_invalid_values_and_future_versions()
    {
        var state = new QuarkViewState { Version = 0, PageSize = -5, Columns = [new("old", Width: 2), new("deleted")],
            Filters = new() { Rules = [new() { Field = "old", Value = "12", Operator = FilterOperator.GreaterThan }, new() { Field = "deleted", Value = "x" }] } };
        var fields = new[] { new FilterField("score", "Score", FilterFieldType.Number) };
        var restored = QuarkViewValidation.Normalize(state, fields, new Dictionary<string, string> { ["old"] = "score" });
        restored.Version.Should().Be(1); restored.PageSize.Should().Be(1);
        restored.Columns.Should().ContainSingle().Which.Width.Should().Be(64);
        restored.Filters.Rules.Should().ContainSingle().Which.Field.Should().Be("score");
        Action future = () => QuarkViewValidation.Normalize(state with { Version = 100 });
        future.Should().Throw<JsonException>();
        Action malformed = () => QuarkViewValidation.Normalize(new() { Filters = new() { Rules = [new() { Field = "score", Value = "no-number" }] } }, fields);
        malformed.Should().Throw<JsonException>();
    }

    [Test]
    public void Workflow_roots_forward_attributes_and_fluent_classes()
    {
        var cut = Render<QueryBuilder>(p => p.Add(c => c.Class, "application-class").Add(c => c.Padding, Padding.Is4).AddUnmatched("data-application", "sample"));
        var root = cut.Find("[data-slot='filter-builder']");
        root.GetAttribute("class").Should().Contain("application-class").And.Contain("p-4");
        root.GetAttribute("data-application").Should().Be("sample");
    }

    [Test]
    public async Task Scheduler_touch_move_preserves_duration_and_changes_resource()
    {
        var start = new DateTimeOffset(2026, 10, 18, 9, 0, 0, TimeSpan.Zero);
        var cut = Render<Scheduler>(p => p.Add(c => c.Events, new[] { new SchedulerEvent("a", "Meeting", start, start.AddHours(1)) }));
        await cut.InvokeAsync(() => cut.Instance.OnTouchDrop("a", false, "2026-10-19", 600, "team"));
        var moved = cut.Instance.Events.Single();
        moved.Start.Should().Be(start.AddDays(1).AddHours(1)); moved.End.Should().Be(moved.Start.AddHours(1)); moved.ResourceId.Should().Be("team");
        await cut.InvokeAsync(() => cut.Instance.OnTouchDrop("a", true, "2026-10-19", 690, "team"));
        cut.Instance.Events.Single().End.Should().Be(moved.Start.AddMinutes(90));
    }

    [Test]
    [NotInParallel] // This verifies a real 40 ms debounce window; concurrent renderer stress changes the input interval.
    public async Task DataTable_debounces_server_search_and_exposes_mixed_selection()
    {
        var queries = new List<string>();
        var cut = Render<DataTable>(p => p.Add(c => c.SelectionEnabled, true)
            .Add(c => c.SelectedKeys, new[] { "a" }).Add(c => c.SearchDebounceMilliseconds, 40)
            .Add(c => c.Data, new DataTableData<SelectOption> { RowKey = x => x.Value,
                Columns = [new() { Key = "name", Title = "Name", Value = x => x.Label }],
                LoadData = (request, _) => { queries.Add(request.State.Search); return ValueTask.FromResult(new DataTableResult<SelectOption>([new("a", "A"), new("b", "B")], 2)); } }));
        cut.Find("input[aria-label='Select all rows']").GetAttribute("data-mixed").Should().Be("true");
        var first = cut.Find("input[aria-label='Search']").InputAsync(new ChangeEventArgs { Value = "a" });
        var second = cut.Find("input[aria-label='Search']").InputAsync(new ChangeEventArgs { Value = "ab" });
        await Task.WhenAll(first, second);
        queries.Should().Equal("", "ab");
    }

    [Test]
    public async Task DataTable_virtual_server_pages_append_and_search_resets_the_shared_viewport()
    {
        var requests = new List<DataTableRequest>();
        var cut = Render<DataTable>(p => p.Add(c => c.Virtualize, true).Add(c => c.State, new QuarkViewState { PageSize = 2 })
            .Add(c => c.Data, new DataTableData<SelectOption> { RowKey = x => x.Value,
                Columns = [new() { Key = "name", Title = "Name", Value = x => x.Label }],
                LoadData = (request, _) => { requests.Add(request); return ValueTask.FromResult(request.State.Search == "new"
                    ? new DataTableResult<SelectOption>([new("n", "New")], 1)
                    : new DataTableResult<SelectOption>(Enumerable.Range(request.PageIndex * 2 + 1, 2).Select(i => new SelectOption(i.ToString(), i.ToString())).ToArray(), 4)); } }));
        cut.FindComponent<VirtualItems>().Instance.ItemKeys.Should().Equal("1", "2");
        await cut.InvokeAsync(cut.FindComponent<VirtualItems>().Instance.RequestAfter);
        cut.FindComponent<VirtualItems>().Instance.ItemKeys.Should().Equal("1", "2", "3", "4");
        cut.FindComponent<VirtualItems>().Instance.HasAfter.Should().BeFalse();
        await cut.InvokeAsync(async () => await cut.Instance.HandleSearch("new"));
        cut.FindComponent<VirtualItems>().Instance.ItemKeys.Should().Equal("n");
        requests.Select(r => r.PageIndex).Should().Equal(0, 1, 0);
        cut.Instance.State.Search.Should().Be("new");
    }

    [Test]
    public void DataTable_local_virtualization_uses_all_records_and_preserves_root_attributes()
    {
        var cut = Render<DataTable>(p => p.Add(c => c.Virtualize, true).Add(c => c.Class, "custom-table").AddUnmatched("data-application", "contacts")
            .Add(c => c.Data, new DataTableData<SelectOption> { RowKey = x => x.Value,
                Items = Enumerable.Range(1, 60).Select(i => new SelectOption(i.ToString(), i.ToString())).ToArray(),
                Columns = [new() { Key = "name", Title = "Name", Value = x => x.Label }] }));
        cut.FindComponent<VirtualItems>().Instance.ItemKeys.Should().HaveCount(60);
        cut.Find("[data-slot='datatable-data']").GetAttribute("class").Should().Contain("custom-table");
        cut.Find("[data-slot='datatable-data']").GetAttribute("data-application").Should().Be("contacts");
    }
}
