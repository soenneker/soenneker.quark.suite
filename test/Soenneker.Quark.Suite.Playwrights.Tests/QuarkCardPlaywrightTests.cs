using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCardPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCardPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Card_demo_matches_shadcn_shell_and_form_content_is_usable()
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
            $"{BaseUrl}cards",
            static p => p.Locator("[data-slot='card']").First,
            expectedTitle: "Card - Quark Suite");

        var card = page.Locator("[data-slot='card']").First;
        await Assertions.Expect(card).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bflex\b"));
        await Assertions.Expect(card).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bflex-col\b"));
        await Assertions.Expect(card).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bgap-4\b"));
        await Assertions.Expect(card).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\brounded-xl\b"));
        await Assertions.Expect(card).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bbg-card\b"));
        await Assertions.Expect(card).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bpy-4\b"));
        await Assertions.Expect(card).ToHaveAttributeAsync("data-size", "default");

        await Assertions.Expect(card.Locator("[data-slot='card-header']").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"has-data-\[slot=card-description\]:grid-rows-\[auto_auto\]"));
        await Assertions.Expect(card.Locator("[data-slot='card-title']").First).ToContainTextAsync("Login to your account");
        await Assertions.Expect(card.Locator("[data-slot='card-description']").First).ToContainTextAsync("Enter your email");
        await Assertions.Expect(card.Locator("[data-slot='card-action']").First).ToBeVisibleAsync();
        await Assertions.Expect(card.Locator("[data-slot='card-content']").First).ToBeVisibleAsync();
        await Assertions.Expect(card.Locator("[data-slot='card-footer']").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bflex\b"));

        await page.GetByLabel("Email").FillAsync("demo@example.com");
        await page.GetByLabel("Password").FillAsync("secret");
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Login", Exact = true }).First).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
