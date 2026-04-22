using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkStepsPlaywrightTests : PlaywrightUnitTest
{
    public QuarkStepsPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Steps_disabled_demo_keeps_disabled_step_inert_and_preserves_current_panel()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}stepsdemo",
            static p => p.Locator("#steps-disabled-demo"),
            expectedTitle: "Steps - Quark Suite");

        var demo = page.Locator("#steps-disabled-demo");
        var current = demo.Locator("#steps-disabled-current");
        var detailsTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Details" }).First;
        var billingTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Billing" }).First;
        var confirmTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Confirm" }).First;

        await Assertions.Expect(current).ToContainTextAsync("details");
        await Assertions.Expect(detailsTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(billingTab).ToHaveAttributeAsync("aria-disabled", "true");
        await Assertions.Expect(billingTab).Not.ToHaveAttributeAsync("href", new System.Text.RegularExpressions.Regex(".+"));

        await billingTab.EvaluateAsync("element => element.click()");

        await Assertions.Expect(current).ToContainTextAsync("details");
        await Assertions.Expect(detailsTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(confirmTab).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(demo.GetByRole(AriaRole.Tabpanel)).ToContainTextAsync("Details panel");
    }
}
