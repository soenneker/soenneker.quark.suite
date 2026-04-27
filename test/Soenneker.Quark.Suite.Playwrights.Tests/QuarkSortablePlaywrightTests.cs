using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSortablePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkSortablePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Sortable_examples_reorder_basic_and_handle_only_lists_while_disabled_list_stays_inert()
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
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}sortables",
            static p => p.Locator("#sortable-basic-demo"),
            expectedTitle: "Sortable - Quark Suite");

        var basicDemo = page.Locator("#sortable-basic-demo");
        var basicList = basicDemo.Locator("#sortable-basic-list");
        var basicOrder = basicDemo.Locator("#sortable-basic-order");
        await WaitForSortableReadyAsync(page, basicList);

        await Assertions.Expect(basicList).ToHaveAttributeAsync("role", "list");
        await Assertions.Expect(basicOrder).ToContainTextAsync("Usage analytics dashboard -> Billing export retry policy -> Empty state polish -> Audit log filters");

        var auditLog = basicList.Locator("[data-sortable-id='audit-log']").First;
        var usageAnalytics = basicList.Locator("[data-sortable-id='usage-analytics']").First;

        await DragSortableItemToTargetAsync(page, auditLog, usageAnalytics);
        if ((await GetSortableItemOrderAsync(basicList))[0] != "audit-log")
            await DragSortableItemToTargetAsync(page, auditLog, usageAnalytics);

        await Assertions.Expect(basicOrder).ToContainTextAsync("Audit log filters -> Usage analytics dashboard -> Billing export retry policy -> Empty state polish");
        (await GetSortableItemOrderAsync(basicList)).Should().Equal(["audit-log", "usage-analytics", "billing-export", "empty-state"]);

        var handleDemo = page.Locator("#sortable-handle-demo");
        var handleList = handleDemo.Locator("#sortable-handle-list");
        var handleState = handleDemo.Locator("#sortable-handle-state");
        await WaitForSortableReadyAsync(page, handleList);

        await Assertions.Expect(handleState).ToContainTextAsync("No changes yet.");
        await Assertions.Expect(handleList).ToHaveAttributeAsync("role", "list");

        var announceRow = handleList.Locator("[data-sortable-id='announce']").First;
        var promoteRow = handleList.Locator("[data-sortable-id='promote']").First;

        await DragSortableItemToTargetAsync(page, announceRow, promoteRow);
        await Assertions.Expect(handleState).ToContainTextAsync("No changes yet.");
        (await GetSortableItemOrderAsync(handleList)).Should().Equal(["build", "smoke", "promote", "announce"]);

        var announceHandle = announceRow.Locator("[data-slot='sortable-handle']").First;
        await Assertions.Expect(announceHandle).ToHaveAttributeAsync("type", "button");
        await Assertions.Expect(announceHandle).ToHaveAttributeAsync("aria-label", "Drag handle");
        await announceHandle.FocusAsync();
        await Assertions.Expect(announceHandle).ToBeFocusedAsync();
        await DragSortableItemToTargetAsync(page, announceHandle, promoteRow);

        await Assertions.Expect(handleState).ToContainTextAsync("announce moved from 4 to 3.");
        (await GetSortableItemOrderAsync(handleList)).Should().Equal(["build", "smoke", "announce", "promote"]);

        var keyboardHandle = handleList.Locator("[data-sortable-id='announce'] [data-slot='sortable-handle']").First;
        await keyboardHandle.FocusAsync();
        await keyboardHandle.PressAsync("ArrowDown");

        await Assertions.Expect(handleState).ToContainTextAsync("announce moved from 3 to 4.");
        await Assertions.Expect(page.Locator("[data-slot='sortable-live-region']").Last).ToContainTextAsync("announce moved from position 3 to 4.");
        (await GetSortableItemOrderAsync(handleList)).Should().Equal(["build", "smoke", "promote", "announce"]);

        var disabledDemo = page.Locator("#sortable-disabled-demo");
        var disabledList = disabledDemo.Locator("#sortable-disabled-list");
        await WaitForSortableReadyAsync(page, disabledList);

        await Assertions.Expect(disabledList).ToHaveAttributeAsync("aria-disabled", "true");
        (await GetSortableItemOrderAsync(disabledList)).Should().Equal(["contract", "dns", "handoff"]);
        await Assertions.Expect(disabledList.Locator("[data-sortable-id='contract']").First).ToHaveAttributeAsync("role", "listitem");

        var handoff = disabledList.Locator("[data-sortable-id='handoff']").First;
        var contract = disabledList.Locator("[data-sortable-id='contract']").First;

        await DragSortableItemToTargetAsync(page, handoff, contract);

        (await GetSortableItemOrderAsync(disabledList)).Should().Equal(["contract", "dns", "handoff"]);
        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
    private static async Task DragSortableItemToTargetAsync(IPage page, ILocator source, ILocator target)
    {
        await source.ScrollIntoViewIfNeededAsync();
        await target.ScrollIntoViewIfNeededAsync();

        var sourceRect = await source.EvaluateAsync<DomRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");

        var targetRect = await target.EvaluateAsync<DomRect>(
            @"element => {
                const rect = element.getBoundingClientRect();
                return { x: rect.x, y: rect.y, width: rect.width, height: rect.height };
            }");

        (sourceRect).Should().NotBeNull();
        (targetRect).Should().NotBeNull();

        var sourceHandle = await source.ElementHandleAsync();
        var targetHandle = await target.ElementHandleAsync();

        (sourceHandle).Should().NotBeNull();
        (targetHandle).Should().NotBeNull();

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
                const targetY = targetRect.top - 4;
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

                dispatchPointer(targetElement, 'pointermove', targetX, targetY, 1);
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

        var initializationError = await list.GetAttributeAsync("data-sortable-init-error");
        (initializationError).Should().BeNull();
    }

    private sealed class DomRect
    {
        public required double X { get; set; }
        public required double Y { get; set; }
        public required double Width { get; set; }
        public required double Height { get; set; }
    }
}
