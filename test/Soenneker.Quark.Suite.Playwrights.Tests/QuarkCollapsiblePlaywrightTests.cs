using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCollapsiblePlaywrightTests : PlaywrightUnitTest
{
    public QuarkCollapsiblePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Collapsible_demo_toggles_content_and_disabled_trigger_stays_closed()
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

        var disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disable the whole collapsible when a panel should stay locked." }).First;
        var locked = disabledSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Production deployment Locked", Exact = false });

        await Assertions.Expect(locked).ToHaveAttributeAsync("data-disabled", "true");
        await Assertions.Expect(locked).ToHaveAttributeAsync("aria-expanded", "true");

        await locked.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(locked).ToHaveAttributeAsync("aria-expanded", "true");
    }
}
