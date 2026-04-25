using AwesomeAssertions;
using Bunit;

namespace Soenneker.Quark.Suite.Tests;

public sealed partial class RenderedShadcnParityTests
{
    [Test]
    public void Table_helpers_emit_accessible_data_table_control_contracts()
    {
        var search = Render<TableSearch>(parameters => parameters
            .Add(p => p.Placeholder, "Search employees..."));

        var searchRoot = search.Find("[data-slot='table-search']");
        var searchInput = search.Find("[data-slot='table-search-input']");
        searchRoot.GetAttribute("class").Should().Contain("sm:max-w-sm");
        searchInput.GetAttribute("aria-label").Should().Be("Search table");
        searchInput.GetAttribute("placeholder").Should().Be("Search employees...");
        searchInput.GetAttribute("class").Should().Contain("focus-visible:ring-[3px]");

        var pageSize = Render<TablePageSizeSelector>();
        var pageSizeRoot = pageSize.Find("[data-slot='table-page-size-selector']");
        var pageSizeSelect = pageSize.Find("[data-slot='table-page-size-select']");
        pageSizeRoot.GetAttribute("class").Should().Contain("flex");
        pageSizeRoot.GetAttribute("class").Should().Contain("gap-2");
        pageSizeSelect.GetAttribute("id").Should().NotBeNullOrWhiteSpace();
        pageSize.Find("label").GetAttribute("for").Should().Be(pageSizeSelect.GetAttribute("id"));
        pageSizeSelect.GetAttribute("class").Should().Contain("focus-visible:ring-[3px]");

        var info = Render<TableInfo>(parameters => parameters
            .Add(p => p.TotalRecords, 0));

        info.Find("[data-slot='table-info']").TextContent.Should().Contain("0-0 of 0");

        var topBar = Render<TableTopBar>();
        topBar.Find("[data-slot='table-top-bar']").GetAttribute("class").Should().Contain("flex-wrap");

        var bottomBar = Render<TableBottomBar>();
        bottomBar.Find("[data-slot='table-bottom-bar']").GetAttribute("class").Should().Contain("flex-wrap");
    }
}
