using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkBreadcrumbPlaywrightTests : PlaywrightUnitTest
{
    public QuarkBreadcrumbPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Breadcrumb_page_and_rtl_examples_expose_current_page_and_navigation_semantics()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}breadcrumbs",
            static p => p.GetByRole(AriaRole.Navigation, new PageGetByRoleOptions { Name = "breadcrumb", Exact = true }).First,
            expectedTitle: "Breadcrumbs - Quark Suite");

        ILocator linkSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "BreadcrumbLink can pass its styling into a child Quark link with AsChild." }).First;
        ILocator navigation = linkSection.Locator("[data-slot='breadcrumb']").First;
        ILocator homeLink = navigation.Locator("[data-slot='breadcrumb-link']").Nth(0);
        ILocator componentsLink = navigation.Locator("[data-slot='breadcrumb-link']").Nth(1);
        ILocator currentPage = navigation.Locator("[data-slot='breadcrumb-page']").First;

        await Assertions.Expect(homeLink).ToHaveAttributeAsync("href", "/");
        await Assertions.Expect(componentsLink).ToHaveAttributeAsync("href", "/components");
        await Assertions.Expect(currentPage).ToHaveAttributeAsync("aria-current", "page");
        await Assertions.Expect(currentPage).ToHaveAttributeAsync("aria-disabled", "true");

        ILocator rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Breadcrumb separators and item flow render correctly in right-to-left layouts." }).First;
        ILocator rtlNavigation = rtlSection.GetByRole(AriaRole.Navigation, new LocatorGetByRoleOptions { Name = "breadcrumb", Exact = true });

        await Assertions.Expect(rtlNavigation).ToHaveAttributeAsync("dir", "rtl");
        await Assertions.Expect(rtlNavigation.GetByText("الرئيسية", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(rtlNavigation.Locator("[data-slot='breadcrumb-separator']")).ToHaveCountAsync(2);
    }

[Fact]
    public async ValueTask Breadcrumb_demo_overflow_menu_opens_from_collapsed_item_and_closes_after_selection()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}breadcrumbs",
            static p => p.GetByRole(AriaRole.Navigation, new PageGetByRoleOptions { Name = "breadcrumb", Exact = true }).First,
            expectedTitle: "Breadcrumbs - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Collapsed breadcrumb trail with an overflow menu in the middle." }).First;
        ILocator trigger = section.Locator("[data-slot='dropdown-menu-trigger']").First;

        await trigger.ClickAsync();

        ILocator menu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Documentation" });
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-haspopup", "menu");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(menu).ToContainTextAsync("Themes");

        await menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Documentation", Exact = true }).ClickAsync();

        await Assertions.Expect(page.Locator("[role='menu']:visible")).ToHaveCountAsync(0);
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
    }
}
