using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;
using Xunit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[Collection("Collection")]
public sealed class QuarkToggleGroupPlaywrightTests : PlaywrightUnitTest
{
    public QuarkToggleGroupPlaywrightTests(QuarkPlaywrightFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
    {
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

        ILocator rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Toggle groups should preserve selection flow in right-to-left layouts." }).First;
        ILocator rtlGroup = rtlSection.Locator("[dir='rtl'] [role='radiogroup']").First;
        ILocator rtlRight = rtlGroup.Locator("button[role='radio'][data-value='right']").First;
        ILocator rtlCenter = rtlGroup.Locator("button[role='radio'][data-value='center']").First;
        ILocator rtlLeft = rtlGroup.Locator("button[role='radio'][data-value='left']").First;

        await Assertions.Expect(rtlGroup).ToHaveAttributeAsync("dir", "rtl");
        await Assertions.Expect(rtlCenter).ToHaveAttributeAsync("aria-checked", "true");

        await rtlCenter.FocusAsync();
        await page.Keyboard.PressAsync("ArrowLeft");
        await Assertions.Expect(rtlLeft).ToBeFocusedAsync();
        await Assertions.Expect(rtlCenter).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(rtlLeft).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("ArrowRight");
        await Assertions.Expect(rtlCenter).ToBeFocusedAsync();
        await Assertions.Expect(rtlCenter).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(rtlRight).ToHaveAttributeAsync("aria-checked", "false");
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
}
