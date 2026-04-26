using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkModelSelectorPlaywrightTests : PlaywrightUnitTest
{
    public QuarkModelSelectorPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask ModelSelector_uses_popover_radio_list_and_updates_value()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady($"{BaseUrl}model-selectors",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Select a model", Exact = true }),
            expectedTitle: "Model Selector - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Select a model", Exact = true });
        await Assertions.Expect(trigger).ToHaveTextAsync(new Regex("GPT-4"));
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");

        await trigger.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(page.Locator("[data-slot='model-selector-label']")).ToHaveTextAsync("Select model");
        await Assertions.Expect(page.Locator("[data-slot='model-selector-radio-item']")).ToHaveCountAsync(4);

        var gemini = page.Locator("[data-slot='model-selector-radio-item']").Filter(new LocatorFilterOptions { HasText = "Gemini 1.5 Flash" });
        await Assertions.Expect(gemini).ToBeVisibleAsync();
        await gemini.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(trigger).ToHaveTextAsync(new Regex("Gemini 1.5 Flash"));
        await Assertions.Expect(page.GetByText("Selected: gemini-1.5-flash")).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
