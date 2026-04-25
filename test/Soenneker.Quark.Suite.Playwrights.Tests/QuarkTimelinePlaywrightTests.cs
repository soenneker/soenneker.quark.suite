using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTimelinePlaywrightTests : PlaywrightUnitTest
{
    public QuarkTimelinePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Timeline_expandable_items_preserve_collapsible_state_across_parent_rerenders()
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
            $"{BaseUrl}timelines",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Re-render timeline", Exact = true }),
            expectedTitle: "Timeline - Quark Suite");

        var expandableSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Collapsible items let you keep the summary readable while preserving detailed implementation notes." }).First;
        var rerenderButton = page.Locator("#timeline-rerender");

        var shippedTrigger = expandableSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "v3.4.0 shipped Apr 5 Search relevance tuning and dashboard caching landed in the same release.", Exact = false });
        var shippedDetails = expandableSection.GetByText("Cache hit rates improved after introducing a tenant-aware key strategy.", new LocatorGetByTextOptions { Exact = false });
        var hotfixTrigger = expandableSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Hotfix under review Draft The patch narrows retry behavior for webhook failures without changing success-path throughput.", Exact = false });
        var hotfixDetails = expandableSection.GetByText("Proposed changes cap retries for 4xx responses", new LocatorGetByTextOptions { Exact = false });

        await Assertions.Expect(shippedTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(shippedDetails).ToBeVisibleAsync();
        await Assertions.Expect(hotfixTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(hotfixDetails).Not.ToBeVisibleAsync();

        await shippedTrigger.ClickAsync();
        await hotfixTrigger.ClickAsync();

        await Assertions.Expect(shippedTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(shippedDetails).Not.ToBeVisibleAsync();
        await Assertions.Expect(hotfixTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(hotfixDetails).ToBeVisibleAsync();

        await rerenderButton.ClickAsync();

        await Assertions.Expect(shippedTrigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(shippedDetails).Not.ToBeVisibleAsync();
        await Assertions.Expect(hotfixTrigger).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(hotfixDetails).ToBeVisibleAsync();
        await Assertions.Expect(expandableSection.GetByText("Parent render count: 1", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Timeline_demo_exposes_list_slots_status_and_current_step_semantics()
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
            $"{BaseUrl}timelines",
            static p => p.Locator("[data-slot='timeline']").First,
            expectedTitle: "Timeline - Quark Suite");

        var timeline = page.Locator("[data-slot='timeline']").First;
        var firstItem = timeline.Locator("[data-slot='timeline-item']").First;
        var currentItem = timeline.Locator("[aria-current='step']").First;
        var icon = timeline.Locator("[data-slot='timeline-icon']").First;
        var connector = timeline.Locator("[data-slot='timeline-connector']").First;
        var title = timeline.Locator("[data-slot='timeline-title']").First;
        var description = timeline.Locator("[data-slot='timeline-description']").First;
        var time = timeline.Locator("[data-slot='timeline-time']").First;

        await Assertions.Expect(timeline).ToHaveAttributeAsync("aria-label", "Timeline");
        await Assertions.Expect(timeline).ToHaveAttributeAsync("data-align", "center");
        await Assertions.Expect(timeline).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bflex-col\b"));
        await Assertions.Expect(firstItem).ToHaveAttributeAsync("data-status", "completed");
        await Assertions.Expect(currentItem).ToHaveAttributeAsync("data-status", "inprogress");
        await Assertions.Expect(currentItem).ToContainTextAsync("Background jobs warming");
        await Assertions.Expect(icon).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bring-8\b"));
        await Assertions.Expect(connector).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bw-0\.5\b"));
        await Assertions.Expect(title).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\bfont-semibold\b"));
        await Assertions.Expect(description).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\btext-muted-foreground\b"));
        await Assertions.Expect(time).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"\btext-muted-foreground\b"));

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
