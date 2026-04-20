using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkLabelPlaywrightTests : PlaywrightUnitTest
{
    public QuarkLabelPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

[Fact]
    public async ValueTask Label_examples_toggle_associated_checkbox_and_radio_controls()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}labels",
            static p => p.GetByText("Accept terms and conditions", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Label - Quark Suite");

        ILocator terms = page.Locator("#label-terms");
        await Assertions.Expect(terms).Not.ToBeCheckedAsync();

        await page.GetByText("Accept terms and conditions", new PageGetByTextOptions { Exact = true }).ClickAsync();
        await Assertions.Expect(terms).ToBeCheckedAsync();

        ILocator controlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Label as caption for checkbox or radio controls." }).First;
        ILocator rememberMe = controlSection.Locator("#label-remember-me");
        ILocator option1 = controlSection.Locator("#label-option1");
        ILocator option2 = controlSection.Locator("#label-option2");

        await Assertions.Expect(rememberMe).Not.ToBeCheckedAsync();
        await Assertions.Expect(option1).Not.ToBeCheckedAsync();
        await Assertions.Expect(option2).Not.ToBeCheckedAsync();

        await controlSection.GetByText("Remember me", new LocatorGetByTextOptions { Exact = true }).ClickAsync();
        await controlSection.GetByText("Option 2", new LocatorGetByTextOptions { Exact = true }).ClickAsync();

        await Assertions.Expect(rememberMe).ToBeCheckedAsync();
        await Assertions.Expect(option2).ToBeCheckedAsync();
        await Assertions.Expect(option1).Not.ToBeCheckedAsync();
    }
}
