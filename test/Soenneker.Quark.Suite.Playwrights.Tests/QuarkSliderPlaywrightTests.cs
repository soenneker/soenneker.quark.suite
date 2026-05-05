using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSliderPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkSliderPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
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
