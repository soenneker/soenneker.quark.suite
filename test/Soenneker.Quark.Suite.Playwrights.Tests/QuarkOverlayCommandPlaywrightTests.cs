using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkOverlayCommandPlaywrightTests : PlaywrightUnitTest
{
    public QuarkOverlayCommandPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Alert_dialog_demo_stays_open_on_outside_click_and_escape()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}alert-dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true }),
            expectedTitle: "Alert Dialogs - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true }).ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Alertdialog, new PageGetByRoleOptions { Name = "Are you absolutely sure?", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("aria-modal", "true");
        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "open");

        await ClickJustOutsideAsync(page, dialog);
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "open");

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "open");
    }

    [Fact]
    public async ValueTask Alert_dialog_demo_cancel_closes()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}alert-dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true }),
            expectedTitle: "Alert Dialogs - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show Dialog", Exact = true });
        await trigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Alertdialog, new PageGetByRoleOptions { Name = "Are you absolutely sure?", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Cancel", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "closed");
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

    [Fact]
    public async ValueTask Alert_dialog_destructive_demo_closes_from_action()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}alert-dialogs",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Delete project", Exact = true }),
            expectedTitle: "Alert Dialogs - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Delete project", Exact = true }).ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Alertdialog, new PageGetByRoleOptions { Name = "Delete this project?", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Delete", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Sheet_demo_uses_portal_and_closes_on_outside_click()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sheets",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Sheet - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First;
        await trigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Edit profile", Exact = true }).First;
        ILocator content = page.Locator("[data-slot='sheet-content'][data-state='open']").First;

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-side", "right");

        await page.WaitForFunctionAsync(
            "() => {" +
            "const content = document.querySelector('[data-slot=\"sheet-content\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!content && document.body.contains(content) && !!main && !main.contains(content);" +
            "}");

        await ClickJustOutsideAsync(page, dialog);

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

    [Fact]
    public async ValueTask Sheet_scrollable_demo_respects_bound_visibility_and_close_button_dismiss()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sheets",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open activity drawer", Exact = true }),
            expectedTitle: "Sheet - Quark Suite");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open activity drawer", Exact = true }).ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Recent activity", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(dialog.GetByText("Review a longer stream of events without leaving the current page.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Close", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).ToHaveAttributeAsync("data-state", "closed");
    }

    [Fact]
    public async ValueTask Command_demo_filters_items_and_preserves_disabled_state()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Command - Quark Suite");

        ILocator command = page.Locator("[data-slot='command']").First;
        ILocator input = command.GetByRole(AriaRole.Combobox);
        await input.FillAsync("zzzz");

        await Assertions.Expect(command.GetByText("No results found.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await input.FillAsync(string.Empty);

        ILocator calculator = command.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Calculator", Exact = false });
        await Assertions.Expect(calculator).ToHaveAttributeAsync("data-disabled", "true");
        await Assertions.Expect(calculator).ToHaveAttributeAsync("aria-disabled", "true");
    }

    [Fact]
    public async ValueTask Command_dialog_demo_selects_item_and_closes_overlay()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true }),
            expectedTitle: "Command - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true });
        await trigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Workspace actions", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        ILocator input = dialog.GetByRole(AriaRole.Combobox);
        await input.FillAsync("bill");

        await dialog.GetByText("Billing", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Dialog selection:", new PageGetByTextOptions { Exact = false })).ToContainTextAsync("Billing");
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

        ILocator tooltip = page.GetByRole(AriaRole.Tooltip, new PageGetByRoleOptions { Name = "Nested tooltip", Exact = true });
        await Assertions.Expect(tooltip).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Hover_card_demo_shows_profile_details_on_hover()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

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
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}hover-cards",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open hover card dialog", Exact = true }),
            expectedTitle: "Hover Cards - Quark Suite");

        ILocator dialogTrigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open hover card dialog", Exact = true });
        await dialogTrigger.ClickAsync();

        ILocator dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Hover card dialog", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        ILocator hoverCardTrigger = dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Show nested hover card", Exact = true });
        await hoverCardTrigger.HoverAsync();

        ILocator hoverCard = page.GetByText("Hover card content inside dialog", new PageGetByTextOptions { Exact = true });
        await Assertions.Expect(hoverCard).ToBeVisibleAsync();
        await Assertions.Expect(dialog).ToBeVisibleAsync();
    }

    private static async Task ClickJustOutsideAsync(IPage page, ILocator locator)
    {
        var box = await locator.BoundingBoxAsync();
        Assert.NotNull(box);
        float x = box.X > 40 ? box.X - 20 : box.X + box.Width + 20;
        float y = box.Y > 40 ? box.Y - 20 : box.Y + 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
