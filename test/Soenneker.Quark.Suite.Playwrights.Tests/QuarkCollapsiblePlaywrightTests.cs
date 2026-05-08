using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCollapsiblePlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCollapsiblePlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Collapsible_file_tree_matches_shadcn_tabs_composition()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/collapsible",
            static p => p.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "File Tree", Exact = true }),
            expectedTitle: "Collapsible - Quark Suite");

        var fileTreeSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use nested collapsibles to build a file tree." }).First;
        var explorerTab = fileTreeSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Explorer", Exact = true });
        var outlineTab = fileTreeSection.GetByRole(AriaRole.Tab, new LocatorGetByRoleOptions { Name = "Outline", Exact = true });

        await Assertions.Expect(explorerTab).ToBeVisibleAsync();
        await Assertions.Expect(explorerTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(outlineTab).ToBeEnabledAsync();

        await outlineTab.ClickAsync();

        await Assertions.Expect(outlineTab).ToHaveAttributeAsync("aria-selected", "true");
        await Assertions.Expect(explorerTab).ToHaveAttributeAsync("aria-selected", "false");
        await Assertions.Expect(fileTreeSection.GetByText("components", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(fileTreeSection.GetByText("LoginForm", new LocatorGetByTextOptions { Exact = true })).Not.ToBeVisibleAsync();
    }

    [Test]
    public async ValueTask Collapsible_basic_expands_down_without_shifting_up()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/collapsible",
            static p => p.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Basic", Exact = true }),
            expectedTitle: "Collapsible - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Product details" }).First;
        var card = basicSection.Locator("[data-slot='card']").First;
        var trigger = basicSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Product details", Exact = true });

        var initialBox = await card.BoundingBoxAsync();
        initialBox.Should().NotBeNull();

        await trigger.ClickAsync();
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        var expandedBox = await card.BoundingBoxAsync();
        expandedBox.Should().NotBeNull();
        expandedBox!.Height.Should().BeGreaterThan(initialBox!.Height);
        expandedBox.Y.Should().BeApproximately(initialBox.Y, 2);
    }

    [Test]
    public async ValueTask Collapsible_order_demo_expands_down_without_shifting_up()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/collapsible",
            static p => p.GetByText("Order #4189", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Collapsible - Quark Suite");

        var orderSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Order #4189" }).First;
        var collapsible = orderSection.Locator("[data-slot='collapsible']").First;
        var trigger = orderSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle details", Exact = true });

        var initialBox = await collapsible.BoundingBoxAsync();
        initialBox.Should().NotBeNull();

        await trigger.ClickAsync();
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        var expandedBox = await collapsible.BoundingBoxAsync();
        expandedBox.Should().NotBeNull();
        expandedBox!.Height.Should().BeGreaterThan(initialBox!.Height);
        expandedBox.Y.Should().BeApproximately(initialBox.Y, 2);
    }

    [Test]
    public async ValueTask Collapsible_settings_panel_matches_shadcn_defaults()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/collapsible",
            static p => p.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Settings Panel", Exact = true }),
            expectedTitle: "Collapsible - Quark Suite");

        var settingsSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a trigger button to reveal additional settings." }).First;
        var radiusX = settingsSection.Locator("#radius-top-left");
        var radiusY = settingsSection.Locator("#radius-top-right");
        var radiusBottomLeft = settingsSection.Locator("#radius-bottom-left");
        var trigger = settingsSection.Locator("[data-slot='collapsible-trigger']").First;

        await Assertions.Expect(radiusX).ToHaveValueAsync("0");
        await Assertions.Expect(radiusY).ToHaveValueAsync("0");
        await Assertions.Expect(radiusBottomLeft).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger.Locator(".lucide-maximize")).ToBeVisibleAsync();

        await trigger.ClickAsync();

        await Assertions.Expect(radiusBottomLeft).ToBeVisibleAsync();
        await Assertions.Expect(trigger.Locator(".lucide-minimize")).ToBeVisibleAsync();
    }
}
