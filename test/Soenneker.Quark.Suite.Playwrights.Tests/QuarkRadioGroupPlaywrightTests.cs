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
public sealed class QuarkRadioGroupPlaywrightTests : PlaywrightUnitTest
{
    public QuarkRadioGroupPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Radio_group_form_demo_requires_selection_and_submits_selected_value()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Notification type", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        ILocator formSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Form-style radio group with validation and submitted value preview." }).First;
        ILocator submit = formSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true });
        ILocator form = formSection.Locator("form");

        await submit.ClickAsync();
        await Assertions.Expect(form).ToContainTextAsync("You need to select a notification type.");

        ILocator notificationGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Notification type", Exact = true });
        ILocator nothingRadio = notificationGroup.GetByRole(AriaRole.Radio).Nth(2);

        await nothingRadio.ClickAsync();

        await Assertions.Expect(form).Not.ToContainTextAsync("You need to select a notification type.");
        await Assertions.Expect(nothingRadio).ToHaveAttributeAsync("aria-checked", "true");

        await submit.ClickAsync();

        await Assertions.Expect(formSection.Locator("pre")).ToContainTextAsync("\"type\": \"none\"");
    }

[Fact]
    public async ValueTask Radio_form_requires_selection_and_updates_checked_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        ILocator formSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Form-style radio group with validation and submitted value preview." }).First;
        ILocator submit = formSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true });

        await submit.ClickAsync();
        await Assertions.Expect(formSection.GetByText("You need to select a notification type.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        ILocator mentions = formSection.GetByRole(AriaRole.Radio, new LocatorGetByRoleOptions { Name = "Direct messages and mentions", Exact = true });
        ILocator all = formSection.GetByRole(AriaRole.Radio, new LocatorGetByRoleOptions { Name = "All new messages", Exact = true });

        await mentions.ClickAsync();

        await Assertions.Expect(mentions).ToHaveAttributeAsync("data-state", "checked");
        await Assertions.Expect(all).ToHaveAttributeAsync("data-state", "unchecked");

        await submit.ClickAsync();
        await Assertions.Expect(formSection.Locator("pre").First).ToContainTextAsync("\"type\": \"mentions\"");
    }

[Fact]
    public async ValueTask Radio_group_demo_home_and_end_keys_move_selection_to_edge_options()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Density options", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        ILocator densityGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Density options", Exact = true });
        ILocator defaultRadio = densityGroup.GetByRole(AriaRole.Radio).Nth(0);
        ILocator comfortableRadio = densityGroup.GetByRole(AriaRole.Radio).Nth(1);
        ILocator compactRadio = densityGroup.GetByRole(AriaRole.Radio).Nth(2);

        await Assertions.Expect(comfortableRadio).ToHaveAttributeAsync("aria-checked", "true");

        await compactRadio.FocusAsync();
        await page.Keyboard.PressAsync("Home");

        await Assertions.Expect(defaultRadio).ToBeFocusedAsync();
        await Assertions.Expect(comfortableRadio).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(defaultRadio).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(compactRadio).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("End");

        await Assertions.Expect(compactRadio).ToBeFocusedAsync();
        await Assertions.Expect(comfortableRadio).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(defaultRadio).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(compactRadio).ToHaveAttributeAsync("aria-checked", "false");

        ILocator disabledGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true });
        ILocator disabledOption = disabledGroup.GetByRole(AriaRole.Radio).Nth(0);
        ILocator option2 = disabledGroup.GetByRole(AriaRole.Radio).Nth(1);
        ILocator option3 = disabledGroup.GetByRole(AriaRole.Radio).Nth(2);

        await option2.FocusAsync();
        await page.Keyboard.PressAsync("Home");

        await Assertions.Expect(option2).ToBeFocusedAsync();
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("End");

        await Assertions.Expect(option3).ToBeFocusedAsync();
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(option3).ToHaveAttributeAsync("aria-checked", "false");
    }

[Fact]
    public async ValueTask Radio_group_demo_disabled_item_stays_unchecked_while_enabled_option_changes()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        ILocator disabledGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true });
        ILocator disabledOption = disabledGroup.GetByRole(AriaRole.Radio).Nth(0);
        ILocator option2 = disabledGroup.GetByRole(AriaRole.Radio).Nth(1);
        ILocator option3 = disabledGroup.GetByRole(AriaRole.Radio).Nth(2);

        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("disabled", "");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");

        await disabledOption.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");

        await option3.ClickAsync();

        await Assertions.Expect(option3).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "false");
    }
}
