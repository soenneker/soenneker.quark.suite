using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkTreeViewPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkTreeViewPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Treeview_async_demo_loads_children_on_expand_and_updates_selection()
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

        await page.GotoAndWaitForReady($"{BaseUrl}treeviews", static p => p.Locator("#treeview-async-demo"), expectedTitle: "TreeView - Quark Suite");

        var asyncSection = page.Locator("#treeview-async-demo");
        var server1 = asyncSection.GetByText("Server 1", new LocatorGetByTextOptions { Exact = true });
        var api = asyncSection.GetByText("API", new LocatorGetByTextOptions { Exact = true });
        var routes = asyncSection.GetByText("Routes", new LocatorGetByTextOptions { Exact = true });

        await Assertions.Expect(asyncSection).ToContainTextAsync("Selected node: None");
        await Assertions.Expect(api).ToHaveCountAsync(0);

        await server1.ClickAsync();

        await Assertions.Expect(api).ToBeVisibleAsync();
        await Assertions.Expect(asyncSection).ToContainTextAsync("Selected node: Server 1");

        await api.ClickAsync();

        await Assertions.Expect(routes).ToBeVisibleAsync();
        await Assertions.Expect(asyncSection).ToContainTextAsync("Selected node: API");
        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Treeview_exposes_treeitem_state_and_keyboard_expand_collapse()
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

        await page.GotoAndWaitForReady($"{BaseUrl}treeviews", static p => p.GetByRole(AriaRole.Tree).First,
            expectedTitle: "TreeView - Quark Suite");

        var tree = page.GetByRole(AriaRole.Tree).First;
        var documents = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Documents", Exact = true });
        var work = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Work", Exact = true });
        var personal = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Personal", Exact = true });
        var applications = tree.GetByRole(AriaRole.Treeitem, new LocatorGetByRoleOptions { Name = "Applications", Exact = true });

        await Assertions.Expect(tree).ToHaveAttributeAsync("aria-label", "Tree view");
        await Assertions.Expect(documents).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(documents).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(documents).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(applications).ToHaveAttributeAsync("tabindex", "-1");

        await documents.PressAsync("ArrowRight");

        await Assertions.Expect(documents).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(work).ToBeVisibleAsync();
        await Assertions.Expect(work).ToHaveAttributeAsync("tabindex", "-1");
        await Assertions.Expect(personal).ToHaveAttributeAsync("tabindex", "-1");

        await documents.PressAsync("ArrowDown");

        await Assertions.Expect(work).ToBeFocusedAsync();
        await Assertions.Expect(work).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(documents).ToHaveAttributeAsync("tabindex", "-1");

        await work.PressAsync("p");

        await Assertions.Expect(personal).ToBeFocusedAsync();
        await Assertions.Expect(personal).ToHaveAttributeAsync("tabindex", "0");

        await page.WaitForTimeoutAsync(1100);
        await personal.PressAsync("w");

        await Assertions.Expect(work).ToBeFocusedAsync();

        await work.PressAsync("ArrowLeft");

        await Assertions.Expect(documents).ToBeFocusedAsync();

        await documents.PressAsync("ArrowLeft");

        await Assertions.Expect(documents).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(work).Not.ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Treeview_single_selection_expands_parent_and_updates_selected_summary()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}treeviews", static p => p.GetByText("Basic TreeView", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "TreeView - Quark Suite");

        var singleSection = page.Locator("div").Filter(new LocatorFilterOptions { HasText = "TreeView with single selection mode. Selected node:" }).First;
        var fileSystem = singleSection.GetByText("File System", new LocatorGetByTextOptions { Exact = true });
        var cDrive = singleSection.GetByText("C:\\", new LocatorGetByTextOptions { Exact = true });
        var programFiles = singleSection.GetByText("Program Files", new LocatorGetByTextOptions { Exact = true });

        await Assertions.Expect(singleSection).ToContainTextAsync("Selected node: None");
        await Assertions.Expect(cDrive).Not.ToBeVisibleAsync();

        await fileSystem.ClickAsync();
        await Assertions.Expect(cDrive).ToBeVisibleAsync();
        await Assertions.Expect(singleSection).ToContainTextAsync("Selected node: File System");

        await cDrive.ClickAsync();
        await Assertions.Expect(programFiles).ToBeVisibleAsync();
        await Assertions.Expect(singleSection).ToContainTextAsync("Selected node: C:\\");
    }

    [Test]
    public async ValueTask Treeview_multiple_selection_and_disabled_nodes_behave_correctly()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady($"{BaseUrl}treeviews", static p => p.GetByText("Multiple Selection", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "TreeView - Quark Suite");

        var multipleSection = page.Locator("div").Filter(new LocatorFilterOptions { HasText = "TreeView with multiple selection mode. Selected nodes:" }).First;
        await multipleSection.GetByText("Project Structure", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        var srcCheckbox = multipleSection.Locator("[data-slot='tree-view-row']").Filter(new LocatorFilterOptions { HasText = "src" })
                                         .GetByRole(AriaRole.Checkbox).First;
        var docsCheckbox = multipleSection.Locator("[data-slot='tree-view-row']").Filter(new LocatorFilterOptions { HasText = "docs" })
                                          .GetByRole(AriaRole.Checkbox).First;

        await srcCheckbox.ClickAsync();
        await docsCheckbox.ClickAsync();

        await Assertions.Expect(multipleSection).ToContainTextAsync("Selected nodes: src, docs");
        await Assertions.Expect(srcCheckbox).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(docsCheckbox).ToHaveAttributeAsync("aria-checked", "true");

        var disabledSection = page.Locator("div").Filter(new LocatorFilterOptions { HasText = "TreeView with some nodes disabled." }).First;
        await disabledSection.GetByText("Available Features", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        var featureC = disabledSection.GetByText("Feature C", new LocatorGetByTextOptions { Exact = true });
        var featureBDisabled = disabledSection.GetByText("Feature B (Disabled)", new LocatorGetByTextOptions { Exact = true });
        await featureC.ClickAsync();

        var disabledNode = disabledSection.Locator("[data-slot='tree-view-node-content']").Filter(new LocatorFilterOptions { HasText = "Feature B (Disabled)" })
                                          .First;
        var disabledStyle = await disabledNode.GetAttributeAsync("style");

        await featureBDisabled.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledSection.GetByText("Sub-feature 1", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        ((disabledStyle ?? string.Empty) ?? string.Empty).Contains("background-color: var(--primary)", StringComparison.OrdinalIgnoreCase).Should().BeFalse();
    }
}
