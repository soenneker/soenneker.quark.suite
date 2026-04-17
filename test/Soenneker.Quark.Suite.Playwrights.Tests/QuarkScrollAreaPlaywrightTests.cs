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
public sealed class QuarkScrollAreaPlaywrightTests : PlaywrightUnitTest
{
    public QuarkScrollAreaPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Scroll_area_examples_scroll_their_viewports_in_vertical_horizontal_and_rtl_layouts()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}scroll-area",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Fixed-height tag list with separators between items." }).Locator("[data-slot='scroll-area-viewport']").First,
            expectedTitle: "Scroll Area - Quark Suite");

        ILocator verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Fixed-height tag list with separators between items." }).First;
        ILocator horizontalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Horizontal scroll areas help showcase collections without forcing every item into a narrow grid." }).First;
        ILocator rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Scroll areas can also present content naturally in right-to-left layouts." }).First;

        int verticalScrollTop = await verticalSection.Locator("[data-slot='scroll-area-viewport']").First.EvaluateAsync<int>(
            "element => { element.scrollTop = 240; return element.scrollTop; }");
        Assert.True(verticalScrollTop > 0);

        int horizontalScrollLeft = await horizontalSection.Locator("[data-slot='scroll-area-viewport']").First.EvaluateAsync<int>(
            "element => { element.scrollLeft = 220; return element.scrollLeft; }");
        Assert.True(horizontalScrollLeft > 0);

        ILocator rtlRoot = rtlSection.Locator("[data-slot='scroll-area']").First;
        await Assertions.Expect(rtlRoot).ToHaveAttributeAsync("dir", "rtl");

        int rtlScrollTop = await rtlSection.Locator("[data-slot='scroll-area-viewport']").First.EvaluateAsync<int>(
            "element => { element.scrollTop = 160; return element.scrollTop; }");
        Assert.True(rtlScrollTop > 0);
    }
}
