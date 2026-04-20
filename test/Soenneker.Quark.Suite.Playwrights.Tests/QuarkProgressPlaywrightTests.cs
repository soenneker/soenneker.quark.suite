using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkProgressPlaywrightTests : PlaywrightUnitTest
{
    public QuarkProgressPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Progress_examples_expose_accessible_labelling_controlled_updates_and_indeterminate_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}progresses",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Associate the bar with a label using matching id and for values." }).First,
            expectedTitle: "Progress - Quark Suite");

        ILocator labelledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Associate the bar with a label using matching id and for values." }).First;
        ILocator labelledProgress = labelledSection.GetByRole(AriaRole.Progressbar, new LocatorGetByRoleOptions { Name = "Upload progress 66%", Exact = true });
        await Assertions.Expect(labelledProgress).ToHaveAttributeAsync("aria-valuenow", "66");
        await Assertions.Expect(labelledProgress).ToHaveAttributeAsync("aria-labelledby", "progress-upload-label");

        ILocator controlledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a slider to drive the bar." }).First;
        ILocator controlledProgress = controlledSection.GetByRole(AriaRole.Progressbar).First;
        ILocator slider = controlledSection.GetByRole(AriaRole.Slider).First;

        await Assertions.Expect(controlledProgress).ToHaveAttributeAsync("aria-valuenow", "50");
        await slider.FocusAsync();
        for (var i = 0; i < 5; i++)
            await slider.PressAsync("ArrowRight");

        await Assertions.Expect(controlledProgress).ToHaveAttributeAsync("aria-valuenow", "55");
        await Assertions.Expect(controlledSection).ToContainTextAsync("Current: 55%.");

        ILocator indeterminateSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Shows activity without a specific percentage." }).First;
        ILocator indeterminateProgress = indeterminateSection.GetByRole(AriaRole.Progressbar).First;
        await Assertions.Expect(indeterminateProgress).ToHaveAttributeAsync("data-state", "indeterminate");
        await Assertions.Expect(indeterminateProgress).Not.ToHaveAttributeAsync("aria-valuenow", new System.Text.RegularExpressions.Regex(".+"));
        await Assertions.Expect(indeterminateProgress).ToHaveAttributeAsync("aria-valuetext", "Preparing assets");
    }
}
