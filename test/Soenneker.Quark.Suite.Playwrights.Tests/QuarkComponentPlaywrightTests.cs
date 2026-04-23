using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.TestEnvironment.Options;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkComponentPlaywrightTests : PlaywrightUnitTest
{
    public QuarkComponentPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
        Logger.LogInformation("Initially loaded");
    }

  //  [Test]
    public async ValueTask Landing_page_loads()
    {
        Logger.LogInformation("Initially loaded");
        await using var session = await CreateSession(new PlaywrightSessionOptions {ReuseBrowserContextAcrossSessions = true, ReusePageAcrossSessions = true}, cancellationToken: System.Threading.CancellationToken.None);
        var page = session.Page;
        var main = page.GetByRole(AriaRole.Main).First;

        await page.GotoAndWaitForReady(
            BaseUrl,
            static p => p.GetByRole(AriaRole.Main).First,
            expectedTitle: "The Foundation for your Design System - Quark Suite");

        await Assertions.Expect(main).ToBeVisibleAsync();
        await Assertions.Expect(page).ToHaveTitleAsync("The Foundation for your Design System - Quark Suite");
    }

  //  [Test]
   // [MemberData(nameof(QuarkComponentSpecs.All), MemberType = typeof(QuarkComponentSpecs))]
    public async ValueTask Component_demo_page_loads(string componentName, string route, string expectedTitle)
    {
        Logger.LogInformation("Loading {ComponentName} demo route {Route}", componentName, route);

        await using var session = await CreateSession(new PlaywrightSessionOptions { ReuseBrowserContextAcrossSessions = true, ReusePageAcrossSessions = true }, cancellationToken: System.Threading.CancellationToken.None);
        var page = session.Page;
        var main = page.GetByRole(AriaRole.Main).First;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}{route.TrimStart('/')}",
            static p => p.GetByRole(AriaRole.Main).First,
            expectedTitle: expectedTitle);

        await Assertions.Expect(main).ToBeVisibleAsync();
        await Assertions.Expect(page).ToHaveTitleAsync(expectedTitle);
    }
}


