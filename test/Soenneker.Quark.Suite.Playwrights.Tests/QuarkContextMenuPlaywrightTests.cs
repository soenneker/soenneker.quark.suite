using AwesomeAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkContextMenuPlaywrightTests : PlaywrightUnitTest
{
    public QuarkContextMenuPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Context_menu_demo_persists_checkbox_and_radio_selection_state_across_reopen()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}context-menus",
            static p => p.GetByText("Right click for view options", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Context Menus - Quark Suite");

        var checkboxTrigger = page.Locator("[data-testid='context-menu-checkbox-trigger']");
        await checkboxTrigger.ScrollIntoViewIfNeededAsync();
        await Assertions.Expect(checkboxTrigger).ToBeVisibleAsync();
        await checkboxTrigger.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        var showBookmarks = page.Locator("[role='menuitemcheckbox']:visible").Filter(new LocatorFilterOptions { HasText = "Show bookmarks bar" }).First;
        var showFullUrls = page.Locator("[role='menuitemcheckbox']:visible").Filter(new LocatorFilterOptions { HasText = "Show full URLs" }).First;

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

        var radioTrigger = page.Locator("[data-testid='context-menu-radio-trigger']");
        await radioTrigger.ScrollIntoViewIfNeededAsync();
        await Assertions.Expect(radioTrigger).ToBeVisibleAsync();
        await radioTrigger.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        var comfortable = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Comfortable" }).First;
        var compact = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Compact" }).First;
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

    [Test]
    public async ValueTask Context_menu_demo_supports_nested_menu_inside_modal_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}context-menus",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open context menu dialog", Exact = true }),
            expectedTitle: "Context Menus - Quark Suite");

        var dialogTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open context menu dialog", Exact = true }).First;
        await dialogTrigger.ClickAsync();

        var dialog = page.Locator("[data-slot='dialog-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Context menu dialog" });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("role", "dialog");

        var contextSurface = dialog.GetByText("Right click dialog surface", new LocatorGetByTextOptions { Exact = true });
        await contextSurface.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        var menu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Pin panel" });
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "More actions", Exact = true }).ClickAsync();

        var submenu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Duplicate view" });
        await Assertions.Expect(submenu).ToBeVisibleAsync();
        await Assertions.Expect(submenu).ToContainTextAsync("Developer Tools");
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Context_menu_demo_opens_from_right_click_and_reveals_submenu()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}context-menus",
            static p => p.GetByText("Right click here", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Context Menus - Quark Suite");

        await page.GetByText("Right click here", new PageGetByTextOptions { Exact = true })
                  .ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        var menu = page.Locator("[role='menu']:visible").First;
        await Assertions.Expect(menu).ToContainTextAsync("Back");
        await page.WaitForFunctionAsync(
            "() => {" +
            "const menu = document.querySelector('[role=\"menu\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!menu && document.body.contains(menu) && !!main && !main.contains(menu);" +
            "}");

        await page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "More Tools", Exact = true }).ClickAsync();

        var submenu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Developer Tools" });
        await Assertions.Expect(submenu).ToBeVisibleAsync();
        await Assertions.Expect(submenu).ToContainTextAsync("Developer Tools");
    }

    [Test]
    public async ValueTask Context_menu_escape_portal_layer_and_console_behavior_match_radix()
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
            $"{BaseUrl}context-menus",
            static p => p.GetByText("Right click here", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Context Menus - Quark Suite");

        await page.GetByText("Right click here", new PageGetByTextOptions { Exact = true })
                  .ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        var menu = page.Locator("[role='menu'][data-state='open']:visible").First;
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(menu).ToHaveAttributeAsync("data-slot", "context-menu-content");

        await page.WaitForFunctionAsync(
            "() => {" +
            "const menu = document.querySelector('[role=\"menu\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "if (!menu || !main || main.contains(menu)) return false;" +
            "return Number.parseInt(getComputedStyle(menu).zIndex, 10) >= 50;" +
            "}");

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(page.Locator("[role='menu'][data-state='open']:visible")).ToHaveCountAsync(0);

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
