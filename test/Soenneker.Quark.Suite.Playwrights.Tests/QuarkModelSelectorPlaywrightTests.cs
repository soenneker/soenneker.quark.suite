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
    public async ValueTask ModelSelector_uses_popover_command_search_and_updates_value()
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
            static p => p.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions { Name = "Select a model", Exact = true }),
            expectedTitle: "Model Selector - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions { Name = "Select a model", Exact = true });
        await Assertions.Expect(trigger).ToHaveTextAsync(new Regex("GPT-5.5"));
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");

        await trigger.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(page.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions { Name = "Search Models..." })).ToBeVisibleAsync();

        var input = page.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions { Name = "Search Models..." });
        await input.FillAsync("gemini");

        var gemini = page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Gemini 2.5 Pro", Exact = true });
        await Assertions.Expect(gemini).ToBeVisibleAsync();
        await gemini.ClickAsync();

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(trigger).ToHaveTextAsync(new Regex("Gemini 2.5 Pro"));
        await Assertions.Expect(page.GetByText("Selected: gemini-2.5-pro")).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
