using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkSortablePlaywrightTests : PlaywrightUnitTest
{
    public QuarkSortablePlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Sortable_examples_reorder_basic_and_handle_only_lists_while_disabled_list_stays_inert()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sortables",
            static p => p.Locator("#sortable-basic-demo"),
            expectedTitle: "Sortable - Quark Suite");

        ILocator basicDemo = page.Locator("#sortable-basic-demo");
        ILocator basicList = basicDemo.Locator("#sortable-basic-list");
        ILocator basicOrder = basicDemo.Locator("#sortable-basic-order");
        await WaitForSortableReadyAsync(page, basicList);

        await Assertions.Expect(basicOrder).ToContainTextAsync("Usage analytics dashboard -> Billing export retry policy -> Empty state polish -> Audit log filters");

        ILocator auditLog = basicList.Locator("[data-sortable-id='audit-log']").First;
        ILocator usageAnalytics = basicList.Locator("[data-sortable-id='usage-analytics']").First;

        await DragSortableItemToTargetAsync(page, auditLog, usageAnalytics);

        await Assertions.Expect(basicOrder).ToContainTextAsync("Audit log filters -> Usage analytics dashboard -> Billing export retry policy -> Empty state polish");
        Assert.Equal(["audit-log", "usage-analytics", "billing-export", "empty-state"], await GetSortableItemOrderAsync(basicList));

        ILocator handleDemo = page.Locator("#sortable-handle-demo");
        ILocator handleList = handleDemo.Locator("#sortable-handle-list");
        ILocator handleState = handleDemo.Locator("#sortable-handle-state");
        await WaitForSortableReadyAsync(page, handleList);

        await Assertions.Expect(handleState).ToContainTextAsync("No changes yet.");

        ILocator announceRow = handleList.Locator("[data-sortable-id='announce']").First;
        ILocator promoteRow = handleList.Locator("[data-sortable-id='promote']").First;

        await DragSortableItemToTargetAsync(page, announceRow, promoteRow);
        await Assertions.Expect(handleState).ToContainTextAsync("No changes yet.");
        Assert.Equal(["build", "smoke", "promote", "announce"], await GetSortableItemOrderAsync(handleList));

        ILocator announceHandle = announceRow.Locator("[data-slot='sortable-handle']").First;
        await DragSortableItemToTargetAsync(page, announceHandle, promoteRow);

        await Assertions.Expect(handleState).ToContainTextAsync("announce moved from 4 to 3.");
        Assert.Equal(["build", "smoke", "announce", "promote"], await GetSortableItemOrderAsync(handleList));

        ILocator disabledDemo = page.Locator("#sortable-disabled-demo");
        ILocator disabledList = disabledDemo.Locator("#sortable-disabled-list");
        await WaitForSortableReadyAsync(page, disabledList);

        await Assertions.Expect(disabledList).ToHaveAttributeAsync("aria-disabled", "true");
        Assert.Equal(["contract", "dns", "handoff"], await GetSortableItemOrderAsync(disabledList));

        ILocator handoff = disabledList.Locator("[data-sortable-id='handoff']").First;
        ILocator contract = disabledList.Locator("[data-sortable-id='contract']").First;

        await DragSortableItemToTargetAsync(page, handoff, contract);

        Assert.Equal(["contract", "dns", "handoff"], await GetSortableItemOrderAsync(disabledList));
    }
    private static async Task DragSortableItemToTargetAsync(IPage page, ILocator source, ILocator target)
    {
        await source.ScrollIntoViewIfNeededAsync();
        await target.ScrollIntoViewIfNeededAsync();

        DomRect? sourceRect = await source.EvaluateAsync<DomRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");

        DomRect? targetRect = await target.EvaluateAsync<DomRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");

        Assert.NotNull(sourceRect);
        Assert.NotNull(targetRect);

        IElementHandle? sourceHandle = await source.ElementHandleAsync();
        IElementHandle? targetHandle = await target.ElementHandleAsync();

        Assert.NotNull(sourceHandle);
        Assert.NotNull(targetHandle);

        await page.EvaluateAsync(
            @"async ({ sourceElement, targetElement }) => {
                if (!sourceElement || !targetElement) {
                    throw new Error('Sortable pointer drag elements were not found.');
                }

                const sourceRect = sourceElement.getBoundingClientRect();
                const targetRect = targetElement.getBoundingClientRect();

                const startX = sourceRect.left + (sourceRect.width / 2);
                const startY = sourceRect.top + (sourceRect.height / 2);
                const targetX = targetRect.left + (targetRect.width / 2);
                const targetY = targetRect.top + 8;
                const pointerId = 1;

                const dispatchPointer = (eventTarget, type, clientX, clientY, buttons) => {
                    eventTarget.dispatchEvent(new PointerEvent(type, {
                        bubbles: true,
                        cancelable: true,
                        composed: true,
                        pointerId,
                        pointerType: 'mouse',
                        isPrimary: true,
                        button: buttons === 0 ? 0 : 0,
                        buttons,
                        clientX,
                        clientY
                    }));
                };

                dispatchPointer(sourceElement, 'pointerdown', startX, startY, 1);

                const steps = 24;
                for (let index = 1; index <= steps; index++) {
                    const progress = index / steps;
                    const x = startX + ((targetX - startX) * progress);
                    const y = startY + ((targetY - startY) * progress);
                    dispatchPointer(document, 'pointermove', x, y, 1);
                    await new Promise(resolve => setTimeout(resolve, 8));
                }

                dispatchPointer(document, 'pointerup', targetX, targetY, 0);
            }",
            new
            {
                sourceElement = sourceHandle,
                targetElement = targetHandle
            });
    }

    private static async Task<string[]> GetSortableItemOrderAsync(ILocator list)
    {
        return await list.Locator("[data-sortable-id]").EvaluateAllAsync<string[]>(
            "elements => elements.map(element => element.getAttribute('data-sortable-id') ?? '')");
    }

    private static async Task WaitForSortableReadyAsync(IPage page, ILocator list)
    {
        await page.WaitForFunctionAsync(
            @"element => {
                if (!element) {
                    return false;
                }

                return !!element.__quarkSortable && element.__quarkSortable.option('forceFallback') === true;
            }",
            await list.ElementHandleAsync(),
            new PageWaitForFunctionOptions
            {
                Timeout = 5000
            });

        string? initializationError = await list.GetAttributeAsync("data-sortable-init-error");
        Assert.Null(initializationError);
    }

    private sealed class DomRect
    {
        public required double X { get; set; }
        public required double Y { get; set; }
        public required double Width { get; set; }
        public required double Height { get; set; }
    }
}
