using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkMenubarPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkMenubarPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Menubar_demo_end_key_moves_focus_to_last_top_level_trigger()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubars",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        var demo = page.Locator("[data-testid='quark-menubar-main-demo']");
        var file = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "File", Exact = true });
        var profiles = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Profiles", Exact = true });

        await file.FocusAsync();
        await file.PressAsync("End");

        await Assertions.Expect(profiles).ToBeFocusedAsync();

        await profiles.PressAsync("Home");

        await Assertions.Expect(file).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Menubar_demo_closes_from_single_outside_click()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubars",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        var demo = page.Locator("[data-testid='quark-menubar-main-demo']");
        var viewTrigger = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "View", Exact = true });
        await viewTrigger.ClickAsync();

        await Assertions.Expect(page.Locator("[role='menu']:visible").First).ToContainTextAsync("Always Show Bookmarks Bar");

        await page.Locator("h1").First.ClickAsync();

        await Assertions.Expect(viewTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("[role='menu']:visible")).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Menubar_rtl_demo_inverts_horizontal_arrow_navigation()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubars",
            static p => p.Locator("[data-testid='quark-menubar-rtl-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        var demo = page.Locator("[data-testid='quark-menubar-rtl-demo']");
        var file = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "ملف", Exact = true });
        var view = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "عرض", Exact = true });

        await file.FocusAsync();
        await file.PressAsync("ArrowRight");

        await Assertions.Expect(view).ToBeFocusedAsync();

        await view.PressAsync("ArrowLeft");
        await Assertions.Expect(file).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Menubar_demo_escape_from_submenu_closes_root_menu()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubars",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']").First,
            expectedTitle: "Menubar - Quark Suite");

        var demo = page.Locator("[data-testid='quark-menubar-main-demo']").First;
        var fileTrigger = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "File", Exact = true });

        await fileTrigger.ClickAsync();

        var menuId = await fileTrigger.GetAttributeAsync("aria-controls");
        (string.IsNullOrWhiteSpace(menuId)).Should().BeFalse();

        var menu = page.Locator($"#{menuId}");
        await Assertions.Expect(menu).ToContainTextAsync("New Tab");
        var submenuTrigger = menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Share", Exact = true });
        await submenuTrigger.ClickAsync();

        var submenuId = await submenuTrigger.GetAttributeAsync("aria-controls");
        (string.IsNullOrWhiteSpace(submenuId)).Should().BeFalse();

        var submenu = page.Locator($"#{submenuId}");
        await Assertions.Expect(submenu).ToContainTextAsync("Email link");
        await Assertions.Expect(submenu).ToContainTextAsync("Messages");

        var emailLink = submenu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Email link", Exact = true });
        await emailLink.FocusAsync();
        await emailLink.PressAsync("Escape");

        await Assertions.Expect(submenu).Not.ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[role='menu']:visible")).ToHaveCountAsync(0);
        await Assertions.Expect(fileTrigger).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Menubar_demo_roves_focus_across_top_level_triggers_and_opens_adjacent_menu_with_arrow_keys()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubars",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        var demo = page.Locator("[data-testid='quark-menubar-main-demo']");
        var file = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "File", Exact = true });
        var edit = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Edit", Exact = true });
        var view = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "View", Exact = true });

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

    [Test]
    public async ValueTask Menubar_demo_persists_radio_and_checkbox_item_state_across_reopen()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubars",
            static p => p.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "Profiles", Exact = true }).First,
            expectedTitle: "Menubar - Quark Suite");

        var profilesTrigger = page.Locator("[data-testid='menubar-radio-trigger']");
        var selectedProfile = page.Locator("[data-testid='menubar-radio-value']");
        await Assertions.Expect(profilesTrigger).ToBeVisibleAsync();

        await profilesTrigger.ClickAsync();
        await Assertions.Expect(profilesTrigger).ToHaveAttributeAsync("aria-expanded", "true");

        var benoit = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Benoit" }).First;
        var luis = page.Locator("[role='menuitemradio']:visible").Filter(new LocatorFilterOptions { HasText = "Luis" }).First;

        await Assertions.Expect(benoit).ToBeVisibleAsync();
        await Assertions.Expect(luis).ToBeVisibleAsync();

        await Assertions.Expect(benoit).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(luis).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(selectedProfile).ToHaveTextAsync("benoit");

        await luis.EvaluateAsync("element => element.click()");
        await Assertions.Expect(selectedProfile).ToHaveTextAsync("luis");
        await Assertions.Expect(profilesTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(luis).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(benoit).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(profilesTrigger).ToHaveAttributeAsync("aria-expanded", "false");

        await profilesTrigger.ClickAsync();
        await Assertions.Expect(profilesTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(benoit).ToBeVisibleAsync();
        await Assertions.Expect(luis).ToBeVisibleAsync();
        await Assertions.Expect(luis).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(benoit).ToHaveAttributeAsync("aria-checked", "false");

        await page.GotoAndWaitForReady(
            $"{BaseUrl}menubars",
            static p => p.Locator("[data-testid='quark-menubar-checkbox-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        var viewTrigger = page.Locator("[data-testid='menubar-checkbox-trigger']");
        await Assertions.Expect(viewTrigger).ToBeVisibleAsync();

        await viewTrigger.ClickAsync();
        await Assertions.Expect(viewTrigger).ToHaveAttributeAsync("aria-expanded", "true");

        var checkboxMenuId = await viewTrigger.GetAttributeAsync("aria-controls");
        (string.IsNullOrWhiteSpace(checkboxMenuId)).Should().BeFalse();

        var checkboxMenu = page.Locator($"#{checkboxMenuId}");
        var bookmarks = checkboxMenu.GetByRole(AriaRole.Menuitemcheckbox, new LocatorGetByRoleOptions { Name = "Always Show Bookmarks Bar", Exact = true });
        var fullUrls = checkboxMenu.GetByRole(AriaRole.Menuitemcheckbox, new LocatorGetByRoleOptions { Name = "Always Show Full URLs", Exact = true });

        await Assertions.Expect(bookmarks).ToBeVisibleAsync();
        await Assertions.Expect(fullUrls).ToBeVisibleAsync();

        await Assertions.Expect(bookmarks).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(fullUrls).ToHaveAttributeAsync("aria-checked", "true");

        await bookmarks.ClickAsync();
        await Assertions.Expect(bookmarks).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(fullUrls).ToHaveAttributeAsync("aria-checked", "true");

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(viewTrigger).ToHaveAttributeAsync("aria-expanded", "false");

        await viewTrigger.ClickAsync();
        await Assertions.Expect(viewTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(bookmarks).ToBeVisibleAsync();
        await Assertions.Expect(fullUrls).ToBeVisibleAsync();
        await Assertions.Expect(bookmarks).ToHaveAttributeAsync("aria-checked", "true");
    }

    [Test]
    public async ValueTask Menubar_demo_portals_above_page_and_has_no_console_errors()
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
            $"{BaseUrl}menubars",
            static p => p.Locator("[data-testid='quark-menubar-main-demo']"),
            expectedTitle: "Menubar - Quark Suite");

        var demo = page.Locator("[data-testid='quark-menubar-main-demo']");
        var fileTrigger = demo.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "File", Exact = true });

        await fileTrigger.ClickAsync();

        var menu = page.Locator("[role='menu'][data-state='open']:visible").First;
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(menu).ToHaveAttributeAsync("data-slot", "menubar-content");

        await page.WaitForFunctionAsync(
            "() => {" +
            "const menu = document.querySelector('[role=\"menu\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "if (!menu || !main || main.contains(menu)) return false;" +
            "return Number.parseInt(getComputedStyle(menu).zIndex, 10) >= 50;" +
            "}");

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(page.Locator("[role='menu'][data-state='open']:visible")).ToHaveCountAsync(0);
        await Assertions.Expect(fileTrigger).ToBeFocusedAsync();

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
