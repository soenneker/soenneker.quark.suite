using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkCompositePlaywrightTests : PlaywrightUnitTest
{
    public QuarkCompositePlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Tabs_demo_switches_selected_trigger_and_visible_panel()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tabs",
            static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Account", Exact = true }).First,
            expectedTitle: "Tabs - Quark Suite");

        ILocator account = page.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Account", Exact = true }).First;
        ILocator password = page.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Password", Exact = true }).First;

        await Assertions.Expect(account).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(page.GetByRole(AriaRole.Tabpanel).First).ToContainTextAsync("Make changes to your account here.");

        await password.ClickAsync();

        await Assertions.Expect(password).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(account).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(page.GetByRole(AriaRole.Tabpanel).First).ToContainTextAsync("Change your password here.");
        await Assertions.Expect(page.GetByLabel("New password", new PageGetByLabelOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Tabs_disabled_and_controlled_examples_respect_selection_rules()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}tabs",
            static p => p.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Enabled", Exact = true }),
            expectedTitle: "Tabs - Quark Suite");

        ILocator disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disabled triggers are skipped by keyboard navigation and cannot be selected." }).First;
        ILocator enabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Enabled", Exact = true });
        ILocator disabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Disabled", Exact = true });
        ILocator anotherEnabled = disabledSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Another enabled", Exact = true });

        await Assertions.Expect(disabled).ToBeDisabledAsync();

        await enabled.ClickAsync();
        await Assertions.Expect(enabled).ToHaveAttributeAsync("aria-selected", "true");

        await disabled.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(enabled).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(disabledSection.GetByText("This content is unreachable because the trigger is disabled.", new LocatorGetByTextOptions { Exact = true })).Not.ToBeVisibleAsync();

        await anotherEnabled.ClickAsync();
        await Assertions.Expect(anotherEnabled).ToHaveAttributeAsync("aria-selected", "true");

        ILocator controlledDescription = page.Locator("p").Filter(new LocatorFilterOptions { HasText = "Selected tab:" }).First;
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Settings", Exact = true }).Last.ClickAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Settings", Exact = true }).Last).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(controlledDescription).ToContainTextAsync("Selected tab: settings-controlled.");
        await Assertions.Expect(page.GetByText("Parent-controlled content for Settings.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Collapsible_demo_toggles_content_and_disabled_trigger_stays_closed()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}collapsibles",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle", Exact = true }),
            expectedTitle: "Collapsible - Quark Suite");

        ILocator demoSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Compact starred-repositories example with a small toggle trigger." }).First;
        ILocator toggle = demoSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle", Exact = true });
        ILocator colors = demoSection.GetByText("@radix-ui/colors", new LocatorGetByTextOptions { Exact = true });

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(colors).Not.ToBeVisibleAsync();

        await toggle.ClickAsync();

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "true");
        await Assertions.Expect(colors).ToBeVisibleAsync();

        await toggle.ClickAsync();

        await Assertions.Expect(toggle).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(colors).Not.ToBeVisibleAsync();

        ILocator disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disable the whole collapsible when a panel should stay locked." }).First;
        ILocator locked = disabledSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Production deployment Locked", Exact = false });

        await Assertions.Expect(locked).ToHaveAttributeAsync("data-disabled", "true");
        await Assertions.Expect(locked).ToHaveAttributeAsync("aria-expanded", "true");

        await locked.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(locked).ToHaveAttributeAsync("aria-expanded", "true");
    }

    [Fact]
    public async ValueTask Collapse_examples_toggle_targets_and_programmatic_controls()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}collapses",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle Collapse", Exact = true }),
            expectedTitle: "Collapses - Quark Suite");

        ILocator basicCollapse = page.Locator("#basicCollapse");
        await Assertions.Expect(basicCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle Collapse", Exact = true }).First.ClickAsync();
        await Assertions.Expect(basicCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        ILocator firstCollapse = page.Locator("#firstCollapse");
        ILocator secondCollapse = page.Locator("#secondCollapse");
        await Assertions.Expect(firstCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));
        await Assertions.Expect(secondCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Both", Exact = true }).First.ClickAsync();
        await Assertions.Expect(firstCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));
        await Assertions.Expect(secondCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        ILocator showButton = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Show", Exact = true }).First;
        ILocator programmaticCollapse = page.Locator(".q-collapse").Last;

        await Assertions.Expect(page.GetByText("State: Collapsed", new PageGetByTextOptions { Exact = false }).First).ToBeVisibleAsync();
        await Assertions.Expect(programmaticCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await showButton.ClickAsync();
        await Assertions.Expect(page.GetByText("State: Expanded", new PageGetByTextOptions { Exact = false }).First).ToBeVisibleAsync();
        await Assertions.Expect(programmaticCollapse).Not.ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Hide", Exact = true }).First.ClickAsync();
        await Assertions.Expect(page.GetByText("State: Collapsed", new PageGetByTextOptions { Exact = false }).First).ToBeVisibleAsync();
        await Assertions.Expect(programmaticCollapse).ToHaveClassAsync(new System.Text.RegularExpressions.Regex("max-h-0"));
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

        ILocator srcCheckbox = multipleSection.Locator(".q-tree-view-node-row")
                                              .Filter(new LocatorFilterOptions { HasText = "src" })
                                              .GetByRole(AriaRole.Checkbox)
                                              .First;
        ILocator docsCheckbox = multipleSection.Locator(".q-tree-view-node-row")
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

        ILocator disabledNode = disabledSection.Locator(".q-tree-view-node-content").Filter(new LocatorFilterOptions { HasText = "Feature B (Disabled)" }).First;
        string? disabledStyle = await disabledNode.GetAttributeAsync("style");

        await featureBDisabled.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledSection.GetByText("Sub-feature 1", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        Assert.DoesNotContain("background-color: var(--primary)", disabledStyle ?? string.Empty, System.StringComparison.OrdinalIgnoreCase);
    }
}
