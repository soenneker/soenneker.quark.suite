using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkNavigationLinksPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkNavigationLinksPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask NavigationLinks_demo_shell_exposes_active_page_and_focusable_links()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, msg) =>
        {
            if (msg.Type == "error")
                consoleErrors.Add(msg.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}buttons",
            static p => p.Locator("a[data-sidebar='menu-button'][aria-current='page']").First,
            expectedTitle: "Buttons - Quark Suite");

        var headerLinks = page.Locator("header a[data-slot='button'][data-size='sm']");
        await Assertions.Expect(headerLinks.First).ToBeVisibleAsync();
        await Assertions.Expect(headerLinks.First).ToHaveAttributeAsync("data-variant", "ghost");
        await Assertions.Expect(headerLinks.First).ToHaveAttributeAsync("data-size", "sm");

        var activeSidebarLink = page.Locator("a[data-sidebar='menu-button'][aria-current='page']").Filter(new LocatorFilterOptions { HasText = "Button" }).First;
        await Assertions.Expect(activeSidebarLink).ToBeVisibleAsync();
        await Assertions.Expect(activeSidebarLink).ToHaveAttributeAsync("data-active", "true");

        await activeSidebarLink.FocusAsync();
        await Assertions.Expect(activeSidebarLink).ToBeFocusedAsync();

        var sidebarProbe = await activeSidebarLink.EvaluateAsync<NavigationLinkProbe>(
            @"element => {
                const style = getComputedStyle(element);
                return {
                    display: style.display,
                    fontSize: style.fontSize,
                    height: style.height,
                    position: style.position
                };
            }");

        sidebarProbe.display.Should().Be("flex");
        sidebarProbe.fontSize.Should().Be("12.8px");
        sidebarProbe.position.Should().Be("relative");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Docs_layout_toggle_expands_shell_without_hiding_or_coloring_sidebar()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(2048, 960);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}buttons",
            static p => p.Locator("aside[data-sidebar='sidebar'][data-shell='navigation']").First,
            expectedTitle: "Buttons - Quark Suite");

        var sidebar = page.Locator("aside[data-sidebar='sidebar'][data-shell='navigation']").First;
        var brand = page.Locator("header a[aria-label='Quark Suite']").First;
        var toggle = page.Locator("header button[title='Toggle sidebar']").First;

        var initialSidebar = await sidebar.EvaluateAsync<LayoutRectProbe>("element => ({ left: element.getBoundingClientRect().left })");
        var initialBrand = await brand.EvaluateAsync<LayoutRectProbe>("element => ({ left: element.getBoundingClientRect().left })");

        await toggle.ClickAsync();
        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(sidebar).ToBeVisibleAsync();

        var expandedSidebar = await sidebar.EvaluateAsync<LayoutRectProbe>("element => ({ left: element.getBoundingClientRect().left })");
        var expandedBrand = await brand.EvaluateAsync<LayoutRectProbe>("element => ({ left: element.getBoundingClientRect().left })");
        var backgroundColor = await sidebar.EvaluateAsync<string>("element => getComputedStyle(element).backgroundColor");

        expandedSidebar.left.Should().BeLessThan(initialSidebar.left - 100);
        expandedBrand.left.Should().BeLessThan(initialBrand.left - 100);
        backgroundColor.Should().Be("rgba(0, 0, 0, 0)");
    }

    [Test]
    public async ValueTask Components_index_title_aligns_with_component_grid_and_uses_docs_heading_weight()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(2048, 960);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components",
            static p => p.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Components" }).First,
            expectedTitle: "Components - Quark Suite");

        var heading = page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Components" }).First;
        var firstComponentLink = page.Locator("main a[href='accordions']").First;

        var headingRect = await heading.EvaluateAsync<LayoutRectProbe>("element => ({ left: element.getBoundingClientRect().left })");
        var linkRect = await firstComponentLink.EvaluateAsync<LayoutRectProbe>("element => ({ left: element.getBoundingClientRect().left })");
        var headingWeight = await heading.EvaluateAsync<string>("element => getComputedStyle(element).fontWeight");
        var headingSlot = await heading.GetAttributeAsync("data-slot");
        var bodyFontFamily = await page.Locator("body").EvaluateAsync<string>("element => getComputedStyle(element).fontFamily");

        headingRect.left.Should().BeApproximately(linkRect.left, 1);
        headingSlot.Should().Be("h1");
        headingWeight.Should().Be("600");
        bodyFontFamily.Should().Contain("Geist");
    }

    [Test]
    public async ValueTask Landing_mobile_header_menu_trigger_opens_header_navigation_sidebar()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(390, 844);

        await page.GotoAndWaitForReady(
            BaseUrl,
            static p => p.Locator("header button[data-slot='sidebar-trigger']").First,
            expectedTitle: "Quark Suite - Blazor Components");

        var trigger = page.Locator("header button[data-slot='sidebar-trigger']").First;
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");

        await trigger.ClickAsync();
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        var mobileSidebar = page.Locator("[data-sidebar='sidebar'][data-mobile='true'][data-state='open']").First;
        await Assertions.Expect(mobileSidebar).ToBeVisibleAsync();
        await Assertions.Expect(mobileSidebar.GetByText("Docs", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(mobileSidebar.GetByText("Components", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Landing_desktop_uses_header_navigation_without_docs_sidebar()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(2048, 960);

        await page.GotoAndWaitForReady(
            BaseUrl,
            static p => p.Locator("header a[data-slot='button']").First,
            expectedTitle: "Quark Suite - Blazor Components");

        await Assertions.Expect(page.Locator("aside[data-sidebar='sidebar'][data-shell='navigation']")).ToHaveCountAsync(0);

        var componentsLink = page.Locator("header a[href='components']").First;
        await Assertions.Expect(componentsLink).ToBeVisibleAsync();
        await Assertions.Expect(componentsLink).ToHaveAttributeAsync("data-slot", "button");
    }

    private sealed class NavigationLinkProbe
    {
        public string? display { get; set; }
        public string? fontSize { get; set; }
        public string? height { get; set; }
        public string? position { get; set; }
    }

    private sealed class LayoutRectProbe
    {
        public double left { get; set; }
    }
}
