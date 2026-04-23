using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkNavigationMenuPlaywrightTests : PlaywrightUnitTest
{
    public QuarkNavigationMenuPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Navigation_menu_demo_home_and_end_keys_move_focus_between_edge_links_inside_open_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped list content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        await gettingStarted.ClickAsync();

        var viewport = section.Locator("[data-slot='navigation-menu-viewport']").First;
        await Assertions.Expect(viewport).ToBeVisibleAsync();

        var introduction = viewport.Locator("[data-slot='navigation-menu-link']").Filter(new LocatorFilterOptions { HasText = "Introduction" }).First;
        var installation = viewport.Locator("[data-slot='navigation-menu-link']").Filter(new LocatorFilterOptions { HasText = "Installation" }).First;
        var typography = viewport.Locator("[data-slot='navigation-menu-link']").Filter(new LocatorFilterOptions { HasText = "Typography" }).First;

        await installation.FocusAsync();
        await installation.PressAsync("Home");

        await Assertions.Expect(introduction).ToBeFocusedAsync();

        await introduction.PressAsync("End");

        await Assertions.Expect(typography).ToBeFocusedAsync();

        await typography.PressAsync("Home");

        await Assertions.Expect(introduction).ToBeFocusedAsync();
    }

[Test]
    public async ValueTask Navigation_menu_demo_closes_from_single_outside_click()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped list content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        var content = page.Locator("[data-slot='navigation-menu-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Introduction" });

        await gettingStarted.ClickAsync();

        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(content).ToBeVisibleAsync();

        await page.Locator("main").ClickAsync(new LocatorClickOptions { Position = new Position { X = 10, Y = 10 } });

        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Navigation_menu_demo_uses_shared_viewport_by_default()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(1400, 1000);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped list content, a direct docs link, shared indicator, and viewport." }).First;
        var root = section.Locator("[data-slot='navigation-menu']").First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });

        await Assertions.Expect(root).ToHaveAttributeAsync("data-viewport", "true");

        await trigger.ClickAsync();

        var viewport = section.Locator("[data-slot='navigation-menu-viewport']").First;
        await Assertions.Expect(viewport).ToBeVisibleAsync();
        await Assertions.Expect(viewport).ToContainTextAsync("Introduction");
        await Assertions.Expect(viewport).ToContainTextAsync("Installation");

        var viewportBox = await viewport.BoundingBoxAsync();
        (viewportBox).Should().NotBeNull();
        (viewportBox.Width >= 320).Should().BeTrue();
        (viewportBox.Height >= 120).Should().BeTrue();
    }

[Test]
    public async ValueTask Navigation_menu_demo_home_and_end_keys_move_focus_to_first_and_last_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped list content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        var components = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Components", Exact = true });
        var docs = section.GetByRole(AriaRole.Link, new LocatorGetByRoleOptions { Name = "Docs", Exact = true });

        await components.FocusAsync();
        await components.PressAsync("Home");

        await Assertions.Expect(gettingStarted).ToBeFocusedAsync();

        await gettingStarted.PressAsync("End");

        await Assertions.Expect(docs).ToBeFocusedAsync();

        await docs.PressAsync("Home");

        await Assertions.Expect(gettingStarted).ToBeFocusedAsync();
    }

[Test]
    public async ValueTask Navigation_menu_demo_marks_active_link_and_direct_docs_link_does_not_open_viewport()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped list content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        var components = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Components", Exact = true });
        var docs = section.GetByRole(AriaRole.Link, new LocatorGetByRoleOptions { Name = "Docs", Exact = true });

        await docs.HoverAsync();

        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(components).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(section.Locator("[data-slot='navigation-menu-content'][data-state='open']")).ToHaveCountAsync(0);
        (await docs.GetAttributeAsync("aria-expanded")).Should().BeNull();

        await gettingStarted.ClickAsync();

        var activeLink = section.Locator("[data-slot='navigation-menu-link'][aria-current='page']").Filter(new LocatorFilterOptions { HasText = "Introduction" }).First;
        await Assertions.Expect(activeLink).ToHaveAttributeAsync("aria-current", "page");
        await Assertions.Expect(activeLink).ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Navigation_menu_demo_switches_visible_content_between_triggers()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        var gettingStarted = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Getting started", Exact = true });
        var components = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Components", Exact = true });

        await gettingStarted.ClickAsync();
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Introduction");

        await components.HoverAsync();

        await Assertions.Expect(components).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Alert Dialog");
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Hover Card");
    }

[Test]
    public async ValueTask Navigation_menu_rtl_demo_inherits_rtl_direction_and_renders_viewport_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}navigation-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "البدء", Exact = true }),
            expectedTitle: "Navigation Menu - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu content and viewport positioning respect right-to-left layouts." }).First;
        var root = section.Locator("[data-slot='navigation-menu']").First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "البدء", Exact = true });

        await Assertions.Expect(root).ToHaveAttributeAsync("dir", "rtl");
        await Assertions.Expect(root).ToHaveAttributeAsync("data-viewport", "true");

        await trigger.ClickAsync();

        var viewport = section.Locator("[data-slot='navigation-menu-viewport']").First;
        var activeLink = viewport.Locator("[data-slot='navigation-menu-link'][aria-current='page']").Filter(new LocatorFilterOptions
        {
            HasText = "المكونات"
        }).First;
        await Assertions.Expect(viewport).ToBeVisibleAsync();
        await Assertions.Expect(viewport).ToContainTextAsync("المكونات");
        await Assertions.Expect(viewport).ToContainTextAsync("الأزرار");
        await Assertions.Expect(activeLink).ToHaveAttributeAsync("aria-current", "page");
    }
}
