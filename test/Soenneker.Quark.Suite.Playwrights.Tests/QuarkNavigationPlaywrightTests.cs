using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkNavigationPlaywrightTests : PlaywrightUnitTest
{
    public QuarkNavigationPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Context_menu_demo_opens_from_right_click_and_reveals_submenu()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}context-menus",
            static p => p.GetByText("Right click here", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Context Menus - Quark Suite");

        await page.GetByText("Right click here", new PageGetByTextOptions { Exact = true })
                  .ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        ILocator menu = page.Locator("[role='menu']:visible").First;
        await Assertions.Expect(menu).ToContainTextAsync("Back");

        await page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "More Tools", Exact = true }).ClickAsync();

        await Assertions.Expect(page.Locator("[role='menu']:visible").Last).ToContainTextAsync("Developer Tools");
    }

    [Fact]
    public async ValueTask Dropdown_demo_exposes_disabled_item_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First.ClickAsync();

        ILocator menu = page.Locator("[role='menu']:visible").First;
        await Assertions.Expect(menu).ToContainTextAsync("My Account");

        ILocator apiItem = page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "API", Exact = true });
        await Assertions.Expect(apiItem).ToHaveAttributeAsync("data-disabled", "");
        await Assertions.Expect(apiItem).ToHaveAttributeAsync("aria-disabled", "true");

    }

    [Fact]
    public async ValueTask Menubar_demo_closes_from_single_outside_click()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubar",
            static p => p.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "View", Exact = true }).First,
            expectedTitle: "Menubar - Quark Suite");

        ILocator viewTrigger = page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "View", Exact = true }).First;
        await viewTrigger.ClickAsync();

        await Assertions.Expect(page.Locator("[role='menu']:visible").First).ToContainTextAsync("Always Show Bookmarks Bar");

        await page.Locator("main").ClickAsync(new LocatorClickOptions { Position = new Position { X = 10, Y = 10 } });

        await Assertions.Expect(viewTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("[role='menu']:visible")).ToHaveCountAsync(0);
    }

    [Fact]
    public async ValueTask Popover_demo_opens_and_dismisses_on_outside_click()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true }),
            expectedTitle: "Popovers - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true });
        await trigger.ClickAsync();

        ILocator content = page.Locator("[data-slot='popover-content'][data-state='open']").First;
        ILocator title = content.GetByText("Dimensions", new LocatorGetByTextOptions { Exact = true });
        await Assertions.Expect(title).ToBeVisibleAsync();

        await ClickJustOutsideAsync(page, title);

        await Assertions.Expect(title).Not.ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Navigation_menu_demo_switches_visible_content_between_triggers()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        ILocator gettingStarted = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true });
        ILocator guides = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Guides", Exact = true });

        await gettingStarted.ClickAsync();
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Introduction");

        await guides.HoverAsync();

        await Assertions.Expect(guides).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Accordion");
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Dialogs");
    }

    private static async Task ClickJustOutsideAsync(IPage page, ILocator locator)
    {
        var box = await locator.BoundingBoxAsync();
        Assert.NotNull(box);
        float x = box.X > 40 ? box.X - 20 : box.X + box.Width + 20;
        float y = box.Y > 40 ? box.Y - 20 : box.Y + 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
