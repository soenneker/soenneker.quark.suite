using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkFormsPlaywrightTests : PlaywrightUnitTest
{
    public QuarkFormsPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async ValueTask Toggle_demo_updates_pressed_state_and_bound_label()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}toggles",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Additional examples" }).GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle bookmark", Exact = true }),
            expectedTitle: "Toggle - Quark Suite");

        ILocator bookmarkToggle = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Additional examples" }).GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Toggle bookmark", Exact = true });

        await Assertions.Expect(bookmarkToggle).ToHaveAttributeAsync("aria-pressed", "false");
        await Assertions.Expect(bookmarkToggle).ToContainTextAsync("Bookmark");

        await bookmarkToggle.ClickAsync();

        await Assertions.Expect(bookmarkToggle).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(bookmarkToggle).ToContainTextAsync("Saved");

        await bookmarkToggle.ClickAsync();

        await Assertions.Expect(bookmarkToggle).ToHaveAttributeAsync("aria-pressed", "false");
        await Assertions.Expect(bookmarkToggle).ToContainTextAsync("Bookmark");
    }

    [Fact]
    public async ValueTask Toggle_group_demo_enforces_single_selection_and_preserves_multiple_selection()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}toggle-groups",
            static p => p.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Toggle italic", Exact = true }),
            expectedTitle: "Toggle Group - Quark Suite");

        ILocator singleItalic = page.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Toggle italic", Exact = true }).First;
        ILocator singleBold = page.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Toggle bold", Exact = true }).First;

        await Assertions.Expect(singleItalic).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(singleBold).ToHaveAttributeAsync("aria-checked", "false");

        await singleBold.ClickAsync();

        await Assertions.Expect(singleBold).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(singleItalic).ToHaveAttributeAsync("aria-checked", "false");

        ILocator multipleBookmark = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle bookmark", Exact = true }).First;
        ILocator multipleStar = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle star", Exact = true });

        await Assertions.Expect(multipleBookmark).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(multipleStar).ToHaveAttributeAsync("aria-pressed", "false");

        await multipleStar.ClickAsync();

        await Assertions.Expect(multipleBookmark).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(multipleStar).ToHaveAttributeAsync("aria-pressed", "true");
    }

    [Fact]
    public async ValueTask Toggle_group_demo_vertical_keyboard_navigation_and_disabled_items_behave_correctly()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}toggle-groups",
            static p => p.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Align top", Exact = true }),
            expectedTitle: "Toggle Group - Quark Suite");

        ILocator verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a vertical orientation when grouped actions need to fit into narrow side rails or inspector panels." }).First;
        ILocator verticalGroup = verticalSection.GetByRole(AriaRole.Radiogroup).First;
        ILocator top = verticalGroup.Locator("button[role='radio'][data-value='top']").First;
        ILocator middle = verticalGroup.Locator("button[role='radio'][data-value='middle']").First;
        ILocator bottom = verticalGroup.Locator("button[role='radio'][data-value='bottom']").First;

        await top.FocusAsync();
        await page.Keyboard.PressAsync("ArrowDown");
        await Assertions.Expect(middle).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("ArrowDown");
        await Assertions.Expect(bottom).ToBeFocusedAsync();
        await Assertions.Expect(bottom).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(middle).ToHaveAttributeAsync("tabindex", "-1");

        ILocator disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disable the entire group or individual items when a formatting choice is unavailable." }).First;
        ILocator disabledGroups = disabledSection.GetByRole(AriaRole.Radiogroup);
        ILocator disabledGroup = disabledGroups.Nth(0);
        ILocator disabledItemGroup = disabledGroups.Nth(1);
        ILocator disabledGroupLeft = disabledGroup.Locator("button[role='radio'][data-value='left']").First;
        ILocator disabledGroupRight = disabledGroup.Locator("button[role='radio'][data-value='right']").First;
        ILocator disabledItemCenter = disabledItemGroup.Locator("button[role='radio'][data-value='center']").First;
        ILocator disabledItemRight = disabledItemGroup.Locator("button[role='radio'][data-value='right']").First;

        await Assertions.Expect(disabledGroupRight).ToHaveAttributeAsync("disabled", "");
        await disabledGroupRight.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(disabledGroupLeft).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(disabledGroupRight).ToHaveAttributeAsync("aria-checked", "false");

        await Assertions.Expect(disabledItemCenter).ToHaveAttributeAsync("disabled", "");
        await Assertions.Expect(disabledItemCenter).ToHaveAttributeAsync("aria-checked", "true");
        await disabledItemCenter.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(disabledItemCenter).ToHaveAttributeAsync("aria-checked", "true");

        await disabledItemRight.ClickAsync();
        await Assertions.Expect(disabledItemRight).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(disabledItemCenter).ToHaveAttributeAsync("aria-checked", "false");
    }

    [Fact]
    public async ValueTask Radio_group_demo_disabled_item_stays_unchecked_while_enabled_option_changes()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        ILocator disabledGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true });
        ILocator disabledOption = disabledGroup.GetByRole(AriaRole.Radio).Nth(0);
        ILocator option2 = disabledGroup.GetByRole(AriaRole.Radio).Nth(1);
        ILocator option3 = disabledGroup.GetByRole(AriaRole.Radio).Nth(2);

        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("disabled", "");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");

        await disabledOption.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");

        await option3.ClickAsync();

        await Assertions.Expect(option3).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "false");
    }

    [Fact]
    public async ValueTask Radio_group_form_demo_requires_selection_and_submits_selected_value()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Notification type", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        ILocator formSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Form-style radio group with validation and submitted value preview." }).First;
        ILocator submit = formSection.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true });
        ILocator form = formSection.Locator("form");

        await submit.ClickAsync();
        await Assertions.Expect(form).ToContainTextAsync("You need to select a notification type.");

        ILocator notificationGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Notification type", Exact = true });
        ILocator nothingRadio = notificationGroup.GetByRole(AriaRole.Radio).Nth(2);

        await nothingRadio.ClickAsync();

        await Assertions.Expect(form).Not.ToContainTextAsync("You need to select a notification type.");
        await Assertions.Expect(nothingRadio).ToHaveAttributeAsync("aria-checked", "true");

        await submit.ClickAsync();

        await Assertions.Expect(formSection.Locator("pre")).ToContainTextAsync("\"type\": \"none\"");
    }

    [Fact]
    public async ValueTask Select_demo_updates_selection_and_closes_listbox()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Combobox).First;

        await Assertions.Expect(trigger).ToContainTextAsync("Select a fruit");

        await trigger.ClickAsync();

        ILocator listbox = page.Locator("[role='listbox']").First;
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "true");

        await page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Banana", Exact = true }).ClickAsync();

        await Assertions.Expect(trigger).ToContainTextAsync("Banana");
        await Assertions.Expect(listbox).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
    }

    [Fact]
    public async ValueTask Select_demo_reopen_marks_selected_option_as_checked()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Combobox).First;

        await trigger.ClickAsync();
        await page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Banana", Exact = true }).ClickAsync();
        await trigger.ClickAsync();

        ILocator selectedOption = page.GetByRole(AriaRole.Option, new PageGetByRoleOptions { Name = "Banana", Exact = true });

        await Assertions.Expect(selectedOption).ToHaveAttributeAsync("data-state", "checked");
        await Assertions.Expect(selectedOption).ToHaveAttributeAsync("aria-selected", "true");
    }

    [Fact]
    public async ValueTask Select_demo_portals_content_and_dismisses_on_outside_click()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Combobox).First,
            expectedTitle: "Select Component - Quark Suite");

        ILocator trigger = page.GetByRole(AriaRole.Combobox).First;
        await trigger.ClickAsync();

        ILocator listbox = page.Locator("[role='listbox']:visible").First;
        await Assertions.Expect(listbox).ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToHaveAttributeAsync("data-state", "open");

        await page.WaitForFunctionAsync(
            "() => {" +
            "const listbox = document.querySelector('[role=\"listbox\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!listbox && document.body.contains(listbox) && !!main && !main.contains(listbox);" +
            "}");

        bool renderedOutsideMain = await page.EvaluateAsync<bool>(
            "() => {" +
            "const listbox = document.querySelector('[role=\"listbox\"][data-state=\"open\"]');" +
            "const main = document.querySelector('main');" +
            "return !!listbox && document.body.contains(listbox) && !!main && !main.contains(listbox);" +
            "}");

        Assert.True(renderedOutsideMain);

        await ClickJustOutsideAsync(page, listbox);

        await Assertions.Expect(trigger).ToHaveAttributeAsync("aria-expanded", "false");
        await Assertions.Expect(listbox).Not.ToBeVisibleAsync();
    }

    [Fact]
    public async ValueTask Select_form_demo_requires_selection_before_submit()
    {
        await using BrowserSession session = await CreateSession();
        IPage page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}selects",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }),
            expectedTitle: "Select Component - Quark Suite");

        ILocator submit = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true });
        await submit.ClickAsync();

        await Assertions.Expect(page.GetByText("Please select an email to display.", new PageGetByTextOptions { Exact = true })).ToBeVisibleAsync();
    }

    private static async Task ClickJustOutsideAsync(IPage page, ILocator locator)
    {
        var box = await locator.BoundingBoxAsync();
        Assert.NotNull(box);
        float x = box.X > 40 ? box.X - 20 : box.X + box.Width + 20;
        float y = box.Y > 40 ? box.Y - 20 : box.Y + 20;
        await page.Mouse.ClickAsync(x, y);
    }
}
