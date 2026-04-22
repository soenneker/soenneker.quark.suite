using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAvatarPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAvatarPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Avatar_fallback_demo_preserves_error_fallback_and_hides_it_after_successful_load()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}avatars",
            static p => p.Locator("#avatar-error-example"),
            expectedTitle: "Avatars - Quark Suite");

        var brokenAvatar = page.Locator("#avatar-error-example");
        var loadedAvatar = page.Locator("#avatar-loaded-example");

        await Assertions.Expect(brokenAvatar.GetByText("QE", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(brokenAvatar.GetByRole(AriaRole.Img)).ToHaveCountAsync(0);

        await Assertions.Expect(loadedAvatar.GetByAltText("Loaded avatar example", new LocatorGetByAltTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(loadedAvatar.GetByText("QL", new LocatorGetByTextOptions { Exact = true })).ToHaveCountAsync(0);
    }
}
