using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Quark;

public sealed class VirtualViewportInterop(IModuleImportUtil modules, QuarkOptions? options = null) : IVirtualViewportInterop
{
    private readonly string _path = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/virtualviewport.js", options);
    public async ValueTask Update(ElementReference element, DotNetObjectReference<VirtualItems> receiver, IReadOnlyList<string> keys, bool follow, bool before, bool after, bool parentScroll, IReadOnlyList<string> disabledKeys)
    {
        var module = await modules.GetContentModuleReference(_path);
        await module.InvokeVoidWithContextAsync("updateItems", element, receiver, keys, follow, before, after, parentScroll, disabledKeys);
    }
    public async ValueTask ScrollToIndex(ElementReference element, int index, string? focusSelector)
    {
        var module = await modules.GetContentModuleReference(_path);
        await module.InvokeVoidWithContextAsync("scrollToIndex", element, index, focusSelector);
    }
    public async ValueTask JumpToEnd(ElementReference element)
    {
        var module = await modules.GetContentModuleReference(_path);
        await module.InvokeVoidWithContextAsync("jumpToLatest", element);
    }
    public async ValueTask Destroy(ElementReference element)
    {
        var module = await modules.GetContentModuleReference(_path);
        await module.InvokeVoidWithContextAsync("destroyAutoScroll", element);
    }
    public async ValueTask DisposeAsync() => await modules.DisposeContentModule(_path);
}
