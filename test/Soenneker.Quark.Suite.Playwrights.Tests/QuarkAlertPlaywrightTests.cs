using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAlertPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkAlertPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Alert_demo_renders_roles_variants_action_and_no_console_errors()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}alerts",
            static p => p.GetByRole(AriaRole.Alert).First,
            expectedTitle: "Alerts - Quark Suite");

        var firstAlert = page.GetByRole(AriaRole.Alert).First;
        var destructive = page.GetByRole(AriaRole.Alert).Filter(new LocatorFilterOptions { HasText = "Payment failed" }).First;
        var actionAlert = page.GetByRole(AriaRole.Alert).Filter(new LocatorFilterOptions { HasText = "Dark mode is now available" }).First;

        await Assertions.Expect(firstAlert).ToContainTextAsync("Payment successful");
        await Assertions.Expect(destructive).ToContainTextAsync("Payment failed");
        await Assertions.Expect(actionAlert.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Enable", Exact = true })).ToBeVisibleAsync();

        string? firstClasses = await firstAlert.GetAttributeAsync("class");
        firstClasses.Should().Contain("group/alert");
        firstClasses.Should().Contain("has-[>svg]:grid-cols-[auto_1fr]");
        firstClasses.Should().Contain("has-[>svg]:gap-x-2");
        firstClasses.Should().Contain("bg-card");
        firstClasses.Should().Contain("text-card-foreground");
        firstClasses.Should().Contain("rounded-lg");
        firstClasses.Should().Contain("px-2.5");
        firstClasses.Should().Contain("py-2");
        (await firstAlert.GetAttributeAsync("style") ?? "").Should().NotContain("border-color");

        string? destructiveClasses = await destructive.GetAttributeAsync("class");
        destructiveClasses.Should().Contain("bg-card");
        destructiveClasses.Should().Contain("text-destructive");
        destructiveClasses.Should().Contain("*:data-[slot=alert-description]:text-destructive/90");
        destructiveClasses.Should().NotContain("bg-alert-danger-bg");
        (await destructive.GetAttributeAsync("style") ?? "").Should().NotContain("color: white");

        var title = firstAlert.Locator("[data-slot='alert-title']").First;
        var description = firstAlert.Locator("[data-slot='alert-description']").First;

        (await title.GetAttributeAsync("class")).Should().Contain("font-medium");
        (await description.GetAttributeAsync("class")).Should().Contain("text-muted-foreground");

        consoleErrors.Should().BeEmpty();
        sawPageError.Should().BeFalse();
    }
}
