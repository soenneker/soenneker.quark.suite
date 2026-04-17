using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkTooltipPlaywrightTests : PlaywrightUnitTest
{
    public QuarkTooltipPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Tooltip_demo_hides_basic_content_after_pointer_leaves_trigger_and_content()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Hover", Exact = true }),
            expectedTitle: "Tooltips - Quark Suite");

        ILocator basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Tooltips work well for terse helper text on icon buttons and compact controls." }).First;
        ILocator basicTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hover", Exact = true });
        ILocator basicTooltip = page.Locator("[data-slot='tooltip-content']").Filter(new LocatorFilterOptions { HasText = "Add to library" });

        await basicTrigger.HoverAsync();
        await Assertions.Expect(basicTooltip).ToBeVisibleAsync();

        ILocator sideSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Choose the side that best fits the surrounding layout." }).First;
        ILocator topTrigger = sideSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Top", Exact = true });
        await topTrigger.HoverAsync();

        await Assertions.Expect(basicTooltip).Not.ToBeVisibleAsync();
    }

[Fact]
    public async ValueTask Tooltip_demo_reveals_basic_content_on_hover()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Hover", Exact = true }),
            expectedTitle: "Tooltips - Quark Suite");

        ILocator basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Tooltips work well for terse helper text on icon buttons and compact controls." }).First;
        ILocator basicTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hover", Exact = true });
        await basicTrigger.HoverAsync();

        ILocator basicTooltip = page.Locator("[data-slot='tooltip-content']").Filter(new LocatorFilterOptions { HasText = "Add to library" });
        await Assertions.Expect(basicTooltip).ToBeVisibleAsync();
        await Assertions.Expect(basicTooltip).ToHaveAttributeAsync("data-state", new System.Text.RegularExpressions.Regex("^(instant-open|delayed-open)$"));
        await Assertions.Expect(basicTrigger).ToHaveAttributeAsync("aria-describedby", new System.Text.RegularExpressions.Regex(".+"));
    }

[Fact]
    public async ValueTask Tooltip_demo_supports_nested_tooltip_inside_modal_dialog()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open tooltip dialog", Exact = true }),
            expectedTitle: "Tooltips - Quark Suite");

        ILocator dialogTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open tooltip dialog", Exact = true });
        await dialogTrigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Tooltip dialog", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        ILocator tooltipTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Show nested tooltip", Exact = true });
        await tooltipTrigger.HoverAsync();

        ILocator tooltip = page.Locator("[data-slot='tooltip-content']").Filter(new LocatorFilterOptions { HasText = "Nested tooltip" });
        await Assertions.Expect(tooltip).ToBeVisibleAsync();
        await Assertions.Expect(tooltip).ToHaveAttributeAsync("data-state", new System.Text.RegularExpressions.Regex("^(instant-open|delayed-open)$"));
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }
}
