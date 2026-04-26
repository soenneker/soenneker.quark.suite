using System.Threading.Tasks;
using System.Collections.Generic;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkBreadcrumbPlaywrightTests : PlaywrightUnitTest
{
    public QuarkBreadcrumbPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Breadcrumb_page_and_rtl_examples_expose_current_page_and_navigation_semantics()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady($"{BaseUrl}breadcrumbs",
            static p => p.GetByRole(AriaRole.Navigation, new PageGetByRoleOptions { Name = "breadcrumb", Exact = true }).First,
            expectedTitle: "Breadcrumbs - Quark Suite");

        var linkSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "BreadcrumbLink can pass its styling into a child Quark link with AsChild." }).First;
        var navigation = linkSection.Locator("[data-slot='breadcrumb']").First;
        var homeLink = navigation.Locator("[data-slot='breadcrumb-link']").Nth(0);
        var componentsLink = navigation.Locator("[data-slot='breadcrumb-link']").Nth(1);
        var currentPage = navigation.Locator("[data-slot='breadcrumb-page']").First;

        await Assertions.Expect(homeLink).ToHaveAttributeAsync("href", "#");
        await Assertions.Expect(componentsLink).ToHaveAttributeAsync("href", "#");
        await Assertions.Expect(currentPage).ToHaveAttributeAsync("aria-current", "page");
        await Assertions.Expect(currentPage).ToHaveAttributeAsync("aria-disabled", "true");
        (await navigation.Locator("[data-slot='breadcrumb-list']").First.GetAttributeAsync("class")).Should().Contain("gap-1.5");

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions
            { HasText = "Breadcrumb separators and item flow render correctly in right-to-left layouts." }).First;
        var rtlNavigation = rtlSection.GetByRole(AriaRole.Navigation, new LocatorGetByRoleOptions { Name = "breadcrumb", Exact = true });

        await Assertions.Expect(rtlNavigation).ToHaveAttributeAsync("dir", "rtl");
        await Assertions.Expect(rtlNavigation.GetByText("الرئيسية", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(rtlNavigation.Locator("[data-slot='breadcrumb-separator']")).ToHaveCountAsync(2);

        consoleErrors.Should().BeEmpty();
        sawPageError.Should().BeFalse();
    }

    [Test]
    public async ValueTask Breadcrumb_demo_overflow_menu_opens_from_collapsed_item_and_closes_after_selection()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady($"{BaseUrl}breadcrumbs",
            static p => p.GetByRole(AriaRole.Navigation, new PageGetByRoleOptions { Name = "breadcrumb", Exact = true }).First,
            expectedTitle: "Breadcrumbs - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Collapsed breadcrumb trail with an overflow menu in the middle." })
                          .First;
        var trigger = section.Locator("[data-slot='dropdown-menu-trigger']").First;

        await trigger.ClickAsync();

        var menu = page.GetByRole(AriaRole.Menu).Filter(new LocatorFilterOptions { HasText = "Documentation" });
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-haspopup", "menu");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(menu).ToBeVisibleAsync();
        await Assertions.Expect(menu).ToContainTextAsync("Themes");

        await menu.GetByRole(AriaRole.Menuitem, new LocatorGetByRoleOptions { Name = "Documentation", Exact = true }).ClickAsync();

        await Assertions.Expect(page.Locator("[role='menu']:visible")).ToHaveCountAsync(0);
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");

        consoleErrors.Should().BeEmpty();
        sawPageError.Should().BeFalse();
    }
}
