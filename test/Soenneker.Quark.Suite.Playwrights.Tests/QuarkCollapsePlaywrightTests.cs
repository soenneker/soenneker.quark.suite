using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using System.Threading;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCollapsePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCollapsePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Collapse_examples_toggle_targets_and_programmatic_controls(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/collapses",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle Collapse", Exact = true }));

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
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async ValueTask Collapse_interpolates_measured_dimensions(bool horizontal, CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/collapses",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle Collapse", Exact = true }));
        var selector = horizontal ? "#horizontalCollapse" : "#basicCollapse";
        var collapse = page.Locator(selector);
        await page.WaitForFunctionAsync("selector => document.querySelector(selector).style.maxHeight || document.querySelector(selector).style.maxWidth", selector);
        await collapse.EvaluateAsync("el => el.style.transitionDuration = '1s'");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions
        {
            Name = horizontal ? "Toggle Horizontal" : "Toggle Collapse", Exact = true
        }).First.ClickAsync();
        await page.WaitForFunctionAsync("selector => document.querySelector(selector).getAnimations().length > 0", selector);
        var ratio = await collapse.EvaluateAsync<double>("""
            (el, horizontal) => {
                for (const animation of el.getAnimations()) {
                    animation.pause();
                    animation.currentTime = 500;
                }
                const dimension = horizontal ? 'width' : 'height';
                return el.getBoundingClientRect()[dimension] / el.firstElementChild.getBoundingClientRect()[dimension];
            }
            """, horizontal);
        ratio.Should().BeInRange(0.1, 0.9);
        await collapse.EvaluateAsync("el => el.getAnimations().forEach(animation => animation.finish())");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions
        {
            Name = horizontal ? "Toggle Horizontal" : "Toggle Collapse", Exact = true
        }).First.ClickAsync();
        await page.WaitForFunctionAsync("selector => document.querySelector(selector).getAnimations().length > 0", selector);
        await collapse.EvaluateAsync("el => el.getAnimations().forEach(animation => animation.finish())");
        var size = await collapse.EvaluateAsync<double>("(el, horizontal) => el.getBoundingClientRect()[horizontal ? 'width' : 'height']", horizontal);
        size.Should().BeLessThan(1);
        await Assertions.Expect(collapse).ToHaveAttributeAsync("inert", "true");
    }
}
