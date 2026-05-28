using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDropdownPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkDropdownPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Dropdown_menu_fades_out_before_unmounting()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/dropdown-menu",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open", Exact = true }).First;
        await trigger.ClickAsync();

        var content = page.Locator("[data-slot='dropdown-menu-content'][data-state='open']").First;
        await Assertions.Expect(content).ToBeVisibleAsync();

        await page.Keyboard.PressAsync("Escape");
        await page.WaitForTimeoutAsync(25);

        var closingContent = page.Locator("[data-slot='dropdown-menu-content'][data-state='closed']").First;
        await Assertions.Expect(closingContent).ToBeAttachedAsync();

        Dictionary<string, string?> snapshot = await closingContent.EvaluateAsync<Dictionary<string, string?>>(
            "element => ({ dataState: element.getAttribute('data-state'), animationDuration: getComputedStyle(element).animationDuration })");

        snapshot["dataState"].Should().Be("closed");
        snapshot["animationDuration"].Should().Be("0.15s");

        await Assertions.Expect(closingContent).Not.ToBeAttachedAsync(new LocatorAssertionsToBeAttachedOptions { Timeout = 1000 });
    }
}
