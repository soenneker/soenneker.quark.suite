using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTagInputPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkTagInputPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask TagInput_label_focus_and_keyboard_add_remove_work()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady($"{BaseUrl}components/tag-input",
            static p => p.GetByRole(AriaRole.Textbox, new() { Name = "Topics", Exact = true }));
        var input = page.GetByRole(AriaRole.Textbox, new() { Name = "Topics", Exact = true });
        await page.GetByText("Topics", new() { Exact = true }).ClickAsync();
        await Assertions.Expect(input).ToBeFocusedAsync();
        await input.FillAsync("Quality");
        await input.PressAsync("Enter");
        var remove = page.GetByRole(AriaRole.Button, new() { Name = "Remove Quality", Exact = true });
        await Assertions.Expect(remove).ToBeVisibleAsync();
        await remove.ClickAsync();
        await Assertions.Expect(remove).ToHaveCountAsync(0);
    }
}
