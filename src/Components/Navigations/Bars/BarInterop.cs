using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="IBarInterop"/>
public sealed class BarInterop : IBarInterop
{
    private readonly AsyncInitializer _horizontalBarInitializer;
    private readonly AsyncInitializer _sidebarInitializer;
    private readonly IResourceLoader _resourceLoader;
    private readonly CancellationScope _cancellationScope = new();

    private const string _horizontalBarCssPath = "_content/Soenneker.Quark.Suite/css/bars/horizontalbar.css";
    private const string _verticalBarCssPath = "_content/Soenneker.Quark.Suite/css/bars/verticalbar.css";

    public BarInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _horizontalBarInitializer = new AsyncInitializer(InitializeHorizontalBarScript);
        _sidebarInitializer = new AsyncInitializer(InitializeSidebarScript);
    }

    private ValueTask InitializeHorizontalBarScript(CancellationToken token)
    {
        return _resourceLoader.LoadStyle(_horizontalBarCssPath, cancellationToken: token);
    }

    private ValueTask InitializeSidebarScript(CancellationToken token)
    {
        return _resourceLoader.LoadStyle(_verticalBarCssPath, cancellationToken: token);
    }

    /// <summary>
    /// Initializes horizontal bar (topbar) functionality.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public async ValueTask InitializeHorizontalBar(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _horizontalBarInitializer.Init(linked);
    }

    /// <summary>
    /// Initializes vertical bar (sidebar) functionality.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public async ValueTask InitializeVerticalBar(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
            await _sidebarInitializer.Init(linked);
    }

    public async ValueTask DisposeAsync()
    {
        await _horizontalBarInitializer.DisposeAsync();

        await _sidebarInitializer.DisposeAsync();

        await _cancellationScope.DisposeAsync();
    }
}
