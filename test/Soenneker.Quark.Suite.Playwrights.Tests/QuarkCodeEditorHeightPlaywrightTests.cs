using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCodeEditorHeightPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async Task Typing_updates_content_and_auto_height_only_when_the_line_count_requires_it()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/codeeditors",
            static p => p.Locator("[data-slot='code-editor'] .monaco-editor").First,
            expectedTitle: "Code Editor - Quark Suite");

        var editor = page.Locator("[data-slot='code-editor']").First;
        var input = editor.GetByRole(AriaRole.Textbox).First;
        await input.FocusAsync();
        await page.Keyboard.PressAsync("ControlOrMeta+A");
        await page.Keyboard.InsertTextAsync("// typing");
        await Assertions.Expect(editor.Locator(".view-lines")).ToContainTextAsync("// typing");
        string? shortStyle = await editor.GetAttributeAsync("style");

        await page.Keyboard.InsertTextAsync("\n1\n2\n3\n4\n5\n6\n7\n8\n9\n10\n11");
        await Assertions.Expect(editor).Not.ToHaveAttributeAsync("style", shortStyle!);
        string? tallerStyle = await editor.GetAttributeAsync("style");
        await page.Keyboard.InsertTextAsync(" unchanged height");
        await Assertions.Expect(editor.Locator(".view-lines")).ToContainTextAsync("unchanged height");
        await Assertions.Expect(editor).ToHaveAttributeAsync("style", tallerStyle!);
    }
}
