using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkComboboxCarouselPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkComboboxCarouselPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Combobox_demo_renders_without_open_disabled_results()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Select a framework").First,
            expectedTitle: "Combobox - Quark Suite");

        await Assertions.Expect(page.Locator("[role='listbox'][data-state='open']")).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Combobox_demo_selects_clicked_option()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}comboboxes",
            static p => p.GetByPlaceholder("Select a framework").First,
            expectedTitle: "Combobox - Quark Suite");

        var input = page.GetByPlaceholder("Select a framework").First;

        await input.ClickAsync();
        await page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Next.js", Exact = true }).First.ClickAsync();

        await Assertions.Expect(input).ToHaveValueAsync("Next.js");
        await Assertions.Expect(page.Locator("[role='listbox'][data-state='open']")).ToHaveCountAsync(0);

        await input.ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Option)).ToHaveCountAsync(FrameworkOptions.Length);
    }

    private static readonly string[] FrameworkOptions =
    [
        "Next.js",
        "SvelteKit",
        "Nuxt.js",
        "Remix",
        "Astro"
    ];

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

        var demo = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A carousel with motion and swipe built using Embla." }).First;
        var previous = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Previous slide", Exact = true });
        var next = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });
        var track = demo.Locator("[data-slot='carousel-content'] > div").First;

        await Assertions.Expect(previous).ToBeDisabledAsync();
        await AssertHorizontalCarouselTrackStyle(track, shouldBeAtStart: true);

        await next.ClickAsync();
        await Assertions.Expect(previous).ToBeEnabledAsync();
        await AssertHorizontalCarouselTrackStyle(track, shouldBeAtStart: false);

        for (var i = 0; i < 3; i++)
            await next.ClickAsync();

        await Assertions.Expect(next).ToBeDisabledAsync();
        await AssertHorizontalCarouselTrackStyle(track, shouldBeAtStart: false);

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

        var defaultSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A carousel with motion and swipe built using Embla." }).First;
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
        await AssertHorizontalCarouselTrackStyle(track, shouldBeAtStart: false);

        var spacingSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "To set the spacing between the items, we use a padding utility on CarouselItem and a negative margin on CarouselContent." }).First;
        var spacingViewport = spacingSection.Locator("[data-slot='carousel-content']").First;
        var spacingTrack = spacingSection.Locator("[data-slot='carousel-content'] > div").First;

        await Assertions.Expect(spacingViewport).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)overflow-hidden(\s|$)"));
        await Assertions.Expect(spacingViewport).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)touch-pan-y(\s|$)"));
        await Assertions.Expect(spacingTrack).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)flex(\s|$)"));
        await Assertions.Expect(spacingTrack).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)-ml-1(\s|$)"));

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use the orientation prop to set the orientation of the carousel." }).First;
        var verticalCarousel = verticalSection.Locator("[data-slot='carousel']").First;
        var verticalTrack = verticalSection.Locator("[data-slot='carousel-content'] > div").First;
        var verticalNext = verticalSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });

        (await verticalCarousel.GetAttributeAsync("data-orientation")).Should().BeNull();
        await Assertions.Expect(verticalTrack).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)flex-col(\s|$)"));
        await verticalNext.ClickAsync();
        await AssertVerticalCarouselTrackStyle(verticalTrack, shouldBeAtStart: false);

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Carousel_api_demo_reports_selected_slide()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}carousels",
            static p => p.GetByText("Slide 1 of 5", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Carousel - Quark Suite");

        var apiSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a state and the setApi props to get an instance of the carousel API." }).First;
        var apiNext = apiSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Next slide", Exact = true });

        await apiNext.ClickAsync();
        await Assertions.Expect(apiSection).ToContainTextAsync("Slide 2 of 5");
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

        var defaultSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A carousel with motion and swipe built using Embla." }).First;
        var defaultViewport = defaultSection.Locator("[data-slot='carousel-content']").First;
        var defaultTrack = defaultSection.Locator("[data-slot='carousel-content'] > div").First;
        await defaultViewport.ScrollIntoViewIfNeededAsync();
        var defaultBox = await defaultViewport.BoundingBoxAsync();
        defaultBox.Should().NotBeNull();

        await page.Mouse.MoveAsync((float)(defaultBox!.X + defaultBox.Width * 0.75), (float)(defaultBox.Y + defaultBox.Height / 2));
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync((float)(defaultBox.X + defaultBox.Width * 0.25), (float)(defaultBox.Y + defaultBox.Height / 2));
        await page.Mouse.UpAsync();

        await AssertHorizontalCarouselTrackStyle(defaultTrack, shouldBeAtStart: false);

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use the orientation prop to set the orientation of the carousel." }).First;
        var verticalViewport = verticalSection.Locator("[data-slot='carousel-content']").First;
        var verticalTrack = verticalSection.Locator("[data-slot='carousel-content'] > div").First;
        await verticalViewport.ScrollIntoViewIfNeededAsync();
        var verticalBox = await verticalViewport.BoundingBoxAsync();
        verticalBox.Should().NotBeNull();

        await page.Mouse.MoveAsync((float)(verticalBox!.X + verticalBox.Width / 2), (float)(verticalBox.Y + verticalBox.Height * 0.75));
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync((float)(verticalBox.X + verticalBox.Width / 2), (float)(verticalBox.Y + verticalBox.Height * 0.25));
        await page.Mouse.UpAsync();

        await AssertVerticalCarouselTrackStyle(verticalTrack, shouldBeAtStart: false);
    }

    private static async Task AssertHorizontalCarouselTrackStyle(ILocator track, bool shouldBeAtStart)
    {
        var style = await track.GetAttributeAsync("style");

        style.Should().Contain("transition: transform 300ms ease-in-out");
        style.Should().Contain("will-change: transform");
        style.Should().MatchRegex(shouldBeAtStart
            ? @"transform:\s*translate3d\(0px,\s*0px,\s*0px\)"
            : @"transform:\s*translate3d\(-[1-9]\d*(?:\.\d+)?px,\s*0px,\s*0px\)");
    }

    private static async Task AssertVerticalCarouselTrackStyle(ILocator track, bool shouldBeAtStart)
    {
        var style = await track.GetAttributeAsync("style");

        style.Should().Contain("transition: transform 300ms ease-in-out");
        style.Should().Contain("will-change: transform");
        style.Should().MatchRegex(shouldBeAtStart
            ? @"transform:\s*translate3d\(0px,\s*0px,\s*0px\)"
            : @"transform:\s*translate3d\(0px,\s*-[1-9]\d*(?:\.\d+)?px,\s*0px\)");
    }
}
