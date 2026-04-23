using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCheckboxPlaywrightTests : PlaywrightUnitTest
{
    public QuarkCheckboxPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Check_demo_indeterminate_parent_and_children_stay_in_sync()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#select-all"),
            expectedTitle: "Checkbox Component - Quark Suite");

        var parent = page.Locator("#select-all");
        var read = page.Locator("#item-read");
        var write = page.Locator("#item-write");
        var execute = page.Locator("#item-execute");

        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "mixed");
        await Assertions.Expect(read).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "false");

        await write.ClickAsync();
        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "mixed");

        await execute.ClickAsync();
        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "true");

        await parent.ClickAsync();
        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(read).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "false");
    }

[Test]
    public async ValueTask Checkbox_indeterminate_demo_select_all_and_child_updates_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Checkbox, new PageGetByRoleOptions { Name = "Select all", Exact = true }),
            expectedTitle: "Checkbox Component - Quark Suite");

        var selectAll = page.Locator("#select-all");
        var read = page.Locator("#item-read");
        var write = page.Locator("#item-write");
        var execute = page.Locator("#item-execute");

        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "mixed");

        await selectAll.ClickAsync();

        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(read).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "true");

        await write.ClickAsync();

        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "mixed");
    }

[Test]
    public async ValueTask Check_table_demo_select_all_and_row_selection_update_header_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#select-all-checkbox"),
            expectedTitle: "Checkbox Component - Quark Suite");

        var header = page.Locator("#select-all-checkbox");
        var row1 = page.Locator("#row-1-checkbox");
        var row2 = page.Locator("#row-2-checkbox");
        var row4 = page.Locator("#row-4-checkbox");
        var row1Tr = row1.Locator("xpath=ancestor::tr");
        var row2Tr = row2.Locator("xpath=ancestor::tr");

        await Assertions.Expect(header).ToHaveAttributeAsync("aria-checked", "mixed");
        await Assertions.Expect(row1).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row2).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(row1Tr).ToHaveAttributeAsync("data-state", "selected");

        await header.ClickAsync();

        await Assertions.Expect(header).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row2).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row4).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row2Tr).ToHaveAttributeAsync("data-state", "selected");

        await row2.ClickAsync();

        await Assertions.Expect(header).ToHaveAttributeAsync("aria-checked", "mixed");
        await Assertions.Expect(row2).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(row2Tr).Not.ToHaveAttributeAsync("data-state", "selected");
    }

[Test]
    public async ValueTask Checkbox_form_multiple_requires_one_selection_and_submits_checked_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }).First,
            expectedTitle: "Checkbox Component - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Form Multiple" }).First;
        var recents = section.Locator("[role='checkbox']").Nth(0);
        var home = section.Locator("[role='checkbox']").Nth(1);
        var submit = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true });

        await recents.ClickAsync();
        await home.ClickAsync();
        await submit.ScrollIntoViewIfNeededAsync();
        await submit.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(section.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        var downloads = section.Locator("[role='checkbox']").Nth(4);
        await downloads.ClickAsync();
        await submit.ScrollIntoViewIfNeededAsync();
        await submit.ClickAsync(new LocatorClickOptions { Force = true });

        var json = section.Locator("pre").First;
        await Assertions.Expect(json).ToContainTextAsync("\"downloads\"");
        await Assertions.Expect(section.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToHaveCountAsync(0);
    }
}
