using AwesomeAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCommandPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCommandPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Command_dialog_demo_selects_item_and_closes_overlay()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true }),
            expectedTitle: "Command - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Workspace actions", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        var input = dialog.GetByRole(AriaRole.Combobox);
        await input.FillAsync("bill");

        await dialog.GetByText("Billing", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Dialog selection:", new PageGetByTextOptions { Exact = false })).ToContainTextAsync("Billing");
    }

    [Test]
    public async ValueTask Command_demo_filters_items_and_preserves_disabled_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Command - Quark Suite");

        var command = page.Locator("[data-slot='command']").First;
        var input = command.GetByRole(AriaRole.Combobox);
        await input.FillAsync("zzzz");

        await Assertions.Expect(command.GetByText("No results found.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await input.FillAsync(string.Empty);

        var calculator = command.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Calculator", Exact = false });
        await Assertions.Expect(calculator).ToHaveAttributeAsync("data-disabled", "true");
        await Assertions.Expect(calculator).ToHaveAttributeAsync("aria-disabled", "true");
    }

    [Test]
    public async ValueTask Command_demo_keyboard_navigation_keeps_input_focus_and_selects_active_item()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Command - Quark Suite");

        var command = page.Locator("[data-slot='command']").First;
        var input = command.GetByRole(AriaRole.Combobox);
        await input.FocusAsync();

        var searchEmoji = command.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Search Emoji", Exact = false });
        var profile = command.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Profile", Exact = false });

        await page.Keyboard.PressAsync("ArrowDown");

        await Assertions.Expect(input).ToBeFocusedAsync();
        await Assertions.Expect(searchEmoji).ToHaveAttributeAsync("data-selected", "true");
        var searchEmojiId = await searchEmoji.GetAttributeAsync("id");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-activedescendant", searchEmojiId!);

        await page.Keyboard.PressAsync("ArrowDown");

        await Assertions.Expect(input).ToBeFocusedAsync();
        await Assertions.Expect(profile).ToHaveAttributeAsync("data-selected", "true");
        var profileId = await profile.GetAttributeAsync("id");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-activedescendant", profileId!);

        await page.Keyboard.PressAsync("Enter");

        await Assertions.Expect(page.GetByText("Last action:", new PageGetByTextOptions { Exact = false })).ToContainTextAsync("Profile");
        await Assertions.Expect(input).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Command_dialog_demo_portals_above_page_and_has_no_console_errors()
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
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true }),
            expectedTitle: "Command - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Workspace actions", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("aria-modal", "true");

        await page.WaitForFunctionAsync(
            "() => {" +
            "const dialog = document.querySelector('[data-slot=\"dialog-content\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "if (!dialog || !main || main.contains(dialog)) return false;" +
            "return Number.parseInt(getComputedStyle(dialog).zIndex, 10) >= 50;" +
            "}");

        await page.WaitForFunctionAsync(
            "() => !!document.querySelector('[data-slot=\"dialog-content\"][data-state=\"open\"]')" +
            "?.closest('[data-bradix-dismissable-layer-ready=\"true\"]')");

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
