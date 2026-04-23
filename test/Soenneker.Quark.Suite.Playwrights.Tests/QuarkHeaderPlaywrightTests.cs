using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkHeaderPlaywrightTests : PlaywrightUnitTest
{
    public QuarkHeaderPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Header_sidebar_shell_demo_preserves_sidebar_and_inset_layout()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}header",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "The header can sit above a sidebar provider and expose the standard sidebar trigger without each layout restyling it." }).First,
            expectedTitle: "Header - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "The header can sit above a sidebar provider and expose the standard sidebar trigger without each layout restyling it." }).First;
        var header = section.Locator("[data-slot='header']").First;
        var sidebar = section.Locator("[data-slot='sidebar']").First;
        var inset = section.Locator("[data-slot='sidebar-inset']").First;

        await Assertions.Expect(header).ToBeVisibleAsync();
        await Assertions.Expect(sidebar).ToBeVisibleAsync();
        await Assertions.Expect(inset).ToBeVisibleAsync();
        await Assertions.Expect(sidebar).ToContainTextAsync("Dashboard");
        await Assertions.Expect(inset).ToContainTextAsync("Use the trigger in the header to toggle the sidebar.");

        var headerBox = await header.BoundingBoxAsync();
        var sidebarBox = await sidebar.BoundingBoxAsync();
        var insetBox = await inset.BoundingBoxAsync();

        (headerBox).Should().NotBeNull();
        (sidebarBox).Should().NotBeNull();
        (insetBox).Should().NotBeNull();

        (headerBox.Height >= 48).Should().BeTrue();
        (sidebarBox.Width >= 200).Should().BeTrue();
        (insetBox.Width >= 200).Should().BeTrue();
    }
}
