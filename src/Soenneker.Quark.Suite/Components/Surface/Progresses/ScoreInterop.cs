using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IScoreInterop"/>
public sealed class ScoreInterop : IScoreInterop
{
    public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
