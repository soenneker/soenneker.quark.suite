using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
namespace Soenneker.Quark;
public sealed class WorkflowInteractionInterop(IModuleImportUtil modules) : IWorkflowInteractionInterop
{
    private async ValueTask<IJSObjectReference> Module() => await modules.GetContentModuleReference("./_content/Soenneker.Quark.Suite/js/workflowinterop.js");
    public async ValueTask Mount<T>(ElementReference element, DotNetObjectReference<T> callback) where T : class => await (await Module()).InvokeVoidWithContextAsync("mountInteractions", element, callback);
    public async ValueTask Update(ElementReference element) => await (await Module()).InvokeVoidWithContextAsync("updateInteractions", element);
    public async ValueTask Unmount(ElementReference element) => await (await Module()).InvokeVoidWithContextAsync("unmountInteractions", element);
}
