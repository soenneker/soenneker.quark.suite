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
public sealed class QuarkResizablePlaywrightTests : PlaywrightUnitTest
{
    public QuarkResizablePlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Resizable_handle_demo_supports_keyboard_resize()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}resizable",
            static p => p.Locator("#resizable-handle-demo"),
            expectedTitle: "Resizable - Quark Suite");

        ILocator handleSection = page.Locator("#resizable-handle-demo");
        ILocator handle = handleSection.Locator("[data-slot='resizable-handle']").First;
        ILocator panels = handleSection.Locator("[data-slot='resizable-panel']");

        double leftBefore = await GetPanelSizeAsync(panels.Nth(0));
        double rightBefore = await GetPanelSizeAsync(panels.Nth(1));

        await Assertions.Expect(handle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await handle.FocusAsync();
        await handle.PressAsync("ArrowRight");

        await Assertions.Expect(handle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        double leftAfter = await GetPanelSizeAsync(panels.Nth(0));
        double rightAfter = await GetPanelSizeAsync(panels.Nth(1));
        Assert.True(leftAfter > leftBefore);
        Assert.True(rightAfter < rightBefore);
    }

[Fact]
    public async ValueTask Resizable_examples_resize_with_pointer_in_horizontal_vertical_and_rtl_layouts()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}resizable",
            static p => p.Locator("#resizable-basic-demo"),
            expectedTitle: "Resizable - Quark Suite");

        ILocator basicHandle = page.Locator("#resizable-basic-demo [data-slot='resizable-handle']").First;
        ILocator basicPanels = page.Locator("#resizable-basic-demo [data-slot='resizable-panel']");
        double basicLeftBefore = await GetPanelSizeAsync(basicPanels.Nth(0));
        double basicRightBefore = await GetPanelSizeAsync(basicPanels.Nth(1));

        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("aria-orientation", "vertical");
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("tabindex", "0");

        await DragHandleAsync(page, basicHandle, 80, 0);

        await Assertions.Expect(basicHandle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        double basicLeftAfter = await GetPanelSizeAsync(basicPanels.Nth(0));
        double basicRightAfter = await GetPanelSizeAsync(basicPanels.Nth(1));
        Assert.True(basicLeftAfter > basicLeftBefore);
        Assert.True(basicRightAfter < basicRightBefore);
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(basicHandle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "50");

        ILocator verticalHandle = page.Locator("#resizable-vertical-demo [data-slot='resizable-handle']").First;
        ILocator verticalPanels = page.Locator("#resizable-vertical-demo [data-slot='resizable-panel']");
        double verticalTopBefore = await GetPanelSizeAsync(verticalPanels.Nth(0));
        double verticalBottomBefore = await GetPanelSizeAsync(verticalPanels.Nth(1));

        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("aria-orientation", "horizontal");
        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("aria-valuenow", "33.33");

        await DragHandleAsync(page, verticalHandle, 0, 60);

        await Assertions.Expect(verticalHandle).Not.ToHaveAttributeAsync("aria-valuenow", "33.33");
        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(verticalHandle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "33.33");
        double verticalTopAfter = await WaitForPanelSizeConditionAsync(verticalPanels.Nth(0), size => size > verticalTopBefore);
        double verticalBottomAfter = await WaitForPanelSizeConditionAsync(verticalPanels.Nth(1), size => size < verticalBottomBefore);
        string? verticalValueNow = await verticalHandle.GetAttributeAsync("aria-valuenow");
        string? verticalLastPercentage = await verticalHandle.GetAttributeAsync("data-resizable-last-percentage");
        Assert.True(verticalTopAfter > verticalTopBefore, $"Vertical top panel did not grow. before={verticalTopBefore}, after={verticalTopAfter}, aria-valuenow={verticalValueNow}, last={verticalLastPercentage}");
        Assert.True(verticalBottomAfter < verticalBottomBefore, $"Vertical bottom panel did not shrink. before={verticalBottomBefore}, after={verticalBottomAfter}, aria-valuenow={verticalValueNow}, last={verticalLastPercentage}");

        ILocator rtlHandle = page.Locator("#resizable-rtl-demo [data-slot='resizable-handle']").First;
        ILocator rtlPanels = page.Locator("#resizable-rtl-demo [data-slot='resizable-panel']");
        double rtlPrimaryBefore = await GetPanelSizeAsync(rtlPanels.Nth(0));
        double rtlSecondaryBefore = await GetPanelSizeAsync(rtlPanels.Nth(1));

        await Assertions.Expect(rtlHandle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(rtlHandle).ToHaveAttributeAsync("aria-valuenow", "50");
        Assert.Equal("rtl", await page.Locator("#resizable-rtl-demo [data-slot='resizable-panel-group']").First.EvaluateAsync<string>(
            "element => getComputedStyle(element).direction"));

        await DragHandleAsync(page, rtlHandle, -80, 0);

        await Assertions.Expect(rtlHandle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(rtlHandle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(rtlHandle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "50");
        double rtlPrimaryAfter = await WaitForPanelSizeConditionAsync(rtlPanels.Nth(0), size => size > rtlPrimaryBefore);
        double rtlSecondaryAfter = await WaitForPanelSizeConditionAsync(rtlPanels.Nth(1), size => size < rtlSecondaryBefore);
        Assert.True(rtlPrimaryAfter > rtlPrimaryBefore);
        Assert.True(rtlSecondaryAfter < rtlSecondaryBefore);
    }

[Fact]
    public async ValueTask Resizable_handle_demo_renders_visible_grip_and_supports_pointer_resize()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}resizable",
            static p => p.Locator("#resizable-handle-demo"),
            expectedTitle: "Resizable - Quark Suite");

        ILocator handleSection = page.Locator("#resizable-handle-demo");
        ILocator handle = handleSection.Locator("[data-slot='resizable-handle']").First;
        ILocator grip = handle.Locator("svg").First;
        ILocator panels = handleSection.Locator("[data-slot='resizable-panel']");

        double leftBefore = await GetPanelSizeAsync(panels.Nth(0));
        double rightBefore = await GetPanelSizeAsync(panels.Nth(1));

        await Assertions.Expect(grip).ToBeVisibleAsync();
        await Assertions.Expect(handle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(handle).ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(handle).ToHaveAttributeAsync("tabindex", "0");

        await DragHandleAsync(page, handle, 100, 0);

        await Assertions.Expect(handle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        double leftAfter = await GetPanelSizeAsync(panels.Nth(0));
        double rightAfter = await GetPanelSizeAsync(panels.Nth(1));
        Assert.True(leftAfter > leftBefore);
        Assert.True(rightAfter < rightBefore);
        await Assertions.Expect(handle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(handle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "50");
    }
    private static async Task<double> GetPanelSizeAsync(ILocator panel)
    {
        string? style = await panel.GetAttributeAsync("style");
        Assert.NotNull(style);

        const string marker = "flex: 0 0 ";
        int start = style!.IndexOf(marker, System.StringComparison.Ordinal);
        Assert.True(start >= 0);

        start += marker.Length;
        int end = style.IndexOf('%', start);
        Assert.True(end > start);

        string value = style[start..end];
        return double.Parse(value, CultureInfo.InvariantCulture);
    }

    private static async Task<double> WaitForPanelSizeConditionAsync(ILocator panel, Func<double, bool> predicate, int timeoutMs = 5000)
    {
        var start = DateTime.UtcNow;
        double lastSize = await GetPanelSizeAsync(panel);

        while ((DateTime.UtcNow - start).TotalMilliseconds < timeoutMs)
        {
            lastSize = await GetPanelSizeAsync(panel);
            if (predicate(lastSize))
                return lastSize;

            await Task.Delay(50);
        }

        return lastSize;
    }

    private static async Task DragHandleAsync(IPage page, ILocator handle, double deltaX, double deltaY)
    {
        await handle.ScrollIntoViewIfNeededAsync();
        await handle.EvaluateAsync(
            @"element => element.scrollIntoView({ block: 'center', inline: 'center', behavior: 'instant' })");

        var rect = await handle.BoundingBoxAsync();
        Assert.NotNull(rect);
        Assert.True(float.IsFinite(rect!.X));
        Assert.True(float.IsFinite(rect.Y));
        Assert.True(float.IsFinite(rect.Width));
        Assert.True(float.IsFinite(rect.Height));

        var dragStart = await handle.EvaluateAsync<DragStartMetrics>(
            @"element => {
                const group = element.closest('[data-slot=""resizable-panel-group""]');
                const handleRect = element.getBoundingClientRect();
                const groupRect = group?.getBoundingClientRect();
                const orientation = element.getAttribute('aria-orientation');
                const valueNow = Number(element.getAttribute('aria-valuenow'));
                const direction = group ? getComputedStyle(group).direction : 'ltr';

                if (!groupRect || !Number.isFinite(valueNow)) {
                    return null;
                }

                const ratio = valueNow / 100;
                const startX = orientation === 'vertical'
                    ? (direction === 'rtl'
                        ? groupRect.right - (groupRect.width * ratio)
                        : groupRect.left + (groupRect.width * ratio))
                    : handleRect.left + (handleRect.width / 2);
                const startY = orientation === 'horizontal'
                    ? groupRect.top + (groupRect.height * ratio)
                    : handleRect.top + (handleRect.height / 2);

                return { startX, startY };
            }");

        Assert.NotNull(dragStart);
        Assert.True(double.IsFinite(dragStart!.StartX));
        Assert.True(double.IsFinite(dragStart.StartY));

        double startX = dragStart.StartX;
        double startY = dragStart.StartY;

        await handle.EvaluateAsync(
            @"async (handleElement, arg) => {
                const startX = Number(arg.startX);
                const startY = Number(arg.startY);
                const endX = Number(arg.endX);
                const endY = Number(arg.endY);

                if (![startX, startY, endX, endY].every(Number.isFinite)) {
                    throw new Error(`Resizable pointer coordinates were non-finite. startX=${startX}, startY=${startY}, endX=${endX}, endY=${endY}`);
                }

                const pointerId = 1;
                const dispatchPointer = (target, type, clientX, clientY, buttons) => {
                    target.dispatchEvent(new PointerEvent(type, {
                        bubbles: true,
                        cancelable: true,
                        composed: true,
                        pointerId,
                        pointerType: 'mouse',
                        isPrimary: true,
                        button: 0,
                        buttons,
                        clientX,
                        clientY
                    }));
                };

                dispatchPointer(handleElement, 'pointerdown', startX, startY, 1);

                const steps = 16;
                for (let index = 1; index <= steps; index++) {
                    const progress = index / steps;
                    const x = startX + ((endX - startX) * progress);
                    const y = startY + ((endY - startY) * progress);
                    dispatchPointer(window, 'pointermove', x, y, 1);
                    await new Promise(resolve => setTimeout(resolve, 8));
                }

                dispatchPointer(window, 'pointerup', endX, endY, 0);
            }",
            new
            {
                startX,
                startY,
                endX = startX + deltaX,
                endY = startY + deltaY
            });
    }

    private sealed class DragStartMetrics
    {
        public double StartX { get; set; }
        public double StartY { get; set; }
    }
}
