using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Quark;

/// <inheritdoc cref="IDatePickerInterop"/>
public sealed class DatePickerInterop : IDatePickerInterop
{
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncSingleton _initializer;

    public DatePickerInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;

        _initializer = new AsyncSingleton(async (token, _) =>
        {
            await _resourceLoader.LoadStyle("_content/Soenneker.Quark.Suite/css/datepicker.css", cancellationToken: token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _initializer.Init(cancellationToken);

    public async ValueTask Attach(ElementReference element, CancellationToken cancellationToken = default)
    {
        // No-op: JS-free mode. Keep signature for compatibility.
        await _initializer.Init(cancellationToken);
    }

    public async ValueTask RegisterOutsideClose<T>(ElementReference container, DotNetObjectReference<T> dotNetRef, string methodName, CancellationToken cancellationToken = default) where T : class
    {
        // No-op: JS-free mode. Outside-close handled via backdrop overlay.
        await _initializer.Init(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
    }
}
