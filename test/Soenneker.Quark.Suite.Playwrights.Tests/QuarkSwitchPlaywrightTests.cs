using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkSwitchPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSwitchPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Switch_inline_content_demo_label_clicks_toggle_wrapped_switches()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}switches",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Inline Content" }).Locator("[role='switch']").First,
            expectedTitle: "Switches - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Inline Content" }).First;
        var notifications = section.Locator("[role='switch']").Nth(0);
        var darkMode = section.Locator("[role='switch']").Nth(1);

        await Assertions.Expect(notifications).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(darkMode).ToHaveAttributeAsync("aria-checked", "false");

        await section.GetByText("Enable notifications", new LocatorGetByTextOptions { Exact = true }).ClickAsync();
        await section.GetByText("Dark mode", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        await Assertions.Expect(notifications).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(darkMode).ToHaveAttributeAsync("aria-checked", "true");
    }

[Fact]
    public async ValueTask Switch_form_toggles_marketing_and_preserves_disabled_security_value()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}switches",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }),
            expectedTitle: "Switches - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Settings-style rows with descriptions and a submitted value preview." }).First;
        var marketing = section.Locator("[role='switch']").Nth(0);
        var security = section.Locator("[role='switch']").Nth(1);

        await Assertions.Expect(marketing).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(security).ToHaveAttributeAsync("aria-checked", "true");

        await marketing.ClickAsync();
        await Assertions.Expect(marketing).ToHaveAttributeAsync("aria-checked", "true");

        await security.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(security).ToHaveAttributeAsync("aria-checked", "true");

        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true }).ClickAsync();

        var json = section.Locator("pre").First;
        await Assertions.Expect(json).ToContainTextAsync("\"marketing_emails\": true");
        await Assertions.Expect(json).ToContainTextAsync("\"security_emails\": true");
    }
}
