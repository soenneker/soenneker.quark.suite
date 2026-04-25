using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSurfacePlaywrightTests : PlaywrightUnitTest
{
    public QuarkSurfacePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Skeleton_and_spinner_demos_match_static_accessibility_contracts()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var pageErrors = new List<string>();

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}skeletons",
            static p => p.Locator("[data-slot='skeleton']").First,
            expectedTitle: "Skeleton - Quark Suite");

        var skeleton = page.Locator("[data-slot='skeleton']").First;
        await Assertions.Expect(skeleton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)animate-pulse(\s|$)"));
        await Assertions.Expect(skeleton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)rounded-md(\s|$)"));
        await Assertions.Expect(skeleton).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)bg-accent(\s|$)"));
        await Assertions.Expect(skeleton).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)bg-muted(\s|$)"));

        await page.GotoAndWaitForReady(
            $"{BaseUrl}spinners",
            static p => p.GetByRole(AriaRole.Status, new PageGetByRoleOptions { Name = "Loading", Exact = true }).First,
            expectedTitle: "Spinners - Quark Suite");

        var spinner = page.GetByRole(AriaRole.Status, new PageGetByRoleOptions { Name = "Loading", Exact = true }).First;
        await Assertions.Expect(spinner).ToHaveAttributeAsync("aria-label", "Loading");
        await Assertions.Expect(spinner).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)animate-spin(\s|$)"));
        await Assertions.Expect(spinner).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)size-4(\s|$)"));

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
