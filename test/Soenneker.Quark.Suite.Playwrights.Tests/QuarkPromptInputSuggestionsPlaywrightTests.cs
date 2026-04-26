using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkPromptInputSuggestionsPlaywrightTests : PlaywrightUnitTest
{
    public QuarkPromptInputSuggestionsPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask PromptInput_submits_with_enter_and_button_without_console_errors()
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

        await page.GotoAndWaitForReady($"{BaseUrl}prompt-inputs",
            static p => p.Locator("[data-slot='prompt-input']"),
            expectedTitle: "Prompt Input - Quark Suite");

        var prompt = page.Locator("[data-slot='prompt-input']");
        await Assertions.Expect(prompt).ToHaveAttributeAsync("role", "group");
        await Assertions.Expect(prompt).ToHaveAttributeAsync("data-global-drop", "false");
        await Assertions.Expect(prompt).ToHaveAttributeAsync("data-multiple", "false");

        var input = page.Locator("[data-slot='prompt-input-textarea']");
        var addAttachment = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Add attachment", Exact = true });
        var send = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Send message", Exact = true });

        await Assertions.Expect(addAttachment).ToBeVisibleAsync();
        await Assertions.Expect(send).ToBeDisabledAsync();

        await input.FillAsync("Summarize this page");
        await input.PressAsync("Enter");
        await Assertions.Expect(input).ToBeDisabledAsync();
        await Assertions.Expect(input).ToHaveValueAsync(string.Empty, new LocatorAssertionsToHaveValueOptions { Timeout = 7000 });

        await input.FillAsync("Line one");
        await input.PressAsync("Shift+Enter");
        await input.PressSequentiallyAsync("Line two");
        (await input.InputValueAsync()).Should().Be("Line one\nLine two");

        await input.PressAsync("Enter");
        await Assertions.Expect(input).ToBeDisabledAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Suggestions_render_chips_and_click_updates_demo_state()
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

        await page.GotoAndWaitForReady($"{BaseUrl}suggestions",
            static p => p.Locator("[data-slot='suggestions']"),
            expectedTitle: "Suggestions - Quark Suite");

        await Assertions.Expect(page.Locator("[data-slot='suggestion']")).ToHaveCountAsync(5);
        await Assertions.Expect(page.Locator("[data-slot='suggestion-list']")).ToBeVisibleAsync();
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "How to learn React?", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Selected: How to learn React?")).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
