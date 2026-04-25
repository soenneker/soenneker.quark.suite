using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkAttachmentsPlaywrightTests : PlaywrightUnitTest
{
    public QuarkAttachmentsPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Attachments_render_ai_elements_slots_variants_and_progress()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        var consoleErrors = new List<string>();
        var pageErrors = new List<string>();
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };
        page.PageError += (_, error) => pageErrors.Add(error);

        await page.GotoAndWaitForReady($"{BaseUrl}attachments",
            static p => p.Locator("[data-slot='attachments']"),
            expectedTitle: "Attachments - Quark Suite");

        var attachments = page.Locator("[data-slot='attachments']");
        await Assertions.Expect(attachments).ToHaveAttributeAsync("data-variant", "list");
        await Assertions.Expect(page.Locator("[data-slot='attachment']")).ToHaveCountAsync(4);
        await Assertions.Expect(page.Locator("[data-slot='attachment-name']").Filter(new LocatorFilterOptions { HasText = "screenshot.png" })).ToBeVisibleAsync();

        var progress = page.Locator("[data-slot='attachment-progress']");
        await Assertions.Expect(progress).ToHaveAttributeAsync("role", "progressbar");
        await Assertions.Expect(progress).ToHaveAttributeAsync("aria-valuenow", "64");

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Remove", Exact = true }).First.FocusAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Remove", Exact = true }).First).ToBeFocusedAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
