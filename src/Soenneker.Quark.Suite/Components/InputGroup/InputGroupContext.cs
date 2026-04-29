using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Soenneker.Quark;

internal sealed class InputGroupContext
{
    private ElementReference? _control;

    public void RegisterControl(ElementReference control)
    {
        _control = control;
    }

    public async ValueTask FocusControl()
    {
        if (_control is not { } control)
            return;

        await control.FocusAsync();
    }
}
