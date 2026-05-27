using AwesomeAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkContextMenuPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkContextMenuPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Context_menu_demo_opens_from_right_click_and_reveals_submenu()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/context-menu",
            static p => p.GetByText("Right click here", new PageGetByTextOptions { Exact = true }).First,
            expectedTitle: "Context Menus - Quark Suite");

        await page.GetByText("Right click here", new PageGetByTextOptions { Exact = true })
                  .First.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

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
    public async ValueTask Context_menu_basic_demo_disables_forward_item()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/context-menu",
            static p => p.GetByText("Right click here", new PageGetByTextOptions { Exact = true }).First,
            expectedTitle: "Context Menus - Quark Suite");

        var basicTrigger = page.Locator("[data-testid='context-menu-demo-trigger']");
        await basicTrigger.ScrollIntoViewIfNeededAsync();
        await basicTrigger.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

        var menu = page.Locator("[role='menu'][data-state='open']:visible").First;
        var forward = menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Forward", Exact = true });

        await Assertions.Expect(forward).ToHaveAttributeAsync("aria-disabled", "true");
        await Assertions.Expect(forward).ToHaveAttributeAsync("data-disabled", string.Empty);
        var opacity = await forward.EvaluateAsync<double>("element => Number.parseFloat(window.getComputedStyle(element).opacity)");
        opacity.Should().BeApproximately(0.5, 0.01);
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
            $"{BaseUrl}components/context-menu",
            static p => p.GetByText("Right click here", new PageGetByTextOptions { Exact = true }).First,
            expectedTitle: "Context Menus - Quark Suite");

        await page.GetByText("Right click here", new PageGetByTextOptions { Exact = true })
                  .First.ClickAsync(new LocatorClickOptions { Button = MouseButton.Right });

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
