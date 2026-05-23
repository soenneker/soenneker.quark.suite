using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTimelinePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkTimelinePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Timeline_demo_exposes_reui_slots_orientation_and_active_step_semantics()
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

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/timelines",
            static p => p.Locator("[data-slot='timeline']").First,
            expectedTitle: "Timeline - Quark Suite");

        var timeline = page.Locator("[data-slot='timeline']").First;
        var firstItem = timeline.Locator("[data-slot='timeline-item']").First;
        var currentItem = timeline.Locator("[aria-current='step']").First;
        var indicator = timeline.Locator("[data-slot='timeline-indicator']").First;
        var separator = timeline.Locator("[data-slot='timeline-separator']").First;
        var title = timeline.Locator("[data-slot='timeline-title']").First;
        var content = timeline.Locator("[data-slot='timeline-content']").First;
        var date = timeline.Locator("[data-slot='timeline-date']").First;

        await Assertions.Expect(timeline).ToHaveAttributeAsync("aria-label", "Timeline");
        await Assertions.Expect(timeline).ToHaveAttributeAsync("data-orientation", "vertical");
        await Assertions.Expect(timeline).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"group/timeline"));
        await Assertions.Expect(firstItem).ToHaveAttributeAsync("data-completed", string.Empty);
        await Assertions.Expect(currentItem).ToContainTextAsync("Beta Release");
        await Assertions.Expect(indicator).ToHaveAttributeAsync("aria-hidden", "true");
        await Assertions.Expect(separator).ToHaveAttributeAsync("aria-hidden", "true");
        await Assertions.Expect(title).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bfont-medium\b"));
        await Assertions.Expect(content).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\btext-muted-foreground\b"));
        await Assertions.Expect(date).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\btext-xs\b"));

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
