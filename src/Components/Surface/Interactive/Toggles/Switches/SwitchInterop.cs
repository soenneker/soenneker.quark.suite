using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="ISwitchInterop"/>
public sealed class SwitchInterop : ISwitchInterop
{
    private readonly AsyncInitializer _initializer;

    public SwitchInterop(IResourceLoader resourceLoader)
    {
        _initializer = new AsyncInitializer(async token =>
        {
            await resourceLoader.LoadStyle("_content/Soenneker.Quark.Suite/css/switch.css", cancellationToken: token);
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}
