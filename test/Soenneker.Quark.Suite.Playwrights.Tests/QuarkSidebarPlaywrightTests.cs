using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSidebarPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSidebarPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Sidebar_mobile_demo_opens_full_screen_sheet_and_closes_from_internal_trigger()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sidebar",
            static p => p.Locator("#sidebar-mobile-demo"),
            expectedTitle: "Sidebar - Quark Suite");

        var demo = page.Locator("#sidebar-mobile-demo");
        var openTrigger = demo.Locator("#sidebar-mobile-open-trigger");
        var closeTrigger = page.Locator("#sidebar-mobile-close-trigger");
        var state = demo.Locator("#sidebar-mobile-state");
        var sheetContent = page.Locator("#sidebar-mobile-sheet-content");
        await Assertions.Expect(state).ToContainTextAsync("closed");
        await Assertions.Expect(openTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(sheetContent).ToHaveCountAsync(0);

        await openTrigger.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("open");
        await Assertions.Expect(openTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(sheetContent).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Menu", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(sheetContent.GetByText("Introduction", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(sheetContent.GetByText("Validation", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await closeTrigger.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("closed");
        await Assertions.Expect(openTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(sheetContent).ToHaveCountAsync(0);
    }

[Test]
    public async ValueTask Sidebar_controlled_demo_tracks_parent_buttons_and_trigger_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sidebar",
            static p => p.Locator("#sidebar-controlled-demo"),
            expectedTitle: "Sidebar - Quark Suite");

        var demo = page.Locator("#sidebar-controlled-demo");
        var trigger = demo.Locator("#sidebar-controlled-trigger");
        var state = demo.Locator("#sidebar-controlled-state");
        var openButton = demo.Locator("#sidebar-controlled-open");
        var closeButton = demo.Locator("#sidebar-controlled-close");
        var sidebar = demo.Locator("[data-slot='sidebar'][data-state]").First;

        await Assertions.Expect(state).ToContainTextAsync("closed");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "collapsed");

        await openButton.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("open");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "expanded");
        await Assertions.Expect(demo.GetByText("Expanded", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await closeButton.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("closed");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "collapsed");
        await Assertions.Expect(demo.GetByText("Collapsed", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await trigger.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("open");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "expanded");
    }
}
