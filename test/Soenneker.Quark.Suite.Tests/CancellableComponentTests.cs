using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class CancellableComponentTests
{
    [Test]
    public async Task Work_token_is_cancelled_and_reset_without_resurrection_after_disposal()
    {
        var component = new Probe();
        await component.Cancel();
        var token = component.CancellationToken;
        token.CanBeCanceled.Should().BeTrue();
        token.IsCancellationRequested.Should().BeFalse();
        await component.Cancel();
        token.IsCancellationRequested.Should().BeTrue();
        await component.ResetCancellation();
        var next = component.CancellationToken;
        next.Should().NotBe(token);
        next.IsCancellationRequested.Should().BeFalse();
        await component.DisposeAsync();
        next.IsCancellationRequested.Should().BeTrue();
        component.CancellationToken.Should().Be(CancellationToken.None);
        await component.DisposeAsync();
    }

    [Test]
    public async Task Linked_token_cancels_component_work()
    {
        using var parent = new CancellationTokenSource();
        await using var component = new Probe(parent.Token);
        var token = component.CancellationToken;
        await parent.CancelAsync();
        token.IsCancellationRequested.Should().BeTrue();
    }

    private sealed class Probe(CancellationToken linkedToken = default) : CancellableComponent(linkedToken);
}
