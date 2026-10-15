using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed class CancellableLayoutContractTests
{
    [Test]
    public async ValueTask Cancel_cancels_current_layout_token_and_reset_creates_fresh_token(CancellationToken cancellationToken)
    {
        await using var layout = new CancellableLayoutProbe();

        var token = layout.CancellationToken;
        token.IsCancellationRequested.Should().BeFalse();

        await layout.Cancel();
        token.IsCancellationRequested.Should().BeTrue();

        await layout.ResetCancellation();
        var resetToken = layout.CancellationToken;

        resetToken.IsCancellationRequested.Should().BeFalse();
        resetToken.Should().NotBe(token);
    }

    [Test]
    public async ValueTask Cancel_before_token_creation_is_noop(CancellationToken cancellationToken)
    {
        await using var layout = new CancellableLayoutProbe();

        await layout.Cancel();

        layout.CancellationToken.IsCancellationRequested.Should().BeFalse();
    }

    [Test]
    public async ValueTask Disposal_before_token_creation_prevents_reset_and_token_resurrection(CancellationToken cancellationToken)
    {
        var layout = new CancellableLayoutProbe();

        await layout.DisposeAsync();
        await layout.Cancel();
        await layout.ResetCancellation();

        layout.CancellationToken.Should().Be(CancellationToken.None);
        await layout.DisposeAsync();
    }

    [Test]
    public async ValueTask Reset_before_token_creation_preserves_linked_parent_cancellation(CancellationToken cancellationToken)
    {
        using var parent = new CancellationTokenSource();
        await using var layout = new CancellableLayoutProbe(parent.Token);

        await layout.ResetCancellation();
        CancellationToken token = layout.CancellationToken;
        await parent.CancelAsync();

        token.IsCancellationRequested.Should().BeTrue();
        await layout.ResetCancellation();
        layout.CancellationToken.IsCancellationRequested.Should().BeTrue();
    }

    [Test]
    public void Token_creation_racing_disposal_never_resurrects_the_layout_work_scope()
    {
        Parallel.For(0, 256, _ =>
        {
            var layout = new CancellableLayoutProbe();
            CancellationToken token = default;
            Parallel.Invoke(() => token = layout.CancellationToken, () => layout.DisposeAsync().AsTask().GetAwaiter().GetResult());

            if (token.CanBeCanceled)
                token.IsCancellationRequested.Should().BeTrue();

            layout.CancellationToken.Should().Be(CancellationToken.None);
            layout.ResetCancellation().AsTask().GetAwaiter().GetResult();
            layout.CancellationToken.Should().Be(CancellationToken.None);
        });
    }

    [Test]
    public async ValueTask Disposal_waits_for_asynchronous_cancellation_callback_completion(CancellationToken cancellationToken)
    {
        var layout = new CancellableLayoutProbe();
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using var release = new ManualResetEventSlim();
        using CancellationTokenRegistration registration = layout.CancellationToken.Register(() =>
        {
            started.TrySetResult();
            release.Wait(cancellationToken);
        });

        ValueTask disposal = layout.DisposeAsync();
        try
        {
            await started.Task.WaitAsync(TimeSpan.FromSeconds(10), cancellationToken);
            disposal.IsCompleted.Should().BeFalse();
            layout.CancellationToken.Should().Be(CancellationToken.None);
        }
        finally
        {
            release.Set();
            await disposal;
        }
    }

    [Test]
    public async ValueTask Disposal_releases_the_source_when_a_callback_throws(CancellationToken cancellationToken)
    {
        var layout = new CancellableLayoutProbe();
        CancellationToken token = layout.CancellationToken;
        using CancellationTokenRegistration registration = token.Register(() => throw new InvalidOperationException("callback failure"));

        await layout.DisposeAsync();

        token.IsCancellationRequested.Should().BeTrue();
        layout.CancellationToken.Should().Be(CancellationToken.None);
        Action readWaitHandle = () => _ = token.WaitHandle;
        readWaitHandle.Should().Throw<ObjectDisposedException>();
    }

    [Test]
    public void Concurrent_first_readers_and_reset_cannot_publish_work_after_disposal()
    {
        Parallel.For(0, 128, iteration =>
        {
            using var parent = new CancellationTokenSource();
            var layout = new CancellableLayoutProbe(parent.Token);
            var tokens = new CancellationToken[3];

            Parallel.Invoke(
                () => tokens[0] = layout.CancellationToken,
                () => tokens[1] = layout.CancellationToken,
                () =>
                {
                    layout.ResetCancellation().AsTask().GetAwaiter().GetResult();
                    tokens[2] = layout.CancellationToken;
                },
                () => layout.DisposeAsync().AsTask().GetAwaiter().GetResult());

            foreach (CancellationToken token in tokens)
            {
                if (!token.CanBeCanceled)
                    continue;

                token.IsCancellationRequested.Should().BeTrue();
                Action readWaitHandle = () => _ = token.WaitHandle;
                readWaitHandle.Should().Throw<ObjectDisposedException>();
            }

            layout.ResetCancellation().AsTask().GetAwaiter().GetResult();
            layout.CancellationToken.Should().Be(CancellationToken.None);
        });
    }
}
