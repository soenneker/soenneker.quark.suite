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
    public async ValueTask Dropdown_demo_keeps_complex_checkbox_and_radio_selection_menu_open()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open menu", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        ILocator complexSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Complex" });
        ILocator trigger = complexSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open menu", Exact = true });

        await trigger.ClickAsync();

        ILocator menu = page.Locator("[role='menu']:visible").First;
        ILocator showSidebar = page.GetByRole(AriaRole.Menuitemcheckbox, new PageGetByRoleOptions { Name = "Show sidebar", Exact = true });
        ILocator showNotifications = page.GetByRole(AriaRole.Menuitemcheckbox, new PageGetByRoleOptions { Name = "Show notifications", Exact = true });
        ILocator viewer = page.GetByRole(AriaRole.Menuitemradio, new PageGetByRoleOptions { Name = "Viewer", Exact = true });
        ILocator editor = page.GetByRole(AriaRole.Menuitemradio, new PageGetByRoleOptions { Name = "Editor", Exact = true });

        await Assertions.Expect(showSidebar).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(showNotifications).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(viewer).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(editor).ToHaveAttributeAsync("aria-checked", "true");

        await showNotifications.ClickAsync();

        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(showNotifications).ToHaveAttributeAsync("aria-checked", "false");

        await viewer.ClickAsync();

        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(viewer).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(editor).ToHaveAttributeAsync("aria-checked", "false");
    }

    [Fact]
    public async ValueTask Dropdown_demo_action_item_opens_dialog_after_menu_selection()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open menu", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        ILocator dialogSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Menu items can launch alert and share flows" });
        ILocator trigger = dialogSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open menu", Exact = true });

        await trigger.ClickAsync();

        ILocator menu = page.Locator("[role='menu']:visible").Filter(new LocatorFilterOptions { HasText = "File Actions" });
        await Assertions.Expect(menu).ToContainTextAsync("Share...");
        ILocator share = menu.Locator("[data-slot='dropdown-menu-item']").Filter(new LocatorFilterOptions { HasText = "Share..." });

        await share.ClickAsync();

        ILocator dialog = page.Locator("[role='dialog'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Share File" });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[role='menu']:visible")).ToHaveCountAsync(0);
        await Assertions.Expect(dialog.GetByLabel("Email Address")).ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Dropdown_demo_supports_nested_menu_inside_dialog()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open dialog", Exact = true }),
            expectedTitle: "Dropdowns - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Nested overlays should still work when the dropdown is opened from inside a dialog." });
        ILocator dialogTrigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open dialog", Exact = true });

        await dialogTrigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Workspace actions", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        ILocator menuTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open menu", Exact = true });
        await menuTrigger.ClickAsync();

        ILocator menu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "More options" });
        await Assertions.Expect(menuTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(menu).ToBeVisibleAsync();

        await menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "More options", Exact = true }).ClickAsync();

        ILocator submenu = page.GetByRole(AriaRole.Menu, new PageGetByRoleOptions { Name = "More options", Exact = true });
        await Assertions.Expect(submenu).ToBeVisibleAsync();
        await Assertions.Expect(submenu).ToContainTextAsync("Save page");
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

        await ClickJustOutsideAsync(page, content);

        await Assertions.Expect(content).Not.ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Popover_demo_controlled_open_state_stays_in_sync_with_external_toggle_and_outside_dismissal()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle popover", Exact = true }),
            expectedTitle: "Popovers - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Controlled Open State" });
        ILocator toggle = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle popover", Exact = true });
        ILocator trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Edit dimensions", Exact = true });

        await toggle.ClickAsync();

        ILocator content = page.Locator("[data-slot='popover-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Update the values below and save them back to the current layer." });
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();

        ILocator main = page.Locator("main");
        var mainBox = await main.BoundingBoxAsync();
        Assert.NotNull(mainBox);
        await page.Mouse.ClickAsync(mainBox.X + mainBox.Width - 10, mainBox.Y + mainBox.Height - 10);

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();

        await trigger.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Popover_demo_supports_nested_popover_inside_dialog()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true }),
            expectedTitle: "Popovers - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A popover opened from content inside a modal dialog." });
        ILocator dialogTrigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open Dialog", Exact = true });

        await dialogTrigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Popover Example", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        ILocator popoverTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open Popover", Exact = true });
        await popoverTrigger.ClickAsync();

        ILocator popover = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Popover in Dialog", Exact = true });
        await Assertions.Expect(popoverTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(popover).ToBeVisibleAsync();

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(popover).Not.ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
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
        var viewport = page.ViewportSize;
        float leftSpace = box.X;
        float rightSpace = (viewport?.Width ?? 0) - (box.X + box.Width);
        float topSpace = box.Y;
        float bottomSpace = (viewport?.Height ?? 0) - (box.Y + box.Height);

        float x = rightSpace >= leftSpace ? box.X + box.Width + 20 : box.X - 20;
        float y = bottomSpace >= topSpace ? box.Y + box.Height + 20 : box.Y - 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
