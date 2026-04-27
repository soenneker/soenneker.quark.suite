using AwesomeAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTabsPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkTabsPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Tabs_manual_activation_moves_focus_without_selection_until_space_or_enter()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tabs", static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Tab A", Exact = true }),
            expectedTitle: "Tabs - Quark Suite");

        var manualSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Radix manual mode: arrow keys move focus; Space or Enter activates the focused tab" }).First;
        var tabA = manualSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Tab A", Exact = true });
        var tabB = manualSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Tab B", Exact = true });

        await Assertions.Expect(tabA).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(manualSection.GetByText("Panel A", new LocatorGetByTextOptions { Exact = false })).ToBeVisibleAsync();

        await tabA.FocusAsync();
        await tabA.PressAsync("ArrowRight");

        await Assertions.Expect(tabB).ToBeFocusedAsync();
        await Assertions.Expect(tabA).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(tabB).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(manualSection.GetByText("Panel A", new LocatorGetByTextOptions { Exact = false })).ToBeVisibleAsync();

        await tabB.PressAsync("Enter");

        await Assertions.Expect(tabB).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(tabA).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(manualSection.GetByText("Panel B", new LocatorGetByTextOptions { Exact = false })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tabs_vertical_and_rtl_examples_follow_orientation_and_direction_keyboard_rules()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tabs",
            static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Account", Exact = true }).First, expectedTitle: "Tabs - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Render the triggers in a side rail." }).First;
        var verticalList = verticalSection.GetByRole(AriaRole.Tablist).First;
        var verticalAccount = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Account", Exact = true }).First;
        var verticalPassword = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Password", Exact = true }).First;

        await Assertions.Expect(verticalList).ToHaveAttributeAsync("aria-orientation", "vertical");
        await Assertions.Expect(verticalAccount).ToHaveAttributeAsync("aria-selected", "false");

        await verticalAccount.FocusAsync();
        await verticalAccount.PressAsync("ArrowDown");

        await Assertions.Expect(verticalPassword).ToBeFocusedAsync();
        await Assertions.Expect(verticalPassword).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(verticalSection.GetByText("Use the arrow keys to move between vertical tabs.", new LocatorGetByTextOptions { Exact = true }))
                        .ToBeVisibleAsync();

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Tabs respect inherited direction in right-to-left layouts." })
                             .First;
        var rtlAccount = rtlSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "الحساب", Exact = true });
        var rtlPassword = rtlSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "كلمة المرور", Exact = true });

        await Assertions.Expect(rtlSection.Locator("[dir='rtl']").First).ToHaveAttributeAsync("dir", "rtl");
        await Assertions.Expect(rtlAccount).ToHaveAttributeAsync("aria-selected", "false");

        await rtlAccount.FocusAsync();
        await rtlAccount.PressAsync("ArrowLeft");

        await Assertions.Expect(rtlPassword).ToBeFocusedAsync();
        await Assertions.Expect(rtlPassword).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(rtlSection.GetByText("اتجاه المكوّن يورث بشكل صحيح من الحاوية.", new LocatorGetByTextOptions { Exact = true }))
                        .ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tabs_disabled_and_controlled_examples_respect_selection_rules()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tabs", static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Enabled", Exact = true }),
            expectedTitle: "Tabs - Quark Suite");

        var disabledSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Disabled triggers are skipped by keyboard navigation and cannot be selected." }).First;
        var enabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Enabled", Exact = true });
        var disabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Disabled", Exact = true });
        var anotherEnabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Another enabled", Exact = true });

        await Assertions.Expect(disabled).ToBeDisabledAsync();

        await enabled.ClickAsync();
        await Assertions.Expect(enabled).ToHaveAttributeAsync("aria-selected", "true");

        await disabled.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(enabled).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions
              .Expect(disabledSection.GetByText("This content is unreachable because the trigger is disabled.", new LocatorGetByTextOptions { Exact = true }))
              .Not.ToBeVisibleAsync();

        await anotherEnabled.ClickAsync();
        await Assertions.Expect(anotherEnabled).ToHaveAttributeAsync("aria-selected", "true");

        var controlledDescription = page.Locator("p").Filter(new LocatorFilterOptions { HasText = "Selected tab:" }).First;
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Settings", Exact = true }).Last.ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Settings", Exact = true }).Last)
                        .ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(controlledDescription).ToContainTextAsync("Selected tab: settings-controlled.");
        await Assertions.Expect(page.GetByText("Parent-controlled content for Settings.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tabs_home_and_end_keys_move_to_edge_tabs_without_breaking_manual_mode()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tabs",
            static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Account", Exact = true }).First, expectedTitle: "Tabs - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Render the triggers in a side rail." }).First;
        var verticalAccount = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Account", Exact = true }).First;
        var verticalNotifications = verticalSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Notifications", Exact = true }).First;

        await verticalAccount.FocusAsync();
        await verticalAccount.PressAsync("End");

        await Assertions.Expect(verticalNotifications).ToBeFocusedAsync();
        await Assertions.Expect(verticalNotifications).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions
              .Expect(verticalSection.GetByText("Vertical layout keeps the tab list visible beside the content.", new LocatorGetByTextOptions { Exact = true }))
              .ToBeVisibleAsync();

        await verticalNotifications.PressAsync("Home");

        await Assertions.Expect(verticalAccount).ToBeFocusedAsync();
        await Assertions.Expect(verticalAccount).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(verticalSection.GetByText("This panel demonstrates vertical tab content.", new LocatorGetByTextOptions { Exact = true }))
                        .ToBeVisibleAsync();

        var manualSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Radix manual mode: arrow keys move focus; Space or Enter activates the focused tab" }).First;
        var tabA = manualSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Tab A", Exact = true });
        var tabB = manualSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Tab B", Exact = true });

        await tabA.FocusAsync();
        await tabA.PressAsync("End");

        await Assertions.Expect(tabB).ToBeFocusedAsync();
        await Assertions.Expect(tabA).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(tabB).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(manualSection.GetByText("Panel A", new LocatorGetByTextOptions { Exact = false })).ToBeVisibleAsync();

        await tabB.PressAsync("Home");

        await Assertions.Expect(tabA).ToBeFocusedAsync();
        await Assertions.Expect(tabA).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(tabB).ToHaveAttributeAsync("aria-selected", "false");
    }

    [Test]
    public async ValueTask Tabs_demo_switches_selected_trigger_and_visible_panel()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tabs",
            static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Account", Exact = true }).First, expectedTitle: "Tabs - Quark Suite");

        var demoSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "The shadcn account/password tabs layout using explicit list, trigger, and content composition." }).First;
        var account = demoSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Account", Exact = true });
        var password = demoSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Password", Exact = true });
        var accountPanel = demoSection.GetByRole(AriaRole.Tabpanel).Filter(new LocatorFilterOptions { HasText = "Make changes to your account here." });
        var passwordPanel = demoSection.GetByRole(AriaRole.Tabpanel).Filter(new LocatorFilterOptions { HasText = "Change your password here." });

        await Assertions.Expect(account).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(accountPanel).ToBeVisibleAsync();
        await Assertions.Expect(passwordPanel).ToHaveCountAsync(0);

        await password.ClickAsync();

        await Assertions.Expect(password).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(account).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(accountPanel).ToHaveCountAsync(0);
        await Assertions.Expect(passwordPanel).ToBeVisibleAsync();
        await Assertions.Expect(demoSection.GetByLabel("New password", new LocatorGetByLabelOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tabs_demo_has_no_console_or_page_errors()
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

        await page.GotoAndWaitForReady($"{BaseUrl}tabs",
            static p => p.GetByRole(AriaRole.Tablist).First,
            expectedTitle: "Tabs - Quark Suite");

        await page.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Password", Exact = true }).First.ClickAsync();
        await Assertions.Expect(page.GetByLabel("New password", new PageGetByLabelOptions { Exact = true })).ToBeVisibleAsync();

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
