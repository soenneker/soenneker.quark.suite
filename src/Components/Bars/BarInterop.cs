using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

///<inheritdoc cref="IBarInterop"/>
public sealed class BarInterop : IBarInterop
{
    private readonly AsyncSingleton _horizontalBarInitializer;
    private readonly AsyncSingleton _sidebarInitializer;

    private const string _horizontalBarCssPath = "_content/Soenneker.Quark.Suite/css/bars/horizontalbar.css";
    private const string _verticalBarCssPath = "_content/Soenneker.Quark.Suite/css/bars/verticalbar.css";

    public BarInterop(IResourceLoader resourceLoader)
    {
        _horizontalBarInitializer = new AsyncSingleton(async (token, arg) =>
        {
            await resourceLoader.LoadStyle(_horizontalBarCssPath, cancellationToken: token);
            return new object();
        });

        _sidebarInitializer = new AsyncSingleton(async (token, arg) =>
        {
            await resourceLoader.LoadStyle(_verticalBarCssPath, cancellationToken: token);
            return new object();
        });
    }

    public ValueTask InitializeHorizontalBar(CancellationToken cancellationToken = default)
    {
        return _horizontalBarInitializer.Init(cancellationToken);
    }

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
