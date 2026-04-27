using System;
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
        await Assertions.Expect(calendar).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("\\[--cell-size:--spacing\\(7\\)\\]"));
        await Assertions.Expect(calendar.GetByRole(AriaRole.Grid)).ToBeVisibleAsync();

        var monthDropdown = calendar.Locator("select.rdp-months_dropdown");
        var dropdownRoot = calendar.Locator(".rdp-dropdown_root").First;
        await Assertions.Expect(monthDropdown).ToHaveAttributeAsync("aria-label", "Choose the Month");
        await Assertions.Expect(dropdownRoot).ToBeVisibleAsync();

        var targetDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
        var target = calendar.Locator($"[data-day='{targetDate:yyyy-MM-dd}']");

        await target.ClickAsync();

        await Assertions.Expect(target).ToHaveAttributeAsync("data-selected-single", "true");
        var targetLabel = await target.GetAttributeAsync("aria-label");
        targetLabel.Should().NotBeNullOrWhiteSpace();

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
