using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkToggleGroupPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkToggleGroupPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Toggle_group_demo_vertical_keyboard_navigation_and_disabled_items_behave_correctly()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}toggle-groups",
            static p => p.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Align top", Exact = true }),
            expectedTitle: "Toggle Group - Quark Suite");

        var verticalSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use a vertical orientation when grouped actions need to fit into narrow side rails or inspector panels." }).First;
        var verticalGroup = verticalSection.GetByRole(AriaRole.Group).First;
        var top = verticalGroup.Locator("button[role='radio'][data-value='top']").First;
        var middle = verticalGroup.Locator("button[role='radio'][data-value='middle']").First;
        var bottom = verticalGroup.Locator("button[role='radio'][data-value='bottom']").First;

        await top.FocusAsync();
        await page.Keyboard.PressAsync("ArrowDown");
        await Assertions.Expect(middle).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("ArrowDown");
        await Assertions.Expect(bottom).ToBeFocusedAsync();
        await Assertions.Expect(bottom).ToHaveAttributeAsync("tabindex", "0");
        await Assertions.Expect(middle).ToHaveAttributeAsync("tabindex", "-1");

        var disabledSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disable the entire group or individual items when a formatting choice is unavailable." }).First;
        var disabledGroups = disabledSection.GetByRole(AriaRole.Group);
        var disabledGroup = disabledGroups.Nth(0);
        var disabledItemGroup = disabledGroups.Nth(1);
        var disabledGroupLeft = disabledGroup.Locator("button[role='radio'][data-value='left']").First;
        var disabledGroupRight = disabledGroup.Locator("button[role='radio'][data-value='right']").First;
        var disabledItemCenter = disabledItemGroup.Locator("button[role='radio'][data-value='center']").First;
        var disabledItemRight = disabledItemGroup.Locator("button[role='radio'][data-value='right']").First;

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

        var rtlSection = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Toggle groups should preserve selection flow in right-to-left layouts." }).First;
        var rtlGroup = rtlSection.Locator("[dir='rtl'] [role='group']").First;
        var rtlRight = rtlGroup.Locator("button[role='radio'][data-value='right']").First;
        var rtlCenter = rtlGroup.Locator("button[role='radio'][data-value='center']").First;
        var rtlLeft = rtlGroup.Locator("button[role='radio'][data-value='left']").First;

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

[Test]
    public async ValueTask Toggle_group_demo_enforces_single_selection_and_preserves_multiple_selection()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}toggle-groups",
            static p => p.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Toggle italic", Exact = true }),
            expectedTitle: "Toggle Group - Quark Suite");

        var singleItalic = page.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Toggle italic", Exact = true }).First;
        var singleBold = page.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = "Toggle bold", Exact = true }).First;

        await Assertions.Expect(singleItalic).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(singleBold).ToHaveAttributeAsync("aria-checked", "false");

        await singleBold.ClickAsync();

        await Assertions.Expect(singleBold).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(singleItalic).ToHaveAttributeAsync("aria-checked", "false");

        var multipleBookmark = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle bookmark", Exact = true }).First;
        var multipleStar = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Toggle star", Exact = true });

        await Assertions.Expect(multipleBookmark).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(multipleStar).ToHaveAttributeAsync("aria-pressed", "false");

        await multipleStar.ClickAsync();

        await Assertions.Expect(multipleBookmark).ToHaveAttributeAsync("aria-pressed", "true");
        await Assertions.Expect(multipleStar).ToHaveAttributeAsync("aria-pressed", "true");
    }
}
