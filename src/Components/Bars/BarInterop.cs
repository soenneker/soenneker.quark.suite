using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

///<inheritdoc cref="IBarInterop"/>
public sealed class BarInterop : IBarInterop
{
    private readonly AsyncSingleton _cssInitializer;

    private const string _cssPath = "_content/Soenneker.Quark.Suite/css/bar.css";

    public BarInterop(IResourceLoader resourceLoader)
    {
        _cssInitializer = new AsyncSingleton(async (token, arg) =>
        {
            await resourceLoader.LoadStyle(_cssPath, cancellationToken: token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _cssInitializer.Init(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        return _cssInitializer.DisposeAsync();
    }
}
