using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
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
        var runtimeErrors = CaptureRuntimeErrors(page);

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
        await Assertions.Expect(openTrigger).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsize-7\b"));
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

        await openTrigger.ClickAsync();
        await Assertions.Expect(sheetContent).ToBeVisibleAsync();

        await sheetContent.GetByText("Validation", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        await page.WaitForURLAsync(new System.Text.RegularExpressions.Regex("/validation-summary$"));
        await Assertions.Expect(page.Locator("#sidebar-mobile-sheet-content")).ToHaveCountAsync(0);
        runtimeErrors.ConsoleErrors.Should().BeEmpty();
        runtimeErrors.PageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Sidebar_controlled_demo_tracks_parent_buttons_and_trigger_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var runtimeErrors = CaptureRuntimeErrors(page);

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
        var menuButton = demo.Locator("[data-slot='sidebar-menu-button']").First;
        var rail = demo.Locator("[data-slot='sidebar-rail']");

        await Assertions.Expect(state).ToContainTextAsync("closed");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(trigger).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsize-7\b"));
        await Assertions.Expect(menuButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bw-full\b"));
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "collapsed");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-collapsible", "icon");
        await Assertions.Expect(menuButton).ToHaveAttributeAsync("title", "Dashboard state");
        await Assertions.Expect(rail).ToHaveAttributeAsync("aria-label", "Toggle Sidebar");
        await Assertions.Expect(rail).ToHaveAttributeAsync("tabindex", "-1");

        await openButton.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("open");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "expanded");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-collapsible", "");
        (await menuButton.GetAttributeAsync("title")).Should().BeNull();
        await Assertions.Expect(demo.GetByText("Expanded", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await closeButton.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("closed");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "collapsed");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-collapsible", "icon");
        await Assertions.Expect(menuButton).ToHaveAttributeAsync("title", "Dashboard state");
        await Assertions.Expect(demo.GetByText("Collapsed", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await rail.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("open");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "expanded");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-collapsible", "");

        await trigger.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("closed");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-state", "collapsed");
        await Assertions.Expect(sidebar).ToHaveAttributeAsync("data-collapsible", "icon");
        runtimeErrors.ConsoleErrors.Should().BeEmpty();
        runtimeErrors.PageErrors.Should().BeEmpty();
    }

    private static RuntimeErrors CaptureRuntimeErrors(IPage page)
    {
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        return new RuntimeErrors(consoleErrors, pageErrors);
    }

    private sealed record RuntimeErrors(List<string> ConsoleErrors, List<string> PageErrors);
}
