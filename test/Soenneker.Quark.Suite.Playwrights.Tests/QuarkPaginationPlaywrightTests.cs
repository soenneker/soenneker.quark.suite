using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkPaginationPlaywrightTests : PlaywrightUnitTest
{
    public QuarkPaginationPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Pagination_interactive_demo_updates_current_page_and_disabled_button_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}paginations",
            static p => p.Locator("#pagination-interactive-demo"),
            expectedTitle: "Paginations - Quark Suite");

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
    }
}
