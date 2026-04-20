using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkHoverCardPlaywrightTests : PlaywrightUnitTest
{
    public QuarkHoverCardPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Hover_card_demo_hides_profile_details_after_pointer_leaves_trigger_and_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hover-cards",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "nextjs", Exact = true }),
            expectedTitle: "Hover Cards - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "nextjs", Exact = true });
        var details = page.GetByText("The React framework for production applications.", new PageGetByTextOptions { Exact = true });

        await trigger.HoverAsync();
        await Assertions.Expect(details).ToBeVisibleAsync();

        var sideSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Change side and alignment to match where the preview should appear." }).First;
        var topStartTrigger = sideSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Top start", Exact = true });
        await topStartTrigger.HoverAsync();

        await Assertions.Expect(details).Not.ToBeVisibleAsync();
    }

[Fact]
    public async ValueTask Hover_card_demo_shows_profile_details_on_hover()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hover-cards",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "nextjs", Exact = true }),
            expectedTitle: "Hover Cards - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "nextjs", Exact = true }).HoverAsync();

        await Assertions.Expect(page.GetByText("The React framework for production applications.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Joined December 2021", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

[Fact]
    public async ValueTask Hover_card_demo_supports_nested_hover_card_inside_modal_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hover-cards",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open hover card dialog", Exact = true }),
            expectedTitle: "Hover Cards - Quark Suite");

        var dialogTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open hover card dialog", Exact = true });
        await dialogTrigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Hover card dialog", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        var hoverCardTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Show nested hover card", Exact = true });
        await hoverCardTrigger.HoverAsync();

        var hoverCard = page.GetByText("Hover card content inside dialog", new PageGetByTextOptions { Exact = true });
        await Assertions.Expect(hoverCard).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }
}
