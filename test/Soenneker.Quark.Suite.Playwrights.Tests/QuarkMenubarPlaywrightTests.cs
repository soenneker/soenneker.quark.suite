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
public sealed class QuarkMenubarPlaywrightTests : PlaywrightUnitTest
{
    public QuarkMenubarPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Menubar_demo_end_key_moves_focus_to_last_top_level_trigger()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubar",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        ILocator demo = page.Locator("[data-testid='quark-menubar-main-demo']");
        ILocator file = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "File", Exact = true });
        ILocator profiles = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Profiles", Exact = true });

        await file.FocusAsync();
        await file.PressAsync("End");

        await Assertions.Expect(profiles).ToBeFocusedAsync();

        await profiles.PressAsync("Home");

        await Assertions.Expect(file).ToBeFocusedAsync();
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
    public async ValueTask Menubar_rtl_demo_inverts_horizontal_arrow_navigation()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubar",
            static p => p.Locator("[data-testid='quark-menubar-rtl-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        ILocator demo = page.Locator("[data-testid='quark-menubar-rtl-demo']");
        ILocator file = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "ملف", Exact = true });
        ILocator view = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "عرض", Exact = true });

        await file.FocusAsync();
        await file.PressAsync("ArrowRight");

        await Assertions.Expect(view).ToBeFocusedAsync();

        await view.PressAsync("ArrowLeft");
        await Assertions.Expect(file).ToBeFocusedAsync();
    }

[Fact]
    public async ValueTask Menubar_demo_escape_closes_submenu_before_parent_menu()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubar",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']").First,
            expectedTitle: "Menubar - Quark Suite");

        ILocator demo = page.Locator("[data-testid='quark-menubar-main-demo']").First;
        ILocator fileTrigger = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "File", Exact = true });

        await fileTrigger.ClickAsync();

        string? menuId = await fileTrigger.GetAttributeAsync("aria-controls");
        Assert.False(string.IsNullOrWhiteSpace(menuId));

        ILocator menu = page.Locator($"#{menuId}");
        await Assertions.Expect(menu).ToContainTextAsync("New Tab");
        ILocator submenuTrigger = menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Share", Exact = true });
        await submenuTrigger.ClickAsync();

        string? submenuId = await submenuTrigger.GetAttributeAsync("aria-controls");
        Assert.False(string.IsNullOrWhiteSpace(submenuId));

        ILocator submenu = page.Locator($"#{submenuId}");
        await Assertions.Expect(submenu).ToContainTextAsync("Email link");
        await Assertions.Expect(submenu).ToContainTextAsync("Messages");

        ILocator emailLink = submenu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Email link", Exact = true });
        await emailLink.FocusAsync();
        await emailLink.PressAsync("Escape");

        await Assertions.Expect(submenu).Not.ToBeVisibleAsync();
        ILocator reopenedMenu = page.Locator("[role='menu']:visible").Filter(new LocatorFilterOptions { HasText = "New Tab" }).First;
        await Assertions.Expect(reopenedMenu).ToBeVisibleAsync();

        ILocator reopenedSubmenuTrigger = reopenedMenu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Share", Exact = true });
        await Assertions.Expect(reopenedSubmenuTrigger).ToBeFocusedAsync();

        await reopenedSubmenuTrigger.PressAsync("Escape");

        await Assertions.Expect(reopenedMenu).Not.ToBeVisibleAsync();
        await Assertions.Expect(fileTrigger).ToBeFocusedAsync();
    }

[Fact]
    public async ValueTask Menubar_demo_roves_focus_across_top_level_triggers_and_opens_adjacent_menu_with_arrow_keys()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubar",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        ILocator demo = page.Locator("[data-testid='quark-menubar-main-demo']");
        ILocator file = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "File", Exact = true });
        ILocator edit = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Edit", Exact = true });
        ILocator view = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "View", Exact = true });

        await file.FocusAsync();
        await file.PressAsync("ArrowRight");
        await Assertions.Expect(edit).ToBeFocusedAsync();
        await Assertions.Expect(edit).ToHaveAttributeAsync("aria-expanded", "false");

        await edit.PressAsync("Enter");
        await Assertions.Expect(edit).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(page.Locator("[role='menu']:visible").First).ToContainTextAsync("Undo");

        await edit.PressAsync("ArrowRight");
        await Assertions.Expect(view).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(edit).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("[role='menu']:visible").First).ToContainTextAsync("Always Show Bookmarks Bar");

        await view.FocusAsync();
        await view.PressAsync("Home");
        await Assertions.Expect(file).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(view).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("[role='menu']:visible").First).ToContainTextAsync("New Tab");
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
}
