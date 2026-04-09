using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// JS callback for pointer/focus-outside dismissal on viewport-positioned hover cards (immediate close).
/// </summary>
public sealed class HoverCardOutsideCloseProxy
{
    private readonly HoverCardContext _context;

    internal HoverCardOutsideCloseProxy(HoverCardContext context)
    {
        _context = context;
    }

    [JSInvokable]
    public Task Close()
    {
        return _context.CloseImmediate();
    }
}
