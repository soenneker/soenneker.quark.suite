using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkNodeEditorGeometryPlaywrightTests(QuarkPlaywrightHost host) : QuarkPlaywrightTest(host)
{
    [Test]
    public async Task Dragged_node_keeps_connected_edge_endpoints_aligned()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/node-editor",
            static p => p.Locator("[data-slot='node-editor-node']").First,
            expectedTitle: "Node Editor - Quark Suite");

        var root = page.Locator("[data-slot='node-editor']").First;
        var edge = root.Locator("[data-edge-id]").First;
        var path = edge.Locator("[data-edge-path]");
        await Assertions.Expect(path).ToHaveAttributeAsync("d", new System.Text.RegularExpressions.Regex("^M"));
        string? previousPath = await path.GetAttributeAsync("d");
        string? sourceId = await edge.GetAttributeAsync("data-source-node");
        var node = root.Locator($"[data-slot='node-editor-node'][data-node-id='{sourceId}']");
        await node.ScrollIntoViewIfNeededAsync();
        var bounds = await node.BoundingBoxAsync();
        await Assert.That(bounds).IsNotNull();
        await page.Mouse.MoveAsync(bounds!.X + 40, bounds.Y + 20);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(bounds.X + 80, bounds.Y + 60, new MouseMoveOptions { Steps = 10 });
        await page.Mouse.UpAsync();
        await Assertions.Expect(path).Not.ToHaveAttributeAsync("d", previousPath!);

        // Convert SVG endpoints to viewport coordinates and compare with the rendered ports.
        await page.WaitForFunctionAsync("""
            () => [...document.querySelector('[data-slot="node-editor"]').querySelectorAll('[data-edge-id]')].every(edge => {
                const path = edge.querySelector('[data-edge-path]');
                if (!path?.getAttribute('d')) return false;
                return ['source', 'target'].every((end, index) => {
                    const nodeId = edge.dataset[end + 'Node'];
                    const portId = edge.dataset[end + 'Port'];
                    const port = [...edge.closest('[data-slot="node-editor"]').querySelectorAll('[data-slot="node-editor-port"]')]
                        .find(port => port.dataset.nodeId === nodeId && port.dataset.portId === portId);
                    if (!port) return false;
                    const rect = port.getBoundingClientRect();
                    const point = path.getPointAtLength(index ? path.getTotalLength() : 0).matrixTransform(path.getScreenCTM());
                    return Math.abs(point.x - rect.left - rect.width / 2) < 1 && Math.abs(point.y - rect.top - rect.height / 2) < 1;
                });
            })
            """);
    }
}
