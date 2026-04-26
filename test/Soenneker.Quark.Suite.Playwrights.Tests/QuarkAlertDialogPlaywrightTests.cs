using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAlertDialogPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAlertDialogPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Alert_dialog_demo_stays_open_on_outside_click_and_escape()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}alert-dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true }),
            expectedTitle: "Alert Dialogs - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true }).ClickAsync();

        var dialog = page.GetByRole(AriaRole.Alertdialog, new PageGetByRoleOptions { Name = "Are you absolutely sure?", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("aria-modal", "true");
        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "open");

        await ClickJustOutsideAsync(page, dialog);
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "open");
    }

    [Test]
    public async ValueTask Alert_dialog_demo_cancel_closes()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}alert-dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true }),
            expectedTitle: "Alert Dialogs - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Alertdialog, new PageGetByRoleOptions { Name = "Are you absolutely sure?", Exact = true });
        var cancel = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Cancel", Exact = true });
        var action = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Continue", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(cancel).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("Shift+Tab");
        await Assertions.Expect(action).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("Tab");
        await Assertions.Expect(cancel).ToBeFocusedAsync();

        await cancel.ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Alert_dialog_destructive_demo_closes_from_action()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}alert-dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Delete project", Exact = true }),
            expectedTitle: "Alert Dialogs - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Delete project", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Alertdialog, new PageGetByRoleOptions { Name = "Delete this project?", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Delete", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Alert_dialog_demo_action_closes_and_restores_trigger_focus()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}alert-dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true }),
            expectedTitle: "Alert Dialogs - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Alertdialog, new PageGetByRoleOptions { Name = "Are you absolutely sure?", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Continue", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

    private static async Task ClickJustOutsideAsync(IPage page, ILocator locator)
    {
        var box = await locator.BoundingBoxAsync();
        (box).Should().NotBeNull();
        var x = box.X > 40 ? box.X - 20 : box.X + box.Width + 20;
        var y = box.Y > 40 ? box.Y - 20 : box.Y + 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
