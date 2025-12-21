using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="ISnackbarInterop"/>
public sealed class SnackbarInterop : ISnackbarInterop
{
    private readonly AsyncInitializer _cssInitializer;

    private const string _cssPath = "_content/Soenneker.Quark.Suite/css/snackbar.css";

    public SnackbarInterop(IResourceLoader resourceLoader)
    {
        var resourceLoader1 = resourceLoader;

        _cssInitializer = new AsyncInitializer(async token =>
        {
            await resourceLoader1.LoadStyle(_cssPath, cancellationToken: token);
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
