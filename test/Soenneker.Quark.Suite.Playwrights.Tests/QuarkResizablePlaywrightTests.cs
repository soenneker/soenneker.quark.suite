using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkResizablePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkResizablePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Resizable_handle_demo_supports_keyboard_resize()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var runtimeErrors = CaptureRuntimeErrors(page);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}resizable",
            static p => p.Locator("#resizable-handle-demo"),
            expectedTitle: "Resizable - Quark Suite");

        var handleSection = page.Locator("#resizable-handle-demo");
        var handle = handleSection.Locator("[data-slot='resizable-handle']").First;
        var panels = handleSection.Locator("[data-slot='resizable-panel']");

        var leftBefore = await GetPanelSize(panels.Nth(0));
        var rightBefore = await GetPanelSize(panels.Nth(1));

        await Assertions.Expect(handle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(handle).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bfocus-visible:ring-offset-1\b"));
        await handle.FocusAsync();
        await handle.PressAsync("ArrowRight");

        await Assertions.Expect(handle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        var leftAfter = await GetPanelSize(panels.Nth(0));
        var rightAfter = await GetPanelSize(panels.Nth(1));
        (leftAfter > leftBefore).Should().BeTrue();
        (rightAfter < rightBefore).Should().BeTrue();

        await handle.PressAsync("Tab");
        var handleStillFocused = await handle.EvaluateAsync<bool>("element => document.activeElement === element");
        handleStillFocused.Should().BeFalse();
        runtimeErrors.ConsoleErrors.Should().BeEmpty();
        runtimeErrors.PageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Resizable_examples_resize_with_pointer_in_horizontal_vertical_and_rtl_layouts()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var runtimeErrors = CaptureRuntimeErrors(page);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}resizable",
            static p => p.Locator("#resizable-basic-demo"),
            expectedTitle: "Resizable - Quark Suite");

        var basicHandle = page.Locator("#resizable-basic-demo [data-slot='resizable-handle']").First;
        var basicPanels = page.Locator("#resizable-basic-demo [data-slot='resizable-panel']");
        var basicLeftBefore = await GetPanelSize(basicPanels.Nth(0));
        var basicRightBefore = await GetPanelSize(basicPanels.Nth(1));

        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("aria-orientation", "vertical");
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(page.Locator("#resizable-basic-demo [data-slot='resizable-panel-group']").First).ToHaveAttributeAsync("aria-orientation", "horizontal");

        await DragHandle(page, basicHandle, 80, 0);

        await Assertions.Expect(basicHandle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        var basicLeftAfter = await GetPanelSize(basicPanels.Nth(0));
        var basicRightAfter = await GetPanelSize(basicPanels.Nth(1));
        (basicLeftAfter > basicLeftBefore).Should().BeTrue();
        (basicRightAfter < basicRightBefore).Should().BeTrue();
        await Assertions.Expect(basicHandle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(basicHandle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "50");

        var verticalHandle = page.Locator("#resizable-vertical-demo [data-slot='resizable-handle']").First;
        var verticalPanels = page.Locator("#resizable-vertical-demo [data-slot='resizable-panel']");
        var verticalTopBefore = await GetPanelSize(verticalPanels.Nth(0));
        var verticalBottomBefore = await GetPanelSize(verticalPanels.Nth(1));

        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("aria-orientation", "horizontal");
        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("aria-valuenow", "33.33");
        await Assertions.Expect(page.Locator("#resizable-vertical-demo [data-slot='resizable-panel-group']").First).ToHaveAttributeAsync("aria-orientation", "vertical");

        await DragHandle(page, verticalHandle, 0, 60);

        await Assertions.Expect(verticalHandle).Not.ToHaveAttributeAsync("aria-valuenow", "33.33");
        await Assertions.Expect(verticalHandle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(verticalHandle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "33.33");
        var verticalTopAfter = await WaitForPanelSizeCondition(verticalPanels.Nth(0), size => size > verticalTopBefore);
        var verticalBottomAfter = await WaitForPanelSizeCondition(verticalPanels.Nth(1), size => size < verticalBottomBefore);
        var verticalValueNow = await verticalHandle.GetAttributeAsync("aria-valuenow");
        var verticalLastPercentage = await verticalHandle.GetAttributeAsync("data-resizable-last-percentage");
        (verticalTopAfter > verticalTopBefore).Should().BeTrue();
        (verticalBottomAfter < verticalBottomBefore).Should().BeTrue();

        var rtlHandle = page.Locator("#resizable-rtl-demo [data-slot='resizable-handle']").First;
        var rtlPanels = page.Locator("#resizable-rtl-demo [data-slot='resizable-panel']");
        var rtlPrimaryBefore = await GetPanelSize(rtlPanels.Nth(0));
        var rtlSecondaryBefore = await GetPanelSize(rtlPanels.Nth(1));

        await Assertions.Expect(rtlHandle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(rtlHandle).ToHaveAttributeAsync("aria-valuenow", "50");
        (await page.Locator("#resizable-rtl-demo [data-slot='resizable-panel-group']").First.EvaluateAsync<string>(
            "element => getComputedStyle(element).direction")).Should().Be("rtl");

        await DragHandle(page, rtlHandle, -80, 0);

        await Assertions.Expect(rtlHandle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(rtlHandle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(rtlHandle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "50");
        var rtlPrimaryAfter = await WaitForPanelSizeCondition(rtlPanels.Nth(0), size => size > rtlPrimaryBefore);
        var rtlSecondaryAfter = await WaitForPanelSizeCondition(rtlPanels.Nth(1), size => size < rtlSecondaryBefore);
        (rtlPrimaryAfter > rtlPrimaryBefore).Should().BeTrue();
        (rtlSecondaryAfter < rtlSecondaryBefore).Should().BeTrue();
        runtimeErrors.ConsoleErrors.Should().BeEmpty();
        runtimeErrors.PageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Resizable_handle_demo_renders_visible_grip_and_supports_pointer_resize()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var runtimeErrors = CaptureRuntimeErrors(page);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}resizable",
            static p => p.Locator("#resizable-handle-demo"),
            expectedTitle: "Resizable - Quark Suite");

        var handleSection = page.Locator("#resizable-handle-demo");
        var handle = handleSection.Locator("[data-slot='resizable-handle']").First;
        var grip = handle.Locator("svg").First;
        var panels = handleSection.Locator("[data-slot='resizable-panel']");

        var leftBefore = await GetPanelSize(panels.Nth(0));
        var rightBefore = await GetPanelSize(panels.Nth(1));

        await Assertions.Expect(grip).ToBeVisibleAsync();
        await Assertions.Expect(handle.Locator("div").First).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bh-4\b.*\bw-3\b.*\brounded-xs\b.*\bborder\b"));
        await Assertions.Expect(grip).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bsize-2\.5\b"));
        await Assertions.Expect(handle).ToHaveAttributeAsync("data-resizable-ready", "true");
        await Assertions.Expect(handle).ToHaveAttributeAsync("aria-valuenow", "50");
        await Assertions.Expect(handle).ToHaveAttributeAsync("tabindex", "0");

        await DragHandle(page, handle, 100, 0);

        await Assertions.Expect(handle).Not.ToHaveAttributeAsync("aria-valuenow", "50");
        var leftAfter = await GetPanelSize(panels.Nth(0));
        var rightAfter = await GetPanelSize(panels.Nth(1));
        (leftAfter > leftBefore).Should().BeTrue();
        (rightAfter < rightBefore).Should().BeTrue();
        await Assertions.Expect(handle).ToHaveAttributeAsync("data-resizable-dotnet", "ok");
        await Assertions.Expect(handle).Not.ToHaveAttributeAsync("data-resizable-last-percentage", "50");
        runtimeErrors.ConsoleErrors.Should().BeEmpty();
        runtimeErrors.PageErrors.Should().BeEmpty();
    }

    private static RuntimeErrors CaptureRuntimeErrors(IPage page)
    {
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, exception) => pageErrors.Add(exception);

        return new RuntimeErrors(consoleErrors, pageErrors);
    }

    private static async Task<double> GetPanelSize(ILocator panel)
    {
        var style = await panel.GetAttributeAsync("style");
        (style).Should().NotBeNull();

        const string marker = "flex: 0 0 ";
        var start = style!.IndexOf(marker, StringComparison.Ordinal);
        (start >= 0).Should().BeTrue();

        start += marker.Length;
        var end = style.IndexOf('%', start);
        (end > start).Should().BeTrue();

        var value = style[start..end];
        return double.Parse(value, CultureInfo.InvariantCulture);
    }

    private static async Task<double> WaitForPanelSizeCondition(ILocator panel, Func<double, bool> predicate, int timeoutMs = 5000)
    {
        var start = DateTime.UtcNow;
        var lastSize = await GetPanelSize(panel);

        while ((DateTime.UtcNow - start).TotalMilliseconds < timeoutMs)
        {
            lastSize = await GetPanelSize(panel);
            if (predicate(lastSize))
                return lastSize;

            await Task.Delay(50);
        }

        return lastSize;
    }

    private static async Task DragHandle(IPage page, ILocator handle, double deltaX, double deltaY)
    {
        await handle.ScrollIntoViewIfNeededAsync();
        await handle.EvaluateAsync(
            @"element => element.scrollIntoView({ block: 'center', inline: 'center', behavior: 'instant' })");

        var rect = await handle.BoundingBoxAsync();
        (rect).Should().NotBeNull();
        (float.IsFinite(rect!.X)).Should().BeTrue();
        (float.IsFinite(rect.Y)).Should().BeTrue();
        (float.IsFinite(rect.Width)).Should().BeTrue();
        (float.IsFinite(rect.Height)).Should().BeTrue();

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

        (dragStart).Should().NotBeNull();
        (double.IsFinite(dragStart!.StartX)).Should().BeTrue();
        (double.IsFinite(dragStart.StartY)).Should().BeTrue();

        var startX = dragStart.StartX;
        var startY = dragStart.StartY;

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

    private sealed record RuntimeErrors(List<string> ConsoleErrors, List<string> PageErrors);
}
