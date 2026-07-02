using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;

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
