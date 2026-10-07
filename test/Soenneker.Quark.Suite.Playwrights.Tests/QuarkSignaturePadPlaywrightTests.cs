using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSignaturePadPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkSignaturePadPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask SignaturePad_waits_for_initialization_then_draws_exports_and_clears()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var errors = new List<string>();
        page.Console += (_, message) => { if (message.Type == "error") errors.Add(message.Text); };
        page.PageError += (_, message) => errors.Add(message);
        await page.RouteAsync("**/_content/Soenneker.Blazor.SignaturePads/**", async route =>
        {
            await Task.Delay(200);
            await route.ContinueAsync();
        });
        await page.GotoAndWaitForReady($"{BaseUrl}components/signature-pad",
            static p => p.GetByText("Ready for a signature.", new() { Exact = true }));
        var canvas = page.Locator("canvas");
        var bounds = (await canvas.BoundingBoxAsync())!;
        await page.Mouse.MoveAsync(bounds.X + 20, bounds.Y + 40);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(bounds.X + 100, bounds.Y + 80, new() { Steps = 8 });
        await page.Mouse.UpAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Export SVG", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Exported an SVG containing", new() { Exact = false })).ToBeVisibleAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Clear", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Signature cleared.", new() { Exact = true })).ToBeVisibleAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Export SVG", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Add a signature before exporting.", new() { Exact = true })).ToBeVisibleAsync();
        errors.Should().BeEmpty();
    }
}
