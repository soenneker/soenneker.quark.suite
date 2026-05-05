using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkPopoverPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkPopoverPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Popover_smoke_aschild_trigger_closes_default_open_popover_without_runtime_error()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var sawRuntimeError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error" && message.Text.Contains("StackOverflowException", StringComparison.Ordinal))
                sawRuntimeError = true;
        };

        page.PageError += (_, _) => sawRuntimeError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}popover-smoke",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true }),
            expectedTitle: "Popover Smoke - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open popover", Exact = true });
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        await trigger.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        (sawRuntimeError).Should().BeFalse();
    }

    private sealed class PopoverRoleProbe
    {
        public string? role { get; set; }
        public string? ariaLabel { get; set; }
        public string? dataState { get; set; }
        public string? selectedText { get; set; }
    }

    private static ILocator DemoTrigger(IPage page) =>
        page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Displays rich content in a portal, triggered by a button." })
            .First.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Open popover", Exact = true });
}
