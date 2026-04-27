using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDatePickerPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkDatePickerPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Date_of_birth_picker_updates_trigger_and_closes_after_selection()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByText("Date of birth", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Date Pickers - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Choose a date between" }).First;
        var trigger = section.Locator("button").First;

        await section.ScrollIntoViewIfNeededAsync();
        await trigger.ClickAsync();

        var calendar = page.Locator("[data-slot='calendar']").Last;
        await Assertions.Expect(calendar).ToBeVisibleAsync();

        await calendar.Locator("[data-day='1995-07-10']").ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync("Jul 10, 1995");
        await Assertions.Expect(page.Locator("[data-slot='calendar']")).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Dropdown_date_picker_closes_after_selecting_a_new_date()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByText("Pick a date with dropdowns", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Date Pickers - Quark Suite");

        var initialDate = DateOnly.FromDateTime(DateTime.Today.AddDays(21));
        var targetDate = initialDate.Day == 1
            ? initialDate.AddDays(1)
            : new DateOnly(initialDate.Year, initialDate.Month, 1);
        var expectedLabel = FormatDatePickerLabel(targetDate);

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "With Dropdowns" }).First;
        var trigger = section.Locator("button").First;

        await section.ScrollIntoViewIfNeededAsync();
        await trigger.ClickAsync();

        var calendar = page.Locator("[data-slot='calendar']").Last;
        await Assertions.Expect(calendar).ToBeVisibleAsync();

        await calendar.Locator($"[data-day='{targetDate:yyyy-MM-dd}']").ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync(expectedLabel);
        await Assertions.Expect(page.Locator("[data-slot='calendar']")).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Date_time_picker_updates_value_and_dismisses_on_outside_click()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByText("Pick a date and time", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Date Pickers - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Date and time" }).First;
        var input = section.Locator("input[type='text'][aria-controls]").First;
        var valueChip = section.GetByText("Value:", new LocatorGetByTextOptions { Exact = false }).First;

        var initialValue = await input.InputValueAsync();
        var initialValueChip = await valueChip.InnerTextAsync();

        await section.ScrollIntoViewIfNeededAsync();
        await input.ClickAsync(new LocatorClickOptions { Force = true });

        var panel = page.Locator("[data-slot='popover-content']").Filter(new LocatorFilterOptions { Has = page.Locator("input[aria-label='Hour']") }).Last;
        await Assertions.Expect(panel).ToBeVisibleAsync();

        await panel.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "+", Exact = true }).Nth(1).EvaluateAsync("element => element.click()");

        var updatedValue = await input.InputValueAsync();
        var updatedValueChip = await valueChip.InnerTextAsync();

        (updatedValue).Should().NotBe(initialValue);
        (updatedValueChip).Should().NotBe(initialValueChip);

        await page.Mouse.ClickAsync(10, 10);

        await Assertions.Expect(page.Locator("[data-slot='popover-content']").Filter(new LocatorFilterOptions { Has = page.Locator("input[aria-label='Hour']") })).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Reusable_date_picker_reopens_on_the_month_of_a_selected_value()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByTestId("component-date-picker"),
            expectedTitle: "Date Pickers - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { Has = page.GetByTestId("component-date-picker") }).First;
        var trigger = section.GetByTestId("component-date-picker");
        var valueChip = section.GetByText("Selected:", new LocatorGetByTextOptions { Exact = false }).First;

        await section.ScrollIntoViewIfNeededAsync();
        await trigger.ClickAsync();

        var calendar = page.Locator("[data-slot='calendar']").Last;
        await Assertions.Expect(calendar).ToBeVisibleAsync();

        await calendar.Locator("[data-day='2024-04-24']").ClickAsync();

        await Assertions.Expect(valueChip).ToContainTextAsync("2024-04-24");
        await Assertions.Expect(trigger).ToContainTextAsync("Apr 24, 2024");

        await trigger.ClickAsync();

        calendar = page.Locator("[data-slot='calendar']").Last;
        await Assertions.Expect(calendar.GetByText("April 2024", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(calendar.Locator("[data-day='2024-04-24'][data-selected-single='true']")).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Reusable_date_picker_tracks_external_value_changes_and_clear_actions()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}datepickers",
            static p => p.GetByTestId("component-date-picker"),
            expectedTitle: "Date Pickers - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { Has = page.GetByTestId("component-date-picker") }).First;
        var trigger = section.GetByTestId("component-date-picker");
        var launchButton = section.GetByTestId("component-date-launch");
        var clearButton = section.GetByTestId("component-date-clear");
        var valueChip = section.GetByText("Selected:", new LocatorGetByTextOptions { Exact = false }).First;

        await section.ScrollIntoViewIfNeededAsync();
        await launchButton.ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync("Jan 15, 2030");
        await Assertions.Expect(valueChip).ToContainTextAsync("2030-01-15");

        await trigger.ClickAsync();

        var calendar = page.Locator("[data-slot='calendar']").Last;
        await Assertions.Expect(calendar).ToBeVisibleAsync();
        await Assertions.Expect(calendar.GetByText("January 2030", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(calendar.Locator("[data-day='2030-01-15'][data-selected-single='true']")).ToBeVisibleAsync();

        await clearButton.ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync("Pick a date");
        await Assertions.Expect(valueChip).ToContainTextAsync("Selected:");
        await Assertions.Expect(valueChip).Not.ToContainTextAsync("2030-01-15");
    }

    private static string FormatDatePickerLabel(DateOnly value)
    {
        return value.ToString("MMM dd, yyyy");
    }
}
