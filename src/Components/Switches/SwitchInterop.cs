using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark.Components.Switches;

///<inheritdoc cref="ISwitchInterop"/>
public sealed class SwitchInterop : ISwitchInterop
{
    private readonly AsyncSingleton _initializer;

    public SwitchInterop(IResourceLoader resourceLoader)
    {
        _initializer = new AsyncSingleton(async (token, _) =>
        {
            await resourceLoader.LoadStyle("_content/Soenneker.Quark.Suite/css/switch.css", cancellationToken: token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}
