using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkInputGroupPlaywrightTests : PlaywrightUnitTest
{
    public QuarkInputGroupPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Input_group_demo_renders_as_a_single_border_without_nested_input_border()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}input-groups",
            static p => p.GetByPlaceholder("Search...").First,
            expectedTitle: "Input Group - Quark Suite");

        ILocator group = page.Locator("[data-slot='input-group']").First;
        ILocator input = group.GetByPlaceholder("Search...");
        ILocator addon = group.Locator("[data-slot='input-group-addon']").First;

        await Assertions.Expect(group).ToBeVisibleAsync();
        await Assertions.Expect(input).ToBeVisibleAsync();
        await Assertions.Expect(addon).ToBeVisibleAsync();

        InputGroupBorderProbe? borderProbe = await group.EvaluateAsync<InputGroupBorderProbe>(
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
                    groupWidth: groupRect.width,
                    addonWidth: addonRect?.width ?? 0
                };
            }");

        Assert.NotNull(borderProbe);
        Assert.Equal("1px", borderProbe.groupBorderTop);
        Assert.Equal("1px", borderProbe.groupBorderRight);
        Assert.Equal("0px", borderProbe.inputBorderTop);
        Assert.Equal("0px", borderProbe.inputBorderRight);
        Assert.True(borderProbe.groupWidth >= 200, $"Expected the input group wrapper to render with width, but measured {borderProbe.groupWidth}.");
        Assert.True(borderProbe.addonWidth >= 16, $"Expected the trailing addon to remain visible, but measured {borderProbe.addonWidth}.");
    }
    private sealed class InputGroupBorderProbe
    {
        public string? groupBorderTop { get; set; }
        public string? groupBorderRight { get; set; }
        public string? inputBorderTop { get; set; }
        public string? inputBorderRight { get; set; }
        public double groupWidth { get; set; }
        public double addonWidth { get; set; }
    }
}
