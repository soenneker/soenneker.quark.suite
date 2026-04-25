using System.Threading.Tasks;
using System.Collections.Generic;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
[NotInParallel]
public sealed class QuarkSonnerPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSonnerPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Sonner_demo_action_button_dismisses_the_rendered_toast()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Toast", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Toast", Exact = true }).ClickAsync();

        var toast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Event has been created" }).First;
        await Assertions.Expect(toast).ToBeVisibleAsync();
        await Assertions.Expect(toast).ToContainTextAsync("Sunday, December 03, 2023 at 9:00 AM");

        await toast.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Undo", Exact = true }).ClickAsync();

        await Assertions.Expect(toast).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Sonner_action_demo_runs_callback_and_shows_follow_up_toast()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "With action", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "With action", Exact = true }).ClickAsync();

        var archivedToast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Message archived" }).First;
        await Assertions.Expect(archivedToast).ToBeVisibleAsync();
        await Assertions.Expect(archivedToast).ToContainTextAsync("You can undo this action from the toast.");

        await archivedToast.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Undo", Exact = true }).ClickAsync();

        await Assertions.Expect(archivedToast).Not.ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Archive undone" }).First).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Sonner_promise_demo_replaces_loading_state_with_success_result()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Promise success", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Promise success", Exact = true }).ClickAsync();

        var loadingToast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Saving changes..." }).First;
        await Assertions.Expect(loadingToast).ToBeVisibleAsync();

        var successToast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Changes saved" }).First;
        await Assertions.Expect(successToast).ToBeVisibleAsync();
        await Assertions.Expect(successToast).ToContainTextAsync("This mirrors Sonner's loading to result flow.");
        await Assertions.Expect(loadingToast).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Sonner_demo_layers_toasts_and_has_no_console_errors()
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
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Toast", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Toast", Exact = true }).ClickAsync();

        var toaster = page.Locator("ol[data-sonner-toaster='true']").First;
        var toast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Event has been created" }).First;

        await Assertions.Expect(toaster).ToHaveAttributeAsync("class", "toaster group");
        await Assertions.Expect(toaster).ToHaveAttributeAsync("data-y-position", "top");
        await Assertions.Expect(toaster).ToHaveAttributeAsync("data-x-position", "center");
        await Assertions.Expect(page.Locator("section[aria-live='polite']").First).ToHaveAttributeAsync("aria-label", "Notifications alt+T");
        await Assertions.Expect(toast).ToBeVisibleAsync();
        (await toast.EvaluateAsync<int>("element => Number(getComputedStyle(element).zIndex) || Number(getComputedStyle(element.parentElement).zIndex) || 0"))
            .Should().BeGreaterThanOrEqualTo(0);

        await page.Keyboard.PressAsync("Alt+T");
        await page.WaitForFunctionAsync("() => document.activeElement?.matches('[data-sonner-toast]') === true");
        await Assertions.Expect(toast).ToBeFocusedAsync();
        await Assertions.Expect(toast).ToHaveAttributeAsync("data-expanded", "true");

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Sonner_short_duration_toast_pauses_on_hover_and_resumes_on_leave()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Short duration", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Short duration", Exact = true }).ClickAsync();

        var matchingToasts = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Paused timer" });
        var toast = matchingToasts.First;
        await Assertions.Expect(toast).ToBeVisibleAsync();

        await toast.HoverAsync();
        await Assertions.Expect(toast).ToHaveAttributeAsync("data-expanded", "true");
        await page.WaitForTimeoutAsync(1100);
        await Assertions.Expect(toast).ToBeVisibleAsync();

        await page.Mouse.MoveAsync(10, 10);
        await Assertions.Expect(toast).ToHaveAttributeAsync("data-expanded", "false");
        await Assertions.Expect(matchingToasts).ToHaveCountAsync(0, new LocatorAssertionsToHaveCountOptions { Timeout = 3000 });
    }

    [Test]
    public async ValueTask Sonner_toast_can_be_swiped_away_without_clicking_actions()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Swipe toast", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Dismiss all", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-sonner-toast]")).ToHaveCountAsync(0, new LocatorAssertionsToHaveCountOptions { Timeout = 3000 });

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Swipe toast", Exact = true }).ClickAsync();

        var matchingToasts = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Swipe me away" });
        var toast = matchingToasts.First;
        await Assertions.Expect(toast).ToBeVisibleAsync();
        await Assertions.Expect(toast).ToHaveAttributeAsync("data-dismissible", "true");
        await page.WaitForFunctionAsync("() => document.querySelector(\"section[aria-live='polite']\")?.__quarkSonnerSwipeRegistered === true");

        var box = await toast.BoundingBoxAsync();
        box.Should().NotBeNull();

        await toast.EvaluateAsync(
            @"element => {
                const pointerId = 37;
                const rect = element.getBoundingClientRect();
                const startX = rect.left + 24;
                const startY = rect.top + rect.height / 2;
                const base = {
                    pointerId,
                    pointerType: 'mouse',
                    button: 0,
                    buttons: 1,
                    clientX: startX,
                    clientY: startY,
                    bubbles: true,
                    cancelable: true
                };

                element.dispatchEvent(new PointerEvent('pointerdown', base));
                element.dispatchEvent(new PointerEvent('pointermove', { ...base, clientX: startX + 180 }));
                element.dispatchEvent(new PointerEvent('pointerup', { ...base, buttons: 0, clientX: startX + 180 }));
            }");

        await Assertions.Expect(toast).ToHaveAttributeAsync("data-swipe-out", "true");
        await Assertions.Expect(matchingToasts).ToHaveCountAsync(0, new LocatorAssertionsToHaveCountOptions { Timeout = 3000 });
    }
}
