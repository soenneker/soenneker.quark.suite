using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

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

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        await gettingStarted.ClickAsync();

        var viewport = section.Locator("[data-slot='navigation-menu-viewport']").First;
        await Assertions.Expect(viewport).ToBeVisibleAsync();

        var quarkSuite = viewport.Locator("[data-slot='navigation-menu-link']").Filter(new LocatorFilterOptions { HasText = "Quark Suite" }).First;
        var installation = viewport.Locator("[data-slot='navigation-menu-link']").Filter(new LocatorFilterOptions { HasText = "Installation" }).First;
        var typography = viewport.Locator("[data-slot='navigation-menu-link']").Filter(new LocatorFilterOptions { HasText = "Typography" }).First;

        await installation.FocusAsync();
        await installation.PressAsync("Home");

        await Assertions.Expect(quarkSuite).ToBeFocusedAsync();

        await quarkSuite.PressAsync("End");

        await Assertions.Expect(typography).ToBeFocusedAsync();

        await typography.PressAsync("Home");

        await Assertions.Expect(quarkSuite).ToBeFocusedAsync();
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

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        var content = page.Locator("[data-slot='navigation-menu-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Quark Suite" });

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

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped content, a direct docs link, shared indicator, and viewport." }).First;
        var root = section.Locator("[data-slot='navigation-menu']").First;
        var trigger = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });

        await Assertions.Expect(root).ToHaveAttributeAsync("data-viewport", "true");

        await trigger.ClickAsync();

        var viewport = section.Locator("[data-slot='navigation-menu-viewport']").First;
        await Assertions.Expect(viewport).ToBeVisibleAsync();
        await Assertions.Expect(viewport).ToContainTextAsync("Quark Suite");
        await Assertions.Expect(viewport).ToContainTextAsync("Installation");

        var viewportBox = await viewport.BoundingBoxAsync();
        Assert.NotNull(viewportBox);
        Assert.True(viewportBox.Width >= 320, $"Expected the shared navigation viewport to expand beyond a sliver, but measured width {viewportBox.Width}.");
        Assert.True(viewportBox.Height >= 120, $"Expected the shared navigation viewport to show full content, but measured height {viewportBox.Height}.");
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

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        var guides = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Guides", Exact = true });
        var docs = section.GetByRole(AriaRole.Link, new LocatorGetByRoleOptions { Name = "Docs", Exact = true });

        await guides.FocusAsync();
        await guides.PressAsync("Home");

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

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Navigation menu with grouped content, a direct docs link, shared indicator, and viewport." }).First;
        var gettingStarted = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Getting started", Exact = true });
        var guides = section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Guides", Exact = true });
        var docs = section.GetByRole(AriaRole.Link, new LocatorGetByRoleOptions { Name = "Docs", Exact = true });

        await docs.HoverAsync();

        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(guides).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(section.Locator("[data-slot='navigation-menu-content'][data-state='open']")).ToHaveCountAsync(0);
        Assert.Null(await docs.GetAttributeAsync("aria-expanded"));

        await gettingStarted.ClickAsync();

        var activeLink = section.Locator("[data-slot='navigation-menu-link'][aria-current='page']").Filter(new LocatorFilterOptions { HasText = "Quark Suite" }).First;
        await Assertions.Expect(activeLink).ToHaveAttributeAsync("data-active", string.Empty);
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
        var guides = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Guides", Exact = true });

        await gettingStarted.ClickAsync();
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Introduction");

        await guides.HoverAsync();

        await Assertions.Expect(guides).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(gettingStarted).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Accordion");
        await Assertions.Expect(page.Locator("main")).ToContainTextAsync("Dialogs");
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
        await Assertions.Expect(activeLink).ToHaveAttributeAsync("data-active", string.Empty);
    }
}
