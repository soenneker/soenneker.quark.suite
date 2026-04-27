using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkLayoutPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkLayoutPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Layout_demos_render_container_grid_and_stack_contracts_without_console_errors()
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
            $"{BaseUrl}containers",
            static p => p.GetByText("Even tighter layout").First,
            expectedTitle: "Container - Quark Suite");

        var defaultContainer = page.Locator("div.max-w-2xl").Filter(new LocatorFilterOptions { HasText = "Even tighter layout" }).First;
        var containerProbe = await defaultContainer
            .EvaluateAsync<ContainerProbe>(
                @"element => {
                    const style = getComputedStyle(element);
                    const rect = element.getBoundingClientRect();
                    return {
                        width: rect.width,
                        maxWidth: style.maxWidth,
                        marginLeft: style.marginLeft,
                        marginRight: style.marginRight,
                        paddingLeft: style.paddingLeft
                    };
                }");

        containerProbe.width.Should().BeGreaterThan(300);
        containerProbe.maxWidth.Should().NotBeNullOrWhiteSpace();
        ParsePixels(containerProbe.paddingLeft).Should().BeGreaterThanOrEqualTo(16);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}grids",
            static p => p.GetByText("Featured").First,
            expectedTitle: "Grid - Quark Suite");

        var gridProbe = await page.GetByText("Featured").First.Locator("xpath=ancestor::div[contains(@class, 'grid-cols-3')][1]")
            .EvaluateAsync<GridProbe>(
                @"element => {
                    const featured = element.querySelector('[data-slot=""grid-item""]');
                    const style = getComputedStyle(element);
                    const featuredStyle = featured ? getComputedStyle(featured) : null;
                    return {
                        display: style.display,
                        templateColumns: style.gridTemplateColumns,
                        columnGap: style.columnGap,
                        featuredColumnEnd: featuredStyle?.gridColumnEnd ?? ''
                    };
                }");

        gridProbe.display.Should().Be("grid");
        gridProbe.templateColumns.Should().Contain("px");
        gridProbe.columnGap.Should().Be("16px");
        gridProbe.featuredColumnEnd.Should().Be("span 2");

        await page.GotoAndWaitForReady(
            $"{BaseUrl}stacks",
            static p => p.GetByText("All").First,
            expectedTitle: "Stack - Quark Suite");

        var horizontalStack = page.Locator("div.flex-wrap").Filter(new LocatorFilterOptions { HasText = "All" }).First;
        var stackProbe = await horizontalStack
            .EvaluateAsync<StackProbe>(
                @"element => {
                    const style = getComputedStyle(element);
                    return {
                        display: style.display,
                        flexDirection: style.flexDirection,
                        flexWrap: style.flexWrap,
                        gap: style.gap
                    };
                }");

        stackProbe.display.Should().Be("flex");
        stackProbe.flexDirection.Should().Be("row");
        stackProbe.flexWrap.Should().Be("wrap");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class ContainerProbe
    {
        public double width { get; set; }
        public string? maxWidth { get; set; }
        public string? marginLeft { get; set; }
        public string? marginRight { get; set; }
        public string? paddingLeft { get; set; }
    }

    private static double ParsePixels(string? value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.EndsWith("px", System.StringComparison.Ordinal))
            return 0;

        return double.TryParse(value[..^2], out var pixels) ? pixels : 0;
    }

    private sealed class GridProbe
    {
        public string? display { get; set; }
        public string? templateColumns { get; set; }
        public string? columnGap { get; set; }
        public string? featuredColumnEnd { get; set; }
    }

    private sealed class StackProbe
    {
        public string? display { get; set; }
        public string? flexDirection { get; set; }
        public string? flexWrap { get; set; }
        public string? gap { get; set; }
    }
}
