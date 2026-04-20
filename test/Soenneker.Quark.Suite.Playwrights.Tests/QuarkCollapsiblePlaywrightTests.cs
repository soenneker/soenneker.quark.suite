using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkCollapsiblePlaywrightTests : PlaywrightUnitTest
{
    public QuarkCollapsiblePlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Collapsible_demo_toggles_content_and_disabled_trigger_stays_closed()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}collapsibles",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle", Exact = true }),
            expectedTitle: "Collapsible - Quark Suite");

        ILocator demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Compact starred-repositories example with a small toggle trigger." }).First;
        ILocator toggle = demoSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle", Exact = true });
        ILocator colors = demoSection.GetByText("@radix-ui/colors", new LocatorGetByTextOptions { Exact = true });

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(colors).Not.ToBeVisibleAsync();

        await toggle.ClickAsync();

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(colors).ToBeVisibleAsync();

        await toggle.ClickAsync();

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(colors).Not.ToBeVisibleAsync();

        ILocator disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disable the whole collapsible when a panel should stay locked." }).First;
        ILocator locked = disabledSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Production deployment Locked", Exact = false });

        await Assertions.Expect(locked).ToHaveAttributeAsync("data-disabled", "true");
        await Assertions.Expect(locked).ToHaveAttributeAsync("aria-expanded", "true");

        await locked.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(locked).ToHaveAttributeAsync("aria-expanded", "true");
    }
}
