using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkStructurePlaywrightTests : PlaywrightUnitTest
{
    public QuarkStructurePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Structure_demos_preserve_native_semantics_and_no_runtime_errors()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, msg) =>
        {
            if (msg.Type == "error")
                consoleErrors.Add(msg.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}semantic-html",
            static p => p.Locator("main[data-slot='main']").First,
            expectedTitle: "Semantic HTML - Quark Suite");

        await Assertions.Expect(page.Locator("main[data-slot='main']").First).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("article[data-slot='article']").First).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("aside[data-slot='aside']").First).ToBeVisibleAsync();

        await page.GotoAndWaitForReady(
            $"{BaseUrl}figures",
            static p => p.Locator("figure[data-slot='figure']").First,
            expectedTitle: "Figures & Figcaptions - Quark Suite");

        await Assertions.Expect(page.Locator("figure[data-slot='figure'] figcaption[data-slot='figcaption']").First).ToBeVisibleAsync();

        await page.GotoAndWaitForReady(
            $"{BaseUrl}fieldsets-demo",
            static p => p.Locator("fieldset[data-slot='field-set']").First,
            expectedTitle: "Fieldsets & Legends - Quark Suite");

        var disabledFieldset = page.Locator("fieldset[data-slot='field-set'][disabled]").First;
        await Assertions.Expect(disabledFieldset).ToBeVisibleAsync();
        await Assertions.Expect(disabledFieldset.Locator("legend[data-slot='field-legend']").First).ToBeVisibleAsync();
        (await disabledFieldset.Locator("input").First.IsDisabledAsync()).Should().BeTrue();

        await page.GotoAndWaitForReady(
            $"{BaseUrl}details-demo",
            static p => p.Locator("details[data-slot='details']").First,
            expectedTitle: "Details & Summary - Quark Suite");

        var details = page.Locator("details[data-slot='details']").First;
        var summary = details.Locator("summary[data-slot='summary']").First;
        await Assertions.Expect(details).Not.ToHaveAttributeAsync("open", string.Empty);
        await summary.ClickAsync();
        await Assertions.Expect(details).ToHaveAttributeAsync("open", string.Empty);
        await summary.FocusAsync();
        await page.Keyboard.PressAsync("Enter");
        await Assertions.Expect(details).Not.ToHaveAttributeAsync("open", string.Empty);
        await Assertions.Expect(summary).ToBeFocusedAsync();
        await page.Keyboard.PressAsync("Space");
        await Assertions.Expect(details).ToHaveAttributeAsync("open", string.Empty);
        await Assertions.Expect(summary).ToBeFocusedAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
