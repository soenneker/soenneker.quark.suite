using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTablePlaywrightTests : PlaywrightUnitTest
{
    public QuarkTablePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Table_demo_matches_shadcn_static_markup_and_accessibility_contract()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var pageErrors = new List<string>();

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tables-basic",
            static p => p.Locator("[data-slot='table-container']").First,
            expectedTitle: "Table - Quark Suite");

        var container = page.Locator("[data-slot='table-container']").First;
        var table = container.Locator("[data-slot='table']").First;
        var caption = table.Locator("[data-slot='table-caption']").First;
        var header = table.Locator("[data-slot='table-header']").First;
        var body = table.Locator("[data-slot='table-body']").First;
        var headCell = table.Locator("[data-slot='table-head']").First;
        var bodyCell = table.Locator("[data-slot='table-cell']").First;
        var row = table.Locator("[data-slot='table-row']").First;
        var footer = page.Locator("[data-slot='table-footer']").First;

        await Assertions.Expect(container).ToHaveClassAsync(new Regex(@"\brelative\b"));
        await Assertions.Expect(container).ToHaveClassAsync(new Regex(@"\bw-full\b"));
        await Assertions.Expect(container).ToHaveClassAsync(new Regex(@"\boverflow-x-auto\b"));

        await Assertions.Expect(table).ToHaveClassAsync(new Regex(@"\bw-full\b"));
        await Assertions.Expect(table).ToHaveClassAsync(new Regex(@"\bcaption-bottom\b"));
        await Assertions.Expect(table).ToHaveClassAsync(new Regex(@"\btext-sm\b"));
        await Assertions.Expect(caption).ToContainTextAsync("A list of your recent invoices.");
        await Assertions.Expect(caption).ToHaveClassAsync(new Regex(@"\btext-muted-foreground\b"));

        await Assertions.Expect(header).ToHaveClassAsync(new Regex(@"\[&_tr\]:border-b"));
        await Assertions.Expect(body).ToHaveClassAsync(new Regex(@"\[&_tr:last-child\]:border-0"));
        await Assertions.Expect(row).ToHaveClassAsync(new Regex(@"\bhas-aria-expanded:bg-muted\/50\b"));
        await Assertions.Expect(headCell).ToHaveAttributeAsync("scope", "col");
        await Assertions.Expect(headCell).ToHaveClassAsync(new Regex(@"\bh-10\b"));
        await Assertions.Expect(headCell).ToHaveClassAsync(new Regex(@"\bfont-medium\b"));
        await Assertions.Expect(bodyCell).ToHaveClassAsync(new Regex(@"\bp-2\b"));
        await Assertions.Expect(bodyCell).ToHaveClassAsync(new Regex(@"\bwhitespace-nowrap\b"));
        await Assertions.Expect(footer).ToHaveClassAsync(new Regex(@"\bborder-t\b"));
        await Assertions.Expect(footer).ToHaveClassAsync(new Regex(@"\bbg-muted\/50\b"));
        await Assertions.Expect(footer).ToHaveClassAsync(new Regex(@"\bfont-medium\b"));

        var invoiceColumnHeader = page.GetByRole(AriaRole.Columnheader, new PageGetByRoleOptions { Name = "Invoice", Exact = true }).First;
        var totalCell = page.GetByRole(AriaRole.Cell, new PageGetByRoleOptions { Name = "Total", Exact = true }).First;
        await Assertions.Expect(invoiceColumnHeader).ToBeVisibleAsync();
        await Assertions.Expect(totalCell).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Table_data_helpers_search_page_size_pagination_and_empty_state_work_without_console_errors()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var pageErrors = new List<string>();

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tables-basic",
            static p => p.Locator("[data-testid='server-table-demo'] [data-slot='table-search-input']").First,
            expectedTitle: "Table - Quark Suite");

        var demo = page.Locator("[data-testid='server-table-demo']").First;
        var table = demo.Locator("[data-slot='table']").First;
        var bodyRows = table.Locator("[data-slot='table-body'] [data-slot='table-row']");
        var searchInput = demo.Locator("[data-slot='table-search-input']").First;
        var pageSizeSelect = demo.Locator("[data-slot='table-page-size-select']").First;
        var tableInfo = demo.Locator("[data-slot='table-info']").First;
        var pagination = demo.Locator("[data-slot='pagination']").First;

        await Assertions.Expect(searchInput).ToHaveAttributeAsync("aria-label", "Search table");
        await Assertions.Expect(pageSizeSelect).ToHaveValueAsync("10");
        await Assertions.Expect(bodyRows).ToHaveCountAsync(10);
        await Assertions.Expect(tableInfo).ToContainTextAsync("1-10 of 150");
        await Assertions.Expect(pagination.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "1", Exact = true })).ToHaveAttributeAsync("aria-current", "page");

        await pageSizeSelect.SelectOptionAsync("25");
        await Assertions.Expect(pageSizeSelect).ToHaveValueAsync("25");
        await Assertions.Expect(bodyRows).ToHaveCountAsync(25);
        await Assertions.Expect(tableInfo).ToContainTextAsync("1-25 of 150");

        await pagination.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Go to next page" }).ClickAsync();
        await Assertions.Expect(tableInfo).ToContainTextAsync("26-50 of 150");
        await Assertions.Expect(pagination.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "2", Exact = true })).ToHaveAttributeAsync("aria-current", "page");

        await searchInput.FillAsync("zzzzzzzzzz-no-employee");
        await Assertions.Expect(demo.Locator("[data-slot='table-empty']")).ToBeVisibleAsync();
        await Assertions.Expect(demo.Locator("[data-slot='table-empty']")).ToContainTextAsync("No employees found");
        await Assertions.Expect(tableInfo).ToContainTextAsync("0-0 of 0");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Table_loader_exposes_status_overlay_while_loading()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var pageErrors = new List<string>();

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}loading-demo",
            static p => p.Locator("[data-slot='table-loader-content']").First,
            expectedTitle: "Loading Demo - Quark Suite");

        var loader = page.Locator("[data-slot='table-loader-content']").First;
        await Assertions.Expect(loader).ToBeVisibleAsync();
        await Assertions.Expect(loader).ToHaveAttributeAsync("role", "status");
        await Assertions.Expect(loader).ToHaveAttributeAsync("aria-live", "polite");
        await Assertions.Expect(loader).ToHaveAttributeAsync("aria-label", "Loading table data");
        await Assertions.Expect(loader).ToHaveCSSAsync("position", "static");

        await Assertions.Expect(loader).ToBeHiddenAsync(new LocatorAssertionsToBeHiddenOptions { Timeout = 5000 });

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
