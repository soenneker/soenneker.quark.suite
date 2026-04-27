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
                    height: style.height,
                    position: style.position
                };
            }");

        sidebarProbe.display.Should().Be("flex");
        sidebarProbe.position.Should().Be("relative");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class NavigationLinkProbe
    {
        public string? display { get; set; }
        public string? height { get; set; }
        public string? position { get; set; }
    }
}
