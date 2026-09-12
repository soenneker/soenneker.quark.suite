using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkOverlayLifecyclePlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async Task Overlay_focus_wraps_current_boundaries_and_releases_owned_scroll_lock()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/dropdown-menu",
            static p => p.GetByRole(AriaRole.Button, new() { Name = "Open", Exact = true }).First,
            expectedTitle: "Dropdowns - Quark Suite");

        await page.EvaluateAsync("""
            async () => {
                const overlay = await import('/_content/Soenneker.Quark.Suite/js/overlayinterop.js');
                const container = document.createElement('div');
                container.id = 'audit-overlay';
                container.style.cssText = 'position:fixed;inset:10px;z-index:99999;background:white';
                container.innerHTML = '<button id="audit-first">First</button><button id="audit-hidden" style="display:none">Hidden</button><button id="audit-last">Last</button>';
                document.body.append(container);
                document.body.style.overflow = 'auto';
                document.body.style.paddingRight = '7px';
                overlay.activate('audit-modal', container, true, true);
                overlay.activate('audit-popover', null, false, false);
                overlay.deactivate('audit-popover', true);
            }
            """);
        await Assertions.Expect(page.Locator("#audit-first")).ToBeFocusedAsync();
        await page.Keyboard.PressAsync("Shift+Tab");
        await Assertions.Expect(page.Locator("#audit-last")).ToBeFocusedAsync();
        await page.Keyboard.PressAsync("Tab");
        await Assertions.Expect(page.Locator("#audit-first")).ToBeFocusedAsync();
        await Assert.That(await page.EvaluateAsync<string>("() => document.body.style.overflow")).IsEqualTo("hidden");

        await page.EvaluateAsync("""
            async () => {
                const overlay = await import('/_content/Soenneker.Quark.Suite/js/overlayinterop.js');
                overlay.deactivate('audit-modal', true);
                document.querySelector('#audit-overlay').remove();
                overlay.releaseScrollLocks();
            }
            """);
        await Assert.That(await page.EvaluateAsync<string>("() => document.body.style.overflow")).IsEqualTo("auto");
        await Assert.That(await page.EvaluateAsync<string>("() => document.body.style.paddingRight")).IsEqualTo("7px");
    }
}
