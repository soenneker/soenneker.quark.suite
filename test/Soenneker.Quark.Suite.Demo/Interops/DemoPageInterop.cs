using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.Extensions.Logging;
using Soenneker.Quark.Suite.Demo.Interops.Abstract;

namespace Soenneker.Quark.Suite.Demo.Interops;

public sealed class DemoPageInterop(IJSRuntime jsRuntime, ILogger<DemoPageInterop> logger) : IDemoPageInterop
{
    public async ValueTask ScrollToTop()
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("quarkDemo.scrollPageToTop");
        }
        catch (JSDisconnectedException)
        {
            // Navigation can finish after the browser connection closes.
        }
        catch (JSException exception)
        {
            logger.LogWarning(exception, "Could not scroll the demo page to the top.");
        }
    }
}
