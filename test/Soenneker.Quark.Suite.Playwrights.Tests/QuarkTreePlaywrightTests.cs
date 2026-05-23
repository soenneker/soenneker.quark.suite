using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTreePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkTreePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Tree_demo_exposes_reui_slots_and_treeitem_state()
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

        await page.GotoAndWaitForReady($"{BaseUrl}components/tree", static p => p.GetByRole(AriaRole.Tree).First,
            expectedTitle: "Tree - Quark Suite");

        var tree = page.GetByRole(AriaRole.Tree, new PageGetByRoleOptions { Name = "CRM navigation", Exact = true });
        var crm = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "CRM", Exact = true });
        var leads = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Leads", Exact = true });
        var qualified = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Qualified Lead", Exact = true });

        await Assertions.Expect(tree).ToHaveAttributeAsync("data-slot", "tree");
        await Assertions.Expect(crm).ToHaveAttributeAsync("data-slot", "tree-item");
        await Assertions.Expect(crm).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(qualified).ToHaveAttributeAsync("data-selected", "true");

        await leads.PressAsync("ArrowLeft");

        await Assertions.Expect(leads).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(qualified).Not.ToBeVisibleAsync();
        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Tree_basic_demo_toggles_and_selects_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}components/tree", static p => p.GetByRole(AriaRole.Tree).First,
            expectedTitle: "Tree - Quark Suite");

        var tree = page.GetByRole(AriaRole.Tree, new PageGetByRoleOptions { Name = "CRM navigation", Exact = true });
        var support = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Support", Exact = true });
        var openTickets = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Open Tickets", Exact = true });

        await Assertions.Expect(support).ToHaveAttributeAsync("aria-expanded", "false");

        await support.ClickAsync();

        await Assertions.Expect(support).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(support).ToHaveAttributeAsync("data-selected", "true");
        await Assertions.Expect(openTickets).ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Tree_file_explorer_renders_composed_labels()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}components/tree", static p => p.GetByText("File Explorer", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Tree - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Compose icons and custom labels for file-system navigation." }).First;
        var tree = section.GetByRole(AriaRole.Tree, new LocatorGetByRoleOptions { Name = "File explorer", Exact = true });
        var button = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "button.tsx", Exact = true });
        var packageJson = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "package.json", Exact = true });

        await Assertions.Expect(button).ToBeVisibleAsync();
        await Assertions.Expect(packageJson).ToBeVisibleAsync();

        await packageJson.ClickAsync();

        await Assertions.Expect(packageJson).ToHaveAttributeAsync("data-selected", "true");
    }

    [Test]
    public async ValueTask Tree_permissions_demo_updates_checkbox_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}components/tree", static p => p.GetByText("Permissions", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Tree - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use tree labels with checkboxes for nested permission editors." }).First;
        var writeContacts = section.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Create and edit contacts", Exact = true });
        var checkbox = writeContacts.GetByRole(AriaRole.Checkbox).First;

        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "false");

        await checkbox.ClickAsync();

        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(writeContacts).ToHaveAttributeAsync("data-selected", "true");
    }
}
