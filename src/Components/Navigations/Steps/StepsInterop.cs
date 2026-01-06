using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

///<inheritdoc cref="IStepsInterop"/>
public sealed class StepsInterop : IStepsInterop
{
    private readonly AsyncInitializer _cssInitializer;

    private const string _cssPath = "_content/Soenneker.Quark.Suite/css/steps.css";

    private readonly IResourceLoader _resourceLoader;

    public StepsInterop(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _cssInitializer = new AsyncInitializer(InitializeCss);
    }

    private ValueTask InitializeCss(CancellationToken token)
    {
        return _resourceLoader.LoadStyle(_cssPath, cancellationToken: token);
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
