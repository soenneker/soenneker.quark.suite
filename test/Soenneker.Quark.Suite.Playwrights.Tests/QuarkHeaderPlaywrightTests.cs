using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkHeaderPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkHeaderPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Docs_layout_metadata_tracks_navigation_and_history()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/headers?audit=1#overview",
            static p => p.Locator("[data-docs-content]"));
        await AssertMetadata("Blazor Header: Branding & Sidebar Layouts | Quark Suite", "headers");

        await page.GetByRole(AriaRole.Link, new() { Name = "Alert", Exact = true }).First.ClickAsync();
        await AssertMetadata("Blazor Alert Messages & Status Callouts | Quark Suite", "alert");
        await Assertions.Expect(page.Locator("[data-docs-content] h1").First).ToHaveTextAsync("Alert");

        await page.GoBackAsync();
        await AssertMetadata("Blazor Header: Branding & Sidebar Layouts | Quark Suite", "headers");

        async ValueTask AssertMetadata(string title, string route)
        {
            await Assertions.Expect(page).ToHaveTitleAsync(title);
            await Assertions.Expect(page.Locator("meta[property='og:title']")).ToHaveAttributeAsync("content", title);
            await Assertions.Expect(page.Locator("link[rel='canonical']"))
                .ToHaveAttributeAsync("href", $"https://quark.soenneker.com/components/{route}");
        }
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
            $"{BaseUrl}components/headers",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "The header can sit above a sidebar provider and expose the standard sidebar trigger without each layout restyling it." }).First);

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
    [Arguments(0)]
    [Arguments(100)]
    public async ValueTask ThemeToggleButton_matches_shadcn_mode_switcher_shell_and_toggles_theme(int callbackDelay)
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

        // Model an interop callback queued while the tooltip is being dismissed.
        await page.AddInitScriptAsync($$"""
            window.disposedCallbackErrors = [];
            window.pendingCallbackCount = 0;
            window.invokeTracked = async (ref, method, ...args) => {
                window.pendingCallbackCount++;
                try {
                    if ({{callbackDelay}} > 0)
                        await new Promise(resolve => setTimeout(resolve, {{callbackDelay}}));
                    return await ref.invokeMethodAsync(method, ...args);
                } catch (error) {
                    window.disposedCallbackErrors.push(`${method}: ${error}`);
                    throw error;
                } finally {
                    window.pendingCallbackCount--;
                }
            };
            """);
        await page.RouteAsync("**/_content/Soenneker.Bradix.Suite/js/bradix/*.js", async route =>
        {
            var response = await route.FetchAsync();
            var source = await response.TextAsync();
            source = System.Text.RegularExpressions.Regex.Replace(source,
                @"([\w.]+)\??\.invokeMethodAsync(?:\?\.)?\(", "globalThis.invokeTracked($1, ");
            await route.FulfillAsync(new() { Response = response, Body = source });
        });

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/headers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle theme", Exact = true }).First);

        var toggle = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle theme", Exact = true }).First;
        var html = page.Locator("html");
        var tooltip = page.GetByRole(AriaRole.Tooltip);

        await Assertions.Expect(toggle).ToHaveAttributeAsync("type", "button");
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-slot", "button");
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-variant", "ghost");
        await Assertions.Expect(toggle).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bgroup/toggle\b"));
        await Assertions.Expect(toggle).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bextend-touch-target\b"));
        await Assertions.Expect(toggle).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsize-8\b"));
        await Assertions.Expect(toggle.Locator("svg").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsize-4\.5\b"));
        await Assertions.Expect(toggle.Locator(".sr-only")).ToHaveTextAsync("Toggle theme");

        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-theme", "system");
        await toggle.HoverAsync();
        await Assertions.Expect(tooltip).ToContainTextAsync("Theme: system. Switch to light mode");

        foreach (var mode in new[] { "light", "dark", "system", "light", "dark", "system" })
        {
            await page.Mouse.MoveAsync(0, 0);
            await toggle.HoverAsync();
            await Assertions.Expect(tooltip).ToBeVisibleAsync();
            await toggle.ClickAsync(new() { Delay = 250 });
            await Assertions.Expect(toggle).ToHaveAttributeAsync("data-theme", mode);
            var storedTheme = await page.EvaluateAsync<string?>("() => localStorage.getItem('quark-theme')");
            storedTheme.Should().Be(mode == "system" ? null : mode);
        }

        await page.EmulateMediaAsync(new PageEmulateMediaOptions { ColorScheme = ColorScheme.Dark });
        await Assertions.Expect(html).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)dark(\s|$)"));
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-theme", "system");

        await page.GetByRole(AriaRole.Link, new() { Name = "Home", Exact = true }).First.ClickAsync();
        await Assertions.Expect(page.Locator("[data-docs-content]")).ToHaveCountAsync(0);
        await toggle.HoverAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Tooltip)).ToBeVisibleAsync();
        await toggle.ClickAsync();
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-theme", "light");
        await page.Mouse.MoveAsync(0, 0);
        await toggle.BlurAsync();
        await toggle.FocusAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Tooltip)).ToBeVisibleAsync();

        await toggle.PressAsync("Enter");
        await Assertions.Expect(toggle).ToHaveAttributeAsync("data-theme", "dark");
        await page.WaitForFunctionAsync("() => window.pendingCallbackCount === 0");
        var disposedCallbacks = await page.EvaluateAsync<string[]>("() => window.disposedCallbackErrors");
        disposedCallbacks.Should().BeEmpty();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Quark_suite_follows_system_theme_when_no_preference_exists()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.EmulateMediaAsync(new PageEmulateMediaOptions { ColorScheme = ColorScheme.Dark });
        await page.AddInitScriptAsync("localStorage.removeItem('quark-theme');");

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/headers",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle theme", Exact = true }).First);

        var isDark = await page.Locator("html").EvaluateAsync<bool>("element => element.classList.contains('dark')");
        var storedTheme = await page.EvaluateAsync<string?>("() => localStorage.getItem('quark-theme')");

        isDark.Should().BeTrue();
        storedTheme.Should().BeNull();
    }
}
