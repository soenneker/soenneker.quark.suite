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
public sealed class QuarkTogglePlaywrightTests : PlaywrightUnitTest
{
    public QuarkTogglePlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Toggle_demo_updates_pressed_state_and_bound_label()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}toggles",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Additional examples" }).GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle bookmark", Exact = true }),
            expectedTitle: "Toggle - Quark Suite");

        ILocator bookmarkToggle = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Additional examples" }).GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle bookmark", Exact = true });

        await Assertions.Expect(bookmarkToggle).ToHaveAttributeAsync("aria-pressed", "false");
        await Assertions.Expect(bookmarkToggle).ToContainTextAsync("Bookmark");

        await bookmarkToggle.ClickAsync();

        await Assertions.Expect(bookmarkToggle).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(bookmarkToggle).ToContainTextAsync("Saved");

        await bookmarkToggle.ClickAsync();

        await Assertions.Expect(bookmarkToggle).ToHaveAttributeAsync("aria-pressed", "false");
        await Assertions.Expect(bookmarkToggle).ToContainTextAsync("Bookmark");
    }

[Fact]
    public async ValueTask Toggle_demo_disabled_and_controlled_examples_stay_in_sync()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}toggles",
            static p => p.Locator("[data-testid='toggle-controlled-demo']"),
            expectedTitle: "Toggle - Quark Suite");

        ILocator disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disabled" }).First;
        ILocator disabledToggle = disabledSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle disabled", Exact = true }).First;

        await Assertions.Expect(disabledToggle).ToHaveAttributeAsync("disabled", "");
        await Assertions.Expect(disabledToggle).ToHaveAttributeAsync("aria-pressed", "false");

        await disabledToggle.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledToggle).ToHaveAttributeAsync("aria-pressed", "false");

        ILocator controlledDemo = page.Locator("[data-testid='toggle-controlled-demo']");
        ILocator controlledToggle = controlledDemo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle notifications", Exact = true });
        ILocator state = controlledDemo.Locator("#toggle-controlled-state");
        ILocator setOn = controlledDemo.Locator("#toggle-controlled-on");
        ILocator setOff = controlledDemo.Locator("#toggle-controlled-off");

        await Assertions.Expect(controlledToggle).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(state).ToContainTextAsync("Pressed: true");

        await setOff.ClickAsync();

        await Assertions.Expect(controlledToggle).ToHaveAttributeAsync("aria-pressed", "false");
        await Assertions.Expect(state).ToContainTextAsync("Pressed: false");

        await controlledToggle.ClickAsync();

        await Assertions.Expect(controlledToggle).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(state).ToContainTextAsync("Pressed: true");

        await setOff.ClickAsync();
        await setOn.ClickAsync();

        await Assertions.Expect(controlledToggle).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(state).ToContainTextAsync("Pressed: true");
    }
}
