using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkOrderedListPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkOrderedListPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask OrderedList_demo_uses_semantic_list_styles_and_interactive_content()
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
            $"{BaseUrl}orderedlists",
            static p => p.Locator("ol[data-slot='ordered-list']").First,
            expectedTitle: "Ordered Lists - Quark Suite");

        var defaultProbe = await page.Locator("ol[data-slot='ordered-list']").First.EvaluateAsync<ListProbe>(
            @"element => {
                const style = getComputedStyle(element);
                const item = element.querySelector('li');
                const itemStyle = getComputedStyle(item);
                return {
                    listStyleType: style.listStyleType,
                    marginTop: style.marginTop,
                    marginLeft: style.marginLeft,
                    firstItemDisplay: itemStyle.display
                };
            }");

        defaultProbe.listStyleType.Should().Be("decimal");
        defaultProbe.marginTop.Should().Be("24px");

        var lowerAlphaProbe = await page.Locator("ol[style*='lower-alpha']").First.EvaluateAsync<ListProbe>(
            @"element => {
                const style = getComputedStyle(element);
                return {
                    listStyleType: style.listStyleType,
                    marginTop: style.marginTop,
                    marginLeft: style.marginLeft,
                    firstItemDisplay: ''
                };
            }");

        lowerAlphaProbe.listStyleType.Should().Be("lower-alpha");

        var unstyled = page.Locator("ol.list-none").First;
        await Assertions.Expect(unstyled).ToBeVisibleAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Start Step 1" }).ClickAsync();
        await Assertions.Expect(page.GetByText("Last clicked: Step 1", new() { Exact = true })).ToBeVisibleAsync();

        var accessible = page.GetByRole(AriaRole.List, new() { Name = "Step-by-step instructions" });
        await Assertions.Expect(accessible).ToBeVisibleAsync();
        await Assertions.Expect(accessible.GetByRole(AriaRole.Listitem, new() { Name = "First instruction" })).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class ListProbe
    {
        public string? listStyleType { get; set; }
        public string? marginTop { get; set; }
        public string? marginLeft { get; set; }
        public string? firstItemDisplay { get; set; }
    }
}
