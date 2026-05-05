using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAvatarPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkAvatarPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Avatar_demo_exposes_images_fallbacks_group_count_and_dropdown_menu()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}avatars",
            static p => p.GetByAltText("@shadcn", new PageGetByAltTextOptions { Exact = true }).First,
            expectedTitle: "Avatars - Quark Suite");

        await Assertions.Expect(page.GetByAltText("@shadcn", new PageGetByAltTextOptions { Exact = true }).First).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("CN", new PageGetByTextOptions { Exact = true }).First).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("+3", new PageGetByTextOptions { Exact = true }).First).ToBeVisibleAsync();

        var dropdownTrigger = page.GetByRole(AriaRole.Button).Filter(new LocatorFilterOptions { Has = page.GetByAltText("shadcn", new PageGetByAltTextOptions { Exact = true }) }).First;
        await dropdownTrigger.ClickAsync();

        var menu = page.GetByRole(AriaRole.Menu);
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Profile", Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Log out", Exact = true })).ToBeVisibleAsync();
    }
}
