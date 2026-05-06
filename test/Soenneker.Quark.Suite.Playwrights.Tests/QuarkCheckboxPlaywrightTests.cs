using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCheckboxPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCheckboxPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Checkbox_keyboard_disabled_and_console_behavior_match_radix()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checkbox",
            static p => p.Locator("#terms-checkbox"),
            expectedTitle: "Checkbox Component - Quark Suite");
        await WaitForInteractive(page);
        await WaitForCheckboxRoot(page, "#terms-checkbox");

        var checkbox = page.Locator("#terms-checkbox");
        var disabled = page.Locator("#toggle-checkbox");

        await Assertions.Expect(checkbox).ToHaveAttributeAsync("role", "checkbox");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "false");

        await checkbox.FocusAsync();
        await Assertions.Expect(checkbox).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("Enter");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("Space");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("data-state", "checked");

        await Assertions.Expect(disabled).ToBeDisabledAsync();
        await Assertions.Expect(disabled).ToHaveAttributeAsync("aria-checked", "false");
        await disabled.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(disabled).ToHaveAttributeAsync("aria-checked", "false");

        consoleErrors.Should().BeEmpty();
    }

    private static async Task WaitForInteractive(IPage page)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'");
    }

    private static Task WaitForCheckboxRoot(IPage page, string selector)
    {
        return page.WaitForFunctionAsync(
            "selector => document.querySelector(selector)?.hasAttribute('data-bradix-checkbox-root') === true",
            selector);
    }
}
