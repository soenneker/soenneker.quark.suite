using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkLabelPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkLabelPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Label_examples_toggle_associated_checkbox_and_radio_controls()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}labels",
            static p => p.GetByText("Accept terms and conditions", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "Label - Quark Suite");

        var terms = page.Locator("#terms");
        await Assertions.Expect(terms).Not.ToBeCheckedAsync();

        await page.GetByText("Accept terms and conditions", new PageGetByTextOptions { Exact = true }).ClickAsync();
        await Assertions.Expect(terms).ToBeCheckedAsync();

        var controlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Label as caption for checkbox or radio controls." }).First;
        var rememberMe = controlSection.Locator("#label-remember-me");
        var option1 = controlSection.GetByRole(AriaRole.Radio, new LocatorGetByRoleOptions { Name = "Option 1", Exact = true });
        var option2 = controlSection.GetByRole(AriaRole.Radio, new LocatorGetByRoleOptions { Name = "Option 2", Exact = true });

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
