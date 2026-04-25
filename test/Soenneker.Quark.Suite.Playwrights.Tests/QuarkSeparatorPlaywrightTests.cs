using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSeparatorPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSeparatorPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Separator_examples_preserve_decorative_defaults_and_semantic_opt_in_metadata()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new System.Collections.Generic.List<string>();
        var sawPageError = false;
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}separators",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Non-decorative separators stay in the accessibility tree with the expected orientation metadata." }).First,
            expectedTitle: "Separator - Quark Suite");

        var demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The shadcn separator layout with a horizontal rule above inline vertical dividers." }).First;
        var decorativeHorizontal = demoSection.Locator("[data-slot='separator'][data-orientation='horizontal']").First;
        var decorativeVerticals = demoSection.Locator("[data-slot='separator'][data-orientation='vertical']");

        await Assertions.Expect(decorativeHorizontal).ToHaveAttributeAsync("role", "none");
        await Assertions.Expect(decorativeHorizontal).Not.ToHaveAttributeAsync("aria-orientation", new System.Text.RegularExpressions.Regex(".+"));
        await Assertions.Expect(decorativeVerticals).ToHaveCountAsync(2);
        await Assertions.Expect(decorativeVerticals.First).ToHaveAttributeAsync("role", "none");

        var semanticSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Non-decorative separators stay in the accessibility tree with the expected orientation metadata." }).First;
        var semanticHorizontal = semanticSection.Locator("[data-slot='separator'][role='separator'][data-orientation='horizontal']").First;
        var semanticVertical = semanticSection.Locator("[data-slot='separator'][role='separator'][data-orientation='vertical']").First;

        await Assertions.Expect(semanticHorizontal).Not.ToHaveAttributeAsync("aria-orientation", new System.Text.RegularExpressions.Regex(".+"));
        await Assertions.Expect(semanticVertical).ToHaveAttributeAsync("aria-orientation", "vertical");

        consoleErrors.Should().BeEmpty();
        sawPageError.Should().BeFalse();
    }
}
