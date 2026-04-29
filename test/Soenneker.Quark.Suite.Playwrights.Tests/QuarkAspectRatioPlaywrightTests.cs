using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAspectRatioPlaywrightTests : QuarkPlaywrightTest
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
            static p => p.Locator("#aspect-ratio-portrait-demo"), expectedTitle: "Aspect Ratio - Quark Suite");

        var landscapeFrame = page.Locator("#aspect-ratio-landscape-demo [data-radix-aspect-ratio-wrapper]");
        var squareFrame = page.Locator("#aspect-ratio-square-demo [data-radix-aspect-ratio-wrapper]");
        var portraitFrame = page.Locator("#aspect-ratio-portrait-demo [data-radix-aspect-ratio-wrapper]");
        var landscapeHost = page.Locator("#aspect-ratio-landscape-demo");
        var squareHost = page.Locator("#aspect-ratio-square-demo");
        var portraitHost = page.Locator("#aspect-ratio-portrait-demo");

        await Assertions.Expect(landscapeHost.Locator("[data-slot='aspect-ratio']").First).ToBeVisibleAsync();
        await Assertions.Expect(squareHost.Locator("[data-slot='aspect-ratio']").First).ToBeVisibleAsync();
        await Assertions.Expect(portraitHost.Locator("[data-slot='aspect-ratio']").First).ToBeVisibleAsync();

        var landscapeBox = await landscapeHost.BoundingBoxAsync();
        var squareBox = await squareHost.BoundingBoxAsync();
        var portraitBox = await portraitHost.BoundingBoxAsync();

        (landscapeBox).Should().NotBeNull();
        (squareBox).Should().NotBeNull();
        (portraitBox).Should().NotBeNull();

        (landscapeBox.Width >= 320).Should().BeTrue();
        (squareBox.Width >= 160).Should().BeTrue();
        (portraitBox.Width >= 160).Should().BeTrue();

        double landscapeRatio = landscapeBox.Width / landscapeBox.Height;
        double squareRatio = squareBox.Width / squareBox.Height;
        double portraitRatio = portraitBox.Width / portraitBox.Height;

        (landscapeRatio).Should().BeInRange(1.70, 1.85);
        (squareRatio).Should().BeInRange(0.95, 1.05);
        (portraitRatio).Should().BeInRange(0.50, 0.62);
    }
}
