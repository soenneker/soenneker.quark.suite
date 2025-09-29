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
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncSingleton _scriptInitializer;

    private const string _module = "Soenneker.Quark/js/datepickerinterop.js";
    private const string _moduleName = "DatePickerInterop";

    public DatePickerInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton(async (token, _) =>
        {
            await _resourceLoader.LoadStyle("_content/Soenneker.Quark/css/datepicker.css", cancellationToken: token);
            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_module, _moduleName, 100, token);
            return new object();
        });
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default) => _scriptInitializer.Init(cancellationToken);

    public async ValueTask Attach(ElementReference element, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.attach", cancellationToken, element);
    }

    public async ValueTask RegisterOutsideClose<T>(ElementReference container, DotNetObjectReference<T> dotNetRef, string methodName, CancellationToken cancellationToken = default) where T : class
    {
        await _scriptInitializer.Init(cancellationToken);
        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.registerOutsideClose", cancellationToken, container, dotNetRef, methodName);
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_module);
        await _scriptInitializer.DisposeAsync();
    }
}
