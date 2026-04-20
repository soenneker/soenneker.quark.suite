using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkTreeViewPlaywrightTests : PlaywrightUnitTest
{
    public QuarkTreeViewPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Treeview_async_demo_loads_children_on_expand_and_updates_selection()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}treeview",
            static p => p.Locator("#treeview-async-demo"),
            expectedTitle: "TreeView - Quark Suite");

        ILocator asyncSection = page.Locator("#treeview-async-demo");
        ILocator server1 = asyncSection.GetByText("Server 1", new LocatorGetByTextOptions { Exact = true });
        ILocator api = asyncSection.GetByText("API", new LocatorGetByTextOptions { Exact = true });
        ILocator routes = asyncSection.GetByText("Routes", new LocatorGetByTextOptions { Exact = true });

        await Assertions.Expect(asyncSection).ToContainTextAsync("Selected node: None");
        await Assertions.Expect(api).ToHaveCountAsync(0);

        await server1.ClickAsync();

        await Assertions.Expect(api).ToBeVisibleAsync();
        await Assertions.Expect(asyncSection).ToContainTextAsync("Selected node: Server 1");

        await api.ClickAsync();

        await Assertions.Expect(routes).ToBeVisibleAsync();
        await Assertions.Expect(asyncSection).ToContainTextAsync("Selected node: API");
    }

[Fact]
    public async ValueTask Treeview_single_selection_expands_parent_and_updates_selected_summary()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}treeview",
            static p => p.GetByText("Basic TreeView", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "TreeView - Quark Suite");

        ILocator singleSection = page.Locator("div").Filter(new LocatorFilterOptions { HasText = "TreeView with single selection mode. Selected node:" }).First;
        ILocator fileSystem = singleSection.GetByText("File System", new LocatorGetByTextOptions { Exact = true });
        ILocator cDrive = singleSection.GetByText("C:\\", new LocatorGetByTextOptions { Exact = true });
        ILocator programFiles = singleSection.GetByText("Program Files", new LocatorGetByTextOptions { Exact = true });

        await Assertions.Expect(singleSection).ToContainTextAsync("Selected node: None");
        await Assertions.Expect(cDrive).Not.ToBeVisibleAsync();

        await fileSystem.ClickAsync();
        await Assertions.Expect(cDrive).ToBeVisibleAsync();
        await Assertions.Expect(singleSection).ToContainTextAsync("Selected node: File System");

        await cDrive.ClickAsync();
        await Assertions.Expect(programFiles).ToBeVisibleAsync();
        await Assertions.Expect(singleSection).ToContainTextAsync("Selected node: C:\\");
    }

[Fact]
    public async ValueTask Treeview_multiple_selection_and_disabled_nodes_behave_correctly()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}treeview",
            static p => p.GetByText("Multiple Selection", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "TreeView - Quark Suite");

        ILocator multipleSection = page.Locator("div").Filter(new LocatorFilterOptions { HasText = "TreeView with multiple selection mode. Selected nodes:" }).First;
        await multipleSection.GetByText("Project Structure", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        ILocator srcCheckbox = multipleSection.Locator("[data-slot='tree-view-row']")
                                              .Filter(new LocatorFilterOptions { HasText = "src" })
                                              .GetByRole(AriaRole.Checkbox)
                                              .First;
        ILocator docsCheckbox = multipleSection.Locator("[data-slot='tree-view-row']")
                                               .Filter(new LocatorFilterOptions { HasText = "docs" })
                                               .GetByRole(AriaRole.Checkbox)
                                               .First;

        await srcCheckbox.ClickAsync();
        await docsCheckbox.ClickAsync();

        await Assertions.Expect(multipleSection).ToContainTextAsync("Selected nodes: src, docs");
        await Assertions.Expect(srcCheckbox).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(docsCheckbox).ToHaveAttributeAsync("aria-checked", "true");

        ILocator disabledSection = page.Locator("div").Filter(new LocatorFilterOptions { HasText = "TreeView with some nodes disabled." }).First;
        await disabledSection.GetByText("Available Features", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        ILocator featureC = disabledSection.GetByText("Feature C", new LocatorGetByTextOptions { Exact = true });
        ILocator featureBDisabled = disabledSection.GetByText("Feature B (Disabled)", new LocatorGetByTextOptions { Exact = true });
        await featureC.ClickAsync();

        ILocator disabledNode = disabledSection.Locator("[data-slot='tree-view-node-content']").Filter(new LocatorFilterOptions { HasText = "Feature B (Disabled)" }).First;
        string? disabledStyle = await disabledNode.GetAttributeAsync("style");

        await featureBDisabled.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledSection.GetByText("Sub-feature 1", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        Assert.DoesNotContain("background-color: var(--primary)", disabledStyle ?? string.Empty, StringComparison.OrdinalIgnoreCase);
    }
}
