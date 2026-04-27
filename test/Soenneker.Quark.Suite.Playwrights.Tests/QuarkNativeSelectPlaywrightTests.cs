using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;
using System.Collections.Generic;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkNativeSelectPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkNativeSelectPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Native_select_demo_updates_bound_value_and_preserves_optgroup_structure()
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
        page.PageError += (_, exception) => pageErrors.Add(exception);

        await page.GotoAndWaitForReady(
            $"{BaseUrl}native-selects",
            static p => p.Locator("select").First,
            expectedTitle: "Native Select - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "A styled native HTML select element with consistent design system integration." }).First;
        var statusSelect = basicSection.Locator("select");

        await statusSelect.SelectOptionAsync(new[] { "done" });

        await Assertions.Expect(statusSelect).ToHaveValueAsync("done");

        var groupsSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use option groups to organize long lists." }).First;
        var animalSelect = groupsSection.Locator("select");

        await animalSelect.SelectOptionAsync(new[] { "eagle" });

        await Assertions.Expect(animalSelect).ToHaveValueAsync("eagle");
        var groupLabels = await animalSelect.EvaluateAsync<string>(
            "element => Array.from(element.querySelectorAll('optgroup')).map(group => group.label).join(',')");
        (groupLabels).Should().Be("Mammals,Birds");

        var styleProbe = await statusSelect.EvaluateAsync<NativeSelectStyleProbe>(
            @"element => {
                const wrapper = element.closest('[data-slot=""native-select-wrapper""]');
                const icon = wrapper.querySelector('[data-slot=""native-select-icon""]');
                const style = getComputedStyle(element);
                const wrapperStyle = getComputedStyle(wrapper);
                const iconStyle = getComputedStyle(icon);
                return {
                    height: style.height,
                    borderRadius: style.borderRadius,
                    Shadow: style.Shadow,
                    wrapperDisplay: wrapperStyle.display,
                    iconOpacity: iconStyle.opacity
                };
            }");

        styleProbe.height.Should().Be("36px");
        styleProbe.wrapperDisplay.Should().Be("block");
        styleProbe.iconOpacity.Should().Be("0.5");
        styleProbe.Shadow.Should().NotBe("none");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Native_select_disabled_demo_keeps_disabled_control_inert_while_small_variant_changes()
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
        page.PageError += (_, exception) => pageErrors.Add(exception);

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

        var smallProbe = await smallSelect.EvaluateAsync<NativeSelectStyleProbe>(
            @"element => {
                const style = getComputedStyle(element);
                return {
                    height: style.height,
                    borderRadius: style.borderRadius,
                    Shadow: style.Shadow,
                    wrapperDisplay: '',
                    iconOpacity: ''
                };
            }");

        smallProbe.height.Should().Be("32px");

        var invalidSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a destructive border and supporting copy" }).First;
        var invalidSelect = invalidSection.Locator("select").First;
        await Assertions.Expect(invalidSelect).ToHaveAttributeAsync("aria-invalid", "true");

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    private sealed class NativeSelectStyleProbe
    {
        public string? height { get; set; }
        public string? borderRadius { get; set; }
        public string? Shadow { get; set; }
        public string? wrapperDisplay { get; set; }
        public string? iconOpacity { get; set; }
    }
}
