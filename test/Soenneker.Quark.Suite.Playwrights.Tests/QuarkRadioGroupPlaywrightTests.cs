using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkRadioGroupPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkRadioGroupPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Radio_group_demo_home_and_end_keys_move_selection_to_edge_options()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Density options", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        var densityGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Density options", Exact = true });
        var defaultRadio = densityGroup.GetByRole(AriaRole.Radio).Nth(0);
        var comfortableRadio = densityGroup.GetByRole(AriaRole.Radio).Nth(1);
        var compactRadio = densityGroup.GetByRole(AriaRole.Radio).Nth(2);

        await Assertions.Expect(comfortableRadio).ToHaveAttributeAsync("aria-checked", "true");

        await compactRadio.FocusAsync();
        await page.Keyboard.PressAsync("Home");

        await Assertions.Expect(defaultRadio).ToBeFocusedAsync();
        await Assertions.Expect(comfortableRadio).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(defaultRadio).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(compactRadio).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("End");

        await Assertions.Expect(compactRadio).ToBeFocusedAsync();
        await Assertions.Expect(comfortableRadio).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(defaultRadio).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(compactRadio).ToHaveAttributeAsync("aria-checked", "false");

        var disabledGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true });
        var disabledOption = disabledGroup.GetByRole(AriaRole.Radio).Nth(0);
        var option2 = disabledGroup.GetByRole(AriaRole.Radio).Nth(1);
        var option3 = disabledGroup.GetByRole(AriaRole.Radio).Nth(2);

        await option2.FocusAsync();
        await page.Keyboard.PressAsync("Home");

        await Assertions.Expect(option2).ToBeFocusedAsync();
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("End");

        await Assertions.Expect(option3).ToBeFocusedAsync();
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(option3).ToHaveAttributeAsync("aria-checked", "false");
    }

[Test]
    public async ValueTask Radio_group_demo_disabled_item_stays_unchecked_while_enabled_option_changes()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}radiogroups",
            static p => p.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true }),
            expectedTitle: "Radio Group - Quark Suite");

        var disabledGroup = page.GetByRole(AriaRole.Radiogroup, new PageGetByRoleOptions { Name = "Disabled radio options", Exact = true });
        var disabledOption = disabledGroup.GetByRole(AriaRole.Radio).Nth(0);
        var option2 = disabledGroup.GetByRole(AriaRole.Radio).Nth(1);
        var option3 = disabledGroup.GetByRole(AriaRole.Radio).Nth(2);

        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("disabled", "");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");

        await disabledOption.ClickAsync(new LocatorClickOptions { Force = true });

        await Assertions.Expect(disabledOption).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "true");

        await option3.ClickAsync();

        await Assertions.Expect(option3).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(option2).ToHaveAttributeAsync("aria-checked", "false");
    }
}
