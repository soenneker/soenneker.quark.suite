using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkCheckboxPlaywrightTests : PlaywrightUnitTest
{
    public QuarkCheckboxPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Check_demo_indeterminate_parent_and_children_stay_in_sync()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#select-all"),
            expectedTitle: "Checkbox Component - Quark Suite");

        ILocator parent = page.Locator("#select-all");
        ILocator read = page.Locator("#item-read");
        ILocator write = page.Locator("#item-write");
        ILocator execute = page.Locator("#item-execute");

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

[Fact]
    public async ValueTask Checkbox_indeterminate_demo_select_all_and_child_updates_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Checkbox, new PageGetByRoleOptions { Name = "Select all", Exact = true }),
            expectedTitle: "Checkbox Component - Quark Suite");

        ILocator selectAll = page.Locator("#select-all");
        ILocator read = page.Locator("#item-read");
        ILocator write = page.Locator("#item-write");
        ILocator execute = page.Locator("#item-execute");

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

[Fact]
    public async ValueTask Check_table_demo_select_all_and_row_selection_update_header_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#select-all-checkbox"),
            expectedTitle: "Checkbox Component - Quark Suite");

        ILocator header = page.Locator("#select-all-checkbox");
        ILocator row1 = page.Locator("#row-1-checkbox");
        ILocator row2 = page.Locator("#row-2-checkbox");
        ILocator row4 = page.Locator("#row-4-checkbox");
        ILocator row1Tr = row1.Locator("xpath=ancestor::tr");
        ILocator row2Tr = row2.Locator("xpath=ancestor::tr");

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

[Fact]
    public async ValueTask Checkbox_form_multiple_requires_one_selection_and_submits_checked_items()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }).First,
            expectedTitle: "Checkbox Component - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Form Multiple" }).First;
        ILocator recents = section.Locator("[role='checkbox']").Nth(0);
        ILocator home = section.Locator("[role='checkbox']").Nth(1);
        ILocator submit = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true });

        await recents.ClickAsync();
        await home.ClickAsync();
        await submit.ClickAsync();

        await Assertions.Expect(section.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        ILocator downloads = section.Locator("[role='checkbox']").Nth(4);
        await downloads.ClickAsync();
        await submit.ClickAsync();

        ILocator json = section.Locator("pre").First;
        await Assertions.Expect(json).ToContainTextAsync("\"downloads\"");
        await Assertions.Expect(section.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToHaveCountAsync(0);
    }
}
