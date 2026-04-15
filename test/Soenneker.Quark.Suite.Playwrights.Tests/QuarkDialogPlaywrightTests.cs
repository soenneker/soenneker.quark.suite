using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkDialogPlaywrightTests : PlaywrightUnitTest
{
    public QuarkDialogPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Dialog_demo_traps_focus_and_restores_trigger_focus_after_escape()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true }),
            expectedTitle: "Dialog - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true });

        await trigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Edit profile", Exact = true });
        ILocator overlay = page.Locator("[data-slot='dialog-overlay'][data-state='open']");

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("aria-modal", "true");
        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "open");
        await Assertions.Expect(overlay).ToHaveAttributeAsync("data-state", "open");

        bool renderedOutsideMain = await page.EvaluateAsync<bool>(
            "() => {" +
            "const dialog = document.querySelector('[data-slot=\"dialog-content\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!dialog && document.body.contains(dialog) && !!main && !main.contains(dialog);" +
            "}");

        Assert.True(renderedOutsideMain);

        for (var i = 0; i < 6; i++)
        {
            await page.Keyboard.PressAsync("Tab");
            Assert.True(await FocusIsWithinAsync(dialog));
        }

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

    [Fact]
    public async ValueTask Dialog_demo_respects_backdrop_dismiss_configuration()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true }),
            expectedTitle: "Dialog - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true });
        await trigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Edit profile", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await ClickJustOutsideActiveDialogAsync(page, dialog);

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();

        ILocator guardedTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open guarded dialog", Exact = true });
        await guardedTrigger.ClickAsync();

        ILocator guardedDialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Delete environment", Exact = true });
        await Assertions.Expect(guardedDialog).ToBeVisibleAsync();

        await ClickJustOutsideActiveDialogAsync(page, guardedDialog);

        await Assertions.Expect(guardedDialog).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Keep environment", Exact = true }).ClickAsync();
        await Assertions.Expect(guardedDialog).Not.ToBeVisibleAsync();
    }

    private static Task<bool> FocusIsWithinAsync(ILocator dialog)
    {
        return dialog.EvaluateAsync<bool>("element => element.contains(document.activeElement)");
    }

    private static async Task ClickJustOutsideActiveDialogAsync(IPage page, ILocator dialog)
    {
        var box = await dialog.BoundingBoxAsync();
        Assert.NotNull(box);
        float x = box.X > 40 ? box.X - 20 : box.X + box.Width + 20;
        float y = box.Y > 40 ? box.Y - 20 : box.Y + 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
