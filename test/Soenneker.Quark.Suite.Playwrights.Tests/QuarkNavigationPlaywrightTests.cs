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
        await page.WaitForFunctionAsync(
            "() => {" +
            "const menu = document.querySelector('[role=\"menu\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!menu && document.body.contains(menu) && !!main && !main.contains(menu);" +
            "}");

        await page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "More Tools", Exact = true }).ClickAsync();

        ILocator submenu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Developer Tools" });
        await Assertions.Expect(submenu).ToBeVisibleAsync();
        await Assertions.Expect(submenu).ToContainTextAsync("Developer Tools");
    }

    [Fact]
    public async ValueTask Context_menu_demo_supports_nested_menu_inside_modal_dialog()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}context-menus",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open context menu dialog", Exact = true }),
            expectedTitle: "Context Menus - Quark Suite");

        ILocator dialogTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open context menu dialog", Exact = true }).First;
        await dialogTrigger.ClickAsync();
        await Assertions.Expect(dialogTrigger).ToHaveAttributeAsync("aria-expanded", "true");

        ILocator dialog = page.Locator("[data-slot='dialog-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Context menu dialog" });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("role", "dialog");

        ILocator contextSurface = dialog.GetByText("Right click dialog surface", new LocatorGetByTextOptions { Exact = true });
        await contextSurface.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        ILocator menu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Pin panel" });
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "More actions", Exact = true }).ClickAsync();

        ILocator submenu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Duplicate view" });
        await Assertions.Expect(submenu).ToBeVisibleAsync();
        await Assertions.Expect(submenu).ToContainTextAsync("Developer Tools");
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Context_menu_demo_persists_checkbox_and_radio_selection_state_across_reopen()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}context-menus",
            static p => p.GetByText("Right click for view options", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Context Menus - Quark Suite");

        ILocator checkboxTrigger = page.Locator("[data-testid='context-menu-checkbox-trigger']");
        await checkboxTrigger.ScrollIntoViewIfNeededAsync();
        await Assertions.Expect(checkboxTrigger).ToBeVisibleAsync();
        await checkboxTrigger.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        ILocator showBookmarks = page.Locator("[role='menuitemcheckbox']:visible").Filter(new LocatorFilterOptions { HasText = "Show bookmarks bar" }).First;
        ILocator showFullUrls = page.Locator("[role='menuitemcheckbox']:visible").Filter(new LocatorFilterOptions { HasText = "Show full URLs" }).First;

        await Assertions.Expect(showBookmarks).ToBeVisibleAsync();
        await Assertions.Expect(showFullUrls).ToBeVisibleAsync();

        await Assertions.Expect(showBookmarks).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(showFullUrls).ToHaveAttributeAsync("aria-checked", "false");

        await showFullUrls.ClickAsync();

        await Assertions.Expect(page.Locator("[role='menuitemcheckbox']:visible")).ToHaveCountAsync(0);

        await checkboxTrigger.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });
        await Assertions.Expect(showBookmarks).ToBeVisibleAsync();
        await Assertions.Expect(showFullUrls).ToBeVisibleAsync();
        await Assertions.Expect(showFullUrls).ToHaveAttributeAsync("aria-checked", "true");

        await page.Mouse.ClickAsync(8, 8);
        await Assertions.Expect(page.Locator("[role='menuitemcheckbox']:visible")).ToHaveCountAsync(0);

        ILocator radioTrigger = page.Locator("[data-testid='context-menu-radio-trigger']");
        await radioTrigger.ScrollIntoViewIfNeededAsync();
        await Assertions.Expect(radioTrigger).ToBeVisibleAsync();
        await radioTrigger.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        ILocator comfortable = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Comfortable" }).First;
        ILocator compact = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Compact" }).First;
        await Assertions.Expect(comfortable).ToBeVisibleAsync();
        await Assertions.Expect(compact).ToBeVisibleAsync();

        await Assertions.Expect(comfortable).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(compact).ToHaveAttributeAsync("aria-checked", "false");

        await compact.ClickAsync();

        await Assertions.Expect(page.Locator("[role='menuitemradio']:visible")).ToHaveCountAsync(0);

        await radioTrigger.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });
        await Assertions.Expect(comfortable).ToBeVisibleAsync();
        await Assertions.Expect(compact).ToBeVisibleAsync();
        await Assertions.Expect(compact).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(comfortable).ToHaveAttributeAsync("aria-checked", "false");
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
    public async ValueTask Menubar_demo_persists_radio_and_checkbox_item_state_across_reopen()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubar",
            static p => p.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "Profiles", Exact = true }).First,
            expectedTitle: "Menubar - Quark Suite");

        ILocator profilesTrigger = page.Locator("[data-testid='menubar-radio-trigger']");
        await Assertions.Expect(profilesTrigger).ToBeVisibleAsync();

        await profilesTrigger.ClickAsync();
        await Assertions.Expect(profilesTrigger).ToHaveAttributeAsync("aria-expanded", "true");

        ILocator benoit = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Benoit" }).First;
        ILocator luis = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Luis" }).First;

        await Assertions.Expect(benoit).ToBeVisibleAsync();
        await Assertions.Expect(luis).ToBeVisibleAsync();

        await Assertions.Expect(benoit).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(luis).ToHaveAttributeAsync("aria-checked", "false");

        await luis.ClickAsync();

        await Assertions.Expect(page.Locator("[role='menuitemradio']:visible")).ToHaveCountAsync(0);

        await profilesTrigger.ClickAsync();
        await Assertions.Expect(profilesTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(benoit).ToBeVisibleAsync();
        await Assertions.Expect(luis).ToBeVisibleAsync();
        await Assertions.Expect(luis).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(benoit).ToHaveAttributeAsync("aria-checked", "false");

        ILocator viewTrigger = page.Locator("[data-testid='menubar-checkbox-trigger']");
        await Assertions.Expect(viewTrigger).ToBeVisibleAsync();

        await viewTrigger.ClickAsync();
        await Assertions.Expect(viewTrigger).ToHaveAttributeAsync("aria-expanded", "true");

        ILocator bookmarks = page.Locator("[role='menuitemcheckbox']:visible").Filter(new LocatorFilterOptions { HasText = "Always Show Bookmarks Bar" }).First;
        ILocator fullUrls = page.Locator("[role='menuitemcheckbox']:visible").Filter(new LocatorFilterOptions { HasText = "Always Show Full URLs" }).First;

        await Assertions.Expect(bookmarks).ToBeVisibleAsync();
        await Assertions.Expect(fullUrls).ToBeVisibleAsync();

        await Assertions.Expect(bookmarks).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(fullUrls).ToHaveAttributeAsync("aria-checked", "true");

        await bookmarks.ClickAsync();

        await Assertions.Expect(page.Locator("[role='menuitemcheckbox']:visible")).ToHaveCountAsync(0);

        await viewTrigger.ClickAsync();
        await Assertions.Expect(viewTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(bookmarks).ToBeVisibleAsync();
        await Assertions.Expect(fullUrls).ToBeVisibleAsync();
        await Assertions.Expect(bookmarks).ToHaveAttributeAsync("aria-checked", "true");
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

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Default popover with a trigger and dimension fields in the content." }).First;
        ILocator trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open popover", Exact = true });
        await trigger.ClickAsync();

        ILocator content = page.Locator("[data-slot='popover-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Set the dimensions for the layer." });
        ILocator title = content.GetByText("Dimensions", new LocatorGetByTextOptions { Exact = true });
        await Assertions.Expect(title).ToBeVisibleAsync();

        ILocator main = page.Locator("main");
        var mainBox = await main.BoundingBoxAsync();
        Assert.NotNull(mainBox);
        await page.Mouse.ClickAsync(mainBox.X + 10, mainBox.Y + 10);

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

    [Fact]
    public async ValueTask Navigation_menu_demo_closes_from_single_outside_click()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped content, a direct docs link, shared indicator, and viewport." }).First;
        ILocator gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        ILocator content = page.Locator("[data-slot='navigation-menu-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Quark Suite" });

        await gettingStarted.ClickAsync();

        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();

        await page.Locator("main").ClickAsync(new LocatorClickOptions { Position = new Position { X = 10, Y = 10 } });

        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();
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
