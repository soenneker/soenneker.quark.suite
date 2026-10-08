using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using System.Threading;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCascaderPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCascaderPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Cascader_renders_named_options_and_selects_a_nested_location(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/cascader",
            static p => p.GetByRole(AriaRole.Button, new() { Name = "Please select", Exact = true }).First);
        var trigger = page.GetByRole(AriaRole.Button, new() { Name = "Please select", Exact = true }).First;
        await trigger.ClickAsync();
        await page.GetByRole(AriaRole.Option, new() { Name = "USA", Exact = true }).ClickAsync();
        await page.GetByRole(AriaRole.Option, new() { Name = "New York", Exact = true }).ClickAsync();
        await page.GetByRole(AriaRole.Option, new() { Name = "Statue of Liberty", Exact = true }).ClickAsync();
        await Assertions.Expect(trigger).ToContainTextAsync("USA / New York / Statue of Liberty");
        await Assertions.Expect(page.GetByText("Selected: usa / new_york / statue_of_liberty", new() { Exact = true })).ToBeVisibleAsync();
    }
}
