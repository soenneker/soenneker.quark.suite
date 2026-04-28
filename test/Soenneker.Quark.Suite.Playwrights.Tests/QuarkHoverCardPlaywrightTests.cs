using AwesomeAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkHoverCardPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkHoverCardPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Hover_card_demo_hides_profile_details_after_pointer_leaves_trigger_and_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hovercards",
            static p => p.Locator("[data-slot='hover-card-trigger']").First,
            expectedTitle: "Hover Cards - Quark Suite");

        var trigger = page.Locator("[data-slot='hover-card-trigger']").First;
        var details = page.Locator("[data-slot='hover-card-content']").Filter(new LocatorFilterOptions { HasText = "The React Framework" });

        await trigger.HoverAsync();
        await Assertions.Expect(details).ToBeVisibleAsync();

        var sideSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Change side and alignment to match where the preview should appear." }).First;
        var topStartTrigger = sideSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Top start", Exact = true });
        await topStartTrigger.HoverAsync();

        await Assertions.Expect(details).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Hover_card_demo_shows_profile_details_on_hover()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hovercards",
            static p => p.Locator("[data-slot='hover-card-trigger']").First,
            expectedTitle: "Hover Cards - Quark Suite");

        await page.Locator("[data-slot='hover-card-trigger']").First.HoverAsync();

        await Assertions.Expect(page.Locator("[data-slot='hover-card-content']").Filter(new LocatorFilterOptions { HasText = "The React Framework" })).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Joined December 2021", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Hover_card_demo_supports_nested_hover_card_inside_modal_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hovercards",
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

    [Test]
    public async ValueTask Hover_card_demo_portals_above_page_and_has_no_console_errors()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hovercards",
            static p => p.Locator("[data-slot='hover-card-trigger']").First,
            expectedTitle: "Hover Cards - Quark Suite");

        await page.Locator("[data-slot='hover-card-trigger']").First.HoverAsync();

        var content = page.Locator("[data-slot='hover-card-content'][data-state='open']:visible").First;
        await Assertions.Expect(content).ToBeVisibleAsync();

        await page.WaitForFunctionAsync(
            "() => {" +
            "const content = document.querySelector('[data-slot=\"hover-card-content\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "if (!content || !main || main.contains(content)) return false;" +
            "return Number.parseInt(getComputedStyle(content).zIndex, 10) >= 50;" +
            "}");

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
