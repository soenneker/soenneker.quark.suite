using System.Threading.Tasks;
using System.Collections.Generic;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDrawerPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkDrawerPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Drawer_demo_updates_goal_state_and_cancel_restores_trigger_focus()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}drawers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true }),
            expectedTitle: "Drawer - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Move Goal", Exact = true });
        var content = page.Locator("[data-slot='drawer-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Calories/day" }).First;
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-direction", "bottom");
        await Assertions.Expect(content).ToHaveAttributeAsync("data-vaul-drawer-direction", "bottom");
        await Assertions.Expect(content).ToContainTextAsync("350");

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Increase", Exact = true }).ClickAsync();
        await Assertions.Expect(content).ToContainTextAsync("360");

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Decrease", Exact = true }).ClickAsync();
        await Assertions.Expect(content).ToContainTextAsync("350");

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Cancel", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

[Test]
    public async ValueTask Drawer_sides_and_rtl_examples_emit_expected_direction_attributes_and_close_from_cancel()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}drawers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "top", Exact = true }),
            expectedTitle: "Drawer - Quark Suite");

        var sidesSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Drawers can open from any edge via `Direction`." }).First;
        foreach (var side in new[] { "top", "right", "bottom", "left" })
        {
            var sideTrigger = sidesSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = side, Exact = true });
            await sideTrigger.ClickAsync();

            var content = page.Locator("[data-slot='drawer-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Set your daily activity goal." }).Last;
            await Assertions.Expect(content).ToHaveAttributeAsync("data-direction", side);
            await Assertions.Expect(content).ToHaveAttributeAsync("data-vaul-drawer-direction", side);

            await content.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Cancel", Exact = true }).ClickAsync();
            await Assertions.Expect(content).ToBeHiddenAsync();
        }

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Drawer chrome and slide direction in RTL layouts." }).First;
        var rtlTrigger = rtlSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "افتح الدرج", Exact = true });
        await rtlTrigger.ClickAsync();

        var rtlContent = page.Locator("[data-slot='drawer-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "تعديل الملف الشخصي" }).Last;
        await Assertions.Expect(rtlContent).ToHaveAttributeAsync("data-direction", "left");
        await Assertions.Expect(rtlContent).ToHaveAttributeAsync("data-vaul-drawer-direction", "left");
        await Assertions.Expect(rtlContent).ToContainTextAsync("حدّث معلوماتك ثم احفظ التغييرات.");

        await rtlContent.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "إلغاء", Exact = true }).ClickAsync();
        await Assertions.Expect(rtlContent).ToBeHiddenAsync();
    }

    [Test]
    public async ValueTask Drawer_demo_layers_above_page_escapes_and_has_no_console_errors()
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
            $"{BaseUrl}drawers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true }),
            expectedTitle: "Drawer - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Move Goal", Exact = true });
        var content = page.Locator("[data-slot='drawer-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Calories/day" }).First;
        var overlay = page.Locator("[data-slot='drawer-overlay'][data-state='open']").First;

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-vaul-drawer-direction", "bottom");
        (await content.EvaluateAsync<int>("element => Number(getComputedStyle(element).zIndex)")).Should().BeGreaterThanOrEqualTo(50);
        (await overlay.EvaluateAsync<int>("element => Number(getComputedStyle(element).zIndex)")).Should().BeGreaterThanOrEqualTo(50);

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
