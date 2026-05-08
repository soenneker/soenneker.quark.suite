using AwesomeAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTabsPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkTabsPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Tabs_demo_switches_selected_trigger_and_visible_panel()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}components/tabs", static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Overview", Exact = true }).First,
            expectedTitle: "Tabs - Quark Suite");

        var demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Displays a set of tabs that switch between related panels." }).First;
        var overview = demoSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Overview", Exact = true });
        var analytics = demoSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Analytics", Exact = true });

        await Assertions.Expect(overview).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(demoSection.GetByText("You have 12 active projects and 3 pending tasks.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await analytics.ClickAsync();

        await Assertions.Expect(analytics).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(overview).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(demoSection.GetByText("Review performance metrics and usage trends.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tabs_vertical_and_rtl_examples_follow_orientation_and_direction_keyboard_rules()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}components/tabs",
            static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Account", Exact = true }).First, expectedTitle: "Tabs - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A vertical tabs list." }).First;
        var verticalList = verticalSection.GetByRole(AriaRole.Tablist).First;
        var verticalAccount = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Account", Exact = true }).First;
        var verticalPassword = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Password", Exact = true }).First;

        await Assertions.Expect(verticalList).ToHaveAttributeAsync("aria-orientation", "vertical");
        await Assertions.Expect(verticalAccount).ToHaveAttributeAsync("aria-selected", "false");

        await verticalAccount.FocusAsync();
        await verticalAccount.PressAsync("ArrowDown");

        await Assertions.Expect(verticalPassword).ToBeFocusedAsync();
        await Assertions.Expect(verticalPassword).ToHaveAttributeAsync("aria-selected", "true");

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Tabs support right-to-left languages." })
                             .First;
        var rtlOverview = rtlSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "نظرة عامة", Exact = true });
        var rtlAnalytics = rtlSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "التحليلات", Exact = true });

        await Assertions.Expect(rtlSection.Locator("[dir='rtl']").First).ToHaveAttributeAsync("dir", "rtl");
        await Assertions.Expect(rtlOverview).ToHaveAttributeAsync("aria-selected", "true");

        await rtlOverview.FocusAsync();
        await rtlOverview.PressAsync("ArrowLeft");

        await Assertions.Expect(rtlAnalytics).ToBeFocusedAsync();
        await Assertions.Expect(rtlAnalytics).ToHaveAttributeAsync("aria-selected", "true");
    }

    [Test]
    public async ValueTask Tabs_disabled_and_controlled_examples_respect_selection_rules()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}components/tabs", static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Home", Exact = true }),
            expectedTitle: "Tabs - Quark Suite");

        var disabledSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "A disabled tab trigger." }).First;
        var enabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Home", Exact = true });
        var disabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Disabled", Exact = true });

        await Assertions.Expect(disabled).ToBeDisabledAsync();

        await enabled.ClickAsync();
        await Assertions.Expect(enabled).ToHaveAttributeAsync("aria-selected", "true");

        await disabled.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(enabled).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(disabled).ToHaveAttributeAsync("aria-selected", "false");
    }

    [Test]
    public async ValueTask Tabs_home_and_end_keys_move_to_edge_tabs()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}components/tabs",
            static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Account", Exact = true }).First, expectedTitle: "Tabs - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A vertical tabs list." }).First;
        var verticalAccount = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Account", Exact = true }).First;
        var verticalNotifications = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Notifications", Exact = true }).First;

        await verticalAccount.FocusAsync();
        await verticalAccount.PressAsync("End");

        await Assertions.Expect(verticalNotifications).ToBeFocusedAsync();
        await Assertions.Expect(verticalNotifications).ToHaveAttributeAsync("aria-selected", "true");

        await verticalNotifications.PressAsync("Home");

        await Assertions.Expect(verticalAccount).ToBeFocusedAsync();
        await Assertions.Expect(verticalAccount).ToHaveAttributeAsync("aria-selected", "true");
    }

    [Test]
    public async ValueTask Tabs_demo_has_no_console_or_page_errors()
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

        await page.GotoAndWaitForReady($"{BaseUrl}components/tabs",
            static p => p.GetByRole(AriaRole.Tablist).First,
            expectedTitle: "Tabs - Quark Suite");

        await page.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Settings", Exact = true }).First.ClickAsync();
        await Assertions.Expect(page.GetByText("Update workspace preferences.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
