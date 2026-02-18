using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Contract for JS interop used by DatePicker / DateTimePicker (e.g. init, attach, outside-click).
/// </summary>
public interface IDatePickerInterop
{
    /// <summary>
    /// Ensures any required resources are loaded and ready.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
