using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkPaginationPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkPaginationPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Pagination_interactive_demo_updates_current_page_and_disabled_button_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}paginations",
            static p => p.Locator("#pagination-interactive-demo"),
            expectedTitle: "Paginations - Quark Suite");

        var firstPagination = page.Locator("nav[data-slot='pagination']").First;
        var firstContent = firstPagination.Locator("[data-slot='pagination-content']").First;
        var ellipsis = firstPagination.Locator("[data-slot='pagination-ellipsis']").First;
        var demoPrevious = firstPagination.GetByLabel("Go to previous page");
        var demoNext = firstPagination.GetByLabel("Go to next page");

        await Assertions.Expect(firstPagination).ToHaveAttributeAsync("role", "navigation");
        await Assertions.Expect(firstPagination).ToHaveAttributeAsync("aria-label", "pagination");
        await Assertions.Expect(firstContent).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bflex-row\b"));
        await Assertions.Expect(ellipsis).ToHaveAttributeAsync("aria-hidden", "true");
        await Assertions.Expect(demoPrevious).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsm:pl-2\.5\b"));
        await Assertions.Expect(demoNext).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsm:pr-2\.5\b"));

        var rtlProbe = await page.Locator("#pagination-rtl-demo svg.rtl\\:rotate-180").First.EvaluateAsync<PaginationIconProbe>(
            @"element => {
                const wrapper = element.closest('[dir=""rtl""], [data-dir=""rtl""]');
                return {
                    className: element.getAttribute('class'),
                    hasRtlAncestor: !!wrapper
                };
            }");
        rtlProbe.className.Should().Contain("rtl:rotate-180");
        rtlProbe.hasRtlAncestor.Should().BeTrue();

        var demo = page.Locator("#pagination-interactive-demo");
        var state = demo.Locator("#pagination-interactive-state");
        var previous = demo.Locator("#pagination-interactive-previous");
        var next = demo.Locator("#pagination-interactive-next");
        var page1 = demo.Locator("#pagination-interactive-page-1");
        var page3 = demo.Locator("#pagination-interactive-page-3");
        var page5 = demo.Locator("#pagination-interactive-page-5");
        var goToLast = demo.Locator("#pagination-interactive-last");
        var goToFirst = demo.Locator("#pagination-interactive-first");

        await Assertions.Expect(state).ToContainTextAsync("Current page: 1 of 5");
        await Assertions.Expect(previous).ToBeDisabledAsync();
        await Assertions.Expect(previous).ToHaveAttributeAsync("disabled", string.Empty);
        await Assertions.Expect(page1).ToHaveAttributeAsync("aria-current", "page");
        await Assertions.Expect(page1).ToHaveAttributeAsync("data-active", "true");
        await Assertions.Expect(page3).Not.ToHaveAttributeAsync("aria-current", new System.Text.RegularExpressions.Regex(".+"));
        await Assertions.Expect(next).ToBeEnabledAsync();

        await next.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("Current page: 2 of 5");
        await Assertions.Expect(previous).ToBeEnabledAsync();
        await Assertions.Expect(demo.Locator("#pagination-interactive-page-2")).ToHaveAttributeAsync("aria-current", "page");

        await page3.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("Current page: 3 of 5");
        await Assertions.Expect(page3).ToHaveAttributeAsync("aria-current", "page");
        await Assertions.Expect(page3).ToHaveAttributeAsync("data-active", "true");

        await goToLast.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("Current page: 5 of 5");
        await Assertions.Expect(page5).ToHaveAttributeAsync("aria-current", "page");
        await Assertions.Expect(next).ToBeDisabledAsync();
        await Assertions.Expect(next).ToHaveAttributeAsync("disabled", string.Empty);

        await goToFirst.ClickAsync();

        await Assertions.Expect(state).ToContainTextAsync("Current page: 1 of 5");
        await Assertions.Expect(previous).ToBeDisabledAsync();
        await Assertions.Expect(page1).ToHaveAttributeAsync("aria-current", "page");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class PaginationIconProbe
    {
        public string? className { get; set; }
        public bool hasRtlAncestor { get; set; }
    }
}
