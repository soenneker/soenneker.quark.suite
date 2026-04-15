using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkComponentPlaywrightTests : PlaywrightUnitTest
{
    public QuarkComponentPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
        Logger.LogInformation("Initially loaded");
    }

    [Fact]
    public async ValueTask Landing_page_loads()
    {
        Logger.LogInformation("Initially loaded");
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            BaseUrl,
            static p => p.Locator("main"),
            expectedTitle: "The Foundation for your Design System - Quark Suite");

        await Assertions.Expect(page.Locator("main")).ToBeVisibleAsync();
        await Assertions.Expect(page).ToHaveTitleAsync("The Foundation for your Design System - Quark Suite");
    }

    [Theory]
    [MemberData(nameof(QuarkComponentSpecs.All), MemberType = typeof(QuarkComponentSpecs))]
    public async ValueTask Component_demo_page_loads(string componentName, string route, string expectedTitle)
    {
        Logger.LogInformation("Loading {ComponentName} demo route {Route}", componentName, route);

        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}{route.TrimStart('/')}",
            static p => p.Locator("main"),
            expectedTitle: expectedTitle);

        await Assertions.Expect(page.Locator("main")).ToBeVisibleAsync();
        await Assertions.Expect(page).ToHaveTitleAsync(expectedTitle);
    }
}
