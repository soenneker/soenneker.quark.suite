using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCollapsiblePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCollapsiblePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Collapsible_demo_toggles_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}collapsibles",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle", Exact = true }),
            expectedTitle: "Collapsible - Quark Suite");

        var demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Compact starred-repositories example with a small toggle trigger." }).First;
        var toggle = demoSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle", Exact = true });
        var colors = demoSection.GetByText("@radix-ui/colors", new LocatorGetByTextOptions { Exact = true });

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(colors).Not.ToBeVisibleAsync();

        await toggle.ClickAsync();

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(colors).ToBeVisibleAsync();

        await toggle.ClickAsync();

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(colors).Not.ToBeVisibleAsync();

    }
}
