using System;
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

        var firstStepper = page.Locator("[data-slot='stepper']").First;
        var stepper = page.Locator("[data-slot='stepper'][data-orientation='vertical']").First;
        var nav = stepper.Locator("[data-slot='stepper-nav']").First;
        var activeTrigger = stepper.Locator("[data-slot='stepper-trigger'][aria-selected='true']").First;
        var firstItem = stepper.Locator("[data-slot='stepper-item']").First;
        var activeContent = stepper.Locator("[data-slot='stepper-content']").First;
        var firstIndicator = stepper.Locator("[data-slot='stepper-indicator']").First;
        var firstSeparator = stepper.Locator("[data-slot='stepper-separator']").First;

        await Assertions.Expect(firstStepper).ToHaveAttributeAsync("data-orientation", "horizontal");
        await Assertions.Expect(stepper).ToHaveAttributeAsync("role", "tablist");
        await Assertions.Expect(stepper).ToHaveAttributeAsync("data-orientation", "vertical");
        await Assertions.Expect(nav).ToHaveAttributeAsync("data-orientation", "vertical");
        await Assertions.Expect(stepper.Locator("[data-slot='stepper-item']")).ToHaveCountAsync(3);
        await Assertions.Expect(firstItem).ToHaveAttributeAsync("data-state", "active");
        await Assertions.Expect(activeTrigger).ToHaveAttributeAsync("data-state", "active");
        await Assertions.Expect(activeTrigger).ToHaveAttributeAsync("aria-controls", "stepper-panel-1");
        await Assertions.Expect(activeTrigger).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(firstIndicator).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bbg-primary\b"));
        await Assertions.Expect(firstSeparator).ToHaveCSSAsync("position", "absolute");
        await Assertions.Expect(activeContent).ToContainTextAsync("Profile content");

        var indicatorBox = await firstIndicator.BoundingBoxAsync();
        var separatorBox = await firstSeparator.BoundingBoxAsync();
        indicatorBox.Should().NotBeNull();
        separatorBox.Should().NotBeNull();
        Math.Abs(separatorBox!.X + separatorBox.Width / 2 - (indicatorBox!.X + indicatorBox.Width / 2)).Should().BeLessThan(2);

        await stepper.GetByRole(AriaRole.Button, new() { NameRegex = new System.Text.RegularExpressions.Regex("^Next$") }).ClickAsync();
        await Assertions.Expect(stepper.Locator("[data-slot='stepper-content']").First).ToContainTextAsync("Business content");

        await stepper.GetByRole(AriaRole.Button, new() { NameRegex = new System.Text.RegularExpressions.Regex("^Back$") }).ClickAsync();
        await Assertions.Expect(stepper.Locator("[data-slot='stepper-content']").First).ToContainTextAsync("Profile content");

        await stepper.GetByRole(AriaRole.Tab, new() { NameRegex = new System.Text.RegularExpressions.Regex("Verify") }).ClickAsync();
        await Assertions.Expect(stepper.Locator("[data-slot='stepper-content']").First).ToContainTextAsync("Verify content");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
