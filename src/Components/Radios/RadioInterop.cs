using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

///<inheritdoc cref="IRadioInterop"/>
public sealed class RadioInterop : IRadioInterop
{
    private readonly AsyncSingleton _initializer;

    public RadioInterop(IResourceLoader resourceLoader)
    {
        var resourceLoader1 = resourceLoader;

        _initializer = new AsyncSingleton(async (token, _) =>
        {
            await resourceLoader1.LoadStyle("_content/Soenneker.Quark.Suite/css/radio.css", cancellationToken: token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}
