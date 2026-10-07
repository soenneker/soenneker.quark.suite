using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkMobilePreviewPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkMobilePreviewPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    [Arguments("consent-manager", "[role=dialog]", 390)]
    [Arguments("prompt-inputs", "textarea", 390)]
    [Arguments("payment-card", "[data-slot=payment-card]", 390)]
    [Arguments("payment-card", "[data-slot=payment-card] .card-container", 375)]
    [Arguments("payment-card", "[data-slot=payment-card] .card-container", 320)]
    public async ValueTask Mobile_preview_keeps_component_inside_its_container(string route, string selector, int width)
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(width, 844);
        await page.GotoAndWaitForReady($"{BaseUrl}components/{route}", p => p.Locator(selector).First);
        var component = page.Locator(selector).First;
        await Assertions.Expect(component).ToBeVisibleAsync();
        var fits = await component.EvaluateAsync<bool>("""
            element => {
                const bounds = element.getBoundingClientRect();
                const preview = element.closest('.preview').getBoundingClientRect();
                return bounds.width > 0 && bounds.left >= preview.left && bounds.right <= preview.right;
            }
            """);
        fits.Should().BeTrue($"the {route} preview should not clip its component on mobile");
        if (route == "payment-card")
        {
            var card = page.Locator("[data-slot=payment-card] .card-container").First;
            await Assertions.Expect(card).ToHaveCSSAsync("aspect-ratio", "8 / 5");
            var bounds = (await card.BoundingBoxAsync())!;
            (bounds.Width / bounds.Height).Should().BeApproximately(1.6f, 0.02f);
        }
    }
}
