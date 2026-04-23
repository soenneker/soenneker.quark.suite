using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkNativeSelectPlaywrightTests : PlaywrightUnitTest
{
    public QuarkNativeSelectPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Native_select_demo_updates_bound_value_and_preserves_optgroup_structure()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}native-selects",
            static p => p.Locator("select").First,
            expectedTitle: "Native Select - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Simple native select control with binding." }).First;
        var fruitSelect = basicSection.Locator("select");

        await fruitSelect.SelectOptionAsync(new[] { "banana" });

        await Assertions.Expect(fruitSelect).ToHaveValueAsync("banana");
        await Assertions.Expect(basicSection).ToContainTextAsync("Selected: banana");

        var groupsSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use option groups to organize long lists." }).First;
        var animalSelect = groupsSection.Locator("select");

        await animalSelect.SelectOptionAsync(new[] { "eagle" });

        await Assertions.Expect(animalSelect).ToHaveValueAsync("eagle");
        var groupLabels = await animalSelect.EvaluateAsync<string>(
            "element => Array.from(element.querySelectorAll('optgroup')).map(group => group.label).join(',')");
        Xunit.Assert.Equal("Mammals,Birds", groupLabels);
    }

[Test]
    public async ValueTask Native_select_disabled_demo_keeps_disabled_control_inert_while_small_variant_changes()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}native-selects",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Small size and disabled state." }).Locator("select").First,
            expectedTitle: "Native Select - Quark Suite");

        var disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Small size and disabled state." }).First;
        var smallSelect = disabledSection.Locator("select").Nth(0);
        var disabledSelect = disabledSection.Locator("select").Nth(1);

        await Assertions.Expect(smallSelect).ToHaveAttributeAsync("data-size", "sm");
        await Assertions.Expect(disabledSelect).ToBeDisabledAsync();
        await Assertions.Expect(disabledSelect).ToHaveValueAsync(string.Empty);

        await smallSelect.SelectOptionAsync(new[] { "one" });

        await Assertions.Expect(smallSelect).ToHaveValueAsync("one");
        await Assertions.Expect(disabledSelect).ToHaveValueAsync(string.Empty);
    }
}
