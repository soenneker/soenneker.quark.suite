using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkCheckboxPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkCheckboxPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Check_demo_indeterminate_parent_and_children_stay_in_sync()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#select-all"),
            expectedTitle: "Checkbox Component - Quark Suite");
        await WaitForInteractive(page);
        await WaitForCheckboxRoot(page, "#select-all");

        var parent = page.Locator("#select-all");
        var read = page.Locator("#item-read");
        var write = page.Locator("#item-write");
        var execute = page.Locator("#item-execute");

        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "mixed");
        await Assertions.Expect(read).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "false");

        await write.ClickAsync();
        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "mixed");

        await execute.ClickAsync();
        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "true");

        await parent.ClickAsync();
        await Assertions.Expect(parent).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(read).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "false");
    }

    [Test]
    public async ValueTask Checkbox_indeterminate_demo_select_all_and_child_updates_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Checkbox, new PageGetByRoleOptions { Name = "Select all", Exact = true }),
            expectedTitle: "Checkbox Component - Quark Suite");
        await WaitForInteractive(page);
        await WaitForCheckboxRoot(page, "#select-all");

        var selectAll = page.Locator("#select-all");
        var read = page.Locator("#item-read");
        var write = page.Locator("#item-write");
        var execute = page.Locator("#item-execute");

        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "mixed");

        await selectAll.ClickAsync();

        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(read).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(execute).ToHaveAttributeAsync("aria-checked", "true");

        await write.ClickAsync();

        await Assertions.Expect(write).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(selectAll).ToHaveAttributeAsync("aria-checked", "mixed");
    }

    [Test]
    public async ValueTask Check_table_demo_select_all_and_row_selection_update_header_state()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#select-all-checkbox"),
            expectedTitle: "Checkbox Component - Quark Suite");
        await WaitForInteractive(page);
        await WaitForCheckboxRoot(page, "#select-all-checkbox");

        var header = page.Locator("#select-all-checkbox");
        var row1 = page.Locator("#row-1-checkbox");
        var row2 = page.Locator("#row-2-checkbox");
        var row4 = page.Locator("#row-4-checkbox");
        var row1Tr = row1.Locator("xpath=ancestor::tr");
        var row2Tr = row2.Locator("xpath=ancestor::tr");

        await Assertions.Expect(header).ToHaveAttributeAsync("aria-checked", "mixed");
        await Assertions.Expect(row1).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row2).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(row1Tr).ToHaveAttributeAsync("data-state", "selected");

        await header.ClickAsync();

        await Assertions.Expect(header).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row2).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row4).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(row2Tr).ToHaveAttributeAsync("data-state", "selected");

        await row2.ClickAsync();

        await Assertions.Expect(header).ToHaveAttributeAsync("aria-checked", "mixed");
        await Assertions.Expect(row2).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(row2Tr).Not.ToHaveAttributeAsync("data-state", "selected");
    }

    [Test]
    public async ValueTask Checkbox_form_multiple_requires_one_selection_and_submits_checked_items()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Submit", Exact = true }).First,
            expectedTitle: "Checkbox Component - Quark Suite");
        await WaitForInteractive(page);
        await WaitForCheckboxRoot(page, "#recents");

        var form = page.Locator("form").Filter(new LocatorFilterOptions { Has = page.Locator("#recents") }).First;
        var recents = page.Locator("#recents");
        var home = page.Locator("#home");
        var submit = form.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Submit", Exact = true });

        await recents.ClickAsync();
        await home.ClickAsync();
        await Assertions.Expect(recents).ToHaveAttributeAsync("aria-checked", "false");
        await Assertions.Expect(home).ToHaveAttributeAsync("aria-checked", "false");
        await submit.ScrollIntoViewIfNeededAsync();
        await submit.ClickAsync();

        await Assertions.Expect(form.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToBeVisibleAsync();

        var downloads = page.Locator("#downloads");
        await downloads.ClickAsync();
        await submit.ScrollIntoViewIfNeededAsync();
        await submit.ClickAsync();

        var json = form.Locator("pre").First;
        await Assertions.Expect(json).ToContainTextAsync("\"downloads\"");
        await Assertions.Expect(form.GetByText("You have to select at least one item.", new LocatorGetByTextOptions { Exact = true })).ToHaveCountAsync(0);
    }

    [Test]
    public async ValueTask Checkbox_keyboard_disabled_and_console_behavior_match_radix()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        List<string> consoleErrors = [];
        page.Console += (_, message) =>
        {
            if (message.Type == "error")
                consoleErrors.Add(message.Text);
        };

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#terms-checkbox"),
            expectedTitle: "Checkbox Component - Quark Suite");
        await WaitForInteractive(page);
        await WaitForCheckboxRoot(page, "#terms-checkbox");

        var checkbox = page.Locator("#terms-checkbox");
        var disabled = page.Locator("#toggle-checkbox");

        await Assertions.Expect(checkbox).ToHaveAttributeAsync("role", "checkbox");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "false");

        await checkbox.FocusAsync();
        await Assertions.Expect(checkbox).ToBeFocusedAsync();

        await page.Keyboard.PressAsync("Enter");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "false");

        await page.Keyboard.PressAsync("Space");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("aria-checked", "true");
        await Assertions.Expect(checkbox).ToHaveAttributeAsync("data-state", "checked");

        await Assertions.Expect(disabled).ToBeDisabledAsync();
        await Assertions.Expect(disabled).ToHaveAttributeAsync("aria-checked", "false");
        await disabled.ClickAsync(new LocatorClickOptions { Force = true });
        await Assertions.Expect(disabled).ToHaveAttributeAsync("aria-checked", "false");

        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Checkbox_hidden_inputs_track_visible_root_size_without_intercepting_pointer_events()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}checks",
            static p => p.Locator("#terms-checkbox"),
            expectedTitle: "Checkbox Component - Quark Suite");
        await WaitForInteractive(page);
        await WaitForCheckboxRoot(page, "#terms-checkbox");
        await WaitForCheckboxRoot(page, "#select-all-checkbox");

        await page.WaitForFunctionAsync(
            """
            () => {
              const roots = Array.from(document.querySelectorAll('[data-slot="checkbox"][data-bradix-checkbox-root]'))
                .filter(root => root.nextElementSibling instanceof HTMLInputElement && root.nextElementSibling.type === 'checkbox');
              if (roots.length < 6) {
                return false;
              }

              return roots.every(root => {
                const input = root.nextElementSibling;
                const rootRect = root.getBoundingClientRect();
                const inputRect = input.getBoundingClientRect();
                const inputStyle = getComputedStyle(input);

                return Math.abs(rootRect.width - inputRect.width) <= 0.5 &&
                  Math.abs(rootRect.height - inputRect.height) <= 0.5 &&
                  Math.round(rootRect.width) === 16 &&
                  Math.round(rootRect.height) === 16 &&
                  inputStyle.position === 'absolute' &&
                  inputStyle.pointerEvents === 'none' &&
                  inputStyle.opacity === '0';
              });
            }
            """);
    }

    private static async Task WaitForInteractive(IPage page)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.WaitForFunctionAsync("() => typeof window.getDotnetRuntime === 'function'");
    }

    private static Task WaitForCheckboxRoot(IPage page, string selector)
    {
        return page.WaitForFunctionAsync(
            "selector => document.querySelector(selector)?.hasAttribute('data-bradix-checkbox-root') === true",
            selector);
    }
}
