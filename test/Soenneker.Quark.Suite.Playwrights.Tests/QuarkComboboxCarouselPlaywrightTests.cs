using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkComboboxCarouselPlaywrightTests : PlaywrightUnitTest
{
    public QuarkComboboxCarouselPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Combobox_input_demo_filters_and_selects_framework()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Select framework...").First,
            expectedTitle: "Combobox - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Input combobox" }).First;
        ILocator input = section.GetByPlaceholder("Select framework...");
        await input.FillAsync("nuxt");

        ILocator listbox = page.Locator("[role='listbox'][data-state='open']").Last;
        ILocator nuxt = listbox.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Nuxt.js", Exact = true });

        await Assertions.Expect(input).ToHaveValueAsync("nuxt");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(nuxt).ToBeVisibleAsync();
        await Assertions.Expect(listbox.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Next.js", Exact = true })).ToHaveCountAsync(0);

        await nuxt.ClickAsync();

        await Assertions.Expect(input).ToHaveValueAsync("Nuxt.js");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.GetByText("Selected value:", new PageGetByTextOptions { Exact = false })).ToContainTextAsync("Nuxt.js");
    }

    [Fact]
    public async ValueTask Combobox_multiple_demo_adds_and_removes_chip_selection()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Add frameworks...").First,
            expectedTitle: "Combobox - Quark Suite");

        ILocator input = page.GetByPlaceholder("Add frameworks...").First;
        await input.FillAsync("astro");

        ILocator listbox = page.Locator("[role='listbox'][data-state='open']").First;
        await listbox.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Astro", Exact = true }).ClickAsync();

        ILocator selectedFrameworks = page.GetByText("Selected frameworks:", new PageGetByTextOptions { Exact = false }).First;
        await Assertions.Expect(selectedFrameworks).ToContainTextAsync("Next.js, Astro");

        ILocator astroChip = page.Locator("[data-slot='combobox-chip'][data-value='Astro']").First;
        await Assertions.Expect(astroChip).ToBeVisibleAsync();

        await astroChip.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Remove selection", Exact = true }).ClickAsync();

        await Assertions.Expect(page.Locator("[data-slot='combobox-chip'][data-value='Astro']")).ToHaveCountAsync(0);
        await Assertions.Expect(selectedFrameworks).Not.ToContainTextAsync("Astro");
    }

    [Fact]
    public async ValueTask Combobox_disabled_demo_does_not_open_results()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Unavailable"),
            expectedTitle: "Combobox - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disabled comboboxes keep the same visual treatment without opening the menu." }).First;
        ILocator disabledInput = section.GetByPlaceholder("Unavailable");
        ILocator disabledTrigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open combobox", Exact = true }).Last;

        await disabledInput.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledInput).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(disabledTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(section.Locator("[role='listbox'][data-state='open']")).ToHaveCountAsync(0);
    }

    [Fact]
    public async ValueTask Combobox_in_dialog_filters_and_selects_without_closing_dialog()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open dialog", Exact = true }),
            expectedTitle: "Combobox - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open dialog", Exact = true }).ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Select framework", Exact = true });
        ILocator input = dialog.GetByPlaceholder("Select framework...");

        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await input.FillAsync("remix");
        ILocator listbox = page.Locator("[role='listbox'][data-state='open']").Last;
        await Assertions.Expect(listbox.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Remix", Exact = true })).ToHaveAttributeAsync("data-highlighted", "");
        await input.PressAsync("Enter");

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(input).ToHaveValueAsync("Remix");
    }

    [Fact]
    public async ValueTask Carousel_demo_advances_and_disables_navigation_at_bounds()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}carousels",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Next slide", Exact = true }).First,
            expectedTitle: "Carousel - Quark Suite");

        ILocator demo = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The default numbered-card carousel." }).First;
        ILocator previous = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Previous slide", Exact = true });
        ILocator next = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });
        ILocator track = demo.Locator("[data-slot='carousel-content'] > div").First;

        await Assertions.Expect(previous).ToBeDisabledAsync();
        await Assertions.Expect(track).ToHaveAttributeAsync("style", "transform: translateX(-0%); transition: transform 300ms ease-in-out");

        await next.ClickAsync();
        await Assertions.Expect(previous).ToBeEnabledAsync();
        await Assertions.Expect(track).ToHaveAttributeAsync("style", "transform: translateX(-20%); transition: transform 300ms ease-in-out");

        for (var i = 0; i < 3; i++)
            await next.ClickAsync();

        await Assertions.Expect(next).ToBeDisabledAsync();
        await Assertions.Expect(track).ToHaveAttributeAsync("style", "transform: translateX(-80%); transition: transform 300ms ease-in-out");
    }

    [Fact]
    public async ValueTask Carousel_loop_demo_wraps_from_last_slide_back_to_first()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}carousels",
            static p => p.GetByText("Slide 1 of 5", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Carousel - Quark Suite");

        ILocator apiSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The API callback now lets the page subscribe to selection changes." }).First;
        ILocator apiNext = apiSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });

        await apiNext.ClickAsync();
        await Assertions.Expect(apiSection).ToContainTextAsync("Slide 2 of 5");

        ILocator loopSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Quark exposes the most visible carousel option directly with `Loop=true`" }).First;
        ILocator loopTrack = loopSection.Locator("[data-slot='carousel-content'] > div").First;
        ILocator loopNext = loopSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });

        for (var i = 0; i < 4; i++)
            await loopNext.ClickAsync();

        await Assertions.Expect(loopTrack).ToHaveAttributeAsync("style", "transform: translateX(-0%); transition: transform 300ms ease-in-out");
        await Assertions.Expect(loopNext).ToBeEnabledAsync();
    }
}
