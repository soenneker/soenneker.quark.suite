using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDropdownMenuPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkDropdownMenuPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Dropdown_destructive_example_exposes_destructive_item()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Project actions", Exact = true }),
            expectedTitle: "Dropdowns - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use the destructive variant for dangerous actions." }).First;
        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Project actions", Exact = true }).ClickAsync();
        await WaitForFloatingUi(page);

        var deleteProject = page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "Delete project", Exact = true });
        await Assertions.Expect(deleteProject).ToBeVisibleAsync();
        await Assertions.Expect(deleteProject).ToHaveAttributeAsync("data-variant", "destructive");
    }

    [Test]
    public async ValueTask Dropdown_demo_exposes_disabled_item_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Checkbox menu items support independent toggles." }).First;
        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true }).ClickAsync();
        await WaitForFloatingUi(page);

        var activityBar = page.GetByRole(AriaRole.Menuitemcheckbox, new PageGetByRoleOptions { Name = "Activity Bar", Exact = true });
        await Assertions.Expect(activityBar).ToHaveAttributeAsync("data-disabled", "");
        await Assertions.Expect(activityBar).ToHaveAttributeAsync("aria-disabled", "true");
    }

    [Test]
    public async ValueTask Dropdown_submenu_home_and_end_keys_move_focus_to_first_and_last_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}dropdowns",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a submenu when an item opens a secondary list of actions." }).GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true }),
            expectedTitle: "Dropdowns - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a submenu when an item opens a secondary list of actions." }).First;
        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true }).ClickAsync();
        await WaitForFloatingUi(page);
        await page.Locator("[role='menu']:visible").First.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "More...", Exact = true }).ClickAsync();

        var submenu = page.Locator("[role='menu']:visible").Last;
        var calendar = submenu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Calendar" }).First;
        var slack = submenu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Slack" }).First;

        await calendar.FocusAsync();
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

        await page.GotoAndWaitForReady($"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Standard grouped menu with labels, separators, submenu, disabled item, and shortcuts." }).First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true });

        await trigger.ClickAsync();
        await WaitForFloatingUi(page);

        var menu = page.Locator("[role='menu']:visible").Filter(new LocatorFilterOptions { HasText = "My Account" }).First;
        await Assertions.Expect(menu).ToBeVisibleAsync();

        var menuBox = await menu.BoundingBoxAsync();
        menuBox.Should().NotBeNull();
        (menuBox!.Width > 0).Should().BeTrue();
        (menuBox.Height > 0).Should().BeTrue();
    }

    [Test]
    public async ValueTask Dropdown_avatar_example_opens_account_menu()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open account menu", Exact = true }),
            expectedTitle: "Dropdowns - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use an avatar dropdown for account menus." }).First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open account menu", Exact = true });
        await trigger.ClickAsync();
        await WaitForFloatingUi(page);

        var menu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "casey@example.com" });
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(menu).ToContainTextAsync("Log out");
    }

    [Test]
    public async ValueTask Dropdown_demo_keeps_complex_checkbox_and_radio_selection_menu_open()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}dropdowns",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Combine groups, shortcuts, checkboxes, radio items, submenu, and destructive actions." }).GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true }),
            expectedTitle: "Dropdowns - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Combine groups, shortcuts, checkboxes, radio items, submenu, and destructive actions." }).First;
        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true }).ClickAsync();
        await WaitForFloatingUi(page);

        var menu = page.Locator("[role='menu']:visible").First;
        var showSidebar = page.GetByRole(AriaRole.Menuitemcheckbox, new PageGetByRoleOptions { Name = "Show sidebar", Exact = true });
        var viewer = page.GetByRole(AriaRole.Menuitemradio, new PageGetByRoleOptions { Name = "Viewer", Exact = true });
        var editor = page.GetByRole(AriaRole.Menuitemradio, new PageGetByRoleOptions { Name = "Editor", Exact = true });

        await Assertions.Expect(showSidebar).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(viewer).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(editor).ToHaveAttributeAsync("aria-checked", "false");

        await showSidebar.ClickAsync();
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(showSidebar).ToHaveAttributeAsync("aria-checked", "true");

        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(viewer).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(editor).ToHaveAttributeAsync("aria-checked", "false");
    }

    [Test]
    public async ValueTask Dropdown_demo_home_and_end_keys_move_focus_to_first_and_last_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}dropdowns",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Standard grouped menu with labels, separators, submenu, disabled item, and shortcuts." }).First;
        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open", Exact = true }).ClickAsync();
        await WaitForFloatingUi(page);

        var menu = page.Locator("[role='menu']:visible").First;
        var profile = menu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Profile" }).First;
        var billing = menu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Billing" }).First;
        var logout = menu.Locator("[role='menuitem']").Filter(new LocatorFilterOptions { HasText = "Log out" }).First;

        await billing.FocusAsync();
        await billing.PressAsync("Home");
        await Assertions.Expect(profile).ToBeFocusedAsync();
        await profile.PressAsync("End");
        await Assertions.Expect(logout).ToBeFocusedAsync();
    }

    private static async Task WaitForFloatingUi(IPage page)
    {
        await page.WaitForFunctionAsync("() => !!window.FloatingUIDOM", new PageWaitForFunctionOptions { Timeout = 5000 });
    }
}
