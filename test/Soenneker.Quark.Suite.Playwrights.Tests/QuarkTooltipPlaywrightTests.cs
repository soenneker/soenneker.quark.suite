using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTooltipPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkTooltipPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Tooltip_demo_hides_basic_content_after_pointer_leaves_trigger_and_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Hover", Exact = true }), expectedTitle: "Tooltips - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Tooltips work well for terse helper text on icon buttons and compact controls." }).First;
        var basicTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hover", Exact = true });
        var basicTooltip = page.Locator("[data-slot='tooltip-content']")
                               .Filter(new LocatorFilterOptions { HasText = "Add to library" });

        await basicTrigger.HoverAsync();
        await Assertions.Expect(basicTooltip).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Additional examples", Exact = true }).HoverAsync();

        await Assertions.Expect(basicTooltip).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Tooltip_demo_reveals_basic_content_on_hover()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Hover", Exact = true }), expectedTitle: "Tooltips - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Tooltips work well for terse helper text on icon buttons and compact controls." }).First;
        var basicTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hover", Exact = true });
        await basicTrigger.HoverAsync();

        var basicTooltip = page.Locator("[data-slot='tooltip-content']")
                               .Filter(new LocatorFilterOptions { HasText = "Add to library" });
        await Assertions.Expect(basicTooltip).ToBeVisibleAsync();
        await Assertions.Expect(basicTrigger).ToHaveAttributeAsync("aria-describedby", new System.Text.RegularExpressions.Regex(".+"));
    }

    [Test]
    public async ValueTask Tooltip_demo_transfers_open_state_between_sibling_triggers()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tooltips", static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Top", Exact = true }),
            expectedTitle: "Tooltips - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Tooltips work well for terse helper text on icon buttons and compact controls." }).First;
        var basicTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hover", Exact = true });
        var sideSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Choose the side that best fits the surrounding layout." }).First;
        var topTrigger = sideSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Top", Exact = true });
        var rightTrigger = sideSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Right", Exact = true });
        var openTopTooltip = page.Locator("[data-slot='tooltip-content']")
                                 .Filter(new LocatorFilterOptions { HasText = "Default placement" });
        var openRightTooltip =
            page.Locator("[data-slot='tooltip-content']")
                .Filter(new LocatorFilterOptions { HasText = "Helpful context on the right" });

        await basicTrigger.HoverAsync();
        var basicTooltip = page.Locator("[data-slot='tooltip-content']")
                               .Filter(new LocatorFilterOptions { HasText = "Add to library" });
        await Assertions.Expect(basicTooltip).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Additional examples", Exact = true }).HoverAsync();
        await Assertions.Expect(basicTooltip).ToHaveCountAsync(0);
        await page.WaitForTimeoutAsync(350);

        await topTrigger.HoverAsync();
        await Assertions.Expect(openTopTooltip).ToBeVisibleAsync();

        await rightTrigger.HoverAsync();

        await Assertions.Expect(openRightTooltip).ToBeVisibleAsync();
        await Assertions.Expect(openTopTooltip).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tooltip_demo_supports_nested_tooltip_inside_modal_dialog()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open tooltip dialog", Exact = true }),
            expectedTitle: "Tooltips - Quark Suite");

        var dialogTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open tooltip dialog", Exact = true });
        await dialogTrigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Tooltip dialog", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        var tooltipTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Show nested tooltip", Exact = true });
        await tooltipTrigger.HoverAsync();

        var tooltip = page.Locator("[data-slot='tooltip-content']")
                          .Filter(new LocatorFilterOptions { HasText = "Nested tooltip" });
        await Assertions.Expect(tooltip).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tooltip_demo_portals_above_page_and_has_no_console_errors()
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

        await page.GotoAndWaitForReady($"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Hover", Exact = true }),
            expectedTitle: "Tooltips - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Tooltips work well for terse helper text on icon buttons and compact controls." }).First;
        var basicTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hover", Exact = true });

        await basicTrigger.HoverAsync();

        var tooltip = page.Locator("[data-slot='tooltip-content']").Filter(new LocatorFilterOptions { HasText = "Add to library" }).First;
        await Assertions.Expect(tooltip).ToBeVisibleAsync();

        await page.WaitForFunctionAsync(
            "() => {" +
            "const tooltip = document.querySelector('[data-slot=\"tooltip-content\"]');" +
            "const main = document.querySelector('main');" +
            "if (!tooltip || !main || main.contains(tooltip)) return false;" +
            "return Number.parseInt(getComputedStyle(tooltip).zIndex, 10) >= 50;" +
            "}");

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
