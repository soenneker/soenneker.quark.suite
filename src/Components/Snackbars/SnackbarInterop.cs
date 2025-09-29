using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

///<inheritdoc cref="ISnackbarInterop"/>
public sealed class SnackbarInterop : ISnackbarInterop
{
    private readonly AsyncSingleton _cssInitializer;

    private const string _cssPath = "_content/Soenneker.Quark.Snackbars/css/snackbar.css";

    public SnackbarInterop(IResourceLoader resourceLoader)
    {
        IResourceLoader resourceLoader1 = resourceLoader;

        _cssInitializer = new AsyncSingleton(async (token, arg) =>
        {
            await resourceLoader1.LoadStyle(_cssPath, cancellationToken: token);
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
