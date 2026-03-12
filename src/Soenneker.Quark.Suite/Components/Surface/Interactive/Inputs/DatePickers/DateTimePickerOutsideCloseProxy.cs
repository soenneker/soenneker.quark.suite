using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Internal proxy class for handling outside click events in DateTimePicker component.
/// </summary>
internal sealed class DateTimePickerOutsideCloseProxy
{
    private readonly DateTimePicker _owner;
    
    public DateTimePickerOutsideCloseProxy(DateTimePicker owner) 
    { 
        _owner = owner; 
    }

    [JSInvokable]
    public Task Close() => _owner.ClosePanel();
}
