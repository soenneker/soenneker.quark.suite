using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkAvatarPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAvatarPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Avatar_fallback_demo_preserves_error_fallback_and_hides_it_after_successful_load()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}avatars",
            static p => p.Locator("#avatar-error-example"),
            expectedTitle: "Avatars - Quark Suite");

        ILocator brokenAvatar = page.Locator("#avatar-error-example");
        ILocator loadedAvatar = page.Locator("#avatar-loaded-example");

        await Assertions.Expect(brokenAvatar.GetByText("QE", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(brokenAvatar.GetByRole(AriaRole.Img)).ToHaveCountAsync(0);

        await Assertions.Expect(loadedAvatar.GetByAltText("Loaded avatar example", new LocatorGetByAltTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(loadedAvatar.GetByText("QL", new LocatorGetByTextOptions { Exact = true })).ToHaveCountAsync(0);
    }
}
