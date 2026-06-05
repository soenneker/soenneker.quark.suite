using System.Threading;
using System.Threading.Tasks;
using Soenneker.Asyncs.Initializers;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="ITablesInterop"/>
public sealed class TablesInterop : ITablesInterop
{
    private readonly AsyncInitializer _styleInitializer;
    private readonly CancellationScope _cancellationScope = new();

    public TablesInterop()
    {
        _styleInitializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token)
    {
        return ValueTask.CompletedTask;
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _styleInitializer.Init(linked);
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _styleInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
