using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkPopoverPlaywrightTests : PlaywrightUnitTest
{
    public QuarkPopoverPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Popover_demo_preserves_explicit_content_role_over_dialog_default()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open custom listbox", Exact = true }));

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open custom listbox", Exact = true });
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

        Assert.NotNull(popupState);
        Assert.Equal("listbox", popupState.role);
        Assert.Equal("Framework choices", popupState.ariaLabel);
        Assert.Equal("open", popupState.dataState);
        Assert.Equal("Astro", popupState.selectedText);
        await Assertions.Expect(page.GetByRole(AriaRole.Dialog)).ToHaveCountAsync(0);
    }

[Fact]
    public async ValueTask Popover_demo_supports_nested_popover_inside_dialog()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Dialog", Exact = true }));

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A popover opened from content inside a modal dialog." });
        ILocator dialogTrigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open Dialog", Exact = true });

        await dialogTrigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Popover Example", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        ILocator popoverTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open Popover", Exact = true });
        await popoverTrigger.ClickAsync();

        ILocator popover = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Popover in Dialog", Exact = true });
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

[Fact]
    public async ValueTask Popover_demo_closes_from_escape_after_opening_from_trigger()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true }),
            expectedTitle: "Popovers - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Default popover with a trigger and dimension fields in the content." }).First;
        ILocator trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open popover", Exact = true });
        await trigger.ClickAsync();

        ILocator content = page.Locator("[data-slot='popover-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Set the dimensions for the layer." });
        ILocator title = content.GetByText("Dimensions", new LocatorGetByTextOptions { Exact = true });
        ILocator widthInput = page.Locator("#popover-width");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(title).ToBeVisibleAsync();
        await Assertions.Expect(widthInput).ToBeFocusedAsync();

        await widthInput.PressAsync("Escape");

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();
    }

[Fact]
    public async ValueTask Popover_demo_opens_and_dismisses_on_outside_click()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true }),
            expectedTitle: "Popovers - Quark Suite");

        ILocator customRoleSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Popover content can expose a non-dialog role when the surface behaves like a picker or listbox." }).First;
        ILocator customRoleTrigger = customRoleSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open custom listbox", Exact = true });
        await Assertions.Expect(customRoleTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await customRoleTrigger.ClickAsync();
        await Assertions.Expect(customRoleTrigger).ToHaveAttributeAsync("aria-expanded", "false");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Default popover with a trigger and dimension fields in the content." }).First;
        ILocator trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open popover", Exact = true });
        await trigger.ClickAsync();

        ILocator content = page.Locator("[data-slot='popover-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Set the dimensions for the layer." });
        ILocator title = content.GetByText("Dimensions", new LocatorGetByTextOptions { Exact = true });
        await Assertions.Expect(title).ToBeVisibleAsync();

        ILocator main = page.Locator("main");
        var mainBox = await main.BoundingBoxAsync();
        Assert.NotNull(mainBox);
        await page.Mouse.ClickAsync(mainBox.X + 10, mainBox.Y + 10);

        await Assertions.Expect(content).Not.ToBeVisibleAsync();
    }

[Fact]
    public async ValueTask Popover_demo_controlled_open_state_stays_in_sync_with_external_toggle_and_outside_dismissal()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popovers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle popover", Exact = true }));

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Controlled Open State" }).First;
        ILocator toggle = page.Locator("#popover-controlled-toggle");
        ILocator trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Edit dimensions", Exact = true });
        ILocator openState = page.Locator("#popover-controlled-state");

        await toggle.ClickAsync();

        ILocator content = page.Locator("[data-slot='popover-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Update the values below and save them back to the current layer." });
        await Assertions.Expect(openState).ToContainTextAsync("Open: true");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();

        ILocator main = page.Locator("main");
        var mainBox = await main.BoundingBoxAsync();
        Assert.NotNull(mainBox);
        await page.Mouse.ClickAsync(mainBox.X + mainBox.Width - 10, mainBox.Y + mainBox.Height - 10);

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();

        await trigger.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();
    }
    private static async Task ClickJustOutsideAsync(IPage page, ILocator locator)
    {
        var box = await locator.BoundingBoxAsync();
        Assert.NotNull(box);
        var viewport = page.ViewportSize;
        float leftSpace = box.X;
        float rightSpace = (viewport?.Width ?? 0) - (box.X + box.Width);
        float topSpace = box.Y;
        float bottomSpace = (viewport?.Height ?? 0) - (box.Y + box.Height);

        float x = rightSpace >= leftSpace ? box.X + box.Width + 20 : box.X - 20;
        float y = bottomSpace >= topSpace ? box.Y + box.Height + 20 : box.Y - 20;
        await page.Mouse.ClickAsync(x, y);
    }

    private sealed class PopoverRoleProbe
    {
        public string? role { get; set; }
        public string? ariaLabel { get; set; }
        public string? dataState { get; set; }
        public string? selectedText { get; set; }
    }
}
