using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark.Build;

/// <summary>
/// No-op implementation of <see cref="ISwitchInterop"/> for headless build-time Blazor rendering.
/// </summary>
public sealed class NoOpSwitchInterop : ISwitchInterop
{
    /// <inheritdoc />
    public ValueTask Initialize(CancellationToken cancellationToken = default) => default;

    /// <inheritdoc />
    public ValueTask DisposeAsync() => default;
}
