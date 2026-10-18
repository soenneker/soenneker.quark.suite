using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
namespace Soenneker.Quark;
public sealed class WorkflowInteractionInterop(IModuleImportUtil modules) : IWorkflowInteractionInterop
{
    private async ValueTask<IJSObjectReference> Module() => await modules.GetContentModuleReference("./_content/Soenneker.Quark.Suite/js/workflowinterop.js");
    public async ValueTask Mount<T>(ElementReference element, DotNetObjectReference<T> callback) where T : class => await (await Module()).InvokeVoidAsync("mountInteractions", element, callback);
    public async ValueTask Update(ElementReference element) => await (await Module()).InvokeVoidAsync("updateInteractions", element);
    public async ValueTask Unmount(ElementReference element) => await (await Module()).InvokeVoidAsync("unmountInteractions", element);
}
