using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSonnerPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async Task Batched_measurement_preserves_natural_toast_heights_and_inline_styles()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/sonner",
            static p => p.GetByRole(AriaRole.Button, new() { Name = "Default", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");
        await page.GetByRole(AriaRole.Button, new() { Name = "Show Toast", Exact = true }).First.ClickAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Default", Exact = true }).ClickAsync();
        await Assertions.Expect(page.Locator("[data-sonner-toast][data-mounted='true']")).ToHaveCountAsync(2);

        bool equivalent = await page.EvaluateAsync<bool>("""
            async () => {
                const { measureToastHeights } = await import('/_content/Soenneker.Quark.Suite/js/sonnerinterop.js');
                const section = document.querySelector('[data-sonner-toaster]').closest('section');
                const toasts = [...section.querySelectorAll('[data-sonner-toast]')];
                const originalStyles = toasts.map(toast => toast.getAttribute('style'));
                const reference = toasts.map(toast => {
                    const height = toast.style.height;
                    toast.style.height = 'auto';
                    const measured = toast.getBoundingClientRect().height;
                    toast.style.height = height;
                    return measured;
                });
                const measured = measureToastHeights(section);
                return toasts.every((toast, index) => reference[index] > 0 &&
                    Math.abs(reference[index] - measured[toast.dataset.toastId]) < 0.01 &&
                    toast.getAttribute('style') === originalStyles[index]);
            }
            """);
        await Assert.That(equivalent).IsTrue();
    }

    [Test]
    public async Task Toast_swipe_tracks_pointer_and_dismisses_after_release()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/sonner",
            static p => p.GetByRole(AriaRole.Button, new() { Name = "Default", Exact = true }),
            expectedTitle: "Sonner - Quark Suite");
        await page.GetByRole(AriaRole.Button, new() { Name = "Default", Exact = true }).ClickAsync();
        var toast = page.Locator("[data-sonner-toast]");
        await Assertions.Expect(toast).ToHaveAttributeAsync("data-mounted", "true");
        await toast.HoverAsync();
        var bounds = await toast.BoundingBoxAsync();
        await Assert.That(bounds).IsNotNull();
        float x = bounds!.X + bounds.Width / 2;
        float y = bounds.Y + bounds.Height / 2;
        await page.Mouse.MoveAsync(x, y);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(x + 120, y, new() { Steps = 12 });
        await Assertions.Expect(toast).ToHaveAttributeAsync("data-swiping", "true");
        await page.WaitForFunctionAsync("() => parseFloat(document.querySelector('[data-sonner-toast]').style.getPropertyValue('--swipe-amount')) >= 100");
        await page.Mouse.UpAsync();
        await Assertions.Expect(toast).ToHaveCountAsync(0);
    }
}
