using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using Soenneker.Playwrights.Extensions.TestPages;
using Soenneker.Playwrights.Tests.Unit;

namespace Soenneker.Quark.Suite.Playwrights.Tests;

[ClassDataSource<QuarkPlaywrightHost>(Shared = SharedType.PerTestSession)]
public sealed class QuarkFloatingWindowPlaywrightTests : QuarkPlaywrightTest
{
    public QuarkFloatingWindowPlaywrightTests(QuarkPlaywrightHost host) : base(host)
    {
    }

    [Test]
    public async ValueTask Visibility_focus_and_callbacks_follow_the_open_state()
    {
        await using var session = await CreateHarnessSession();
        var page = session.Page;
        var window = page.Locator("#floating-window-a");

        await page.GetByTestId("open-a").ClickAsync();

        await Assertions.Expect(window).ToBeVisibleAsync();
        await Assertions.Expect(window).ToBeFocusedAsync();
        await Assertions.Expect(window).ToHaveAttributeAsync("data-state", "open");
        await Assertions.Expect(page.GetByTestId("a-show-count")).ToHaveTextAsync("1");
        var initialBox = await window.BoundingBoxAsync();
        initialBox.Should().NotBeNull();
        initialBox!.X.Should().BeApproximately(40, 0.5f);
        initialBox.Y.Should().BeApproximately(50, 0.5f);

        await window.GetByRole(AriaRole.Button, new LocatorGetByRoleOptions { Name = "Close", Exact = true }).ClickAsync();

        await Assertions.Expect(window).Not.ToBeVisibleAsync();
        await Assertions.Expect(window).ToHaveAttributeAsync("data-state", "closed");
        await Assertions.Expect(page.GetByTestId("a-hide-count")).ToHaveTextAsync("1");

        await page.GetByTestId("toggle-focus").ClickAsync();
        await page.GetByTestId("open-a").ClickAsync();

        await Assertions.Expect(window).ToBeVisibleAsync();
        await Assertions.Expect(window).Not.ToBeFocusedAsync();
        await Assertions.Expect(page.GetByTestId("open-a")).ToBeFocusedAsync();
        await Assertions.Expect(page.GetByTestId("a-show-count")).ToHaveTextAsync("2");
    }

    [Test]
    public async ValueTask Touch_pointer_drag_and_resize_update_geometry_and_callbacks()
    {
        await using var session = await CreateHarnessSession();
        var page = session.Page;
        await page.GetByTestId("open-a").ClickAsync();

        var window = page.Locator("#floating-window-a");
        var titleBar = window.Locator("[data-slot='floating-window-titlebar']");
        var beforeDrag = await window.BoundingBoxAsync();
        var titleBox = await titleBar.BoundingBoxAsync();
        beforeDrag.Should().NotBeNull();
        titleBox.Should().NotBeNull();

        await DispatchTouchPointer(titleBar, "pointerdown", 41, titleBox!.X + 20, titleBox.Y + 20, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 41, titleBox.X + 100, titleBox.Y + 80, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 41, titleBox.X + 100, titleBox.Y + 80, 0);

        var afterDrag = await window.BoundingBoxAsync();
        afterDrag.Should().NotBeNull();
        afterDrag!.X.Should().BeGreaterThan(beforeDrag!.X + 50);
        afterDrag.Y.Should().BeGreaterThan(beforeDrag.Y + 30);
        await Assertions.Expect(page.GetByTestId("a-drag-start-count")).ToHaveTextAsync("1");
        await Assertions.Expect(page.GetByTestId("a-drag-end-count")).ToHaveTextAsync("1");

        var resizeHandle = window.Locator("[data-direction='se']");
        var handleBox = await resizeHandle.BoundingBoxAsync();
        handleBox.Should().NotBeNull();

        await DispatchTouchPointer(resizeHandle, "pointerdown", 42, handleBox!.X + 2, handleBox.Y + 2, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 42, handleBox.X + 72, handleBox.Y + 52, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 42, handleBox.X + 72, handleBox.Y + 52, 0);

        var afterResize = await window.BoundingBoxAsync();
        afterResize.Should().NotBeNull();
        afterResize!.Width.Should().BeGreaterThan(afterDrag.Width + 40);
        afterResize.Height.Should().BeGreaterThan(afterDrag.Height + 30);
    }

    [Test]
    public async ValueTask Stacking_and_runtime_option_updates_take_effect_without_recreation()
    {
        await using var session = await CreateHarnessSession();
        var page = session.Page;
        await page.GetByTestId("open-a").ClickAsync();
        await page.GetByTestId("open-b").ClickAsync();

        var windowA = page.Locator("#floating-window-a");
        var windowB = page.Locator("#floating-window-b");
        var titleA = windowA.Locator("[data-slot='floating-window-titlebar']");
        var titleBox = await titleA.BoundingBoxAsync();
        titleBox.Should().NotBeNull();

        await DispatchTouchPointer(titleA, "pointerdown", 43, titleBox!.X + 15, titleBox.Y + 15, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 43, titleBox.X + 15, titleBox.Y + 15, 0);

        var zA = await windowA.EvaluateAsync<int>("element => Number(element.style.zIndex)");
        var zB = await windowB.EvaluateAsync<int>("element => Number(element.style.zIndex)");
        zA.Should().BeGreaterThan(zB);

        await page.GetByTestId("toggle-draggable").ClickAsync();
        var beforeDisabledDrag = await windowA.BoundingBoxAsync();
        await DispatchTouchPointer(titleA, "pointerdown", 44, titleBox.X + 15, titleBox.Y + 15, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 44, titleBox.X + 100, titleBox.Y + 90, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 44, titleBox.X + 100, titleBox.Y + 90, 0);
        var afterDisabledDrag = await windowA.BoundingBoxAsync();
        afterDisabledDrag!.X.Should().BeApproximately(beforeDisabledDrag!.X, 0.5f);
        afterDisabledDrag.Y.Should().BeApproximately(beforeDisabledDrag.Y, 0.5f);

        await page.GetByTestId("toggle-resizable").ClickAsync();
        await Assertions.Expect(windowA.Locator("[data-direction='se']")).ToHaveCountAsync(0);
        await page.GetByTestId("toggle-resizable").ClickAsync();
        await Assertions.Expect(windowA.Locator("[data-direction='se']")).ToHaveCountAsync(1);
    }

    [Test]
    public async ValueTask Viewport_resize_keeps_a_constrained_window_fully_visible()
    {
        await using var session = await CreateHarnessSession();
        var page = session.Page;
        await page.SetViewportSizeAsync(900, 700);
        await page.GetByTestId("open-a").ClickAsync();
        await page.GetByTestId("move-a-to-edge").ClickAsync();

        await page.SetViewportSizeAsync(480, 360);
        var window = page.Locator("#floating-window-a");
        await Assertions.Expect(window).ToBeVisibleAsync();
        await page.WaitForTimeoutAsync(100);

        var box = await window.BoundingBoxAsync();
        box.Should().NotBeNull();
        box!.X.Should().BeGreaterThanOrEqualTo(0);
        box.Y.Should().BeGreaterThanOrEqualTo(0);
        (box.X + box.Width).Should().BeLessThanOrEqualTo(480.5f);
        (box.Y + box.Height).Should().BeLessThanOrEqualTo(360.5f);
    }

    [Test]
    public async ValueTask Rapid_centered_show_then_hide_cancels_the_pending_open_frame()
    {
        await using var session = await CreateHarnessSession();
        var page = session.Page;
        var window = page.Locator("#floating-window-centered");

        await page.GetByTestId("race-centered").ClickAsync();
        await page.WaitForTimeoutAsync(100);

        await Assertions.Expect(window).Not.ToBeVisibleAsync();
        await Assertions.Expect(window).ToHaveAttributeAsync("data-state", "closed");
        await Assertions.Expect(page.GetByTestId("centered-show-count")).ToHaveTextAsync("0");
        await Assertions.Expect(page.GetByTestId("centered-hide-count")).ToHaveTextAsync("1");
    }

    [Test]
    public async ValueTask Runtime_enable_and_draggable_changes_rebind_behavior()
    {
        await using var session = await CreateHarnessSession();
        var page = session.Page;
        var window = page.Locator("#floating-window-a");
        var titleBar = window.Locator("[data-slot='floating-window-titlebar']");

        await page.GetByTestId("open-a").ClickAsync();
        await Assertions.Expect(window).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByTestId("a-show-count")).ToHaveTextAsync("1");

        await page.GetByTestId("toggle-enabled").ClickAsync();
        await Assertions.Expect(window).Not.ToBeVisibleAsync();
        await Assertions.Expect(window).ToHaveAttributeAsync("data-state", "closed");

        await page.GetByTestId("toggle-enabled").ClickAsync();
        await Assertions.Expect(window).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByTestId("a-show-count")).ToHaveTextAsync("2");

        await page.GetByTestId("toggle-draggable").ClickAsync();
        var disabledStart = await window.BoundingBoxAsync();
        var titleBox = await titleBar.BoundingBoxAsync();
        disabledStart.Should().NotBeNull();
        titleBox.Should().NotBeNull();

        await DispatchTouchPointer(titleBar, "pointerdown", 51, titleBox!.X + 20, titleBox.Y + 20, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 51, titleBox.X + 100, titleBox.Y + 80, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 51, titleBox.X + 100, titleBox.Y + 80, 0);

        var disabledEnd = await window.BoundingBoxAsync();
        disabledEnd!.X.Should().BeApproximately(disabledStart!.X, 0.5f);
        disabledEnd.Y.Should().BeApproximately(disabledStart.Y, 0.5f);

        await page.GetByTestId("toggle-draggable").ClickAsync();
        titleBox = await titleBar.BoundingBoxAsync();
        titleBox.Should().NotBeNull();

        await DispatchTouchPointer(titleBar, "pointerdown", 52, titleBox!.X + 20, titleBox.Y + 20, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 52, titleBox.X + 100, titleBox.Y + 80, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 52, titleBox.X + 100, titleBox.Y + 80, 0);

        var reboundEnd = await window.BoundingBoxAsync();
        reboundEnd!.X.Should().BeGreaterThan(disabledEnd.X + 50);
        reboundEnd.Y.Should().BeGreaterThan(disabledEnd.Y + 30);
        await Assertions.Expect(page.GetByTestId("a-drag-start-count")).ToHaveTextAsync("1");
        await Assertions.Expect(page.GetByTestId("a-drag-end-count")).ToHaveTextAsync("1");
    }

    [Test]
    public async ValueTask North_and_west_resize_handles_enforce_minimum_and_maximum_dimensions()
    {
        await using var session = await CreateHarnessSession();
        var page = session.Page;
        await page.GetByTestId("open-bounded").ClickAsync();

        var window = page.Locator("#floating-window-bounded");
        await Assertions.Expect(window).ToBeVisibleAsync();
        await page.WaitForTimeoutAsync(200);

        var westHandle = window.Locator("[data-direction='w']");
        var westBox = await westHandle.BoundingBoxAsync();
        westBox.Should().NotBeNull();

        await DispatchTouchPointer(westHandle, "pointerdown", 61, westBox!.X + 2, westBox.Y + 20, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 61, westBox.X - 198, westBox.Y + 20, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 61, westBox.X - 198, westBox.Y + 20, 0);

        (await window.EvaluateAsync<int>("element => element.offsetWidth")).Should().Be(360);
        (await window.EvaluateAsync<int>("element => parseInt(element.style.left, 10)")).Should().Be(360);

        westBox = await westHandle.BoundingBoxAsync();
        westBox.Should().NotBeNull();
        await DispatchTouchPointer(westHandle, "pointerdown", 62, westBox!.X + 2, westBox.Y + 20, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 62, westBox.X + 302, westBox.Y + 20, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 62, westBox.X + 302, westBox.Y + 20, 0);

        (await window.EvaluateAsync<int>("element => element.offsetWidth")).Should().Be(240);
        (await window.EvaluateAsync<int>("element => parseInt(element.style.left, 10)")).Should().Be(480);

        var northHandle = window.Locator("[data-direction='n']");
        var northBox = await northHandle.BoundingBoxAsync();
        northBox.Should().NotBeNull();
        await DispatchTouchPointer(northHandle, "pointerdown", 63, northBox!.X + 20, northBox.Y + 2, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 63, northBox.X + 20, northBox.Y - 98, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 63, northBox.X + 20, northBox.Y - 98, 0);

        (await window.EvaluateAsync<int>("element => element.offsetHeight")).Should().Be(260);
        (await window.EvaluateAsync<int>("element => parseInt(element.style.top, 10)")).Should().Be(260);

        northBox = await northHandle.BoundingBoxAsync();
        northBox.Should().NotBeNull();
        await DispatchTouchPointer(northHandle, "pointerdown", 64, northBox!.X + 20, northBox.Y + 2, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointermove", 64, northBox.X + 20, northBox.Y + 302, 1);
        await DispatchTouchPointer(page.Locator("body"), "pointerup", 64, northBox.X + 20, northBox.Y + 302, 0);

        (await window.EvaluateAsync<int>("element => element.offsetHeight")).Should().Be(180);
        (await window.EvaluateAsync<int>("element => parseInt(element.style.top, 10)")).Should().Be(340);
    }

    private async ValueTask<Soenneker.Playwrights.Session.BrowserSession> CreateHarnessSession()
    {
        var session = await CreateSession();
        await session.Page.GotoAndWaitForReady(
            $"{BaseUrl}test/floating-window",
            static page => page.GetByTestId("open-a"),
            expectedTitle: "Floating Window Test Harness");
        return session;
    }

    private static async Task DispatchTouchPointer(ILocator target, string type, int pointerId, double x, double y, int buttons)
    {
        await target.DispatchEventAsync(type, new
        {
            pointerId,
            pointerType = "touch",
            isPrimary = true,
            button = 0,
            buttons,
            clientX = x,
            clientY = y,
            bubbles = true,
            cancelable = true
        });
    }
}
