using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDialogPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkDialogPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Dialog_demo_traps_focus_and_restores_trigger_focus_after_escape()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true }),
            expectedTitle: "Dialog - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true });

        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Edit profile", Exact = true });
        var overlay = page.Locator("[data-slot='dialog-overlay'][data-state='open']");

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("aria-modal", "true");
        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "open");
        await Assertions.Expect(overlay).ToHaveAttributeAsync("data-state", "open");
        (await WaitForDialogTabBoundaryAsync(dialog, first: true)).Should().BeTrue();

        var renderedOutsideMain = await page.EvaluateAsync<bool>(
            "() => {" +
            "const dialog = document.querySelector('[data-slot=\"dialog-content\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!dialog && document.body.contains(dialog) && !!main && !main.contains(dialog);" +
            "}");

        (renderedOutsideMain).Should().BeTrue();

        await page.Keyboard.PressAsync("Shift+Tab");
        (await WaitForDialogTabBoundaryAsync(dialog, first: false)).Should().BeTrue();

        await page.Keyboard.PressAsync("Tab");
        (await WaitForDialogTabBoundaryAsync(dialog, first: true)).Should().BeTrue();

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

    [Test]
    public async ValueTask Dialog_demo_respects_backdrop_dismiss_configuration()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true }),
            expectedTitle: "Dialog - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Edit profile", Exact = true });
        var overlay = page.Locator("[data-slot='dialog-overlay'][data-state='open']").First;
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(overlay).ToBeVisibleAsync();

        await ClickBackdropAsync(page, dialog);

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();

        var guardedTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open guarded dialog", Exact = true });
        await guardedTrigger.ClickAsync();

        var guardedDialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Delete environment", Exact = true });
        var guardedOverlay = page.Locator("[data-slot='dialog-overlay'][data-state='open']").First;
        await Assertions.Expect(guardedDialog).ToBeVisibleAsync();
        await Assertions.Expect(guardedOverlay).ToBeVisibleAsync();

        await ClickBackdropAsync(page, guardedDialog);

        await Assertions.Expect(guardedDialog).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Keep environment", Exact = true }).ClickAsync();
        await Assertions.Expect(guardedDialog).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Dialog_demo_supports_select_nested_inside_modal_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type is "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Chat Settings", Exact = true }),
            expectedTitle: "Dialog - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Chat Settings", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Chat Settings", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await WaitForBodyScrollLockAsync(page, expectedLocked: true);
        await AssertPinchZoomRemainsAllowedAsync(page);

        var selectTrigger = dialog.GetByRole(AriaRole.Combobox, new LocatorGetByRoleOptions { Name = "Theme", Exact = true });
        await selectTrigger.ClickAsync();

        var listbox = page.GetByRole(AriaRole.Listbox);
        await Assertions.Expect(selectTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        await WaitForBodyScrollLockAsync(page, expectedLocked: true);
        await AssertPinchZoomRemainsAllowedAsync(page);

        var dark = listbox.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Dark", Exact = true });
        await dark.ClickAsync();

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(selectTrigger).ToContainTextAsync("Dark");
        await WaitForBodyScrollLockAsync(page, expectedLocked: true);

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await WaitForBodyScrollLockAsync(page, expectedLocked: false);

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

    private static async Task WaitForBodyScrollLockAsync(IPage page, bool expectedLocked)
    {
        var predicate = expectedLocked
            ? "() => document.body.style.overflow === 'hidden'"
            : "() => document.body.style.overflow !== 'hidden'";

        await page.WaitForFunctionAsync(predicate);
        var overflow = await page.EvaluateAsync<string>("() => document.body.style.overflow || ''");

        if (expectedLocked)
            overflow.Should().Be("hidden");
        else
            overflow.Should().BeEmpty();
    }

    private static async Task AssertPinchZoomRemainsAllowedAsync(IPage page)
    {
        var touchAction = await page.EvaluateAsync<string>("() => document.documentElement.style.touchAction || ''");
        touchAction.Should().NotBe("none");
    }

    private static Task<bool> FocusIsWithinAsync(ILocator dialog)
    {
        return dialog.EvaluateAsync<bool>("element => element.contains(document.activeElement)");
    }

    private static async Task<bool> WaitForFocusWithinAsync(ILocator dialog, int attempts = 10)
    {
        for (var i = 0; i < attempts; i++)
        {
            if (await FocusIsWithinAsync(dialog))
                return true;

            await Task.Delay(25);
        }

        return false;
    }

    private static Task<bool> ActiveElementMatchesDialogTabBoundaryAsync(ILocator dialog, bool first)
    {
        return dialog.EvaluateAsync<bool>(
            @"(element, isFirst) => {
                const selector = [
                    'button:not([disabled])',
                    '[href]',
                    'input:not([disabled]):not([type=""hidden""])',
                    'select:not([disabled])',
                    'textarea:not([disabled])',
                    '[tabindex]:not([tabindex=""-1""])'
                ].join(',');

                const tabbables = Array.from(element.querySelectorAll(selector)).filter(node => {
                    if (!(node instanceof HTMLElement)) {
                        return false;
                    }

                    if (node.getAttribute('aria-hidden') === 'true') {
                        return false;
                    }

                    const style = window.getComputedStyle(node);
                    if (style.display === 'none' || style.visibility === 'hidden') {
                        return false;
                    }

                    return node.getClientRects().length > 0;
                });

                if (tabbables.length === 0) {
                    return false;
                }

                const boundary = isFirst ? tabbables[0] : tabbables[tabbables.length - 1];
                return document.activeElement === boundary;
            }",
            first);
    }

    private static async Task<bool> WaitForDialogTabBoundaryAsync(ILocator dialog, bool first, int attempts = 12)
    {
        for (var i = 0; i < attempts; i++)
        {
            if (await ActiveElementMatchesDialogTabBoundaryAsync(dialog, first))
                return true;

            await Task.Delay(25);
        }

        return false;
    }

    private static async Task<string> ClickBackdropAsync(IPage page, ILocator dialog)
    {
        var rectJson = await dialog.EvaluateAsync<string>(
            "element => {" +
            "const rect = element.getBoundingClientRect();" +
            "return JSON.stringify({ x: rect.x, y: rect.y, width: rect.width, height: rect.height });" +
            "}");

        using var document = JsonDocument.Parse(rectJson);
        var rect = document.RootElement;

        var x = rect.GetProperty("x").GetSingle();
        var y = rect.GetProperty("y").GetSingle();
        var width = rect.GetProperty("width").GetSingle();

        var viewportWidth = page.ViewportSize?.Width ?? 1280;

        var clickX = x > 40 ? x - 20 : x + width + 20;
        if (clickX < 8)
            clickX = 8;
        else if (clickX > viewportWidth - 8)
            clickX = viewportWidth - 8;

        var clickY = y > 40 ? y - 20 : y + 20;
        if (clickY < 8)
            clickY = 8;

        var pointJson = JsonSerializer.Serialize(new { x = clickX, y = clickY });
        var targetDebug = await page.EvaluateAsync<string>(
            "(json) => {" +
            "const point = JSON.parse(json);" +
            "const element = document.elementFromPoint(point.x, point.y);" +
            "if (!element) return 'null';" +
            "const id = element.id ?? '';" +
            "const slot = element.getAttribute('data-slot') ?? '';" +
            "return `${element.tagName.toLowerCase()}#${id}[data-slot=${slot}] class=${element.className}`;" +
            "}",
            pointJson);

        await page.Mouse.ClickAsync(clickX, clickY, new MouseClickOptions { Button = MouseButton.Left });
        return $"rect={rectJson}; click=({clickX}, {clickY}); target={targetDebug}";
    }

}
