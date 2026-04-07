using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class PopoverOutsideCloseProxy
{
    private readonly PopoverContext _context;

    internal PopoverOutsideCloseProxy(PopoverContext context)
    {
        _context = context;
    }

    [JSInvokable]
    public Task Close()
    {
        return _context.Close();
    }
}
