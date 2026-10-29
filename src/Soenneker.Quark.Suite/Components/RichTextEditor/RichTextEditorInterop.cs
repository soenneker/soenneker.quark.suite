using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
namespace Soenneker.Quark;
public sealed class RichTextEditorInterop(IModuleImportUtil modules) : IRichTextEditorInterop
{
    private async ValueTask<IJSObjectReference> Module() => await modules.GetContentModuleReference("./_content/Soenneker.Quark.Suite/js/richtextinterop.js");
    public async ValueTask Mount(ElementReference element, DotNetObjectReference<RichTextEditor> callback) => await (await Module()).InvokeVoidWithContextAsync("mount", element, callback);
    public async ValueTask SetContent(ElementReference element, string html, bool readOnly) => await (await Module()).InvokeVoidWithContextAsync("setContent", element, html, readOnly);
    public async ValueTask Execute(ElementReference element, string command, string? value = null, string? label = null) => await (await Module()).InvokeVoidWithContextAsync("execute", element, command, value, label);
    public async ValueTask Unmount(ElementReference element) => await (await Module()).InvokeVoidWithContextAsync("unmount", element);
}
