using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using System.Threading;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkQuestionnairePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkQuestionnairePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Questionnaire_keeps_native_selection_in_sync_with_answers(CancellationToken cancellationToken)
    {
        await using var session = await CreateSession(cancellationToken: cancellationToken);
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/questionnaire",
            static p => p.GetByRole(AriaRole.Radio, new() { Name = "Both together", Exact = true }));

        var both = page.GetByRole(AriaRole.Radio, new() { Name = "Both together", Exact = true });
        await both.ClickAsync();
        await Assertions.Expect(both).ToBeCheckedAsync();
        await Assertions.Expect(both.Locator("..")).ToHaveAttributeAsync("data-checked", "");
        await page.GetByRole(AriaRole.Button, new() { Name = "Next", Exact = true }).ClickAsync();

        var focused = page.GetByRole(AriaRole.Radio, new() { Name = "Focused", Exact = true });
        await focused.FocusAsync();
        await focused.PressAsync("Space");
        await Assertions.Expect(focused).ToBeCheckedAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Previous", Exact = true }).ClickAsync();
        await Assertions.Expect(both).ToBeCheckedAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Next", Exact = true }).ClickAsync();
        await Assertions.Expect(focused).ToBeCheckedAsync();
        await page.GetByRole(AriaRole.Button, new() { Name = "Share answers", Exact = true }).ClickAsync();
        await Assertions.Expect(page.GetByText("Submitted: direction: both, detail: focused", new() { Exact = true })).ToBeVisibleAsync();
    }
}
