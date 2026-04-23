using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkComboboxCarouselPlaywrightTests : PlaywrightUnitTest
{
    public QuarkComboboxCarouselPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Combobox_input_demo_filters_and_selects_framework()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Select framework...").First,
            expectedTitle: "Combobox - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Input combobox" }).First;
        var input = section.GetByPlaceholder("Select framework...");
        await input.ClickAsync();
        await input.FillAsync("nuxt");

        var listboxId = await input.GetAttributeAsync("aria-controls");
        Xunit.Assert.False(string.IsNullOrWhiteSpace(listboxId));
        var listbox = page.Locator($"#{listboxId}");
        var nuxt = listbox.Locator("[data-slot='combobox-item']").Filter(new LocatorFilterOptions { HasText = "Nuxt.js" }).First;

        await Assertions.Expect(input).ToHaveValueAsync("nuxt");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(listbox).ToHaveAttributeAsync("data-state", "open");
        await Assertions.Expect(listbox).ToHaveAttributeAsync("role", "listbox");
        await Assertions.Expect(nuxt).ToBeVisibleAsync();
        await Assertions.Expect(listbox.Locator("[data-slot='combobox-item']").Filter(new LocatorFilterOptions { HasText = "Next.js" })).ToHaveCountAsync(0);

        await nuxt.ClickAsync();

        await Assertions.Expect(input).ToHaveValueAsync("Nuxt.js");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.GetByText("Selected value:", new PageGetByTextOptions { Exact = false })).ToContainTextAsync("Nuxt.js");
    }

    [Test]
    public async ValueTask Combobox_demo_positions_the_listbox_beneath_the_trigger_when_opened()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByRole(AriaRole.Main).First);

        var trigger = page.GetByRole(AriaRole.Combobox).First;
        await Assertions.Expect(trigger).ToBeVisibleAsync();
        await trigger.ClickAsync();

        var listboxId = await trigger.GetAttributeAsync("aria-controls");
        Xunit.Assert.False(string.IsNullOrWhiteSpace(listboxId));

        var listbox = page.Locator($"#{listboxId}");
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        await page.WaitForFunctionAsync(
            "element => { const box = element.getBoundingClientRect(); return box.y > 0 && box.width >= 180; }",
            await listbox.ElementHandleAsync());

        var triggerBox = await trigger.BoundingBoxAsync();
        var listboxBox = await listbox.BoundingBoxAsync();

        Xunit.Assert.NotNull(triggerBox);
        Xunit.Assert.NotNull(listboxBox);
        Xunit.Assert.True(listboxBox.Y > 0, $"Expected listbox top ({listboxBox.Y}) to remain inside the viewport.");
        Xunit.Assert.True(System.Math.Abs(listboxBox.X - triggerBox.X) <= 220,
            $"Expected listbox X ({listboxBox.X}) to stay anchored near the trigger X ({triggerBox.X}).");
        Xunit.Assert.True(listboxBox.Width >= 180, $"Expected listbox width to be measurable, but was {listboxBox.Width}.");
    }

    [Test]
    public async ValueTask Combobox_multiple_demo_adds_and_removes_chip_selection()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Add frameworks...").First,
            expectedTitle: "Combobox - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use chips plus an inline search input to select and remove multiple values from the same combobox." }).First;
        var input = section.GetByPlaceholder("Add frameworks...");
        await input.ClickAsync();
        await input.FillAsync("astro");

        var listboxId = await input.GetAttributeAsync("aria-controls");
        Xunit.Assert.False(string.IsNullOrWhiteSpace(listboxId));
        var listbox = page.Locator($"#{listboxId}");
        var astro = listbox.Locator("[data-slot='combobox-item']").Filter(new LocatorFilterOptions { HasText = "Astro" }).First;
        await Assertions.Expect(listbox).ToHaveAttributeAsync("data-state", "open");
        await Assertions.Expect(listbox).ToHaveAttributeAsync("role", "listbox");
        await Assertions.Expect(astro).ToBeVisibleAsync();
        await astro.ClickAsync();

        var selectedFrameworks = section.GetByText("Selected frameworks:", new LocatorGetByTextOptions { Exact = false }).First;
        await Assertions.Expect(selectedFrameworks).ToContainTextAsync("Next.js, Astro");

        var astroChip = section.Locator("[data-slot='combobox-chip'][data-value='Astro']").First;
        await Assertions.Expect(astroChip).ToBeVisibleAsync();

        await astroChip.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Remove selection", Exact = true }).ClickAsync();

        await Assertions.Expect(page.Locator("[data-slot='combobox-chip'][data-value='Astro']")).ToHaveCountAsync(0);
        await Assertions.Expect(selectedFrameworks).Not.ToContainTextAsync("Astro");
    }

    [Test]
    public async ValueTask Combobox_disabled_demo_does_not_open_results()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Unavailable"),
            expectedTitle: "Combobox - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disabled comboboxes keep the same visual treatment without opening the menu." }).First;
        var disabledInput = section.GetByPlaceholder("Unavailable");
        var disabledTrigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open combobox", Exact = true }).Last;

        await disabledInput.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledInput).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(disabledTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(section.Locator("[role='listbox'][data-state='open']")).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Combobox_in_dialog_filters_and_selects_without_closing_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open dialog", Exact = true }),
            expectedTitle: "Combobox - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open dialog", Exact = true }).ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Select framework", Exact = true });
        var input = dialog.GetByPlaceholder("Select framework...");

        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await input.ClickAsync();
        await input.FillAsync("remix");
        var listboxId = await input.GetAttributeAsync("aria-controls");
        Xunit.Assert.False(string.IsNullOrWhiteSpace(listboxId));
        var listbox = page.Locator($"#{listboxId}");
        var remix = listbox.Locator("[data-slot='combobox-item']").Filter(new LocatorFilterOptions { HasText = "Remix" }).First;
        await Assertions.Expect(listbox).ToHaveAttributeAsync("data-state", "open");
        await Assertions.Expect(listbox).ToHaveAttributeAsync("role", "listbox");
        await Assertions.Expect(remix).ToBeVisibleAsync();
        await remix.ClickAsync();

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(input).ToHaveValueAsync("Remix");
    }

    [Test]
    public async ValueTask Carousel_demo_advances_and_disables_navigation_at_bounds()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}carousels",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Next slide", Exact = true }).First,
            expectedTitle: "Carousel - Quark Suite");

        var demo = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The default numbered-card carousel." }).First;
        var previous = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Previous slide", Exact = true });
        var next = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });
        var track = demo.Locator("[data-slot='carousel-content'] > div").First;

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

    [Test]
    public async ValueTask Carousel_loop_demo_wraps_from_last_slide_back_to_first()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}carousels",
            static p => p.GetByText("Slide 1 of 5", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Carousel - Quark Suite");

        var apiSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The API callback now lets the page subscribe to selection changes." }).First;
        var apiNext = apiSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });

        await apiNext.ClickAsync();
        await Assertions.Expect(apiSection).ToContainTextAsync("Slide 2 of 5");

        var loopSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Quark exposes the most visible carousel option directly with `Loop=true`" }).First;
        var loopTrack = loopSection.Locator("[data-slot='carousel-content'] > div").First;
        var loopNext = loopSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });

        for (var i = 0; i < 4; i++)
            await loopNext.ClickAsync();

        await Assertions.Expect(loopTrack).ToHaveAttributeAsync("style", "transform: translateX(-0%); transition: transform 300ms ease-in-out");
        await Assertions.Expect(loopNext).ToBeEnabledAsync();
    }
}
