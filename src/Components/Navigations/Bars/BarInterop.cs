using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="IBarInterop"/>
public sealed class BarInterop : IBarInterop
{
    private readonly AsyncInitializer _horizontalBarInitializer;
    private readonly AsyncInitializer _sidebarInitializer;

    private const string _horizontalBarCssPath = "_content/Soenneker.Quark.Suite/css/bars/horizontalbar.css";
    private const string _verticalBarCssPath = "_content/Soenneker.Quark.Suite/css/bars/verticalbar.css";

    public BarInterop(IResourceLoader resourceLoader)
    {
        _horizontalBarInitializer = new AsyncInitializer(async token =>
        {
            await resourceLoader.LoadStyle(_horizontalBarCssPath, cancellationToken: token);
        });

        _sidebarInitializer = new AsyncInitializer(async token =>
        {
            await resourceLoader.LoadStyle(_verticalBarCssPath, cancellationToken: token);
        });
    }

    /// <summary>
    /// Initializes horizontal bar (topbar) functionality.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public ValueTask InitializeHorizontalBar(CancellationToken cancellationToken = default)
    {
        return _horizontalBarInitializer.Init(cancellationToken);
    }

    /// <summary>
    /// Initializes vertical bar (sidebar) functionality.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A value task representing the initialization operation.</returns>
    public ValueTask InitializeVerticalBar(CancellationToken cancellationToken = default)
    {
        return _sidebarInitializer.Init(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _horizontalBarInitializer.DisposeAsync();

        await _sidebarInitializer.DisposeAsync();
    }
}
