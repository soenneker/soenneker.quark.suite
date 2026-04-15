using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkDatePickerPlaywrightTests : PlaywrightUnitTest
{
    public QuarkDatePickerPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Date_of_birth_picker_updates_trigger_and_closes_after_selection()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByText("Date of birth", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Date Pickers - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Date of Birth" }).First;
        ILocator trigger = section.Locator("button").First;

        await section.ScrollIntoViewIfNeededAsync();
        await trigger.ClickAsync();

        ILocator calendar = page.Locator("[data-slot='calendar']").Last;
        await Assertions.Expect(calendar).ToBeVisibleAsync();

        await calendar.Locator("[data-day='1995-07-10']").ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync("Jul 10, 1995");
        await Assertions.Expect(page.Locator("[data-slot='calendar']")).ToHaveCountAsync(0);
    }

    [Fact]
    public async ValueTask Dropdown_date_picker_closes_after_selecting_a_new_date()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByText("Pick a date with dropdowns", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Date Pickers - Quark Suite");

        DateOnly initialDate = DateOnly.FromDateTime(DateTime.Today.AddDays(21));
        DateOnly targetDate = new(initialDate.AddMonths(1).Year, initialDate.AddMonths(1).Month, 1);
        string expectedLabel = targetDate.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "With Dropdowns" }).First;
        ILocator trigger = section.Locator("button").First;

        await section.ScrollIntoViewIfNeededAsync();
        await trigger.ClickAsync();

        ILocator calendar = page.Locator("[data-slot='calendar']").Last;
        await Assertions.Expect(calendar).ToBeVisibleAsync();

        await calendar.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next month", Exact = true }).ClickAsync();
        await calendar.Locator($"[data-day='{targetDate:yyyy-MM-dd}']").ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync(expectedLabel);
        await Assertions.Expect(page.Locator("[data-slot='calendar']")).ToHaveCountAsync(0);
    }

    [Fact]
    public async ValueTask Date_time_picker_updates_value_and_dismisses_on_outside_click()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByText("Pick a date and time", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Date Pickers - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Date and time" }).First;
        ILocator input = section.Locator("input[type='text']").First;
        ILocator valueChip = section.GetByText("Value:", new LocatorGetByTextOptions { Exact = false }).First;

        string initialValue = await input.InputValueAsync();
        string initialValueChip = await valueChip.InnerTextAsync();

        await section.ScrollIntoViewIfNeededAsync();
        await input.ClickAsync();

        ILocator panel = page.Locator(".q-calendar-panel").Last;
        await Assertions.Expect(panel).ToBeVisibleAsync();

        await panel.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "+", Exact = true }).Nth(1).ClickAsync();

        string updatedValue = await input.InputValueAsync();
        string updatedValueChip = await valueChip.InnerTextAsync();

        Assert.NotEqual(initialValue, updatedValue);
        Assert.NotEqual(initialValueChip, updatedValueChip);

        await page.Mouse.ClickAsync(10, 10);

        await Assertions.Expect(page.Locator(".q-calendar-panel")).ToHaveCountAsync(0);
    }
}
