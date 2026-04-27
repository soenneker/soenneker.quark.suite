using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkEmptyPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkEmptyPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Empty_demo_renders_shadcn_composition_and_embedded_controls()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}empties",
            static p => p.GetByText("No Projects Yet", new PageGetByTextOptions { Exact = true }).First,
            expectedTitle: "Empty - Quark Suite");

        var demo = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "No Projects Yet" }).First;
        var empty = demo.Locator("[data-slot='empty']").First;
        var header = demo.Locator("[data-slot='empty-header']").First;
        var media = demo.Locator("[data-slot='empty-icon']").First;
        var title = demo.Locator("[data-slot='empty-title']").First;
        var description = demo.Locator("[data-slot='empty-description']").First;
        var content = demo.Locator("[data-slot='empty-content']").First;
        var action = demo.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Create Project", Exact = true });

        await Assertions.Expect(empty).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)gap-4(\s|$)"));
        await Assertions.Expect(empty).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)rounded-xl(\s|$)"));
        await Assertions.Expect(empty).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)p-6(\s|$)"));
        await Assertions.Expect(header).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)max-w-sm(\s|$)"));
        await Assertions.Expect(media).ToHaveAttributeAsync("data-variant", "icon");
        await Assertions.Expect(media).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)size-8(\s|$)"));
        await Assertions.Expect(title).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)text-sm(\s|$)"));
        await Assertions.Expect(description).ToHaveTextAsync("You haven't created any projects yet. Get started by creating your first project.");
        await Assertions.Expect(content).ToHaveClassAsync(new System.Text.RegularExpressions.Regex(@"(^|\s)gap-2(\s|$)"));

        await action.FocusAsync();
        await Assertions.Expect(action).ToBeFocusedAsync();

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Empty states also support right-to-left copy" }).First;
        await Assertions.Expect(rtlSection.GetByText("لا توجد مشاريع بعد", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(rtlSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "إنشاء مشروع", Exact = true })).ToBeVisibleAsync();

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }
}
