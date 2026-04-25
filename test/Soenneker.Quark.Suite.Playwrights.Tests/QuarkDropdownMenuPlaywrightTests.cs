using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDropdownMenuPlaywrightTests : PlaywrightUnitTest
{
    public QuarkDropdownMenuPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Dropdown_demo_action_item_opens_dialog_after_menu_selection()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open menu", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");
        await WaitForInteractiveAsync(page);

        var dialogSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Menu items can launch alert and share flows" });
        var trigger = dialogSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open menu", Exact = true });

        await trigger.ClickAsync();
        await WaitForFloatingUiAsync(page);

        var menu = page.Locator("[role='menu']:visible").Filter(new LocatorFilterOptions { HasText = "File Actions" });
        await Assertions.Expect(menu).ToContainTextAsync("Share...");
        var share = menu.Locator("[data-slot='dropdown-menu-item']").Filter(new LocatorFilterOptions { HasText = "Share..." });

        await share.ClickAsync();

        var dialog = page.Locator("[role='dialog'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Share File" });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[role='menu']:visible")).ToHaveCountAsync(0);
        await Assertions.Expect(dialog.GetByLabel("Email Address")).ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Dropdown_demo_exposes_disabled_item_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");
        await WaitForInteractiveAsync(page);

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First.ClickAsync();
        await WaitForFloatingUiAsync(page);

        var menu = page.Locator("[role='menu']:visible").First;
        await Assertions.Expect(menu).ToContainTextAsync("My Account");

        var apiItem = page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "API", Exact = true });
        await Assertions.Expect(apiItem).ToHaveAttributeAsync("data-disabled", "");
        await Assertions.Expect(apiItem).ToHaveAttributeAsync("aria-disabled", "true");

    }

[Test]
    public async ValueTask Dropdown_submenu_home_and_end_keys_move_focus_to_first_and_last_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Invite users", Exact = true }),
            expectedTitle: "Dropdowns - Quark Suite");
        await WaitForInteractiveAsync(page);

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use nested dropdown submenu primitives to expose secondary actions." }).First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Invite users", Exact = true });

        await trigger.ClickAsync();
        await WaitForFloatingUiAsync(page);
        var menu = page.Locator("[role='menu']:visible").First;
        await menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "More...", Exact = true }).ClickAsync();

        var submenu = page.Locator("[role='menu']:visible").Last;
        var calendar = submenu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Calendar" }).First;
        var slack = submenu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Slack" }).First;

        await calendar.FocusAsync();
        await calendar.PressAsync("Home");

        await Assertions.Expect(calendar).ToBeFocusedAsync();

        await calendar.PressAsync("End");

        await Assertions.Expect(slack).ToBeFocusedAsync();

        await slack.PressAsync("Home");

        await Assertions.Expect(calendar).ToBeFocusedAsync();
    }

[Test]
    public async ValueTask Dropdown_demo_positions_menu_below_trigger_and_above_surrounding_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(1400, 1000);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");
        await WaitForInteractiveAsync(page);

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Standard grouped menu with labels, separators, and account actions." }).First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true });

        await trigger.ClickAsync();
        await WaitForFloatingUiAsync(page);

        var menu = page.Locator("[role='menu']:visible").Filter(new LocatorFilterOptions { HasText = "My Account" }).First;
        await Assertions.Expect(menu).ToBeVisibleAsync();

        var triggerBox = await trigger.BoundingBoxAsync();
        var menuBox = await menu.BoundingBoxAsync();

        (triggerBox).Should().NotBeNull();
        (menuBox).Should().NotBeNull();
        (menuBox.Y >= triggerBox.Y + triggerBox.Height - 2).Should().BeTrue();

        var topElementRole = await menu.EvaluateAsync<string>(
            @"element => {
                const rect = element.getBoundingClientRect();
                const target = document.elementFromPoint(rect.left + (rect.width / 2), rect.top + 12);
                return target?.closest('[role]')?.getAttribute('role') ?? '';
            }");

        (topElementRole).Should().Be("menu");
    }

[Test]
    public async ValueTask Dropdown_demo_supports_nested_menu_inside_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open dialog", Exact = true }),
            expectedTitle: "Dropdowns - Quark Suite");
        await WaitForInteractiveAsync(page);

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Nested overlays should still work when the dropdown is opened from inside a dialog." });
        var dialogTrigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open dialog", Exact = true });

        await dialogTrigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Workspace actions", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        var menuTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open menu", Exact = true });
        await menuTrigger.ClickAsync();
        await WaitForFloatingUiAsync(page);

        var menu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "More options" });
        await Assertions.Expect(menuTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(menu).ToBeVisibleAsync();

        await menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "More options", Exact = true }).ClickAsync();

        var submenuTrigger = menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "More options", Exact = true });
        var submenu = page.GetByRole(AriaRole.Menu, new PageGetByRoleOptions { Name = "More options", Exact = true });
        await Assertions.Expect(submenu).ToBeVisibleAsync();
        await Assertions.Expect(submenu).ToContainTextAsync("Save page");

        await submenuTrigger.PressAsync("Escape");

        await Assertions.Expect(submenu).Not.ToBeVisibleAsync();
        await Assertions.Expect(menu).Not.ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Dropdown_demo_keeps_complex_checkbox_and_radio_selection_menu_open()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open menu", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");
        await WaitForInteractiveAsync(page);

        var complexSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Complex" });
        var trigger = complexSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open menu", Exact = true });

        await trigger.ClickAsync();
        await WaitForFloatingUiAsync(page);

        var menu = page.Locator("[role='menu']:visible").First;
        var showSidebar = page.GetByRole(AriaRole.Menuitemcheckbox, new PageGetByRoleOptions { Name = "Show sidebar", Exact = true });
        var showNotifications = page.GetByRole(AriaRole.Menuitemcheckbox, new PageGetByRoleOptions { Name = "Show notifications", Exact = true });
        var viewer = page.GetByRole(AriaRole.Menuitemradio, new PageGetByRoleOptions { Name = "Viewer", Exact = true });
        var editor = page.GetByRole(AriaRole.Menuitemradio, new PageGetByRoleOptions { Name = "Editor", Exact = true });

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

[Test]
    public async ValueTask Dropdown_demo_home_and_end_keys_move_focus_to_first_and_last_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");
        await WaitForInteractiveAsync(page);

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Standard grouped menu with labels, separators, and account actions." }).First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true });

        await trigger.ClickAsync();
        await WaitForFloatingUiAsync(page);

        var menu = page.Locator("[role='menu']:visible").First;
        var profile = menu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Profile" }).First;
        var billing = menu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Billing" }).First;
        var logout = menu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Log out" }).First;

        await billing.FocusAsync();
        await billing.PressAsync("Home");

        await Assertions.Expect(profile).ToBeFocusedAsync();

        await profile.PressAsync("End");

        await Assertions.Expect(logout).ToBeFocusedAsync();

        await logout.PressAsync("Home");

        await Assertions.Expect(profile).ToBeFocusedAsync();
    }

    private static async Task WaitForInteractiveAsync(IPage page)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'");
    }

    private static async Task WaitForFloatingUiAsync(IPage page)
    {
        await page.WaitForFunctionAsync("() => !!window.FloatingUIDOM", new PageWaitForFunctionOptions { Timeout = 15000 });
    }
}
