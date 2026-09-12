using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkColorPickerPointerPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async Task Drag_commits_the_final_color_and_keeps_the_thumb_aligned()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/color-picker",
            static p => p.Locator("[data-slot='color-picker-canvas']").First,
            expectedTitle: "Color Picker - Quark Suite");

        var canvas = page.Locator("[data-slot='color-picker-canvas']").First;
        await canvas.ScrollIntoViewIfNeededAsync();
        var bounds = await canvas.BoundingBoxAsync();
        await Assert.That(bounds).IsNotNull();
        await page.Mouse.MoveAsync(bounds!.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(bounds.X + bounds.Width * 0.75f, bounds.Y + bounds.Height * 0.25f,
            new MouseMoveOptions { Steps = 20 });
        await page.Mouse.UpAsync();

        await Assertions.Expect(page.Locator("[data-slot='field-description']").First)
            .Not.ToContainTextAsync("#8b5cf6");
        await page.WaitForFunctionAsync("""
            () => {
                const canvas = document.querySelector('[data-slot="color-picker-canvas"]');
                const thumb = canvas.querySelector('[data-slot="color-picker-thumb"]');
                const bounds = canvas.getBoundingClientRect();
                const marker = thumb.getBoundingClientRect();
                return Math.abs(marker.left + marker.width / 2 - bounds.left - bounds.width * .75) < 2 &&
                    Math.abs(marker.top + marker.height / 2 - bounds.top - bounds.height * .25) < 2;
            }
            """);

        var whiteCell = canvas.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Saturation 0, lightness 100", Exact = true });
        await whiteCell.FocusAsync();
        await Assertions.Expect(whiteCell).ToHaveCSSAsync("opacity", "1");
        await whiteCell.PressAsync("Enter");
        await Assertions.Expect(page.Locator("[data-slot='field-description']").First).ToContainTextAsync("#ffffff");
    }
}
