using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using DataTablesDemoPage = Soenneker.Quark.Suite.Demo.Pages.Components.DataTables;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void DataTable_renders_native_table_from_table_content()
    {
        var cut = Render<DataTable>(parameters => parameters
            .Add(p => p.TableContent, BasicTableContent));

        var table = cut.Find("table[data-slot='table']");
        table.TextContent.Should().Contain("Cell");
        table.ClassList.Should().Contain("q-datatable");
        table.ClassList.Should().Contain("w-full");
        table.ClassList.Should().Contain("text-sm");
        table.ClassList.Should().Contain("caption-bottom");
    }

    [Test]
    public void Anchor_and_span_render_themeable_data_slots()
    {
        var anchor = Render<Anchor>(parameters => parameters
            .Add(p => p.Href, "/details")
            .Add(p => p.ChildContent, "Details"));

        anchor.Find("a").GetAttribute("data-slot").Should().Be("anchor");

        var span = Render<Span>(parameters => parameters
            .Add(p => p.ChildContent, "Label"));

        span.Find("span").GetAttribute("data-slot").Should().Be("span");
    }

    [Test]
    public void DataTable_preserves_table_child_content_path()
    {
        var cut = Render<DataTable>(parameters => parameters
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenComponent<Table>(0);
                builder.AddAttribute(1, nameof(Table.ChildContent), BasicTableContent);
                builder.CloseComponent();
            }));

        cut.FindAll("table").Should().HaveCount(1);
        cut.Find("table[data-slot='table']").TextContent.Should().Contain("Cell");
    }

    [Test]
    public void DataTable_page_size_selector_is_opt_in()
    {
        var cut = Render<DataTable>(parameters => parameters
            .Add(p => p.TableContent, BasicTableContent));

        cut.FindAll("[data-slot='datatable-page-size-selector']").Should().BeEmpty();
    }

    [Test]
    public void DataTable_can_render_configured_page_size_selector()
    {
        var options = new DataTableOptions
        {
            DefaultPageSize = 40,
            ShowPageSizeSelector = true,
            PageSizeOptions = [40, 80, 120]
        };

        var cut = Render<DataTable>(parameters => parameters
            .Add(p => p.Options, options)
            .Add(p => p.TotalRecords, 120)
            .Add(p => p.TableContent, BasicTableContent));

        cut.Find("[data-slot='datatable-bottom-bar']");
        cut.Find("[data-slot='datatable-info']").TextContent.Should().Contain("Page 1");
        cut.Find("[data-slot='datatable-info']").TextContent.Should().Contain("1-40 of 120 records");
        cut.Find("[data-slot='datatable-page-size-selector']").TextContent.Should().Contain("40 items");
        var trigger = cut.Find("[data-slot='datatable-page-size-select']");

        trigger.GetAttribute("aria-label").Should().Be("Rows per page");
        trigger.GetAttribute("class").Should().Contain("cursor-pointer");
        trigger.GetAttribute("class").Should().Contain("disabled:cursor-not-allowed");
        trigger.GetAttribute("class").Should().Contain("!border-0");
        trigger.GetAttribute("class").Should().Contain("!p-0");
        cut.FindComponent<Select<int>>();
        cut.FindComponent<SelectContent>();
        cut.Markup.Should().NotContain("data-slot=\"dropdown-menu-content\"");
        cut.Markup.Should().NotContain("data-[position=popper]:h-(--radix-select-trigger-height)");
    }

    [Test]
    public void DataTable_page_size_selector_component_defaults_to_inline_trigger()
    {
        var cut = Render<DataTablePageSizeSelector>(parameters => parameters
            .Add(p => p.PageSize, 40)
            .Add(p => p.PageSizeOptions, [40, 80, 120]));

        var trigger = cut.Find("[data-slot='datatable-page-size-select']");

        cut.Find("[data-slot='datatable-page-size-selector']").TextContent.Should().Contain("40 items");
        trigger.GetAttribute("class").Should().Contain("!border-0");
        trigger.GetAttribute("class").Should().Contain("!p-0");
        cut.FindComponent<Select<int>>();
        cut.FindComponent<SelectContent>();
        cut.Markup.Should().NotContain("data-slot=\"dropdown-menu-content\"");
        cut.Markup.Should().NotContain("data-[position=popper]:h-(--radix-select-trigger-height)");
    }

    [Test]
    public void DataTable_options_clone_preserves_page_size_settings()
    {
        var options = new DataTableOptions
        {
            DefaultPageSize = 40,
            ShowPageSizeSelector = true,
            PageSizeOptions = [40, 80, 120],
            PageSizeItemSingularText = "domain",
            PageSizeItemPluralText = "domains",
            PageInfoRecordText = "domains",
            PageSizeSelectorLabel = "Show",
            PageSizeSelectorSuffix = "per page"
        };

        var clone = options.Clone();

        clone.Should().Be(options);
        clone.PageSizeOptions.Should().NotBeSameAs(options.PageSizeOptions);

        clone.PageSizeOptions[0] = 20;
        options.PageSizeOptions[0].Should().Be(40);
        clone.Should().NotBe(options);
    }

    [Test]
    public void Table_cells_use_logical_RTL_safe_alignment_and_checkbox_padding()
    {
        IRenderedComponent<Th> head = Render<Th>(parameters => parameters
            .Add(component => component.ChildContent, "Header"));
        IRenderedComponent<Td> cell = Render<Td>(parameters => parameters
            .Add(component => component.ChildContent, "Cell"));

        head.Find("th").ClassList.Should().Contain("text-start");
        head.Find("th").ClassList.Should().Contain("[&:has([role=checkbox])]:pe-0");
        head.Find("th").ClassList.Should().NotContain("[&:has([role=checkbox])]:pr-0");
        cell.Find("td").ClassList.Should().Contain("[&:has([role=checkbox])]:pe-0");
        cell.Find("td").ClassList.Should().NotContain("[&:has([role=checkbox])]:pr-0");
    }

    [Test]
    public async Task ExpandableTr_uncontrolled_trigger_opens_and_closes_detail_row()
    {
        var cut = Render<ExpandableTr>(parameters => parameters
            .Add(p => p.Colspan, 2)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenElement(0, "td");
                builder.OpenComponent<ExpandableTrTrigger>(1);
                builder.AddAttribute(2, nameof(ExpandableTrTrigger.AsChild), true);
                builder.AddAttribute(3, nameof(ExpandableTrTrigger.ChildContent), (RenderFragment)(triggerBuilder =>
                {
                    triggerBuilder.OpenComponent<Button>(0);
                    triggerBuilder.AddAttribute(1, nameof(Button.ChildContent), (RenderFragment)(buttonBuilder => buttonBuilder.AddContent(0, "Toggle")));
                    triggerBuilder.CloseComponent();
                }));
                builder.CloseComponent();
                builder.CloseElement();
            })
            .Add(p => p.DetailContent, builder => builder.AddContent(0, "Details")));

        var closedDetail = cut.Find("tr[data-slot='table-row-detail']");
        closedDetail.GetAttribute("data-state").Should().Be("closed");
        closedDetail.GetAttribute("aria-hidden").Should().Be("true");
        cut.Find("[data-slot='table-row-detail-transition']").ClassList.Should().Contain("grid-rows-[0fr]");
        cut.Find("[data-slot='table-row-detail-inner']").HasAttribute("inert").Should().BeTrue();

        await cut.Find("button[data-slot='table-row-trigger']").ClickAsync(new MouseEventArgs());

        cut.Find("tr[data-slot='table-row-detail']").TextContent.Should().Contain("Details");
        cut.Find("tr[data-slot='table-row-detail']").GetAttribute("data-state").Should().Be("open");
        cut.Find("td[data-slot='table-row-detail-cell']").GetAttribute("colspan").Should().Be("2");
        cut.Find("[data-slot='table-row-detail-transition']").ClassList.Should().Contain("grid-rows-[1fr]");
        cut.Find("[data-slot='table-row-detail-inner']").HasAttribute("inert").Should().BeFalse();
        cut.Find("button[data-slot='table-row-trigger']").GetAttribute("aria-expanded").Should().Be("true");

        await cut.Find("button[data-slot='table-row-trigger']").ClickAsync(new MouseEventArgs());

        cut.Find("tr[data-slot='table-row-detail']").GetAttribute("data-state").Should().Be("closed");
        cut.Find("[data-slot='table-row-detail-transition']").ClassList.Should().Contain("grid-rows-[0fr]");
        cut.Find("button[data-slot='table-row-trigger']").GetAttribute("aria-expanded").Should().Be("false");
    }

    [Test]
    public async Task ExpandableTr_controlled_trigger_notifies_expanded_changed()
    {
        bool? requestedState = null;
        var cut = Render<ExpandableTr>(parameters => parameters
            .Add(p => p.Expanded, false)
            .Add(p => p.ExpandedChanged, expanded => requestedState = expanded)
            .Add(p => p.ChildContent, builder =>
            {
                builder.OpenElement(0, "td");
                builder.OpenComponent<ExpandableTrTrigger>(1);
                builder.AddAttribute(2, nameof(ExpandableTrTrigger.AsChild), true);
                builder.AddAttribute(3, nameof(ExpandableTrTrigger.ChildContent), (RenderFragment)(triggerBuilder =>
                {
                    triggerBuilder.OpenComponent<Button>(0);
                    triggerBuilder.CloseComponent();
                }));
                builder.CloseComponent();
                builder.CloseElement();
            })
            .Add(p => p.DetailContent, builder => builder.AddContent(0, "Details")));

        await cut.Find("button[data-slot='table-row-trigger']").ClickAsync(new MouseEventArgs());

        requestedState.Should().BeTrue();
    }

    [Test]
    public async Task DataTables_demo_expandable_rows_change_the_controlled_row()
    {
        var cut = Render<DataTablesDemoPage>();

        await cut.Find("button[aria-label='Expand details for 3b5a577f']").ClickAsync(new MouseEventArgs());

        cut.Find("button[aria-label='Collapse details for 3b5a577f']").GetAttribute("aria-expanded").Should().Be("true");
        cut.FindAll("tr[data-slot='table-row-detail'][data-state='open']").Should().ContainSingle();

        await cut.Find("button[aria-label='Collapse details for 3b5a577f']").ClickAsync(new MouseEventArgs());

        cut.Find("button[aria-label='Expand details for 3b5a577f']").GetAttribute("aria-expanded").Should().Be("false");
        cut.FindAll("tr[data-slot='table-row-detail'][data-state='open']").Should().BeEmpty();
    }

    private static readonly RenderFragment BasicTableContent = builder =>
    {
        builder.OpenElement(0, "tbody");
        builder.OpenElement(1, "tr");
        builder.OpenElement(2, "td");
        builder.AddContent(3, "Cell");
        builder.CloseElement();
        builder.CloseElement();
        builder.CloseElement();
    };
}
