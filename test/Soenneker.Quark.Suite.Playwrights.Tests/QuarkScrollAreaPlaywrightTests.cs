using System.Threading.Tasks;
using System.Collections.Generic;
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
        List<string> consoleErrors = [];
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady($"{BaseUrl}scroll-area",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Augments native scroll functionality for custom, cross-browser styling." })
                         .Locator("[data-slot='scroll-area-viewport']").First, expectedTitle: "Scroll Area - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Augments native scroll functionality for custom, cross-browser styling." })
                                  .First;
        var horizontalSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Horizontal scroll areas help showcase collections without forcing every item into a narrow grid." }).First;
        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Scroll areas can also present content naturally in right-to-left layouts." }).First;

        var verticalScrollTop = await verticalSection.Locator("[data-slot='scroll-area-viewport']").First
                                                     .EvaluateAsync<int>("element => { element.scrollTop = 240; return element.scrollTop; }");
        (verticalScrollTop > 0).Should().BeTrue();
        var verticalRoot = verticalSection.Locator("[data-slot='scroll-area']").First;
        await verticalRoot.HoverAsync();
        await Assertions.Expect(verticalRoot).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("relative"));
        await Assertions.Expect(verticalSection.Locator("[data-slot='scroll-area-viewport']").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("focus-visible:ring-\\[3px\\]"));
        var verticalScrollbar = verticalSection.Locator("[data-slot='scroll-area-scrollbar'][data-orientation='vertical']").First;
        await Assertions.Expect(verticalScrollbar).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("data-\\[orientation=vertical\\]:w-2\\.5"));
        await Assertions.Expect(verticalScrollbar.Locator("[data-slot='scroll-area-thumb']").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("bg-border"));

        var horizontalScrollLeft = await horizontalSection.Locator("[data-slot='scroll-area-viewport']").First
                                                          .EvaluateAsync<int>("element => { element.scrollLeft = 220; return element.scrollLeft; }");
        (horizontalScrollLeft > 0).Should().BeTrue();
        await horizontalSection.Locator("[data-slot='scroll-area']").First.HoverAsync();
        await Assertions.Expect(horizontalSection.Locator("[data-slot='scroll-area-scrollbar'][data-orientation='horizontal']").First)
                        .ToHaveClassAsync(new System.Text.RegularExpressions.Regex("data-\\[orientation=horizontal\\]:h-2\\.5"));

        var rtlRoot = rtlSection.Locator("[data-slot='scroll-area']").First;
        await Assertions.Expect(rtlRoot).ToHaveAttributeAsync("dir", "rtl");

        var rtlScrollTop = await rtlSection.Locator("[data-slot='scroll-area-viewport']").First
                                           .EvaluateAsync<int>("element => { element.scrollTop = 160; return element.scrollTop; }");
        (rtlScrollTop > 0).Should().BeTrue();
        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
