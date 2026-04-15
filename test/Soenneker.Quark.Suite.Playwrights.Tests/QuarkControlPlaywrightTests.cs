using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkControlPlaywrightTests : PlaywrightUnitTest
{
    public QuarkControlPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Checkbox_indeterminate_demo_select_all_and_child_updates_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Checkbox, new PageGetByRoleOptions { Name = "Select all", Exact = true }),
            expectedTitle: "Checkbox Component - Quark Suite");

        ILocator selectAll = page.Locator("#select-all");
        ILocator read = page.Locator("#item-read");
        ILocator write = page.Locator("#item-write");
        ILocator execute = page.Locator("#item-execute");

        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "mixed");

        await selectAll.ClickAsync();

        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(read).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "true");

        await write.ClickAsync();

        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "mixed");
    }

    [Fact]
    public async ValueTask Checkbox_form_multiple_requires_one_selection_and_submits_checked_items()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }).First,
            expectedTitle: "Checkbox Component - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Form Multiple" }).First;
        ILocator recents = section.Locator("[role='checkbox']").Nth(0);
        ILocator home = section.Locator("[role='checkbox']").Nth(1);
        ILocator submit = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true });

        await recents.ClickAsync();
        await home.ClickAsync();
        await submit.ClickAsync();

        await Assertions.Expect(section.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        ILocator downloads = section.Locator("[role='checkbox']").Nth(4);
        await downloads.ClickAsync();
        await submit.ClickAsync();

        ILocator json = section.Locator("pre").First;
        await Assertions.Expect(json).ToContainTextAsync("\"downloads\"");
        await Assertions.Expect(section.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToHaveCountAsync(0);
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
    public async ValueTask Switch_form_toggles_marketing_and_preserves_disabled_security_value()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}switches",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }),
            expectedTitle: "Switches - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Settings-style rows with descriptions and a submitted value preview." }).First;
        ILocator marketing = section.Locator("[role='switch']").Nth(0);
        ILocator security = section.Locator("[role='switch']").Nth(1);

        await Assertions.Expect(marketing).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(security).ToHaveAttributeAsync("aria-checked", "true");

        await marketing.ClickAsync();
        await Assertions.Expect(marketing).ToHaveAttributeAsync("aria-checked", "true");

        await security.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(security).ToHaveAttributeAsync("aria-checked", "true");

        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true }).ClickAsync();

        ILocator json = section.Locator("pre").First;
        await Assertions.Expect(json).ToContainTextAsync("\"marketing_emails\": true");
        await Assertions.Expect(json).ToContainTextAsync("\"security_emails\": true");
    }

    [Fact]
    public async ValueTask Slider_demo_thumb_interaction_updates_value_and_disabled_slider_does_not_move()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        ILocator demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).First;
        ILocator demoSlider = demoSection.GetByRole(AriaRole.Slider).First;
        await Assertions.Expect(demoSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        await demoSlider.FocusAsync();
        for (var i = 0; i < 5; i++)
            await demoSlider.PressAsync("ArrowRight");
        await Assertions.Expect(demoSlider).ToHaveAttributeAsync("aria-valuenow", "55");
        await Assertions.Expect(demoSection).ToContainTextAsync("Current: 55.");

        ILocator disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Set Disabled to prevent interaction." }).First;
        ILocator disabledSlider = disabledSection.GetByRole(AriaRole.Slider).First;

        await Assertions.Expect(disabledSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        await disabledSlider.FocusAsync();
        await disabledSlider.PressAsync("ArrowRight");
        await Assertions.Expect(disabledSlider).ToHaveAttributeAsync("aria-valuenow", "50");
    }
}
