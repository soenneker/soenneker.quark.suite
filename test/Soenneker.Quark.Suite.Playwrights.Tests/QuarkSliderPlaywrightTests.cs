using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkSliderPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSliderPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Slider_demo_horizontal_track_click_updates_value_and_step_demo_snaps_to_increment()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        ILocator demoSection = page.Locator("[data-slot='preview'] [data-testid='slider-demo-primary']").First;
        ILocator demoRoot = demoSection.Locator("[data-slot='slider']").First;
        ILocator demoSlider = demoSection.Locator("[role='slider']:visible").First;
        await Assertions.Expect(demoSlider).ToHaveAttributeAsync("aria-valuenow", "50");
        var demoRootBox = await demoRoot.EvaluateAsync<SliderRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");
        Assert.NotNull(demoRootBox);
        Assert.True(demoRootBox!.Width > 0);
        Assert.True(demoRootBox.Height > 0);

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

        ILocator stepSection = page.Locator("[data-slot='preview'] [data-testid='slider-demo-step']").First;
        ILocator stepRoot = stepSection.Locator("[data-slot='slider']").First;
        ILocator stepSlider = stepSection.Locator("[role='slider']:visible").First;
        await Assertions.Expect(stepSlider).ToHaveAttributeAsync("aria-valuenow", "30");
        var stepRootBox = await stepRoot.EvaluateAsync<SliderRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");
        Assert.NotNull(stepRootBox);
        Assert.True(stepRootBox!.Width > 0);
        Assert.True(stepRootBox.Height > 0);

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

[Fact]
    public async ValueTask Slider_demo_home_and_end_keys_jump_to_minimum_and_maximum_values()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        ILocator demoSection = page.Locator("section:visible").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).First;
        ILocator slider = demoSection.GetByRole(AriaRole.Slider).First;

        await Assertions.Expect(slider).ToHaveAttributeAsync("aria-valuenow", "50");

        await slider.FocusAsync();
        await slider.PressAsync("Home");

        await Assertions.Expect(slider).ToHaveAttributeAsync("aria-valuenow", "0");
        await Assertions.Expect(demoSection).ToContainTextAsync("Current: 0.");

        await slider.PressAsync("End");

        await Assertions.Expect(slider).ToHaveAttributeAsync("aria-valuenow", "100");
        await Assertions.Expect(demoSection).ToContainTextAsync("Current: 100.");
    }

[Fact]
    public async ValueTask Slider_range_and_multiple_thumb_demos_expose_named_thumbs_and_update_section_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Two-thumb range." }).GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Minimum", Exact = true }),
            expectedTitle: "Sliders - Quark Suite");

        ILocator rangeSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Two-thumb range." }).First;
        ILocator minimum = rangeSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Minimum", Exact = true });
        ILocator maximum = rangeSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Maximum", Exact = true });

        await Assertions.Expect(minimum).ToHaveAttributeAsync("aria-valuenow", "25");
        await Assertions.Expect(maximum).ToHaveAttributeAsync("aria-valuenow", "50");

        await minimum.FocusAsync();
        await minimum.PressAsync("ArrowRight");
        await maximum.FocusAsync();
        await maximum.PressAsync("ArrowRight");

        await Assertions.Expect(minimum).ToHaveAttributeAsync("aria-valuenow", "30");
        await Assertions.Expect(maximum).ToHaveAttributeAsync("aria-valuenow", "55");
        await Assertions.Expect(rangeSection).ToContainTextAsync("Current: 30 – 55.");

        ILocator multipleSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Several thumbs on one track." }).First;
        ILocator first = multipleSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Value 1 of 3", Exact = true });
        ILocator second = multipleSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Value 2 of 3", Exact = true });
        ILocator third = multipleSection.GetByRole(AriaRole.Slider, new LocatorGetByRoleOptions { Name = "Value 3 of 3", Exact = true });

        await Assertions.Expect(first).ToHaveAttributeAsync("aria-valuenow", "10");
        await Assertions.Expect(second).ToHaveAttributeAsync("aria-valuenow", "20");
        await Assertions.Expect(third).ToHaveAttributeAsync("aria-valuenow", "70");

        await second.FocusAsync();
        await second.PressAsync("ArrowRight");

        await Assertions.Expect(second).ToHaveAttributeAsync("aria-valuenow", "30");
        await Assertions.Expect(multipleSection).ToContainTextAsync("Values: 10, 30, 70.");
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

        ILocator demoSection = page.Locator("section:visible").Filter(new LocatorFilterOptions { HasText = "Single thumb with min, max, and step." }).First;
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

[Fact]
    public async ValueTask Slider_vertical_demo_and_rtl_demo_follow_radix_orientation_and_direction_behavior()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sliders",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Vertical sliders." }).GetByRole(AriaRole.Slider).First,
            expectedTitle: "Sliders - Quark Suite");

        ILocator verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Vertical sliders." }).First;
        ILocator leftSlider = verticalSection.GetByRole(AriaRole.Slider).Nth(0);

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

        ILocator rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Slider direction follows DirectionProvider." }).First;
        ILocator rtlSlider = rtlSection.GetByRole(AriaRole.Slider).First;

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
