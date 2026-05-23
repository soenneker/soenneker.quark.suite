using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkStepperPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkStepperPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Stepper_demo_exposes_reui_slots_and_active_state()
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
            $"{BaseUrl}components/stepper",
            static p => p.Locator("[data-slot='stepper']").First,
            expectedTitle: "Stepper - Quark Suite");

        var stepper = page.Locator("[data-slot='stepper']").First;
        var nav = stepper.Locator("[data-slot='stepper-nav']").First;
        var activeTrigger = stepper.Locator("[data-slot='stepper-trigger'][aria-selected='true']").First;
        var firstItem = stepper.Locator("[data-slot='stepper-item']").First;
        var activeContent = stepper.Locator("[data-slot='stepper-content']").First;

        await Assertions.Expect(stepper).ToHaveAttributeAsync("role", "tablist");
        await Assertions.Expect(stepper).ToHaveAttributeAsync("data-orientation", "horizontal");
        await Assertions.Expect(nav).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bgroup/stepper-nav\b"));
        await Assertions.Expect(firstItem).ToHaveAttributeAsync("data-state", "completed");
        await Assertions.Expect(activeTrigger).ToHaveAttributeAsync("data-state", "active");
        await Assertions.Expect(activeTrigger).ToHaveAttributeAsync("aria-controls", "stepper-panel-2");
        await Assertions.Expect(activeTrigger).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(stepper.Locator("[data-slot='stepper-indicator']").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bdata-\[state=active\]:bg-primary\b"));
        await Assertions.Expect(stepper.Locator("[data-slot='stepper-separator']").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bgroup-data-\[orientation=horizontal\]/stepper-nav:flex-1\b"));
        await Assertions.Expect(activeContent).ToContainTextAsync("Security verification is active.");

        await stepper.GetByRole(AriaRole.Tab, new() { NameRegex = new System.Text.RegularExpressions.Regex("Finish") }).ClickAsync();
        await Assertions.Expect(stepper.Locator("[data-slot='stepper-content']").First).ToContainTextAsync("Final review is active.");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
