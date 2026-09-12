using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkResizablePointerPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    [Arguments("resizable-basic-demo", false, false)]
    [Arguments("resizable-vertical-demo", true, false)]
    [Arguments("resizable-rtl-demo", false, true)]
    public async Task Drag_updates_panel_geometry_and_keyboard_resizing_still_works(string demoId, bool vertical, bool rtl)
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/resizable",
            static p => p.Locator("[data-resizable-ready='true']").First,
            expectedTitle: "Resizable - Quark Suite");

        var group = page.Locator($"#{demoId} [data-slot='resizable-panel-group']").First;
        var handle = group.Locator(":scope > [data-slot='resizable-handle']").First;
        await handle.ScrollIntoViewIfNeededAsync();
        await Assertions.Expect(handle).ToHaveAttributeAsync("data-resizable-ready", "true");
        // Wait for the demo layout to settle before using viewport coordinates.
        await handle.HoverAsync(new LocatorHoverOptions { Position = new Position { X = 0, Y = 0 } });
        var bounds = await group.BoundingBoxAsync();
        var grip = await handle.BoundingBoxAsync();
        await Assert.That(bounds).IsNotNull();
        await Assert.That(grip).IsNotNull();
        var targetX = vertical ? grip!.X + grip.Width / 2 : bounds!.X + bounds.Width * (rtl ? 0.25f : 0.75f);
        // The horizontal handle's midpoint intersects a nested vertical group's handle.
        var startY = grip!.Y + grip.Height * (vertical ? 0.5f : 0.25f);
        var targetY = vertical ? bounds!.Y + bounds.Height * 0.75f : startY;
        await page.Mouse.MoveAsync(grip!.X + grip.Width / 2, startY);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(targetX, targetY, new MouseMoveOptions { Steps = 20 });
        await page.Mouse.UpAsync();
        await Assertions.Expect(handle).ToHaveAttributeAsync("aria-valuenow", "75");

        var panel = await group.Locator(":scope > [data-slot='resizable-panel']").First.BoundingBoxAsync();
        var actual = vertical ? panel!.Height / bounds!.Height : panel!.Width / bounds!.Width;
        await Assert.That(actual).IsBetween(0.73f, 0.77f);
        await handle.FocusAsync();
        await handle.PressAsync(vertical ? "ArrowDown" : rtl ? "ArrowLeft" : "ArrowRight");
        await Assertions.Expect(handle).ToHaveAttributeAsync("aria-valuenow", "80");
    }
}
