using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAspectRatioPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAspectRatioPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Aspect_ratio_examples_preserve_landscape_square_and_portrait_frames()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}aspect-ratios",
            static p => p.Locator("section").Filter(new LocatorFilterOptions
                { HasText = "Portrait ratios work well for mobile previews and editorial imagery." }).First, expectedTitle: "Aspect Ratio - Quark Suite");

        var landscapeFrame = page.Locator("#aspect-ratio-landscape-demo [data-radix-aspect-ratio-wrapper]");
        var squareFrame = page.Locator("#aspect-ratio-square-demo [data-radix-aspect-ratio-wrapper]");
        var portraitFrame = page.Locator("#aspect-ratio-portrait-demo [data-radix-aspect-ratio-wrapper]");
        var landscapeHost = page.Locator("#aspect-ratio-landscape-demo");
        var squareHost = page.Locator("#aspect-ratio-square-demo");
        var portraitHost = page.Locator("#aspect-ratio-portrait-demo");

        await Assertions.Expect(landscapeFrame).ToHaveAttributeAsync("style", new System.Text.RegularExpressions.Regex(@"padding-bottom:\s*56\.25"));
        await Assertions.Expect(squareFrame).ToHaveAttributeAsync("style", new System.Text.RegularExpressions.Regex(@"padding-bottom:\s*100(\.0+)?%"));
        await Assertions.Expect(portraitFrame).ToHaveAttributeAsync("style", new System.Text.RegularExpressions.Regex(@"padding-bottom:\s*177\.777"));

        var landscapeBox = await landscapeHost.BoundingBoxAsync();
        var squareBox = await squareHost.BoundingBoxAsync();
        var portraitBox = await portraitHost.BoundingBoxAsync();

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