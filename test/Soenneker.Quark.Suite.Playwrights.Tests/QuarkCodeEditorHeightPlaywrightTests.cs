using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using System.Threading;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCodeEditorHeightPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async ValueTask Offscreen_editors_load_Monaco_only_after_scrolling_into_view(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.AddInitScriptAsync("""
            performance.setResourceTimingBufferSize(5000);
            document.addEventListener('DOMContentLoaded', () => {
                const style = document.createElement('style');
                style.textContent = '[class~="group/code-editor"] { margin-top: 200vh !important; }';
                document.head.append(style);
            });
            """);
        await page.GotoAndWaitForReady($"{BaseUrl}components/codeeditors",
            static p => p.Locator("[data-slot='code-editor']").First);
        await page.WaitForFunctionAsync("performance.getEntriesByType('resource').some(e => e.name.includes('/monacointerop'))");
        await Assertions.Expect(page.Locator(".monaco-editor")).ToHaveCountAsync(0);
        bool downloaded = await page.EvaluateAsync<bool>("performance.getEntriesByType('resource').some(e => e.name.includes('/monaco-editor/'))");
        if (downloaded)
            throw new System.InvalidOperationException("An offscreen editor downloaded Monaco before entering the viewport.");

        var editor = page.Locator("[data-slot='code-editor']").First;
        await editor.ScrollIntoViewIfNeededAsync();
        await Assertions.Expect(editor.Locator(".monaco-editor")).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Typing_updates_content_and_auto_height_only_when_the_line_count_requires_it(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/codeeditors",
            static p => p.Locator("[data-slot='code-editor'] .monaco-editor").First);

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
