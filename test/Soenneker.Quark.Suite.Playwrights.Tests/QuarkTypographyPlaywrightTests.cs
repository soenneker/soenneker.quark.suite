using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTypographyPlaywrightTests : PlaywrightUnitTest
{
    public QuarkTypographyPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Typography_demo_matches_shadcn_text_contracts_and_has_no_runtime_errors()
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
            $"{BaseUrl}typography",
            static p => p.Locator("[data-slot='h1']").First,
            expectedTitle: "Typography - Quark Suite");

        var h1 = page.Locator("[data-slot='h1']").First;
        var h2 = page.Locator("[data-slot='h2']").First;
        var paragraph = page.Locator("[data-slot='paragraph']").First;
        var blockquote = page.Locator("[data-slot='blockquote']").First;
        var code = page.Locator("code[data-slot='code']").First;
        var lead = page.Locator("[data-slot='lead']").First;
        var muted = page.Locator("[data-slot='muted']").First;
        var small = page.Locator("[data-slot='small']").First;

        await Assertions.Expect(h1).ToHaveClassAsync(new Regex(@"\btext-4xl\b"));
        await Assertions.Expect(h1).ToHaveClassAsync(new Regex(@"\btext-balance\b"));
        await Assertions.Expect(h1).Not.ToHaveClassAsync(new Regex(@"\blg:text-5xl\b"));
        await Assertions.Expect(h2).ToHaveClassAsync(new Regex(@"\bborder-b\b"));
        await Assertions.Expect(h2).ToHaveClassAsync(new Regex(@"\bpb-2\b"));
        await Assertions.Expect(paragraph).ToHaveClassAsync(new Regex(@"\bleading-7\b"));
        await Assertions.Expect(blockquote).ToHaveClassAsync(new Regex(@"\bborder-l-2\b"));
        await Assertions.Expect(blockquote).ToHaveClassAsync(new Regex(@"\bitalic\b"));
        await Assertions.Expect(code).ToHaveClassAsync(new Regex(@"\brounded\b"));
        await Assertions.Expect(code).Not.ToHaveClassAsync(new Regex(@"\brounded-md\b"));
        await Assertions.Expect(lead).ToHaveClassAsync(new Regex(@"\btext-xl\b"));
        await Assertions.Expect(muted).ToHaveClassAsync(new Regex(@"\btext-muted-foreground\b"));
        await Assertions.Expect(small).ToHaveClassAsync(new Regex(@"\bleading-none\b"));

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
