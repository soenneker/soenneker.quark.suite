using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkSelectPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkSelectPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Select_form_demo_requires_selection_before_submit()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }),
            expectedTitle: "Select Component - Quark Suite");

        var submit = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true });
        await submit.ClickAsync();

        await Assertions.Expect(page.GetByText("Please select an email to display.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Select_demo_portals_content_and_dismisses_on_outside_click()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Combobox).First;
        await trigger.ClickAsync();

        var listbox = page.Locator("[role='listbox']:visible").First;
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToHaveAttributeAsync("data-state", "open");
        await page.WaitForFunctionAsync(
            "() => !!document.querySelector('[role=\"listbox\"][data-state=\"open\"]')" +
            "?.closest('[data-bradix-dismissable-layer-ready=\"true\"]')");

        await page.WaitForFunctionAsync(
            "() => {" +
            "const listbox = document.querySelector('[role=\"listbox\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!listbox && document.body.contains(listbox) && !!main && !main.contains(listbox);" +
            "}");

        var renderedOutsideMain = await page.EvaluateAsync<bool>(
            "() => {" +
            "const listbox = document.querySelector('[role=\"listbox\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!listbox && document.body.contains(listbox) && !!main && !main.contains(listbox);" +
            "}");

        (renderedOutsideMain).Should().BeTrue();

        await page.Mouse.ClickAsync(10, 10);

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(listbox).Not.ToBeVisibleAsync();
    }

[Test]
    public async ValueTask Select_demo_home_and_end_keys_move_focus_to_first_and_last_enabled_options()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Combobox).First;
        await trigger.ClickAsync();

        var listbox = page.Locator("[role='listbox']:visible").First;
        var apple = page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Apple", Exact = true });
        var pineapple = page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Pineapple", Exact = true });

        await Assertions.Expect(apple).ToHaveAttributeAsync("data-highlighted", string.Empty);

        await listbox.FocusAsync();
        await page.Keyboard.PressAsync("End");

        await Assertions.Expect(pineapple).ToHaveAttributeAsync("data-highlighted", string.Empty);

        await listbox.FocusAsync();
        await page.Keyboard.PressAsync("Home");

        await Assertions.Expect(apple).ToHaveAttributeAsync("data-highlighted", string.Empty);
    }

[Test]
    public async ValueTask Select_native_form_demo_requires_selection_and_submits_selected_value()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByTestId("select-native-form-result"),
            expectedTitle: "Select Component - Quark Suite");

        var form = page.GetByTestId("select-native-form");
        var result = page.GetByTestId("select-native-form-result");
        var hiddenSelect = page.Locator("select[name='framework']");

        await Assertions.Expect(result).ToContainTextAsync("No submission yet.");
        await Assertions.Expect(hiddenSelect).ToHaveAttributeAsync("required", string.Empty);
        var isInitiallyValid = await hiddenSelect.EvaluateAsync<bool>("element => element.checkValidity()");
        (isInitiallyValid).Should().BeFalse();

        var trigger = form.GetByRole(AriaRole.Combobox).First;
        await trigger.ClickAsync();
        var listbox = page.Locator("[role='listbox']:visible").First;
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        var astroOption = listbox.GetByRole(AriaRole.Option, new LocatorGetByRoleOptions { Name = "Astro", Exact = true });
        await astroOption.FocusAsync();
        await Assertions.Expect(astroOption).ToBeFocusedAsync();
        await page.Keyboard.PressAsync("Enter");

        await Assertions.Expect(trigger).ToContainTextAsync("Astro");
        await Assertions.Expect(hiddenSelect).ToHaveValueAsync("astro");
        var isValidAfterSelection = await hiddenSelect.EvaluateAsync<bool>("element => element.checkValidity()");
        (isValidAfterSelection).Should().BeTrue();

        await form.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit native form", Exact = true }).ClickAsync();
        await Assertions.Expect(result).ToContainTextAsync("framework=astro");
    }

[Test]
    public async ValueTask Select_demo_updates_selection_and_closes_listbox()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Combobox).First;

        await Assertions.Expect(trigger).ToContainTextAsync("Select a fruit");

        await trigger.ClickAsync();

        var listbox = page.Locator("[role='listbox']").First;
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        await page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Banana", Exact = true }).ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync("Banana");
        await Assertions.Expect(listbox).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

[Test]
    public async ValueTask Select_demo_reopen_marks_selected_option_as_checked()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Combobox).First;

        await trigger.ClickAsync();
        await page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Banana", Exact = true }).ClickAsync();
        await trigger.ClickAsync();

        var selectedOption = page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Banana", Exact = true });

        await Assertions.Expect(selectedOption).ToHaveAttributeAsync("data-state", "checked");
        await Assertions.Expect(selectedOption).ToHaveAttributeAsync("aria-selected", "true");
    }

[Test]
    public async ValueTask Select_demo_typeahead_moves_focus_to_matching_enabled_option()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        var trigger = page.GetByRole(AriaRole.Combobox).First;
        await trigger.ClickAsync();

        var contentId = await trigger.GetAttributeAsync("aria-controls");
        (string.IsNullOrWhiteSpace(contentId)).Should().BeFalse();

        var content = page.Locator($"#{contentId}");
        var apple = content.Locator("[role='option']").Filter(new LocatorFilterOptions { HasText = "Apple" }).First;
        var highlightedGrapes = content.Locator("[role='option'][data-highlighted]").Filter(new LocatorFilterOptions { HasText = "Grapes" });

        await Assertions.Expect(apple).ToBeFocusedAsync();
        await page.Keyboard.PressAsync("g");

        (await highlightedGrapes.CountAsync() > 0).Should().BeTrue();
    }

[Test]
    public async ValueTask Select_demo_typeahead_skips_disabled_matching_option()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Separate options with labels and a separator." }).First,
            expectedTitle: "Select Component - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Separate options with labels and a separator." }).First;
        var trigger = section.GetByRole(AriaRole.Combobox).First;
        await trigger.ClickAsync();

        var contentId = await trigger.GetAttributeAsync("aria-controls");
        (string.IsNullOrWhiteSpace(contentId)).Should().BeFalse();

        var content = page.Locator($"#{contentId}");
        var apple = content.Locator("[role='option']").Filter(new LocatorFilterOptions { HasText = "Apple" }).First;
        var carrot = content.Locator("[role='option']:visible").Filter(new LocatorFilterOptions { HasText = "Carrot" }).First;
        var highlightedCourgette = content.Locator("[role='option'][data-highlighted]").Filter(new LocatorFilterOptions { HasText = "Courgette" });

        await Assertions.Expect(carrot).ToHaveAttributeAsync("aria-disabled", "true");
        await Assertions.Expect(apple).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("c");

        var highlightedTexts = await content.Locator("[role='option'][data-highlighted]").AllTextContentsAsync();
        (await highlightedCourgette.CountAsync() > 0).Should().BeTrue();
        await Assertions.Expect(carrot).Not.ToHaveAttributeAsync("data-highlighted", string.Empty);
    }
    private static async Task ClickJustOutside(IPage page, ILocator locator)
    {
        var box = await locator.BoundingBoxAsync();
        (box).Should().NotBeNull();
        var x = box.X > 40 ? box.X - 20 : box.X + box.Width + 20;
        var y = box.Y > 40 ? box.Y - 20 : box.Y + 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
