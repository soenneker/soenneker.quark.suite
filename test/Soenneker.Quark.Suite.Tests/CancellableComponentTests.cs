using System;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;

namespace Soenneker.Quark.Suite.Tests;

public sealed class CancellableComponentTests
{
    [Test]
    public async ValueTask Work_token_is_cancelled_and_reset_without_resurrection_after_disposal(CancellationToken cancellationToken)
    {
        var component = new CancellableComponentProbe();
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
    public async ValueTask Linked_token_cancels_component_work(CancellationToken cancellationToken)
    {
        using var parent = new CancellationTokenSource();
        await using var component = new CancellableComponentProbe(parent.Token);
        var token = component.CancellationToken;
        await parent.CancelAsync();
        token.IsCancellationRequested.Should().BeTrue();
    }

    [Test]
    public async ValueTask Disposal_without_work_prevents_later_token_creation_and_reset(CancellationToken cancellationToken)
    {
        using var parent = new CancellationTokenSource();
        var component = new CancellableComponentProbe(parent.Token);

        await component.Cancel();
        await component.DisposeAsync();
        await component.ResetCancellation();
        await parent.CancelAsync();

        component.CancellationToken.Should().Be(CancellationToken.None);
    }

    [Test]
    public async ValueTask Reset_before_work_preserves_linked_parent_cancellation(CancellationToken cancellationToken)
    {
        using var parent = new CancellationTokenSource();
        await using var component = new CancellableComponentProbe(parent.Token);

        await component.ResetCancellation();
        CancellationToken token = component.CancellationToken;
        await parent.CancelAsync();

        token.IsCancellationRequested.Should().BeTrue();
        await component.ResetCancellation();
        component.CancellationToken.IsCancellationRequested.Should().BeTrue();
    }

    [Test]
    public void Token_creation_racing_disposal_does_not_resurrect_the_resource()
    {
        Parallel.For(0, 256, _ =>
        {
            var component = new CancellableComponentProbe();
            CancellationToken token = default;
            Parallel.Invoke(() => token = component.CancellationToken, () => component.DisposeAsync().AsTask().GetAwaiter().GetResult());

            if (token.CanBeCanceled)
                token.IsCancellationRequested.Should().BeTrue();

            component.CancellationToken.Should().Be(CancellationToken.None);
            component.ResetCancellation().AsTask().GetAwaiter().GetResult();
            component.CancellationToken.Should().Be(CancellationToken.None);
        });
    }

    [Test]
    public async ValueTask Disposal_completes_when_a_cancellation_callback_throws(CancellationToken cancellationToken)
    {
        var component = new CancellableComponentProbe();
        CancellationToken token = component.CancellationToken;
        using CancellationTokenRegistration registration = token.Register(() => throw new InvalidOperationException("callback failure"));

        await component.DisposeAsync();

        token.IsCancellationRequested.Should().BeTrue();
        component.CancellationToken.Should().Be(CancellationToken.None);
        Action readWaitHandle = () => _ = token.WaitHandle;
        readWaitHandle.Should().Throw<ObjectDisposedException>();
    }
}
