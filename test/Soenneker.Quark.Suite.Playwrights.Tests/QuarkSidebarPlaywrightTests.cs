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
public sealed class QuarkSidebarPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSidebarPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Sidebar_mobile_demo_opens_full_screen_sheet_and_closes_from_internal_trigger()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sidebar",
            static p => p.Locator("#sidebar-mobile-demo"),
            expectedTitle: "Sidebar - Quark Suite");

        ILocator demo = page.Locator("#sidebar-mobile-demo");
        ILocator openTrigger = demo.Locator("#sidebar-mobile-open-trigger");
        ILocator closeTrigger = page.Locator("#sidebar-mobile-close-trigger");
        ILocator state = demo.Locator("#sidebar-mobile-state");
        ILocator sheetContent = page.Locator("#sidebar-mobile-sheet-content");
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

[Fact]
    public async ValueTask Sidebar_controlled_demo_tracks_parent_buttons_and_trigger_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sidebar",
            static p => p.Locator("#sidebar-controlled-demo"),
            expectedTitle: "Sidebar - Quark Suite");

        ILocator demo = page.Locator("#sidebar-controlled-demo");
        ILocator trigger = demo.Locator("#sidebar-controlled-trigger");
        ILocator state = demo.Locator("#sidebar-controlled-state");
        ILocator openButton = demo.Locator("#sidebar-controlled-open");
        ILocator closeButton = demo.Locator("#sidebar-controlled-close");
        ILocator sidebar = demo.Locator("[data-slot='sidebar'][data-state]").First;

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
