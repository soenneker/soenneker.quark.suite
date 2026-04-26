using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkButtonPlaywrightTests : PlaywrightUnitTest
{
    public QuarkButtonPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Button_demo_matches_shadcn_contract_and_interactions()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, msg) =>
        {
            if (msg.Type == "error")
                consoleErrors.Add(msg.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}buttons",
            static p => p.Locator("[data-slot='button']").First,
            expectedTitle: "Buttons - Quark Suite");

        var defaultButton = page.Locator("[data-slot='button'][data-variant='default'][data-size='default']").Filter(new LocatorFilterOptions { HasText = "Button" }).First;
        await Assertions.Expect(defaultButton).ToHaveAttributeAsync("data-variant", "default");
        await Assertions.Expect(defaultButton).ToHaveAttributeAsync("data-size", "default");
        await Assertions.Expect(defaultButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)h-8(\s|$)"));
        await Assertions.Expect(defaultButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)px-2\.5(\s|$)"));
        await Assertions.Expect(defaultButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\[a\]:hover:bg-primary/80"));
        await Assertions.Expect(defaultButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"group/button|bg-clip-padding|select-none"));

        var iconButton = page.GetByRole(AriaRole.Button, new() { Name = "Submit", Exact = true }).First;
        await Assertions.Expect(iconButton).ToHaveAttributeAsync("data-size", "icon");
        await Assertions.Expect(iconButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)size-8(\s|$)"));

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
