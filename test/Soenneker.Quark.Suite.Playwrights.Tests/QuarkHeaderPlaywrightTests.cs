using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkHeaderPlaywrightTests : PlaywrightUnitTest
{
    public QuarkHeaderPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Header_sidebar_shell_demo_preserves_sidebar_and_inset_layout()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}header",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "The header can sit above a sidebar provider and expose the standard sidebar trigger without each layout restyling it." }).First,
            expectedTitle: "Header - Quark Suite");

        ILocator section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The header can sit above a sidebar provider and expose the standard sidebar trigger without each layout restyling it." }).First;
        ILocator header = section.Locator("[data-slot='header']").First;
        ILocator sidebar = section.Locator("[data-slot='sidebar']").First;
        ILocator inset = section.Locator("[data-slot='sidebar-inset']").First;

        await Assertions.Expect(header).ToBeVisibleAsync();
        await Assertions.Expect(sidebar).ToBeVisibleAsync();
        await Assertions.Expect(inset).ToBeVisibleAsync();
        await Assertions.Expect(sidebar).ToContainTextAsync("Dashboard");
        await Assertions.Expect(inset).ToContainTextAsync("Use the trigger in the header to toggle the sidebar.");

        LocatorBoundingBoxResult? headerBox = await header.BoundingBoxAsync();
        LocatorBoundingBoxResult? sidebarBox = await sidebar.BoundingBoxAsync();
        LocatorBoundingBoxResult? insetBox = await inset.BoundingBoxAsync();

        Assert.NotNull(headerBox);
        Assert.NotNull(sidebarBox);
        Assert.NotNull(insetBox);

        Assert.True(headerBox.Height >= 48, $"Expected the header shell to keep a usable height, but measured {headerBox.Height}.");
        Assert.True(sidebarBox.Width >= 200, $"Expected the sidebar to remain visible beside the content, but measured {sidebarBox.Width}.");
        Assert.True(insetBox.Width >= 200, $"Expected the sidebar inset content to remain visible, but measured {insetBox.Width}.");
    }
}
