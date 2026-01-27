using System.Threading;
using System.Threading.Tasks;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="ITablesInterop"/>
public sealed class TablesInterop : ITablesInterop
{
    private readonly AsyncInitializer _styleInitializer;
    private readonly CancellationScope _cancellationScope = new();

    private readonly IResourceLoader _resourceLoader;

    public TablesInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _styleInitializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token)
    {
        return _resourceLoader.LoadStyle("_content/Soenneker.Quark.Suite/css/table.css", cancellationToken: token);
    }

    /// <summary>
    /// Initializes table functionality by loading required CSS resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _styleInitializer.Init(linked);
    }

    public async ValueTask DisposeAsync()
    {
        await _styleInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
