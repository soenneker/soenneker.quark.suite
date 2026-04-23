using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCommandPlaywrightTests : PlaywrightUnitTest
{
    public QuarkCommandPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Command_dialog_demo_selects_item_and_closes_overlay()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true }),
            expectedTitle: "Command - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open command palette", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Workspace actions", Exact = true });
        await Assertions.Expect(dialog).ToBeVisibleAsync();

        var input = dialog.GetByRole(AriaRole.Combobox);
        await input.FillAsync("bill");

        await dialog.GetByText("Billing", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Dialog selection:", new PageGetByTextOptions { Exact = false })).ToContainTextAsync("Billing");
    }

[Test]
    public async ValueTask Command_demo_filters_items_and_preserves_disabled_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}commands",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Command - Quark Suite");

        var command = page.Locator("[data-slot='command']").First;
        var input = command.GetByRole(AriaRole.Combobox);
        await input.FillAsync("zzzz");

        await Assertions.Expect(command.GetByText("No results found.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await input.FillAsync(string.Empty);

        var calculator = command.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Calculator", Exact = false });
        await Assertions.Expect(calculator).ToHaveAttributeAsync("data-disabled", "true");
        await Assertions.Expect(calculator).ToHaveAttributeAsync("aria-disabled", "true");
    }
}
