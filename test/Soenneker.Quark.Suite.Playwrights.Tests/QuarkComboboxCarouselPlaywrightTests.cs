using System.Collections.Generic;
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

        await Assertions.Expect(spacingViewport).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)overflow-hidden(\s|$)"));
        await Assertions.Expect(spacingViewport).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)touch-pan-y(\s|$)"));
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
