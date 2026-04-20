using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkAspectRatioPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAspectRatioPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Aspect_ratio_examples_preserve_landscape_square_and_portrait_frames()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}aspect-ratios",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Portrait ratios work well for mobile previews and editorial imagery." }).First,
            expectedTitle: "Aspect Ratio - Quark Suite");

        ILocator landscapeFrame = page.Locator("#aspect-ratio-landscape-demo [data-radix-aspect-ratio-wrapper]");
        ILocator squareFrame = page.Locator("#aspect-ratio-square-demo [data-radix-aspect-ratio-wrapper]");
        ILocator portraitFrame = page.Locator("#aspect-ratio-portrait-demo [data-radix-aspect-ratio-wrapper]");
        ILocator landscapeHost = page.Locator("#aspect-ratio-landscape-demo");
        ILocator squareHost = page.Locator("#aspect-ratio-square-demo");
        ILocator portraitHost = page.Locator("#aspect-ratio-portrait-demo");

        await Assertions.Expect(landscapeFrame).ToHaveAttributeAsync("style", new System.Text.RegularExpressions.Regex(@"padding-bottom:\s*56\.25"));
        await Assertions.Expect(squareFrame).ToHaveAttributeAsync("style", new System.Text.RegularExpressions.Regex(@"padding-bottom:\s*100(\.0+)?%"));
        await Assertions.Expect(portraitFrame).ToHaveAttributeAsync("style", new System.Text.RegularExpressions.Regex(@"padding-bottom:\s*177\.777"));

        LocatorBoundingBoxResult? landscapeBox = await landscapeHost.BoundingBoxAsync();
        LocatorBoundingBoxResult? squareBox = await squareHost.BoundingBoxAsync();
        LocatorBoundingBoxResult? portraitBox = await portraitHost.BoundingBoxAsync();

        Assert.NotNull(landscapeBox);
        Assert.NotNull(squareBox);
        Assert.NotNull(portraitBox);

        Assert.True(landscapeBox.Width >= 320, $"Expected the landscape aspect ratio host to render with width, but measured {landscapeBox.Width}.");
        Assert.True(squareBox.Width >= 160, $"Expected the square aspect ratio host to render with width, but measured {squareBox.Width}.");
        Assert.True(portraitBox.Width >= 160, $"Expected the portrait aspect ratio host to render with width, but measured {portraitBox.Width}.");

        double landscapeRatio = landscapeBox.Width / landscapeBox.Height;
        double squareRatio = squareBox.Width / squareBox.Height;
        double portraitRatio = portraitBox.Width / portraitBox.Height;

        Assert.InRange(landscapeRatio, 1.70, 1.85);
        Assert.InRange(squareRatio, 0.95, 1.05);
        Assert.InRange(portraitRatio, 0.50, 0.62);
    }
}
