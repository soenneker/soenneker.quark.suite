using System.Threading.Tasks;
using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Tests;

public sealed class InfiniteScrollTests : BunitContext
{
    public InfiniteScrollTests()
    {
        Services.AddLogging();
        Services.AddDefaultQuarkOptionsAsScoped();
        Services.AddQuarkInfiniteScrollAsScoped();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Test]
    public async Task Prevents_overlapping_requests_and_rearms_after_completion()
    {
        var completion = new TaskCompletionSource();
        int calls = 0;
        var cut = Render<InfiniteScroll>(p => p.Add(c => c.HasMore, true)
            .Add(c => c.LoadMore, async () => { calls++; await completion.Task; })
            .Add(c => c.LoadingContent, builder => builder.AddContent(0, "Loading")));
        Task first = cut.InvokeAsync(() => cut.Instance.RequestMore());
        cut.WaitForAssertion(() => cut.Find("[role=status]").TextContent.Should().Be("Loading"));
        await cut.InvokeAsync(() => cut.Instance.RequestMore());
        calls.Should().Be(1);
        completion.SetResult();
        await first;
        cut.WaitForAssertion(() => cut.FindAll("[role=status]").Count.Should().Be(0));
        await cut.InvokeAsync(() => cut.Instance.RequestMore());
        calls.Should().Be(2);
    }

    [Test]
    public async Task Stops_when_exhausted_loading_or_disabled()
    {
        int calls = 0;
        var cut = Render<InfiniteScroll>(p => p.Add(c => c.LoadMore, () => calls++));
        await cut.InvokeAsync(() => cut.Instance.RequestMore());
        cut.Render(p => p.Add(c => c.HasMore, true).Add(c => c.Loading, true));
        await cut.InvokeAsync(() => cut.Instance.RequestMore());
        cut.Render(p => p.Add(c => c.Loading, false).Add(c => c.Disabled, true));
        await cut.InvokeAsync(() => cut.Instance.RequestMore());
        calls.Should().Be(0);
        cut.Render(p => p.Add(c => c.Disabled, false));
        await cut.InvokeAsync(() => cut.Instance.RequestMore());
        calls.Should().Be(1);
    }

    [Test]
    public async Task Disposal_disconnects_observation()
    {
        var cut = Render<InfiniteScroll>(p => p.Add(c => c.HasMore, true)
            .Add(c => c.LoadMore, () => { }));
        await cut.InvokeAsync(() => cut.Instance.DisposeAsync().AsTask());
        JSInterop.Invocations.Should().Contain(call => call.Identifier == "destroy");
    }
}
