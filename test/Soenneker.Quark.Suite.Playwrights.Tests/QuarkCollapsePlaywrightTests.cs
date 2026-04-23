using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCollapsePlaywrightTests : PlaywrightUnitTest
{
    public QuarkCollapsePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Collapse_examples_toggle_targets_and_programmatic_controls()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}collapses",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle Collapse", Exact = true }),
            expectedTitle: "Collapses - Quark Suite");

        var basicCollapse = page.Locator("#basicCollapse");
        await Assertions.Expect(basicCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle Collapse", Exact = true }).First.ClickAsync();
        await Assertions.Expect(basicCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        var firstCollapse = page.Locator("#firstCollapse");
        var secondCollapse = page.Locator("#secondCollapse");
        await Assertions.Expect(firstCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));
        await Assertions.Expect(secondCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Both", Exact = true }).First.ClickAsync();
        await Assertions.Expect(firstCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));
        await Assertions.Expect(secondCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        var showButton = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show", Exact = true }).First;
        var programmaticCollapse = page.Locator("[data-slot='collapse']").Last;

        await Assertions.Expect(page.GetByText("State: Collapsed", new PageGetByTextOptions { Exact = false }).First).ToBeVisibleAsync();
        await Assertions.Expect(programmaticCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await showButton.ClickAsync();
        await Assertions.Expect(page.GetByText("State: Expanded", new PageGetByTextOptions { Exact = false }).First).ToBeVisibleAsync();
        await Assertions.Expect(programmaticCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Hide", Exact = true }).First.ClickAsync();
        await Assertions.Expect(page.GetByText("State: Collapsed", new PageGetByTextOptions { Exact = false }).First).ToBeVisibleAsync();
        await Assertions.Expect(programmaticCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));
    }
}
