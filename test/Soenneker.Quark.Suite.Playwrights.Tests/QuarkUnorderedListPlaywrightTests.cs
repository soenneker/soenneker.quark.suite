using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkUnorderedListPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkUnorderedListPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask UnorderedList_demo_matches_typography_list_contract()
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
        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/unorderedlists",
            static p => p.Locator("[data-slot='unordered-list']").First,
            expectedTitle: "Unordered List - Quark Suite");

        var firstList = page.Locator("[data-slot='unordered-list']").First;
        var firstItem = firstList.Locator("[data-slot='unordered-list-item']").First;

        await Assertions.Expect(firstList).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bmy-6\b"));
        await Assertions.Expect(firstList).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bml-6\b"));
        await Assertions.Expect(firstList).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\blist-disc\b"));
        await Assertions.Expect(firstItem).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
