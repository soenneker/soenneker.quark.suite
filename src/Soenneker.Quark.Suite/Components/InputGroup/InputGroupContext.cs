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

    public ValueTask FocusControl() => _control is { } control ? control.FocusAsync() : ValueTask.CompletedTask;
}
