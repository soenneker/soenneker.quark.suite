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
        await Assertions.Expect(defaultButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)h-9(\s|$)"));
        await Assertions.Expect(defaultButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)px-4(\s|$)"));
        await Assertions.Expect(defaultButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)hover:bg-primary/90(\s|$)"));
        await Assertions.Expect(defaultButton).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"group/button|bg-clip-padding|select-none"));

        var iconButton = page.GetByRole(AriaRole.Button, new() { Name = "Upload", Exact = true });
        await Assertions.Expect(iconButton).ToHaveAttributeAsync("data-size", "icon");
        await Assertions.Expect(iconButton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)size-9(\s|$)"));

        var disabledLink = page.GetByRole(AriaRole.Link, new() { Name = "Disabled Link", Exact = true });
        await Assertions.Expect(disabledLink).ToHaveAttributeAsync("aria-disabled", "true");
        await Assertions.Expect(disabledLink).ToHaveAttributeAsync("tabindex", "-1");
        await disabledLink.ClickAsync(new LocatorClickOptions { Force = true });
        page.Url.Should().Contain("/buttons");

        var asChildLink = page.GetByRole(AriaRole.Link, new() { Name = "Login", Exact = true });
        await Assertions.Expect(asChildLink).ToHaveAttributeAsync("data-slot", "button");
        await Assertions.Expect(asChildLink).ToHaveAttributeAsync("href", new System.Text.RegularExpressions.Regex("input-demo"));

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
