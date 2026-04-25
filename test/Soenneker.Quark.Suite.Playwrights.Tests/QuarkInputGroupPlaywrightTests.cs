using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkInputGroupPlaywrightTests : PlaywrightUnitTest
{
    public QuarkInputGroupPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Input_group_demo_renders_as_a_single_border_without_nested_input_border()
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
            $"{BaseUrl}input-groups",
            static p => p.GetByPlaceholder("Search...").First,
            expectedTitle: "Input Group - Quark Suite");

        var group = page.Locator("[data-slot='input-group']").First;
        var input = group.GetByPlaceholder("Search...");
        var addon = group.Locator("[data-slot='input-group-addon']").First;

        await Assertions.Expect(group).ToBeVisibleAsync();
        await Assertions.Expect(input).ToBeVisibleAsync();
        await Assertions.Expect(addon).ToBeVisibleAsync();

        var borderProbe = await group.EvaluateAsync<InputGroupBorderProbe>(
            @"element => {
                const input = element.querySelector('[data-slot=""input-group-control""]');
                const addon = element.querySelector('[data-slot=""input-group-addon""]');
                const groupStyle = getComputedStyle(element);
                const inputStyle = input ? getComputedStyle(input) : null;
                const groupRect = element.getBoundingClientRect();
                const addonRect = addon ? addon.getBoundingClientRect() : null;

                return {
                    groupBorderTop: groupStyle.borderTopWidth,
                    groupBorderRight: groupStyle.borderRightWidth,
                    inputBorderTop: inputStyle?.borderTopWidth,
                    inputBorderRight: inputStyle?.borderRightWidth,
                    groupHeight: groupRect.height,
                    groupWidth: groupRect.width,
                    addonWidth: addonRect?.width ?? 0
                };
            }");

        (borderProbe).Should().NotBeNull();
        (borderProbe.groupBorderTop).Should().Be("1px");
        (borderProbe.groupBorderRight).Should().Be("1px");
        (borderProbe.inputBorderTop).Should().Be("0px");
        (borderProbe.inputBorderRight).Should().Be("0px");
        (borderProbe.groupHeight).Should().BeInRange(35, 38);
        (borderProbe.groupWidth >= 200).Should().BeTrue();
        (borderProbe.addonWidth >= 16).Should().BeTrue();

        await addon.ClickAsync();
        await Assertions.Expect(input).ToBeFocusedAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class InputGroupBorderProbe
    {
        public string? groupBorderTop { get; set; }
        public string? groupBorderRight { get; set; }
        public string? inputBorderTop { get; set; }
        public string? inputBorderRight { get; set; }
        public double groupHeight { get; set; }
        public double groupWidth { get; set; }
        public double addonWidth { get; set; }
    }
}
