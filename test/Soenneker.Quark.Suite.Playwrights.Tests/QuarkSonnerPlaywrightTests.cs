using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkSonnerPlaywrightTests : PlaywrightUnitTest
{
    public QuarkSonnerPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Sonner_demo_action_button_dismisses_the_rendered_toast()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Toast", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Toast", Exact = true }).ClickAsync();

        ILocator toast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Event has been created" }).First;
        await Assertions.Expect(toast).ToBeVisibleAsync();
        await Assertions.Expect(toast).ToContainTextAsync("Sunday, December 03, 2023 at 9:00 AM");

        await toast.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Undo", Exact = true }).ClickAsync();

        await Assertions.Expect(toast).Not.ToBeVisibleAsync();
    }

[Fact]
    public async ValueTask Sonner_action_demo_runs_callback_and_shows_follow_up_toast()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "With action", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "With action", Exact = true }).ClickAsync();

        ILocator archivedToast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Message archived" }).First;
        await Assertions.Expect(archivedToast).ToBeVisibleAsync();
        await Assertions.Expect(archivedToast).ToContainTextAsync("You can undo this action from the toast.");

        await archivedToast.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Undo", Exact = true }).ClickAsync();

        await Assertions.Expect(archivedToast).Not.ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Archive undone" }).First).ToBeVisibleAsync();
    }

[Fact]
    public async ValueTask Sonner_promise_demo_replaces_loading_state_with_success_result()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sonner",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Promise success", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Promise success", Exact = true }).ClickAsync();

        ILocator loadingToast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Saving changes..." }).First;
        await Assertions.Expect(loadingToast).ToBeVisibleAsync();

        ILocator successToast = page.Locator("[data-sonner-toast]").Filter(new LocatorFilterOptions { HasText = "Changes saved" }).First;
        await Assertions.Expect(successToast).ToBeVisibleAsync();
        await Assertions.Expect(successToast).ToContainTextAsync("This mirrors Sonner's loading to result flow.");
        await Assertions.Expect(loadingToast).Not.ToBeVisibleAsync();
    }
}
