using System.Threading.Tasks;
using AwesomeAssertions;
using System.Threading;

namespace Soenneker.Quark.Suite.Tests;

public sealed class CancellableLayoutContractTests
{
    [Test]
    public async ValueTask Cancel_cancels_current_layout_token_and_reset_creates_fresh_token(CancellationToken cancellationToken)
    {
        await using var layout = new TestCancellableLayout();

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
        await using var layout = new TestCancellableLayout();

        await layout.Cancel();

        layout.CancellationToken.IsCancellationRequested.Should().BeFalse();
    }

    private sealed class TestCancellableLayout : CancellableLayout;
}
