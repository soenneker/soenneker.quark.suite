using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.TestEnvironment.Options;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

public abstract class QuarkPlaywrightTest : PlaywrightUnitTest
{
    private const int DefaultTimeoutMs = 5_000;
    private const int DefaultNavigationTimeoutMs = 30_000;

    static QuarkPlaywrightTest()
    {
        Assertions.SetDefaultExpectTimeout(DefaultTimeoutMs);
    }

    protected QuarkPlaywrightTest(QuarkPlaywrightHost host) : base(host)
    {
    }

    protected new async ValueTask<BrowserSession> CreateSession(PlaywrightSessionOptions? sessionOptions = null, CancellationToken cancellationToken = default)
    {
        var session = await base.CreateSession(sessionOptions, cancellationToken);
        session.Page.SetDefaultTimeout(DefaultTimeoutMs);
        session.Page.SetDefaultNavigationTimeout(DefaultNavigationTimeoutMs);

        return session;
    }

}
