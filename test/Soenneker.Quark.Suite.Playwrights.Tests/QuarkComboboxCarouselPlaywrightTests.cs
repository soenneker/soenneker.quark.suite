using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

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
        (string.IsNullOrWhiteSpace(listboxId)).Should().BeFalse();
        var listbox = page.Locator($"#{listboxId}");
        var nuxt = listbox.Locator("[data-slot='combobox-item']").Filter(new LocatorFilterOptions { HasText = "Nuxt.js" }).First;

        await Assertions.Expect(input).ToHaveValueAsync("nuxt");
        await Assertions.Expect(input).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(listbox).ToHaveAttributeAsync("data-state", "open");
        await Assertions.Expect(listbox).ToHaveAttributeAsync("role", "listbox");
        await Assertions.Expect(nuxt).ToBeVisibleAsync();
        await Assertions.Expect(listbox.Locator("[data-slot='combobox-item']").Filter(new LocatorFilterOptions { HasText = "Next.js" })).ToHaveCountAsync(0);

        await nuxt.ClickAsync();

        var selectedInputValue = await input.InputValueAsync();
        selectedInputValue.Should().BeOneOf(string.Empty, "Nuxt.js");
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
        (string.IsNullOrWhiteSpace(listboxId)).Should().BeFalse();

        var listbox = page.Locator($"#{listboxId}");
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        await page.WaitForFunctionAsync(
            "element => { const box = element.getBoundingClientRect(); return box.y > 0 && box.width >= 180; }",
            await listbox.ElementHandleAsync());

        var triggerBox = await trigger.BoundingBoxAsync();
        var listboxBox = await listbox.BoundingBoxAsync();

        (triggerBox).Should().NotBeNull();
        (listboxBox).Should().NotBeNull();
        (listboxBox.Y > 0).Should().BeTrue();
        (System.Math.Abs(listboxBox.X - triggerBox.X) <= 220).Should().BeTrue();
        (listboxBox.Width >= 180).Should().BeTrue();
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
        (string.IsNullOrWhiteSpace(listboxId)).Should().BeFalse();
        var listbox = page.Locator($"#{listboxId}");
        var astro = listbox.Locator("[data-slot='combobox-item']").Filter(new LocatorFilterOptions { HasText = "Astro" }).First;
        await Assertions.Expect(listbox).ToBeVisibleAsync();
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
        (string.IsNullOrWhiteSpace(listboxId)).Should().BeFalse();
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
    public async ValueTask Combobox_demo_portals_listbox_above_page_and_has_no_console_errors()
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
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Select framework...").First,
            expectedTitle: "Combobox - Quark Suite");

        var input = page.GetByPlaceholder("Select framework...").First;
        await input.ClickAsync();

        var listboxId = await input.GetAttributeAsync("aria-controls");
        (string.IsNullOrWhiteSpace(listboxId)).Should().BeFalse();

        var listbox = page.Locator($"#{listboxId}");
        await Assertions.Expect(listbox).ToBeVisibleAsync();

        await page.WaitForFunctionAsync(
            "id => {" +
            "const listbox = document.getElementById(id);" +
            "const main = document.querySelector('main');" +
            "if (!listbox || !main || main.contains(listbox)) return false;" +
            "const positioned = listbox.closest('[data-radix-popper-content-wrapper]') || listbox;" +
            "return Number.parseInt(getComputedStyle(positioned).zIndex, 10) >= 50;" +
            "}",
            listboxId);

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Carousel_demo_advances_and_disables_navigation_at_bounds()
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

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Carousel_demo_matches_shadcn_markup_and_keyboard_axis_behavior()
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
            $"{BaseUrl}carousels",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Next slide", Exact = true }).First,
            expectedTitle: "Carousel - Quark Suite");

        var defaultSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The default numbered-card carousel." }).First;
        var carousel = defaultSection.Locator("[data-slot='carousel']").First;
        var item = defaultSection.Locator("[data-slot='carousel-item']").First;
        var next = defaultSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });
        var track = defaultSection.Locator("[data-slot='carousel-content'] > div").First;

        await Assertions.Expect(carousel).ToHaveAttributeAsync("role", "region");
        await Assertions.Expect(carousel).ToHaveAttributeAsync("aria-roledescription", "carousel");
        await Assertions.Expect(item).ToHaveAttributeAsync("role", "group");
        await Assertions.Expect(item).ToHaveAttributeAsync("aria-roledescription", "slide");

        await next.FocusAsync();
        await page.Keyboard.PressAsync("ArrowRight");
        await Assertions.Expect(track).ToHaveAttributeAsync("style", "transform: translateX(-20%); transition: transform 300ms ease-in-out");

        var spacingSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Adjust `CarouselContent` and `CarouselItem` spacing" }).First;
        var spacingViewport = spacingSection.Locator("[data-slot='carousel-content']").First;
        var spacingTrack = spacingSection.Locator("[data-slot='carousel-content'] > div").First;

        await Assertions.Expect(spacingViewport).ToHaveClassAsync("overflow-hidden");
        await Assertions.Expect(spacingTrack).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)flex(\s|$)"));
        await Assertions.Expect(spacingTrack).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)-ml-4(\s|$)"));
        await Assertions.Expect(spacingTrack).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)-ml-1(\s|$)"));

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Switch the orientation to vertical for stacked slide layouts." }).First;
        var verticalCarousel = verticalSection.Locator("[data-slot='carousel']").First;
        var verticalTrack = verticalSection.Locator("[data-slot='carousel-content'] > div").First;
        var verticalNext = verticalSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });

        await Assertions.Expect(verticalCarousel).ToHaveAttributeAsync("data-orientation", "vertical");
        await Assertions.Expect(verticalTrack).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)flex-col(\s|$)"));
        await verticalNext.ClickAsync();
        (await verticalTrack.GetAttributeAsync("style")).Should().MatchRegex(@"transform: translateY\(-(?:0|20)%\); transition: transform 300ms ease-in-out");

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
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

    [Test]
    public async ValueTask Carousel_viewport_drag_advances_horizontal_and_vertical_slides()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}carousels",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Next slide", Exact = true }).First,
            expectedTitle: "Carousel - Quark Suite");

        var defaultSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The default numbered-card carousel." }).First;
        var defaultViewport = defaultSection.Locator("[data-slot='carousel-content']").First;
        var defaultTrack = defaultSection.Locator("[data-slot='carousel-content'] > div").First;
        await defaultViewport.ScrollIntoViewIfNeededAsync();
        LocatorBoundingBoxResult? defaultBox = await defaultViewport.BoundingBoxAsync();
        defaultBox.Should().NotBeNull();

        await page.Mouse.MoveAsync((float)(defaultBox!.X + defaultBox.Width * 0.75), (float)(defaultBox.Y + defaultBox.Height / 2));
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync((float)(defaultBox.X + defaultBox.Width * 0.25), (float)(defaultBox.Y + defaultBox.Height / 2));
        await page.Mouse.UpAsync();

        await Assertions.Expect(defaultTrack).ToHaveAttributeAsync("style", "transform: translateX(-20%); transition: transform 300ms ease-in-out");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Switch the orientation to vertical for stacked slide layouts." }).First;
        var verticalViewport = verticalSection.Locator("[data-slot='carousel-content']").First;
        var verticalTrack = verticalSection.Locator("[data-slot='carousel-content'] > div").First;
        await verticalViewport.ScrollIntoViewIfNeededAsync();
        LocatorBoundingBoxResult? verticalBox = await verticalViewport.BoundingBoxAsync();
        verticalBox.Should().NotBeNull();

        await page.Mouse.MoveAsync((float)(verticalBox!.X + verticalBox.Width / 2), (float)(verticalBox.Y + verticalBox.Height * 0.75));
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync((float)(verticalBox.X + verticalBox.Width / 2), (float)(verticalBox.Y + verticalBox.Height * 0.25));
        await page.Mouse.UpAsync();

        (await verticalTrack.GetAttributeAsync("style")).Should().MatchRegex(@"transform: translateY\(-(?:0|20)%\); transition: transform 300ms ease-in-out");
    }
}
