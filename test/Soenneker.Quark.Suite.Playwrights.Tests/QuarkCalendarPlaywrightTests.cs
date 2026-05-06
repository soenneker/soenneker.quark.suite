using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCalendarPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCalendarPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Calendar_demo_matches_shadcn_daypicker_surface_and_selects_dates_without_errors()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}calendars",
            static p => p.Locator("[data-slot='calendar']").First,
            expectedTitle: "Calendar - Quark Suite");

        var calendar = page.Locator("[data-slot='calendar']").First;
        await Assertions.Expect(calendar).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("(^| )p-3( |$)"));
        await Assertions.Expect(calendar).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("\\[--cell-size:--spacing\\(8\\)\\]"));
        await Assertions.Expect(calendar.GetByRole(AriaRole.Grid)).ToBeVisibleAsync();

        var monthDropdown = calendar.Locator("select.rdp-months_dropdown");
        var dropdownRoot = calendar.Locator(".rdp-dropdown_root").First;
        await Assertions.Expect(monthDropdown).ToHaveAttributeAsync("aria-label", "Choose the Month");
        await Assertions.Expect(dropdownRoot).ToBeVisibleAsync();

        var target = calendar.Locator("[data-day]:not([data-disabled])").First;

        await target.ClickAsync();

        await Assertions.Expect(target).ToHaveAttributeAsync("data-selected", "true");
        var targetLabel = await target.Locator("button").First.GetAttributeAsync("aria-label");
        targetLabel.Should().NotBeNullOrWhiteSpace();

        var presets = page.Locator("[data-slot='calendar']").Nth(5);
        var presetFooter = presets.Locator("xpath=ancestor::*[@data-slot='card'][1]//*[@data-slot='card-footer']");
        var presetLabels = new[] { "Today", "Tomorrow", "In 3 days", "In a week", "In 2 weeks" };

        await Assertions.Expect(presetFooter).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("(^| )bg-muted/50( |$)"));

        foreach (var presetLabel in presetLabels)
        {
            var presetButton = page.GetByRole(AriaRole.Button, new() { Name = presetLabel, Exact = true });
            await Assertions.Expect(presetButton).ToBeVisibleAsync();
            await Assertions.Expect(presetButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("(^| )flex-1( |$)"));
        }

        await page.GetByRole(AriaRole.Button, new() { Name = "Tomorrow", Exact = true }).ClickAsync();
        await Assertions.Expect(presets.Locator("td[data-day='2026-05-01']")).ToHaveAttributeAsync("data-selected", "true");

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
