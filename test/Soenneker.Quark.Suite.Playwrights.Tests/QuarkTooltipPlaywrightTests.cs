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
        ILocator basicTooltip = page.Locator("[data-slot='tooltip-content'][data-state='instant-open'], [data-slot='tooltip-content'][data-state='delayed-open']")
            .Filter(new LocatorFilterOptions { HasText = "Add to library" });

        await basicTrigger.HoverAsync();
        await Assertions.Expect(basicTooltip).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Additional examples", Exact = true }).HoverAsync();

        await Assertions.Expect(basicTooltip).ToHaveCountAsync(0);
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

        ILocator basicTooltip = page.Locator("[data-slot='tooltip-content'][data-state='instant-open'], [data-slot='tooltip-content'][data-state='delayed-open']")
            .Filter(new LocatorFilterOptions { HasText = "Add to library" });
        await Assertions.Expect(basicTooltip).ToBeVisibleAsync();
        await Assertions.Expect(basicTrigger).ToHaveAttributeAsync("aria-describedby", new System.Text.RegularExpressions.Regex(".+"));
    }

[Fact]
    public async ValueTask Tooltip_demo_transfers_open_state_between_sibling_triggers()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tooltips",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Top", Exact = true }),
            expectedTitle: "Tooltips - Quark Suite");

        ILocator basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Tooltips work well for terse helper text on icon buttons and compact controls." }).First;
        ILocator basicTrigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hover", Exact = true });
        ILocator sideSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Choose the side that best fits the surrounding layout." }).First;
        ILocator topTrigger = sideSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Top", Exact = true });
        ILocator rightTrigger = sideSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Right", Exact = true });
        ILocator openTopTooltip = page.Locator("[data-slot='tooltip-content'][data-state='instant-open'], [data-slot='tooltip-content'][data-state='delayed-open']")
            .Filter(new LocatorFilterOptions { HasText = "Default placement" });
        ILocator openRightTooltip = page.Locator("[data-slot='tooltip-content'][data-state='instant-open'], [data-slot='tooltip-content'][data-state='delayed-open']")
            .Filter(new LocatorFilterOptions { HasText = "Helpful context on the right" });

        await basicTrigger.HoverAsync();
        await Assertions.Expect(page.Locator("[data-slot='tooltip-content'][data-state='instant-open'], [data-slot='tooltip-content'][data-state='delayed-open']")
            .Filter(new LocatorFilterOptions { HasText = "Add to library" })).ToBeVisibleAsync();

        await topTrigger.HoverAsync();
        await Assertions.Expect(openTopTooltip).ToBeVisibleAsync();

        await rightTrigger.HoverAsync();

        await Assertions.Expect(openRightTooltip).ToBeVisibleAsync();
        await Assertions.Expect(openTopTooltip).Not.ToBeVisibleAsync();
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

        ILocator tooltip = page.Locator("[data-slot='tooltip-content'][data-state='instant-open'], [data-slot='tooltip-content'][data-state='delayed-open']")
            .Filter(new LocatorFilterOptions { HasText = "Nested tooltip" });
        await Assertions.Expect(tooltip).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }
}
