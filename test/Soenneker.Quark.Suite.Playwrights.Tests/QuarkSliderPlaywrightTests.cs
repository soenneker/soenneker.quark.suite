using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSliderPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSliderPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Slider_demo_horizontal_track_click_updates_value_and_step_demo_snaps_to_increment()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        var demoSection = page.Locator("[data-slot='preview'] [data-testid='slider-demo-primary']").First;
        var demoRoot = demoSection.Locator("[data-js-ready='true']").First;
        var demoSlider = demoSection.Locator("[role='slider']:visible").First;
        await Assertions.Expect(demoSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        var demoRootBox = await demoRoot.EvaluateAsync<SliderRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");
        (demoRootBox).Should().NotBeNull();
        (demoRootBox!.Width > 0).Should().BeTrue();
        (demoRootBox.Height > 0).Should().BeTrue();

        await demoRoot.ClickAsync(new LocatorClickOptions
        {
            Position = new Position
            {
                X = (float) (demoRootBox.Width * 0.8),
                Y = (float) (demoRootBox.Height / 2)
            }
        });

        await Assertions.Expect(demoSlider).ToHaveAttributeAsync("aria-valuenow", "80");
        await Assertions.Expect(page.GetByText("Single thumb with min, max, and step. Current: 80.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        var stepSection = page.Locator("[data-slot='preview'] [data-testid='slider-demo-step']").First;
        var stepRoot = stepSection.Locator("[data-js-ready='true']").First;
        var stepSlider = stepSection.Locator("[role='slider']:visible").First;
        await Assertions.Expect(stepSlider).ToHaveAttributeAsync("aria-valuenow", "30");
        var stepRootBox = await stepRoot.EvaluateAsync<SliderRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");
        (stepRootBox).Should().NotBeNull();
        (stepRootBox!.Width > 0).Should().BeTrue();
        (stepRootBox.Height > 0).Should().BeTrue();

        await stepRoot.ClickAsync(new LocatorClickOptions
        {
            Position = new Position
            {
                X = (float) (stepRootBox.Width * 0.62),
                Y = (float) (stepRootBox.Height / 2)
            }
        });

        await Assertions.Expect(stepSlider).ToHaveAttributeAsync("aria-valuenow", "60");
        await Assertions.Expect(page.GetByText("Step 5; current value: 60.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Slider_demo_home_and_end_keys_jump_to_minimum_and_maximum_values()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        var demoSection = page.Locator("section:visible").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).First;
        var slider = demoSection.GetByRole(AriaRole.Slider).First;

        await Assertions.Expect(slider).ToHaveAttributeAsync("aria-valuenow", "50");

        await slider.FocusAsync();
        await slider.PressAsync("Home");

        await Assertions.Expect(slider).ToHaveAttributeAsync("aria-valuenow", "0");
        await Assertions.Expect(demoSection).ToContainTextAsync("Current: 0.");

        await slider.PressAsync("End");

        await Assertions.Expect(slider).ToHaveAttributeAsync("aria-valuenow", "100");
        await Assertions.Expect(demoSection).ToContainTextAsync("Current: 100.");
    }

[Test]
    public async ValueTask Slider_range_and_multiple_thumb_demos_expose_named_thumbs_and_update_section_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Two-thumb range." }).GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Minimum", Exact = true }),
            expectedTitle: "Sliders - Quark Suite");

        var rangeSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Two-thumb range." }).First;
        var minimum = rangeSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Minimum", Exact = true });
        var maximum = rangeSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Maximum", Exact = true });

        await Assertions.Expect(minimum).ToHaveAttributeAsync("aria-valuenow", "25");
        await Assertions.Expect(maximum).ToHaveAttributeAsync("aria-valuenow", "50");

        await minimum.FocusAsync();
        await minimum.PressAsync("ArrowRight");
        await maximum.FocusAsync();
        await maximum.PressAsync("ArrowRight");

        await Assertions.Expect(minimum).ToHaveAttributeAsync("aria-valuenow", "30");
        await Assertions.Expect(maximum).ToHaveAttributeAsync("aria-valuenow", "55");
        await Assertions.Expect(rangeSection).ToContainTextAsync("Current: 30 â€“ 55.");

        var multipleSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Several thumbs on one track." }).First;
        var first = multipleSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Value 1 of 3", Exact = true });
        var second = multipleSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Value 2 of 3", Exact = true });
        var third = multipleSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Value 3 of 3", Exact = true });

        await Assertions.Expect(first).ToHaveAttributeAsync("aria-valuenow", "10");
        await Assertions.Expect(second).ToHaveAttributeAsync("aria-valuenow", "20");
        await Assertions.Expect(third).ToHaveAttributeAsync("aria-valuenow", "70");

        await second.FocusAsync();
        await second.PressAsync("ArrowRight");

        await Assertions.Expect(second).ToHaveAttributeAsync("aria-valuenow", "30");
        await Assertions.Expect(multipleSection).ToContainTextAsync("Values: 10, 30, 70.");
    }

[Test]
    public async ValueTask Slider_demo_thumb_interaction_updates_value_and_disabled_slider_does_not_move()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        var demoSection = page.Locator("section:visible").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).First;
        var demoSlider = demoSection.GetByRole(AriaRole.Slider).First;
        await Assertions.Expect(demoSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        await demoSlider.FocusAsync();
        for (var i = 0; i < 5; i++)
            await demoSlider.PressAsync("ArrowRight");
        await Assertions.Expect(demoSlider).ToHaveAttributeAsync("aria-valuenow", "55");
        await Assertions.Expect(demoSection).ToContainTextAsync("Current: 55.");

        var disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Set Disabled to prevent interaction." }).First;
        var disabledSlider = disabledSection.GetByRole(AriaRole.Slider).First;

        await Assertions.Expect(disabledSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        await disabledSlider.FocusAsync();
        await disabledSlider.PressAsync("ArrowRight");
        await Assertions.Expect(disabledSlider).ToHaveAttributeAsync("aria-valuenow", "50");
    }

[Test]
    public async ValueTask Slider_vertical_demo_and_rtl_demo_follow_radix_orientation_and_direction_behavior()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Vertical sliders." }).GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Vertical sliders." }).First;
        var leftSlider = verticalSection.GetByRole(AriaRole.Slider).Nth(0);

        await Assertions.Expect(leftSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(leftSlider).ToHaveAttributeAsync("aria-orientation", "vertical");

        await leftSlider.FocusAsync();
        await Assertions.Expect(leftSlider).ToBeFocusedAsync();
        await leftSlider.PressAsync("ArrowUp");

        await Assertions.Expect(leftSlider).ToHaveAttributeAsync("aria-valuenow", "51");
        await Assertions.Expect(verticalSection).ToContainTextAsync("Left: 51");

        await leftSlider.PressAsync("ArrowDown");

        await Assertions.Expect(leftSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(verticalSection).ToContainTextAsync("Left: 50");

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Slider direction follows DirectionProvider." }).First;
        var rtlSlider = rtlSection.GetByRole(AriaRole.Slider).First;

        await Assertions.Expect(rtlSlider).ToHaveAttributeAsync("aria-valuenow", "75");

        await rtlSlider.FocusAsync();
        await page.Keyboard.PressAsync("ArrowRight");

        await Assertions.Expect(rtlSlider).ToHaveAttributeAsync("aria-valuenow", "74");
        await Assertions.Expect(rtlSection).ToContainTextAsync("Value: 74.");

        await page.Keyboard.PressAsync("ArrowLeft");

        await Assertions.Expect(rtlSlider).ToHaveAttributeAsync("aria-valuenow", "75");
        await Assertions.Expect(rtlSection).ToContainTextAsync("Value: 75.");
    }

    private sealed class SliderRect
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
