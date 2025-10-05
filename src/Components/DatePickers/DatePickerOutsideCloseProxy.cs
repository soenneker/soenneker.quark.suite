using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Internal proxy class for handling outside click events in DatePicker component.
/// </summary>
internal sealed class DatePickerOutsideCloseProxy
{
    private readonly DatePicker _owner;
    
    public DatePickerOutsideCloseProxy(DatePicker owner) 
    { 
        _owner = owner; 
    }

    [JSInvokable]
    public Task Close() => _owner.ClosePanel();
}
