using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Quark;

public sealed class CalendarInterop : ICalendarInterop
{
    private readonly IModuleImportUtil _modules;
    private readonly string _path;

    public CalendarInterop(IModuleImportUtil modules, QuarkOptions? options = null)
    {
        _modules = modules;
        _path = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/calendarinterop.js", options);
    }

    public async ValueTask Initialize(ElementReference element, DotNetObjectReference<Calendar> receiver) =>
        await (await _modules.GetContentModuleReference(_path)).InvokeQuarkVoidAsync("initialize", element, receiver);

    public async ValueTask Focus(ElementReference element, string date) =>
        await (await _modules.GetContentModuleReference(_path)).InvokeQuarkVoidAsync("focus", element, date);

    public async ValueTask Destroy(ElementReference element) =>
        await (await _modules.GetContentModuleReference(_path)).InvokeQuarkVoidAsync("destroy", element);

    public async ValueTask DisposeAsync() => await _modules.DisposeContentModule(_path);
}
