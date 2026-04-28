using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkHeaderPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkHeaderPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Header_sidebar_shell_demo_preserves_sidebar_and_inset_layout()
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
            $"{BaseUrl}headers",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "The header can sit above a sidebar provider and expose the standard sidebar trigger without each layout restyling it." }).First,
            expectedTitle: "Header - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The header can sit above a sidebar provider and expose the standard sidebar trigger without each layout restyling it." }).First;
        var header = section.Locator("[data-slot='header']").First;
        var sidebar = section.Locator("[data-slot='sidebar']").First;
        var inset = section.Locator("[data-slot='sidebar-inset']").First;

        await Assertions.Expect(header).ToBeVisibleAsync();
        await Assertions.Expect(sidebar).ToBeVisibleAsync();
        await Assertions.Expect(inset).ToBeVisibleAsync();
        await Assertions.Expect(sidebar).ToContainTextAsync("Dashboard");
        await Assertions.Expect(inset).ToContainTextAsync("Use the trigger in the header to toggle the sidebar.");

        var headerBox = await header.BoundingBoxAsync();
        var sidebarBox = await sidebar.BoundingBoxAsync();
        var insetBox = await inset.BoundingBoxAsync();

        (headerBox).Should().NotBeNull();
        (sidebarBox).Should().NotBeNull();
        (insetBox).Should().NotBeNull();

        (headerBox.Height >= 48).Should().BeTrue();
        (sidebarBox.Width >= 200).Should().BeTrue();
        (insetBox.Width >= 200).Should().BeTrue();
        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask ThemeToggleButton_matches_shadcn_mode_switcher_shell_and_toggles_theme()
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
            $"{BaseUrl}headers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle theme", Exact = true }).First,
            expectedTitle: "Header - Quark Suite");

        var toggle = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle theme", Exact = true }).First;
        var html = page.Locator("html");

        await Assertions.Expect(toggle).ToHaveAttributeAsync("type", "button");
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-slot", "button");
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-variant", "ghost");
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-size", "icon");
        await Assertions.Expect(toggle).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bgroup/toggle\b"));
        await Assertions.Expect(toggle).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bextend-touch-target\b"));
        await Assertions.Expect(toggle).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsize-8\b"));
        await Assertions.Expect(toggle.Locator("svg")).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsize-4\.5\b"));
        await Assertions.Expect(toggle.Locator(".sr-only")).ToHaveTextAsync("Toggle theme");

        var wasDark = await html.EvaluateAsync<bool>("element => element.classList.contains('dark')");

        await toggle.ClickAsync();

        await Assertions.Expect(html).ToHaveClassAsync(wasDark
            ? new System.Text.RegularExpressions.Regex(@"^(?!.*(?:^|\s)dark(?:\s|$)).*$")
            : new System.Text.RegularExpressions.Regex(@"(^|\s)dark(\s|$)"));

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
