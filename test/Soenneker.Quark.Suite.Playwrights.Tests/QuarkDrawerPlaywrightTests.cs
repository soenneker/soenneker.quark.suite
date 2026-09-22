using System.Threading.Tasks;
using System.Collections.Generic;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkDrawerPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkDrawerPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

[Test]
    public async ValueTask Drawer_demo_updates_goal_state_and_cancel_restores_trigger_focus()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/drawer",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true }));

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Move Goal", Exact = true });
        var content = page.Locator("[data-slot='drawer-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Calories/day" }).First;
        var handle = content.Locator("[data-slot='drawer-handle']");
        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-vaul-drawer-direction", "bottom");
        await Assertions.Expect(content).ToContainTextAsync("350");
        await Assertions.Expect(handle).ToBeVisibleAsync();

        var contentBox = await content.BoundingBoxAsync();
        contentBox.Should().NotBeNull();
        contentBox!.Height.Should().BeLessThanOrEqualTo(page.ViewportSize!.Height * 0.5f + 1);

        var handleBox = await handle.BoundingBoxAsync();
        handleBox.Should().NotBeNull();
        handleBox!.Height.Should().BeInRange(7, 9);

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Increase", Exact = true }).ClickAsync();
        await Assertions.Expect(content).ToContainTextAsync("360");

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Decrease", Exact = true }).ClickAsync();
        await Assertions.Expect(content).ToContainTextAsync("350");

        await dialog.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Cancel", Exact = true }).ClickAsync();

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
        await page.WaitForFunctionAsync("() => document.body.style.overflow === '' && document.body.style.paddingRight === ''");
        (await page.EvaluateAsync<string>("() => document.body.style.overflow")).Should().BeEmpty();
        (await page.EvaluateAsync<string>("() => document.body.style.paddingRight")).Should().BeEmpty();
    }

    [Test]
    public async ValueTask Drawer_demo_layers_above_page_escapes_and_has_no_console_errors()
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
            $"{BaseUrl}components/drawer",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true }));

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Open Drawer", Exact = true });
        await trigger.ClickAsync();

        var dialog = page.GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = "Move Goal", Exact = true });
        var content = page.Locator("[data-slot='drawer-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Calories/day" }).First;
        var overlay = page.Locator("[data-slot='drawer-overlay'][data-state='open']").First;

        await Assertions.Expect(dialog).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-vaul-drawer-direction", "bottom");
        (await content.EvaluateAsync<int>("element => Number(getComputedStyle(element).zIndex)")).Should().BeGreaterThanOrEqualTo(50);
        (await overlay.EvaluateAsync<int>("element => Number(getComputedStyle(element).zIndex)")).Should().BeGreaterThanOrEqualTo(50);

        await page.Keyboard.PressAsync("Escape");

        await Assertions.Expect(dialog).Not.ToBeVisibleAsync();
        await Assertions.Expect(trigger).ToBeFocusedAsync();
        sawPageError.Should().BeFalse();
        consoleErrors.Should().BeEmpty();
    }

    [Test]
    public async ValueTask Drawer_right_slides_full_width_and_stays_closed_after_backdrop_dismissal()
    {
        await using var session = await CreateSession();
        var page = session.Page;
        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/drawer",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Scrollable Content", Exact = true }));

        var trigger = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Scrollable Content", Exact = true });
        var content = page.Locator("[data-slot='drawer-content']").Filter(new LocatorFilterOptions { HasText = "Excepteur sint occaecat" }).First;
        for (var attempt = 0; attempt < 3; attempt++)
        {
            await trigger.ClickAsync();
            await Assertions.Expect(content).ToBeVisibleAsync();
            (await content.EvaluateAsync<bool>("""
                element => {
                    const animation = element.getAnimations()[0];
                    const time = animation.currentTime;
                    animation.currentTime = 0;
                    const translation = new DOMMatrixReadOnly(getComputedStyle(element).transform).m41;
                    const width = element.getBoundingClientRect().width;
                    animation.currentTime = time;
                    return Math.abs(translation - width) < 1;
                }
                """)).Should().BeTrue();
            (await content.EvaluateAsync<string>("element => getComputedStyle(element).animationFillMode")).Should().Be("both");
            (await content.EvaluateAsync<string>("element => getComputedStyle(element).animationDuration")).Should().Be("0.3s");

            // Observe the end of the exit animation before Blazor removes the element.
            await content.EvaluateAsync("""
                element => {
                    window.drawerExitStyle = null;
                    element.addEventListener('animationend', event => {
                        if (event.target !== element || element.dataset.state !== 'closed') return;
                        const style = getComputedStyle(element);
                        window.drawerExitStyle = {
                            translation: new DOMMatrixReadOnly(style.transform).m41,
                            width: element.getBoundingClientRect().width
                        };
                    });
                }
                """);
            await page.Locator("[data-slot='drawer-overlay'][data-state='open']").ClickAsync(new LocatorClickOptions
            {
                Position = new Position { X = 5, Y = 5 }
            });
            await page.WaitForFunctionAsync("() => window.drawerExitStyle !== null");
            (await page.EvaluateAsync<bool>("() => window.drawerExitStyle.translation >= window.drawerExitStyle.width - 1")).Should().BeTrue();
            await Assertions.Expect(page.Locator("[data-slot='drawer-content']")).ToHaveCountAsync(0);
            await Assertions.Expect(trigger).ToBeFocusedAsync();
        }
    }

    [Test]
    public async ValueTask Drawer_scrollable_demo_opens_from_right_with_full_lorem_content()
    {
        await using var session = await CreateSession();
        var page = session.Page;

        await page.GotoAndWaitForReady(
            $"{BaseUrl}components/drawer",
            static p => p.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Scrollable Content", Exact = true }));

        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Scrollable Content", Exact = true }).ClickAsync();

        var content = page.Locator("[data-slot='drawer-content'][data-state='open']").Filter(new LocatorFilterOptions { HasText = "Excepteur sint occaecat" }).First;
        await Assertions.Expect(content).ToBeVisibleAsync();
        await Assertions.Expect(content).ToHaveAttributeAsync("data-vaul-drawer-direction", "right");
        await Assertions.Expect(content).ToContainTextAsync("mollit anim id est laborum.");

        var contentBox = await content.BoundingBoxAsync();
        contentBox.Should().NotBeNull();
        contentBox!.Height.Should().BeGreaterThan(contentBox.Width);

        await page.Keyboard.PressAsync("Escape");
        await Assertions.Expect(content).Not.ToBeVisibleAsync();
    }
}
