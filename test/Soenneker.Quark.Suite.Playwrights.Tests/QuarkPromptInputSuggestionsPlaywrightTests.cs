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
        await Assertions.Expect(prompt).ToHaveAttributeAsync("data-global-drop", "true");
        await Assertions.Expect(prompt).ToHaveAttributeAsync("data-multiple", "true");

        var input = page.Locator("[data-slot='prompt-input-textarea']");
        var fileInput = page.Locator("[data-slot='prompt-input-file-input']");
        await Assertions.Expect(fileInput).ToHaveAttributeAsync("type", "file");
        await page.WaitForFunctionAsync("() => document.querySelector('[data-slot=\"prompt-input-file-input\"]')?.__quarkPromptInputAttachmentsRegistered === true");

        await fileInput.SetInputFilesAsync(new[]
        {
            new FilePayload
            {
                Name = "notes.md",
                MimeType = "text/markdown",
                Buffer = System.Text.Encoding.UTF8.GetBytes("# Notes")
            }
        });
        await Assertions.Expect(page.Locator("[data-slot='attachment-name']").Filter(new LocatorFilterOptions { HasText = "notes.md" })).ToHaveCountAsync(1);
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Remove notes.md", Exact = true })).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Search", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Web search: on", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions { Name = "Model", Exact = true }).ClickAsync();
        await page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Claude 4 Opus", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Model: claude-opus-4-20250514", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "More actions", Exact = true }).ClickAsync();
        await page.GetByRole(AriaRole.Menuitem, new PageGetByRoleOptions { Name = "Add screenshot", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Screenshots: 1", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        await input.FillAsync("Summarize this page");
        await input.PressAsync("Enter");
        await Assertions.Expect(page.GetByText("Submitted: Summarize this page")).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByText("Submitted files: notes.md", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        (await input.InputValueAsync()).Should().Be("Summarize this page");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Remove notes.md", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("notes.md", new PageGetByTextOptions { Exact = true })).Not.ToBeVisibleAsync();
        await page.WaitForFunctionAsync("() => document.querySelector('[data-slot=\"prompt-input-file-input\"]')?.__quarkPromptInputAttachmentsRegistered === true && document.querySelector('[data-slot=\"prompt-input-file-input\"]')?.__quarkPromptInputGlobalDrop === true");

        await input.FillAsync("Line one");
        await input.PressAsync("Shift+Enter");
        await input.PressSequentiallyAsync("Line two");
        await Assertions.Expect(page.GetByText("Submitted: Line one Line two", new PageGetByTextOptions { Exact = true })).Not.ToBeVisibleAsync();
        (await input.InputValueAsync()).Should().Be("Line one\nLine two");

        await input.PressAsync("Enter");
        await Assertions.Expect(page.GetByText("Submitted: Line one Line two", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        (await input.InputValueAsync()).Should().Be("Line one\nLine two");

        await input.FillAsync("Send with button");
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Send", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Submitted: Send with button")).ToBeVisibleAsync();

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

        await Assertions.Expect(page.Locator("[data-slot='suggestion']")).ToHaveCountAsync(3);
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Extract action items", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Selected: Extract action items")).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
