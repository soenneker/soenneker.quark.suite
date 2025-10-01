using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

///<inheritdoc cref="ITreeViewInterop"/>
public sealed class TreeViewInterop : ITreeViewInterop
{
    private readonly AsyncSingleton _initializer;

    public TreeViewInterop(IResourceLoader resourceLoader)
    {
        _initializer = new AsyncSingleton(async (token, _) =>
        {
            await resourceLoader.LoadStyle("_content/Soenneker.Quark.Suite/css/treeview.css", cancellationToken: token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public ValueTask DisposeAsync()
    {
        return _initializer.DisposeAsync();
    }
}