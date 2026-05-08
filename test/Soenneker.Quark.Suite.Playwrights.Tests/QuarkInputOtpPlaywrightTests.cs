using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkInputOtpPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkInputOtpPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Input_otp_demo_matches_shadcn_slot_accessibility_focus_and_has_no_console_errors()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        var sawPageError = false;

        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        page.PageError += (_, _) => sawPageError = true;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Accessible one-time password component with copy-paste functionality." }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Accessible one-time password component with copy-paste functionality." }).First;
        var root = section.Locator("[role='group']").First;
        var input = section.Locator("input[data-slot='input-otp']").First;
        var slots = section.Locator("[data-slot='input-otp-slot']");
        var firstSlot = slots.Nth(0);

        await Assertions.Expect(root).ToHaveAttributeAsync("role", "group");
        await Assertions.Expect(input).ToHaveAttributeAsync("autocomplete", "one-time-code");
        await Assertions.Expect(input).ToHaveAttributeAsync("maxlength", "6");

        (await firstSlot.EvaluateAsync<double>("element => element.getBoundingClientRect().width")).Should().BeApproximately(32, 0.5);
        (await firstSlot.EvaluateAsync<double>("element => element.getBoundingClientRect().height")).Should().BeApproximately(32, 0.5);
        (await firstSlot.EvaluateAsync<string>("element => getComputedStyle(element).textAlign")).Should().Be("start");

        await Assertions.Expect(input).ToHaveValueAsync("123456");
        await Assertions.Expect(slots.Nth(0)).ToHaveTextAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveTextAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveTextAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToHaveTextAsync("4");
        await Assertions.Expect(slots.Nth(4)).ToHaveTextAsync("5");
        await Assertions.Expect(slots.Nth(5)).ToHaveTextAsync("6");

        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

[Test]
    public async ValueTask Input_otp_digits_only_demo_distributes_pasted_digits_and_filters_non_numeric_characters()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Four Digits" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Four Digits" }).First;
        var input = section.Locator("input[data-slot='input-otp']").First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await input.FillAsync("");
        await input.ClickAsync();
        await Assertions.Expect(slots.First).ToHaveAttributeAsync("data-active", "true");
        await page.Keyboard.TypeAsync("12A3");

        await Assertions.Expect(input).ToHaveValueAsync("123");
        await Assertions.Expect(slots.Nth(0)).ToHaveTextAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveTextAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveTextAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToBeEmptyAsync();
    }

[Test]
    public async ValueTask Input_otp_seeded_examples_render_initial_bound_values()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Separator" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var separator = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Separator" }).First;
        var separatorSlots = separator.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(separatorSlots.Nth(0)).ToHaveTextAsync("1");
        await Assertions.Expect(separatorSlots.Nth(1)).ToHaveTextAsync("2");
        await Assertions.Expect(separatorSlots.Nth(2)).ToHaveTextAsync("3");
        await Assertions.Expect(separatorSlots.Nth(3)).ToHaveTextAsync("4");
        await Assertions.Expect(separatorSlots.Nth(4)).ToHaveTextAsync("5");
        await Assertions.Expect(separatorSlots.Nth(5)).ToHaveTextAsync("6");

        var alphaNumeric = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Alphanumeric" }).First;
        var alphaSlots = alphaNumeric.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(alphaSlots.Nth(0)).ToHaveTextAsync("A");
        await Assertions.Expect(alphaSlots.Nth(1)).ToHaveTextAsync("9");
        await Assertions.Expect(alphaSlots.Nth(2)).ToBeEmptyAsync();
    }

[Test]
    public async ValueTask Input_otp_disabled_demo_preserves_existing_value_and_blocks_edits()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use the disabled prop to disable the input." }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use the disabled prop to disable the input." }).First;
        var input = section.Locator("input[data-slot='input-otp']").First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(input).ToBeDisabledAsync();
        await Assertions.Expect(slots.Nth(0)).ToHaveTextAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveTextAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveTextAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToHaveTextAsync("4");
        await Assertions.Expect(slots.Nth(4)).ToHaveTextAsync("5");
        await Assertions.Expect(slots.Nth(5)).ToHaveTextAsync("6");

        await slots.Nth(2).ClickAsync(new LocatorClickOptions { Force = true });
        await page.Keyboard.PressAsync("Backspace");
        await page.Keyboard.InsertTextAsync("9");

        await Assertions.Expect(input).ToHaveValueAsync("123456");
        await Assertions.Expect(slots.Nth(0)).ToHaveTextAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveTextAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveTextAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToHaveTextAsync("4");
        await Assertions.Expect(slots.Nth(4)).ToHaveTextAsync("5");
        await Assertions.Expect(slots.Nth(5)).ToHaveTextAsync("6");
    }

[Test]
    public async ValueTask Input_otp_controlled_demo_keeps_slots_in_sync_with_typed_value()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use the value and onChange props to control the input value." }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Use the value and onChange props to control the input value." }).First;
        var input = section.Locator("input[data-slot='input-otp']").First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await input.ClickAsync();
        await page.Keyboard.TypeAsync("482917");

        await Assertions.Expect(input).ToHaveValueAsync("482917");
        await Assertions.Expect(slots.Nth(0)).ToHaveTextAsync("4");
        await Assertions.Expect(slots.Nth(1)).ToHaveTextAsync("8");
        await Assertions.Expect(slots.Nth(2)).ToHaveTextAsync("2");
        await Assertions.Expect(slots.Nth(3)).ToHaveTextAsync("9");
        await Assertions.Expect(slots.Nth(4)).ToHaveTextAsync("1");
        await Assertions.Expect(slots.Nth(5)).ToHaveTextAsync("7");
    }

[Test]
    public async ValueTask Input_otp_seeded_demo_marks_last_filled_slot_active_when_focused()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "Separator" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "Separator" }).First;
        var input = section.Locator("input[data-slot='input-otp']").First;
        var slots = section.Locator("[data-slot='input-otp-slot']");
        var last = slots.Nth(5);

        await input.ClickAsync();
        await Assertions.Expect(input).ToBeFocusedAsync();
        await Assertions.Expect(last).ToHaveAttributeAsync("data-active", "true");
    }

[Test]
    public async ValueTask Input_otp_rtl_demo_renders_seeded_value_in_rtl_preview()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/input-otp",
            static p => p.Locator("section").Filter(new LocatorFilterOptions { HasText = "رمز التحقق" }).Locator("[data-slot='input-otp-slot']").First,
            expectedTitle: "Input OTP - Quark Suite");

        var section = page.Locator("section").Filter(new LocatorFilterOptions { HasText = "رمز التحقق" }).First;
        var input = section.Locator("input[data-slot='input-otp']").First;
        var slots = section.Locator("[data-slot='input-otp-slot']");

        await Assertions.Expect(input).ToHaveAttributeAsync("inputmode", "numeric");
        await Assertions.Expect(input).ToHaveValueAsync("123456");
        await Assertions.Expect(slots.Nth(0)).ToHaveTextAsync("1");
        await Assertions.Expect(slots.Nth(1)).ToHaveTextAsync("2");
        await Assertions.Expect(slots.Nth(2)).ToHaveTextAsync("3");
        await Assertions.Expect(slots.Nth(3)).ToHaveTextAsync("4");
        await Assertions.Expect(slots.Nth(4)).ToHaveTextAsync("5");
        await Assertions.Expect(slots.Nth(5)).ToHaveTextAsync("6");
    }
}
