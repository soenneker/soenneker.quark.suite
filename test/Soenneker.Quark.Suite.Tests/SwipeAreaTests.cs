using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
namespace Soenneker.Quark.Suite.Tests;

public sealed class SwipeAreaTests : BunitContext
{
    public SwipeAreaTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddQuarkSwipeAreaAsScoped();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Test]
    public async Task Dispatches_only_requested_directions_and_respects_disabled()
    {
        int left = 0, right = 0, up = 0, down = 0;
        var cut = Render<SwipeArea>(p => p.Add(c => c.OnSwipeLeft, () => left++)
            .Add(c => c.OnSwipeRight, () => right++).Add(c => c.OnSwipeUp, () => up++)
            .Add(c => c.OnSwipeDown, () => down++));
        foreach (string direction in new[] { "left", "right", "up", "down", "invalid" })
            await cut.InvokeAsync(() => cut.Instance.NotifySwipe(direction));
        (left, right, up, down).Should().Be((1, 1, 1, 1));
        cut.Render(p => p.Add(c => c.Disabled, true));
        await cut.InvokeAsync(() => cut.Instance.NotifySwipe("right"));
        right.Should().Be(1);
    }

    [Test]
    public async Task Does_not_overlap_navigation_callbacks()
    {
        int calls = 0;
        var completion = new TaskCompletionSource();
        var cut = Render<SwipeArea>(p => p.Add(c => c.OnSwipeRight, async () => { calls++; await completion.Task; }));
        Task first = cut.InvokeAsync(() => cut.Instance.NotifySwipe("right"));
        await cut.InvokeAsync(() => cut.Instance.NotifySwipe("right"));
        calls.Should().Be(1);
        completion.SetResult(); await first;
        await cut.InvokeAsync(() => cut.Instance.NotifySwipe("right"));
        calls.Should().Be(2);
    }

    [Test]
    public async Task Disposal_removes_listeners_and_ignores_late_callbacks()
    {
        int calls = 0;
        var cut = Render<SwipeArea>(p => p.Add(c => c.OnSwipeLeft, () => calls++));
        await cut.InvokeAsync(() => cut.Instance.DisposeAsync().AsTask());
        await cut.InvokeAsync(() => cut.Instance.NotifySwipe("left"));
        calls.Should().Be(0);
        JSInterop.Invocations.Should().Contain(call => call.Identifier == "destroy");
    }
}
