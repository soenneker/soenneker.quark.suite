using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
[NotInParallel]
public sealed partial class QuarkDemoRouteSmokePlaywrightTests : PlaywrightUnitTest
{
    public QuarkDemoRouteSmokePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Demo_routes_render_without_browser_or_blazor_errors()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var runtimeErrors = new List<string>();

        await page.GotoAsync(BaseUrl, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = 15000
        });

        await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'", new PageWaitForFunctionOptions { Timeout = 15000 });
        string landingBodyText = await page.Locator("body").InnerTextAsync(new LocatorInnerTextOptions { Timeout = 5000 });
        landingBodyText.Should().NotBeNullOrWhiteSpace();

        page.Console += (_, message) =>
        {
            if (message.Type == "error" && !IsIgnoredConsoleError(message.Text))
                runtimeErrors.Add($"console: {message.Text}");
        };

        page.PageError += (_, exception) => runtimeErrors.Add($"page: {exception}");

        var failures = new List<string>();

        foreach (string route in DiscoverDemoRoutes())
        {
            runtimeErrors.Clear();

            try
            {
                await page.GotoAsync($"{BaseUrl}{route.TrimStart('/')}", new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 15000
                });

                await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'", new PageWaitForFunctionOptions { Timeout = 15000 });

                bool hasVisibleBlazorError = await page.EvaluateAsync<bool>(
                    @"() => {
                        const errorUi = document.querySelector('#blazor-error-ui');
                        if (!errorUi) return false;

                        const style = getComputedStyle(errorUi);
                        return style.display !== 'none' && style.visibility !== 'hidden' && errorUi.offsetParent !== null;
                    }");

                if (hasVisibleBlazorError)
                    failures.Add($"{route}: visible Blazor error UI");

                string bodyText = await page.Locator("body").InnerTextAsync(new LocatorInnerTextOptions { Timeout = 5000 });

                if (string.IsNullOrWhiteSpace(bodyText))
                    failures.Add($"{route}: empty page body");

                if (runtimeErrors.Count > 0)
                    failures.Add($"{route}: {string.Join(" | ", runtimeErrors.Distinct())}");
            }
            catch (Exception exception)
            {
                failures.Add($"{route}: {exception.Message}");
            }
        }

        failures.Should().BeEmpty();
    }

    private static IReadOnlyList<string> DiscoverDemoRoutes()
    {
        string root = FindRepositoryRoot();
        string pagesRoot = Path.Combine(root, "test", "Soenneker.Quark.Suite.Demo", "Pages");

        return Directory.EnumerateFiles(pagesRoot, "*.razor", SearchOption.AllDirectories)
            .SelectMany(file => DemoPageRouteRegex().Matches(File.ReadAllText(file)).Select(match => match.Groups[1].Value))
            .Where(route => !route.Contains('{', StringComparison.Ordinal))
            .Where(route => route != "/")
            .Distinct(StringComparer.Ordinal)
            .Order(StringComparer.Ordinal)
            .ToArray();
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "Soenneker.Quark.Suite.slnx")))
                return directory.FullName;

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate Soenneker.Quark.Suite.slnx from the test output directory.");
    }

    private static bool IsIgnoredConsoleError(string text)
    {
        if (text.Contains("favicon", StringComparison.OrdinalIgnoreCase))
            return true;

        if (text.Contains("MONO_WASM", StringComparison.Ordinal) &&
            (text.Contains("Response body loading was aborted", StringComparison.OrdinalIgnoreCase) ||
             text.Contains("TypeError: Failed to fetch", StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        if (text.Contains("Error in mono_download_assets", StringComparison.Ordinal) &&
            text.Contains("TypeError: Failed to fetch", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    [GeneratedRegex("@page\\s+\\\"([^\\\"]+)\\\"", RegexOptions.Compiled)]
    private static partial Regex DemoPageRouteRegex();
}
