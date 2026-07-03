using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAutoSavePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkAutoSavePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask AutoSave_input_tabs_to_next_input_with_one_tab()
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

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/autosave",
            static p => p.Locator("#autosave-input"),
            expectedTitle: "Autosave - Quark Suite");

        var first = page.Locator("#autosave-input");
        var next = page.Locator("#autosave-textinput");

        await first.ClickAsync();
        await first.FillAsync("Acme Workspace Updated");
        await page.EvaluateAsync(
            """
            ([firstId, nextId]) => {
                const keep = new Set([firstId, nextId]);
                const selector = 'a[href], button, input, select, textarea, [tabindex]';

                for (const element of document.querySelectorAll(selector)) {
                    if (keep.has(element.id)) {
                        element.removeAttribute('tabindex');
                    } else {
                        element.setAttribute('tabindex', '-1');
                    }
                }
            }
            """,
            new[] { "autosave-input", "autosave-textinput" });
        await page.Keyboard.PressAsync("Tab");

        await Assertions.Expect(next).ToBeFocusedAsync();
        await page.WaitForTimeoutAsync(1_200);
        await Assertions.Expect(next).ToBeFocusedAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
