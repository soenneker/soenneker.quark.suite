using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkPopoverPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkPopoverPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Popover_smoke_aschild_trigger_closes_default_open_popover_without_runtime_error()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var sawRuntimeError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error" && message.Text.Contains("StackOverflowException", StringComparison.Ordinal))
                sawRuntimeError = true;
        };

        page.PageError += (_, _) => sawRuntimeError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popover-smoke",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true }),
            expectedTitle: "Popover Smoke - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true });
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        await trigger.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        (sawRuntimeError).Should().BeFalse();
    }

[Test]
    public async ValueTask Popover_demo_preserves_explicit_content_role_over_dialog_default()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open custom listbox", Exact = true }));

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open custom listbox", Exact = true });
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        var popupState = await page.EvaluateAsync<PopoverRoleProbe>(
            @"() => {
                const popup = document.querySelector('[role=""listbox""][aria-label=""Framework choices""]');
                const astro = popup?.querySelector('[role=""option""][aria-selected=""true""]');

                return {
                    role: popup?.getAttribute('role'),
                    ariaLabel: popup?.getAttribute('aria-label'),
                    dataState: popup?.getAttribute('data-state'),
                    selectedText: astro?.textContent?.trim()
                };
            }");

        (popupState).Should().NotBeNull();
        (popupState.role).Should().Be("listbox");
        (popupState.ariaLabel).Should().Be("Framework choices");
        (popupState.dataState).Should().Be("open");
        (popupState.selectedText).Should().Be("Astro");
        await Assertions.Expect(page.GetByRole(AriaRole.Dialog)).ToHaveCountAsync(0);
    }

[Test]
    public async ValueTask Popover_demo_supports_nested_popover_inside_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true }));

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A popover opened from content inside a modal dialog." });
        var dialogTrigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open Dialog", Exact = true });

        await dialogTrigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Popover Example", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        var popoverTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open Popover", Exact = true });
        await popoverTrigger.ClickAsync();

        var popover = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Popover in Dialog", Exact = true });
        await Assertions.Expect(popoverTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(popover).ToBeVisibleAsync();

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(popover).Not.ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(popoverTrigger).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(dialogTrigger).ToBeFocusedAsync();
    }

[Test]
    public async ValueTask Popover_demo_closes_from_escape_after_opening_from_trigger()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            DemoTrigger,
            expectedTitle: "Popovers - Quark Suite");

        await page.WaitForFunctionAsync("() => !!globalThis.FloatingUICore && !!globalThis.FloatingUIDOM");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Displays rich content in a portal, triggered by a button." }).First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open popover", Exact = true });
        await trigger.ClickAsync();

        var contentId = await trigger.GetAttributeAsync("aria-controls");
        contentId.Should().NotBeNullOrWhiteSpace();
        var content = page.Locator($"#{contentId}");
        var title = content.GetByText("Dimensions", new LocatorGetByTextOptions { Exact = true });
        var widthInput = page.Locator("#popover-width");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(title).ToBeVisibleAsync();
        await Assertions.Expect(widthInput).ToBeFocusedAsync();

        await widthInput.PressAsync("Escape");

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Popover_demo_opens_and_dismisses_on_outside_click()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            DemoTrigger,
            expectedTitle: "Popovers - Quark Suite");

        var customRoleSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Popover content can expose a non-dialog role when the surface behaves like a picker or listbox." }).First;
        var customRoleTrigger = customRoleSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open custom listbox", Exact = true });
        await Assertions.Expect(customRoleTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await customRoleTrigger.ClickAsync();
        await Assertions.Expect(customRoleTrigger).ToHaveAttributeAsync("aria-expanded", "false");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Displays rich content in a portal, triggered by a button." }).First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open popover", Exact = true });
        await trigger.ClickAsync();

        var content = page.Locator("[data-slot='popover-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Set the dimensions for the layer." });
        var title = content.GetByText("Dimensions", new LocatorGetByTextOptions { Exact = true });
        await Assertions.Expect(title).ToBeVisibleAsync();

        await page.Locator("h1").First.ClickAsync();

        await Assertions.Expect(content).Not.ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Popover_demo_controlled_open_state_stays_in_sync_with_external_toggle_and_outside_dismissal()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle popover", Exact = true }));

        var toggle = page.Locator("#popover-controlled-toggle");
        var openState = page.Locator("#popover-controlled-state");

        await toggle.ClickAsync();

        var content = page.Locator("[data-slot='popover-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Update the values below and save them back to the current layer." });
        var contentId = await content.GetAttributeAsync("id");
        (string.IsNullOrWhiteSpace(contentId)).Should().BeFalse();
        var trigger = page.Locator($"button[aria-controls='{contentId}']");
        await Assertions.Expect(openState).ToContainTextAsync("Open: true");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();

        await page.Locator("h1").First.ClickAsync();

        await Assertions.Expect(openState).ToContainTextAsync("Open: false");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();

        await trigger.ClickAsync();

        await Assertions.Expect(openState).ToContainTextAsync("Open: true");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();
    }
    private sealed class PopoverRoleProbe
    {
        public string? role { get; set; }
        public string? ariaLabel { get; set; }
        public string? dataState { get; set; }
        public string? selectedText { get; set; }
    }

    private static ILocator DemoTrigger(IPage page) =>
        page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Displays rich content in a portal, triggered by a button." })
            .First.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open popover", Exact = true });
}
