using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSheetPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkSheetPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Sheet_demo_uses_portal_and_closes_on_outside_click()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sheets",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Sheet - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First;
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Edit profile", Exact = true }).First;
        var content = page.Locator("[data-slot='sheet-content'][data-state='open']").First;

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-side", "right");

        await page.WaitForFunctionAsync(
            "() => {" +
            "const content = document.querySelector('[data-slot=\"sheet-content\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!content && document.body.contains(content) && !!main && !main.contains(content);" +
            "}");

        await ClickJustOutside(page, dialog);

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Sheet_scrollable_demo_respects_bound_visibility_and_close_button_dismiss()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sheets",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open activity drawer", Exact = true }),
            expectedTitle: "Sheet - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open activity drawer", Exact = true }).ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Recent activity", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog.GetByText("Review a longer stream of events without leaving the current page.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Close", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Sheet_demo_escape_restores_focus_layers_above_page_and_has_no_console_errors()
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
            $"{BaseUrl}sheets",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Sheet - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First;
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Edit profile", Exact = true }).First;
        var content = page.Locator("[data-slot='sheet-content'][data-state='open']").First;
        var overlay = page.Locator("[data-slot='sheet-overlay'][data-state='open']").First;

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-side", "right");
        (await content.EvaluateAsync<int>("element => Number(getComputedStyle(element).zIndex)")).Should().BeGreaterThanOrEqualTo(50);
        (await overlay.EvaluateAsync<int>("element => Number(getComputedStyle(element).zIndex)")).Should().BeGreaterThanOrEqualTo(50);

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

    private static async Task ClickJustOutside(IPage page, ILocator locator)
    {
        var box = await locator.BoundingBoxAsync();
        (box).Should().NotBeNull();
        var x = box.X > 40 ? box.X - 20 : box.X + box.Width + 20;
        var y = box.Y > 40 ? box.Y - 20 : box.Y + 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
