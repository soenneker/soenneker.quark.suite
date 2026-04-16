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
        ILocator overlay = page.Locator("[data-slot='dialog-overlay'][data-state='open']").First;
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(overlay).ToBeVisibleAsync();

        await ClickBackdropAsync(page, dialog, overlay);

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();

        ILocator guardedTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open guarded dialog", Exact = true });
        await guardedTrigger.ClickAsync();

        ILocator guardedDialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Delete environment", Exact = true });
        ILocator guardedOverlay = page.Locator("[data-slot='dialog-overlay'][data-state='open']").First;
        await Assertions.Expect(guardedDialog).ToBeVisibleAsync();
        await Assertions.Expect(guardedOverlay).ToBeVisibleAsync();

        await ClickBackdropAsync(page, guardedDialog, guardedOverlay);

        await Assertions.Expect(guardedDialog).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Keep environment", Exact = true }).ClickAsync();
        await Assertions.Expect(guardedDialog).Not.ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Dialog_demo_supports_select_nested_inside_modal_dialog()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Chat Settings", Exact = true }),
            expectedTitle: "Dialog - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Chat Settings", Exact = true });
        await trigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Chat Settings", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        ILocator selectTrigger = dialog.GetByRole(AriaRole.Combobox, new LocatorGetByRoleOptions { Name = "Theme", Exact = true });
        await selectTrigger.ClickAsync();

        ILocator listbox = page.GetByRole(AriaRole.Listbox);
        await Assertions.Expect(selectTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(listbox).ToBeVisibleAsync();

        ILocator dark = listbox.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Dark", Exact = true });
        await dark.ClickAsync();

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(selectTrigger).ToContainTextAsync("Dark");
    }

    private static Task<bool> FocusIsWithinAsync(ILocator dialog)
    {
        return dialog.EvaluateAsync<bool>("element => element.contains(document.activeElement)");
    }

    private static async Task ClickBackdropAsync(IPage page, ILocator dialog, ILocator overlay)
    {
        var dialogBox = await dialog.BoundingBoxAsync();
        var overlayBox = await overlay.BoundingBoxAsync();

        Assert.NotNull(dialogBox);
        Assert.NotNull(overlayBox);

        float x = overlayBox.X + 24;
        float y = overlayBox.Y + 24;

        bool topLeftHitsDialog = x >= dialogBox.X && x <= dialogBox.X + dialogBox.Width &&
                                 y >= dialogBox.Y && y <= dialogBox.Y + dialogBox.Height;

        if (topLeftHitsDialog)
        {
            x = overlayBox.X + overlayBox.Width - 24;
            y = overlayBox.Y + overlayBox.Height - 24;
        }

        await page.Mouse.ClickAsync(x, y);
    }

}
