using System.Threading.Tasks;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Session;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkInputOtpPlaywrightTests : PlaywrightUnitTest
{
    public QuarkInputOtpPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Input_otp_digits_only_demo_distributes_pasted_digits_and_filters_non_numeric_characters()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Digits Only" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Digits Only" }).First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await slots.First.ClickAsync();
        await page.Keyboard.InsertTextAsync("12A34B");

        await Assertions.Expect(slots.Nth(0)).ToHaveValueAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveValueAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveValueAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToHaveValueAsync("4");
        await Assertions.Expect(slots.Nth(4)).ToHaveValueAsync(string.Empty);
        await Assertions.Expect(slots.Nth(5)).ToHaveValueAsync(string.Empty);
    }

[Test]
    public async ValueTask Input_otp_seeded_examples_render_initial_bound_values()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "With Separator" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var separator = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "With Separator" }).First;
        var separatorSlots = separator.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(separatorSlots.Nth(0)).ToHaveValueAsync("1");
        await Assertions.Expect(separatorSlots.Nth(1)).ToHaveValueAsync("2");
        await Assertions.Expect(separatorSlots.Nth(2)).ToHaveValueAsync("3");
        await Assertions.Expect(separatorSlots.Nth(3)).ToHaveValueAsync("4");
        await Assertions.Expect(separatorSlots.Nth(4)).ToHaveValueAsync("5");
        await Assertions.Expect(separatorSlots.Nth(5)).ToHaveValueAsync("6");

        var alphaNumeric = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Alphanumeric" }).First;
        var alphaSlots = alphaNumeric.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(alphaSlots.Nth(0)).ToHaveValueAsync("A");
        await Assertions.Expect(alphaSlots.Nth(1)).ToHaveValueAsync("9");
        await Assertions.Expect(alphaSlots.Nth(2)).ToHaveValueAsync(string.Empty);
    }

[Test]
    public async ValueTask Input_otp_disabled_demo_preserves_existing_value_and_blocks_edits()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disabled" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Disabled" }).First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(slots.Nth(0)).ToHaveValueAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveValueAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveValueAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToHaveValueAsync("4");
        await Assertions.Expect(slots.Nth(4)).ToHaveValueAsync("5");
        await Assertions.Expect(slots.Nth(5)).ToHaveValueAsync("6");

        await Assertions.Expect(slots.First).ToBeDisabledAsync();
        await slots.Nth(2).ClickAsync(new LocatorClickOptions { Force = true });
        await page.Keyboard.PressAsync("Backspace");
        await page.Keyboard.InsertTextAsync("9");

        await Assertions.Expect(slots.Nth(0)).ToHaveValueAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveValueAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveValueAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToHaveValueAsync("4");
        await Assertions.Expect(slots.Nth(4)).ToHaveValueAsync("5");
        await Assertions.Expect(slots.Nth(5)).ToHaveValueAsync("6");
    }

[Test]
    public async ValueTask Input_otp_controlled_demo_buttons_keep_slots_in_sync()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { Has = p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Fill sample", Exact = true }) }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions
        {
            Has = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Fill sample", Exact = true })
        }).First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(slots.Nth(0)).ToHaveValueAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveValueAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveValueAsync(string.Empty);
        await Assertions.Expect(section).ToContainTextAsync("Current controlled value: 12");

        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Fill sample", Exact = true }).ClickAsync();

        await Assertions.Expect(slots.Nth(0)).ToHaveValueAsync("4");
        await Assertions.Expect(slots.Nth(1)).ToHaveValueAsync("8");
        await Assertions.Expect(slots.Nth(2)).ToHaveValueAsync("2");
        await Assertions.Expect(slots.Nth(3)).ToHaveValueAsync("9");
        await Assertions.Expect(slots.Nth(4)).ToHaveValueAsync("1");
        await Assertions.Expect(slots.Nth(5)).ToHaveValueAsync("7");
        await Assertions.Expect(section).ToContainTextAsync("Current controlled value: 482917");

        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Clear", Exact = true }).ClickAsync();

        for (var i = 0; i < 6; i++)
        {
            await Assertions.Expect(slots.Nth(i)).ToHaveValueAsync(string.Empty);
        }
    }

[Test]
    public async ValueTask Input_otp_seeded_demo_home_and_end_keys_move_focus_to_first_and_last_filled_slots()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "With Separator" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "With Separator" }).First;
        var slots = section.Locator("[data-slot='input-otp-slot']");
        var first = slots.Nth(0);
        var last = slots.Nth(5);

        await first.ClickAsync();
        await first.PressAsync("End");
        await Assertions.Expect(last).ToBeFocusedAsync();

        await last.PressAsync("Home");
        await Assertions.Expect(first).ToBeFocusedAsync();
    }

[Test]
    public async ValueTask Input_otp_default_value_demo_restores_uncontrolled_digits_on_form_reset()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { Has = p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Reset OTP", Exact = true }) }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { Has = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Reset OTP", Exact = true }) }).First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(slots.Nth(0)).ToHaveValueAsync("2");
        await Assertions.Expect(slots.Nth(1)).ToHaveValueAsync("4");
        await Assertions.Expect(slots.Nth(2)).ToHaveValueAsync("6");
        await Assertions.Expect(slots.Nth(3)).ToHaveValueAsync("8");

        await slots.Nth(3).ClickAsync();
        await page.Keyboard.PressAsync("Backspace");

        await Assertions.Expect(slots.Nth(3)).ToHaveValueAsync(string.Empty);

        await section.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Reset OTP", Exact = true }).ClickAsync();

        await Assertions.Expect(slots.Nth(0)).ToHaveValueAsync("2");
        await Assertions.Expect(slots.Nth(1)).ToHaveValueAsync("4");
        await Assertions.Expect(slots.Nth(2)).ToHaveValueAsync("6");
        await Assertions.Expect(slots.Nth(3)).ToHaveValueAsync("8");
        await Assertions.Expect(section.Locator("input[type='hidden'][name='otp-reset']")).ToHaveValueAsync("2468");
    }
}
