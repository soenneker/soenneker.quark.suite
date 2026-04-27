using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkProgressPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkProgressPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Progress_examples_expose_accessible_labelling_controlled_updates_and_indeterminate_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}progresses",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Associate the bar with a label using matching id and for values." }).First,
            expectedTitle: "Progress - Quark Suite");

        var labelledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Associate the bar with a label using matching id and for values." }).First;
        var labelledProgress = labelledSection.GetByRole(AriaRole.Progressbar, new LocatorGetByRoleOptions { Name = "Upload progress 66%", Exact = true });
        await Assertions.Expect(labelledProgress).ToHaveAttributeAsync("aria-valuenow", "66");
        await Assertions.Expect(labelledProgress).ToHaveAttributeAsync("data-state", "loading");
        await Assertions.Expect(labelledProgress).ToHaveAttributeAsync("aria-labelledby", "progress-upload-label");

        var controlledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a slider to drive the bar." }).First;
        var controlledProgress = controlledSection.GetByRole(AriaRole.Progressbar).First;
        var slider = controlledSection.GetByRole(AriaRole.Slider).First;

        await Assertions.Expect(controlledProgress).ToHaveAttributeAsync("aria-valuenow", "50");
        await slider.FocusAsync();
        for (var i = 0; i < 5; i++)
            await slider.PressAsync("ArrowRight");

        await Assertions.Expect(controlledProgress).ToHaveAttributeAsync("aria-valuenow", "55");
        await Assertions.Expect(controlledSection).ToContainTextAsync("Current: 55%.");

        var indeterminateSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Shows activity without a specific percentage." }).First;
        var indeterminateProgress = indeterminateSection.GetByRole(AriaRole.Progressbar).First;
        await Assertions.Expect(indeterminateProgress).ToHaveAttributeAsync("data-state", "indeterminate");
        await Assertions.Expect(indeterminateProgress).Not.ToHaveAttributeAsync("aria-valuenow", new System.Text.RegularExpressions.Regex(".+"));
        await Assertions.Expect(indeterminateProgress).ToHaveAttributeAsync("aria-valuetext", "Preparing assets");

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
