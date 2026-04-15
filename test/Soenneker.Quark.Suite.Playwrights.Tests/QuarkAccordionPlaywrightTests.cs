using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkAccordionPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAccordionPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Accordion_demo_supports_vertical_keyboard_navigation_and_skips_disabled_items()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "What are your shipping options?", Exact = true }),
            expectedTitle: "Accordion - Quark Suite");

        ILocator firstBasicTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "How do I reset my password?", Exact = true });
        ILocator secondBasicTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Can I change my subscription plan?", Exact = true });
        ILocator thirdBasicTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "What payment methods do you accept?", Exact = true });

        await firstBasicTrigger.PressAsync("ArrowDown");
        await ExpectActiveElementAsync(page, secondBasicTrigger);

        await secondBasicTrigger.PressAsync("End");
        await ExpectActiveElementAsync(page, thirdBasicTrigger);

        await thirdBasicTrigger.PressAsync("Home");
        await ExpectActiveElementAsync(page, firstBasicTrigger);

        ILocator enabledTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Can I access my account history?", Exact = true });
        ILocator disabledTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Premium feature information", Exact = true });
        ILocator trailingTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "How do I update my email address?", Exact = true });

        await Assertions.Expect(disabledTrigger).ToBeDisabledAsync();

        await enabledTrigger.PressAsync("ArrowDown");
        await ExpectActiveElementAsync(page, trailingTrigger);

        await disabledTrigger.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(disabledTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.GetByText("Upgrade your plan to access this content.", new PageGetByTextOptions { Exact = true })).Not.ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Accordion_multiple_demo_keeps_multiple_items_open()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "What are your shipping options?", Exact = true }),
            expectedTitle: "Accordion - Quark Suite");

        ILocator firstTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Notification Settings", Exact = true });
        ILocator secondTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Privacy & Security", Exact = true });
        await Assertions.Expect(firstTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await secondTrigger.ClickAsync();

        await Assertions.Expect(firstTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(secondTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(page.GetByText("Manage how you receive notifications. You can enable email alerts for updates or push notifications for mobile devices.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Control your privacy settings and security preferences. Enable two-factor authentication, manage connected devices, review active sessions, and configure data sharing preferences. You can also download your data or delete your account.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    private static async Task ExpectActiveElementAsync(IPage page, ILocator locator)
    {
        string? id = await locator.GetAttributeAsync("id");
        Assert.False(string.IsNullOrWhiteSpace(id));

        await page.WaitForFunctionAsync(
            "(expectedId) => document.activeElement?.id === expectedId",
            id);
    }
}
