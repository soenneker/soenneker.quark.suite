using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkScrollAreaPlaywrightTests : PlaywrightUnitTest
{
    public QuarkScrollAreaPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Scroll_area_examples_scroll_their_viewports_in_vertical_horizontal_and_rtl_layouts()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}scroll-area",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Fixed-height tag list with separators between items." })
                         .Locator("[data-slot='scroll-area-viewport']").First, expectedTitle: "Scroll Area - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Fixed-height tag list with separators between items." })
                                  .First;
        var horizontalSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Horizontal scroll areas help showcase collections without forcing every item into a narrow grid." }).First;
        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Scroll areas can also present content naturally in right-to-left layouts." }).First;

        var verticalScrollTop = await verticalSection.Locator("[data-slot='scroll-area-viewport']").First
                                                     .EvaluateAsync<int>("element => { element.scrollTop = 240; return element.scrollTop; }");
        (verticalScrollTop > 0).Should().BeTrue();

        var horizontalScrollLeft = await horizontalSection.Locator("[data-slot='scroll-area-viewport']").First
                                                          .EvaluateAsync<int>("element => { element.scrollLeft = 220; return element.scrollLeft; }");
        (horizontalScrollLeft > 0).Should().BeTrue();

        var rtlRoot = rtlSection.Locator("[data-slot='scroll-area']").First;
        await Assertions.Expect(rtlRoot).ToHaveAttributeAsync("dir", "rtl");

        var rtlScrollTop = await rtlSection.Locator("[data-slot='scroll-area-viewport']").First
                                           .EvaluateAsync<int>("element => { element.scrollTop = 160; return element.scrollTop; }");
        (rtlScrollTop > 0).Should().BeTrue();
    }
}