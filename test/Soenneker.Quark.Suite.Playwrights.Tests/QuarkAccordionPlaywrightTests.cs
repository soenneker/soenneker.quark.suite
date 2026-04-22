using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAccordionPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAccordionPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Accordion_demo_supports_vertical_keyboard_navigation_and_skips_disabled_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "What are your shipping options?", Exact = true }),
            expectedTitle: "Accordion - Quark Suite");

        var firstBasicTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "How do I reset my password?", Exact = true });
        var secondBasicTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Can I change my subscription plan?", Exact = true });
        var thirdBasicTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "What payment methods do you accept?", Exact = true });

        await firstBasicTrigger.PressAsync("ArrowDown");
        await ExpectActiveElementAsync(page, secondBasicTrigger);

        await secondBasicTrigger.PressAsync("End");
        await ExpectActiveElementAsync(page, thirdBasicTrigger);

        await thirdBasicTrigger.PressAsync("Home");
        await ExpectActiveElementAsync(page, firstBasicTrigger);

        var enabledTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Can I access my account history?", Exact = true });
        var disabledTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Premium feature information", Exact = true });
        var trailingTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "How do I update my email address?", Exact = true });

        await Assertions.Expect(disabledTrigger).ToBeDisabledAsync();

        await enabledTrigger.PressAsync("ArrowDown");
        await ExpectActiveElementAsync(page, trailingTrigger);

        await disabledTrigger.ScrollIntoViewIfNeededAsync();
        await disabledTrigger.EvaluateAsync("element => element.click()");
        await Assertions.Expect(disabledTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.GetByText("Upgrade your plan to access this content.", new PageGetByTextOptions { Exact = true })).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Accordion_multiple_demo_keeps_multiple_items_open()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "What are your shipping options?", Exact = true }),
            expectedTitle: "Accordion - Quark Suite");

        var firstTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Notification Settings", Exact = true });
        var secondTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Privacy & Security", Exact = true });
        await Assertions.Expect(firstTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await secondTrigger.ClickAsync();

        await Assertions.Expect(firstTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(secondTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(page.GetByText("Manage how you receive notifications. You can enable email alerts for updates or push notifications for mobile devices.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Control your privacy settings and security preferences. Enable two-factor authentication, manage connected devices, review active sessions, and configure data sharing preferences. You can also download your data or delete your account.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Accordion_basic_demo_only_keeps_one_item_open_and_allows_collapse()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "How do I reset my password?", Exact = true }),
            expectedTitle: "Accordion - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A basic accordion that shows one item at a time. The first item is open by default." }).First;
        var firstTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "How do I reset my password?", Exact = true });
        var secondTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Can I change my subscription plan?", Exact = true });
        var firstPanelId = await firstTrigger.GetAttributeAsync("aria-controls");
        var secondPanelId = await secondTrigger.GetAttributeAsync("aria-controls");

        Assert.False(string.IsNullOrWhiteSpace(firstPanelId));
        Assert.False(string.IsNullOrWhiteSpace(secondPanelId));

        var firstContent = page.Locator($"#{firstPanelId}");
        var secondContent = page.Locator($"#{secondPanelId}");

        await Assertions.Expect(firstTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(firstContent).ToBeVisibleAsync();

        await secondTrigger.ClickAsync();

        await Assertions.Expect(firstTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(secondTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(firstContent).Not.ToBeVisibleAsync();
        await Assertions.Expect(secondContent).ToBeVisibleAsync();

        await secondTrigger.ClickAsync();

        await Assertions.Expect(secondTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(secondContent).Not.ToBeVisibleAsync();
        await Assertions.Expect(firstContent).ToHaveCountAsync(0);
        await Assertions.Expect(secondContent).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Accordion_demo_preserves_container_width_when_switching_open_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(1400, 1000);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "What are your shipping options?", Exact = true }),
            expectedTitle: "Accordion - Quark Suite");

        var demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Official shadcn accordion demo." }).First;
        var accordion = demoSection.Locator("[data-slot='accordion']").First;
        var accordionContainer = accordion.Locator("xpath=..").First;
        var shippingTrigger = demoSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "What are your shipping options?", Exact = true });
        var returnsTrigger = demoSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "What is your return policy?", Exact = true });

        var shippingPanelId = await shippingTrigger.GetAttributeAsync("aria-controls");
        Assert.False(string.IsNullOrWhiteSpace(shippingPanelId));

        var shippingContent = page.Locator($"#{shippingPanelId}");
        var initialBox = await accordionContainer.BoundingBoxAsync();
        Assert.NotNull(initialBox);
        Assert.InRange(initialBox.Width, 300, 420);

        var shippingContentBox = await shippingContent.BoundingBoxAsync();
        Assert.NotNull(shippingContentBox);
        Assert.True(shippingContentBox.Height >= 40, $"Expected the initially open accordion panel to render full content height, but measured {shippingContentBox.Height}.");

        await returnsTrigger.ClickAsync();

        var afterSwitchBox = await accordionContainer.BoundingBoxAsync();
        Assert.NotNull(afterSwitchBox);
        Assert.InRange(Math.Abs(afterSwitchBox.Width - initialBox.Width), 0, 2);

        var returnsPanelId = await returnsTrigger.GetAttributeAsync("aria-controls");
        Assert.False(string.IsNullOrWhiteSpace(returnsPanelId));

        var returnsContent = page.Locator($"#{returnsPanelId}");
        var returnsContentBox = await returnsContent.BoundingBoxAsync();
        Assert.NotNull(returnsContentBox);
        Assert.True(returnsContentBox.Height >= 40, $"Expected the newly opened accordion panel to render full content height, but measured {returnsContentBox.Height}.");
    }

    [Test]
    public async ValueTask Accordion_rtl_demo_preserves_single_open_state_and_rtl_keyboard_navigation()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "كيف يمكنني إعادة تعيين كلمة المرور؟", Exact = true }),
            expectedTitle: "Accordion - Quark Suite");

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Official shadcn RTL example." }).First;
        var firstTrigger = rtlSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "كيف يمكنني إعادة تعيين كلمة المرور؟", Exact = true });
        var secondTrigger = rtlSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "هل يمكنني تغيير خطة الاشتراك الخاصة بي؟", Exact = true });
        var thirdTrigger = rtlSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "ما هي طرق الدفع التي تقبلونها؟", Exact = true });

        await firstTrigger.PressAsync("ArrowDown");
        await ExpectActiveElementAsync(page, secondTrigger);

        await secondTrigger.PressAsync("End");
        await ExpectActiveElementAsync(page, thirdTrigger);

        await secondTrigger.ClickAsync();

        await Assertions.Expect(firstTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(secondTrigger).ToHaveAttributeAsync("aria-expanded", "true");
    }

    [Test]
    public async ValueTask Accordion_force_mount_demo_keeps_closed_content_mounted_with_closed_state_metadata()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}accordions",
            static p => p.Locator("[data-testid='accordion-force-mount-demo']"),
            expectedTitle: "Accordion - Quark Suite");

        var demo = page.Locator("[data-testid='accordion-force-mount-demo']");
        var trigger = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Keep details mounted for animation", Exact = true });
        var contentInner = demo.Locator(".accordion-force-mount-content").First;
        var content = contentInner.Locator("xpath=..");

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(content).ToHaveAttributeAsync("data-state", "closed");
        await Assertions.Expect(contentInner).ToContainTextAsync("Force mounted accordion details remain in the DOM while closed.");

        await trigger.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToHaveAttributeAsync("data-state", "open");
    }

    private static async Task ExpectActiveElementAsync(IPage page, ILocator locator)
    {
        var id = await locator.GetAttributeAsync("id");
        Assert.False(string.IsNullOrWhiteSpace(id));

        await page.WaitForFunctionAsync(
            "(expectedId) => document.activeElement?.id === expectedId",
            id);
    }
}
