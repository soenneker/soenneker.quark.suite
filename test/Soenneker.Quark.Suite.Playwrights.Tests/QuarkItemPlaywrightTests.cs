using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkItemPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkItemPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Item_demo_matches_shadcn_structure_and_interactions()
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
            $"{BaseUrl}items",
            static p => p.GetByText("Default item").First,
            expectedTitle: "Item - Quark Suite");

        var firstItem = page.Locator("[data-slot='item']").First;
        await Assertions.Expect(firstItem).ToBeVisibleAsync();
        await Assertions.Expect(firstItem).ToHaveAttributeAsync("data-variant", "outline");
        await Assertions.Expect(firstItem).ToHaveAttributeAsync("data-size", "default");

        var classProbe = await firstItem.EvaluateAsync<ItemClassProbe>(
            @"element => {
                const style = getComputedStyle(element);
                return {
                    display: style.display,
                    flexWrap: style.flexWrap,
                    borderRadius: style.borderRadius,
                    paddingTop: style.paddingTop,
                    columnGap: style.columnGap
                };
            }");

        classProbe.display.Should().Be("flex");
        classProbe.flexWrap.Should().Be("wrap");
        classProbe.paddingTop.Should().Be("10px");
        classProbe.columnGap.Should().Be("10px");

        var itemGroup = page.Locator("[data-slot='item-group']").First;
        await Assertions.Expect(itemGroup).ToHaveAttributeAsync("role", "list");
        await Assertions.Expect(itemGroup.Locator("[data-slot='item-separator']")).ToBeVisibleAsync();

        var linkItem = page.Locator("a[data-slot='item']").First;
        await Assertions.Expect(linkItem).ToBeVisibleAsync();
        await linkItem.FocusAsync();
        await Assertions.Expect(linkItem).ToBeFocusedAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Actions" }).ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Menuitem, new() { Name = "Edit" })).ToBeVisibleAsync();
        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(page.GetByRole(AriaRole.Menuitem, new() { Name = "Edit" })).ToBeHiddenAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class ItemClassProbe
    {
        public string? display { get; set; }
        public string? flexWrap { get; set; }
        public string? borderRadius { get; set; }
        public string? paddingTop { get; set; }
        public string? columnGap { get; set; }
    }
}
