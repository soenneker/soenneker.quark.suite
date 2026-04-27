using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkFormExtensionPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkFormExtensionPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask CurrencyInput_formats_on_blur_and_keeps_accessible_field_association()
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

        await page.GotoAndWaitForReady(
            $"{BaseUrl}currency-inputs",
            static p => p.Locator("[data-slot='currency-input']").First,
            expectedTitle: "CurrencyInput - Quark Suite");

        var input = page.Locator("#priceInput");
        await Assertions.Expect(input).ToHaveAttributeAsync("data-slot", "input");
        await Assertions.Expect(page.GetByLabel("Price")).ToBeVisibleAsync();

        await input.ClickAsync();
        await input.FillAsync("4321.987");
        await page.Keyboard.PressAsync("Tab");

        await Assertions.Expect(input).ToHaveValueAsync("4,321.99");
        await Assertions.Expect(page.GetByText("Bound value: $4,321.99")).ToBeVisibleAsync();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask DateInput_uses_native_modes_constraints_and_updates_bound_text()
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

        await page.GotoAndWaitForReady(
            $"{BaseUrl}dateinputs",
            static p => p.GetByText("Basic Date Input", new PageGetByTextOptions { Exact = true }),
            expectedTitle: "DateInputs - Quark Suite");

        var basicSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Basic Date Input" }).First;
        var basicInput = page.Locator("input[type='date'][data-slot='input']").First;

        await Assertions.Expect(basicInput).ToHaveAttributeAsync("type", "date");
        await basicInput.FillAsync("2026-04-24");
        await Assertions.Expect(page.GetByText("Selected date: 2026-04-24")).ToBeVisibleAsync();

        await Assertions.Expect(page.Locator("input[type='datetime-local'][data-slot='input']").First).ToBeVisibleAsync();

        await Assertions.Expect(page.Locator("input[type='month'][data-slot='input']").First).ToBeVisibleAsync();

        var restrictedInput = page.Locator($"input[type='date'][min='{System.DateTime.Today:yyyy-MM-dd}']").First;
        await Assertions.Expect(restrictedInput).ToHaveAttributeAsync("min", System.DateTime.Today.ToString("yyyy-MM-dd"));
        await Assertions.Expect(restrictedInput).ToHaveAttributeAsync("max", System.DateTime.Today.AddDays(30).ToString("yyyy-MM-dd"));

        await Assertions.Expect(page.Locator("input[type='date'][disabled][data-slot='input']").First).ToBeDisabledAsync();
        (await page.Locator("input[type='date'][readonly][data-slot='input']").First.GetAttributeAsync("readonly")).Should().NotBeNull();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask MemoInput_preserves_textarea_semantics_validation_attrs_and_input_events()
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

        await page.GotoAndWaitForReady(
            $"{BaseUrl}memoinputs",
            static p => p.Locator("textarea[data-slot='textarea']").First,
            expectedTitle: "MemoInputs - Quark Suite");

        var basic = page.Locator("textarea[data-slot='textarea']").First;
        await Assertions.Expect(basic).ToHaveAttributeAsync("rows", "3");
        await basic.FillAsync("A memo with\nmultiple lines");
        await Assertions.Expect(basic).ToHaveValueAsync("A memo with\nmultiple lines");

        (await page.Locator("textarea[required][data-slot='textarea']").First.GetAttributeAsync("required")).Should().NotBeNull();
        await Assertions.Expect(page.Locator("textarea[maxlength='100'][data-slot='textarea']").First).ToBeVisibleAsync();
        await Assertions.Expect(page.Locator("textarea[minlength='10'][data-slot='textarea']").First).ToBeVisibleAsync();

        await Assertions.Expect(page.Locator("textarea[disabled][data-slot='textarea']").First).ToBeDisabledAsync();
        (await page.Locator("textarea[readonly][data-slot='textarea']").First.GetAttributeAsync("readonly")).Should().NotBeNull();

        consoleErrors.Should().BeEmpty();
        pageErrors.Should().BeEmpty();
    }
}
