using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Quark.Suite.Demo.Interops.Abstract;

namespace Soenneker.Quark.Suite.Demo.Interops;

public sealed class DemoPageInterop(IJSRuntime jsRuntime) : IDemoPageInterop
{
    public ValueTask ScrollToTop() => jsRuntime.InvokeVoidAsync("quarkDemo.scrollPageToTop");
}
