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
    public async ValueTask Dropdown_menu_exposes_exit_motion_and_unmounts_after_close()
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
        await Assertions.Expect(content).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bdata-\[state=closed\]:animate-out\b"));
        await Assertions.Expect(content).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bdata-\[state=closed\]:fade-out-0\b"));
        string animationDuration = await content.EvaluateAsync<string>("element => getComputedStyle(element).animationDuration");
        animationDuration.Should().Be("0.15s");

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(page.Locator("[data-slot='dropdown-menu-content'][data-state='open']")).ToHaveCountAsync(0);
        await Assertions.Expect(content).Not.ToBeAttachedAsync(new LocatorAssertionsToBeAttachedOptions { Timeout = 1000 });
    }
}
