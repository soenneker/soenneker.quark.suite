using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkStepsPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkStepsPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Steps_disabled_demo_keeps_disabled_step_inert_and_preserves_current_panel()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}stepsdemo",
            static p => p.Locator("#steps-disabled-demo"),
            expectedTitle: "Steps - Quark Suite");

        var demo = page.Locator("#steps-disabled-demo");
        var current = demo.Locator("#steps-disabled-current");
        var detailsTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Details" }).First;
        var billingTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Billing" }).First;
        var confirmTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Confirm" }).First;

        await Assertions.Expect(demo.Locator("[role='tablist']")).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bflex-wrap\b"));
        await Assertions.Expect(current).ToContainTextAsync("details");
        await Assertions.Expect(detailsTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(detailsTab).ToHaveAttributeAsync("aria-current", "step");
        await Assertions.Expect(detailsTab).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(billingTab).ToHaveAttributeAsync("aria-disabled", "true");
        await Assertions.Expect(billingTab).ToHaveAttributeAsync("tabindex", "-1");
        await Assertions.Expect(billingTab).Not.ToHaveAttributeAsync("href", new System.Text.RegularExpressions.Regex(".+"));

        await billingTab.EvaluateAsync("element => element.click()");

        await Assertions.Expect(current).ToContainTextAsync("details");
        await Assertions.Expect(detailsTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(confirmTab).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(demo.GetByRole(AriaRole.Tabpanel)).ToContainTextAsync("Details panel");

        await detailsTab.FocusAsync();
        await page.Keyboard.PressAsync("ArrowRight");

        await Assertions.Expect(confirmTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(confirmTab).ToBeFocusedAsync();
        await Assertions.Expect(current).ToContainTextAsync("confirm");
        await Assertions.Expect(demo.GetByRole(AriaRole.Tabpanel)).ToContainTextAsync("Confirm panel");

        await page.Keyboard.PressAsync("Home");

        await Assertions.Expect(detailsTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(detailsTab).ToBeFocusedAsync();
        await Assertions.Expect(current).ToContainTextAsync("details");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Steps_validation_demo_gates_state_and_url_fragment_navigation()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}stepsdemo#review",
            static p => p.Locator("#steps-validation-demo"),
            expectedTitle: "Steps - Quark Suite");

        var demo = page.Locator("#steps-validation-demo");
        var current = demo.Locator("#steps-validation-current");
        var personalTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Personal Information" }).First;
        var contactTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Contact Information" }).First;
        var reviewTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Review & Submit" }).First;
        var next = demo.Locator("#steps-validation-next");

        await Assertions.Expect(current).ToContainTextAsync("personal");
        await Assertions.Expect(personalTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(reviewTab).ToHaveAttributeAsync("aria-selected", "false");

        await page.GotoAndWaitForReady(
            $"{BaseUrl}stepsdemo",
            static p => p.Locator("#steps-validation-demo"),
            expectedTitle: "Steps - Quark Suite");

        demo = page.Locator("#steps-validation-demo");
        current = demo.Locator("#steps-validation-current");
        personalTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Personal Information" }).First;
        contactTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Contact Information" }).First;
        reviewTab = demo.Locator("[role='tab']").Filter(new LocatorFilterOptions { HasText = "Review & Submit" }).First;
        next = demo.Locator("#steps-validation-next");

        await reviewTab.ClickAsync();

        await Assertions.Expect(current).ToContainTextAsync("personal");
        await Assertions.Expect(personalTab).ToHaveAttributeAsync("aria-selected", "true");
        page.Url.Should().NotContain("#review");

        await page.Locator("#firstName").FillAsync("Ada");
        await page.Locator("#lastName").FillAsync("Lovelace");
        await next.ClickAsync();

        await Assertions.Expect(current).ToContainTextAsync("contact");
        await Assertions.Expect(contactTab).ToHaveAttributeAsync("aria-selected", "true");
        page.Url.Should().EndWith("#contact");

        await next.ClickAsync();

        await Assertions.Expect(current).ToContainTextAsync("contact");
        await Assertions.Expect(contactTab).ToHaveAttributeAsync("aria-selected", "true");
        page.Url.Should().EndWith("#contact");

        await page.Locator("#email").FillAsync("ada@example.com");
        await next.ClickAsync();

        await Assertions.Expect(current).ToContainTextAsync("review");
        await Assertions.Expect(reviewTab).ToHaveAttributeAsync("aria-selected", "true");
        page.Url.Should().EndWith("#review");
        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
