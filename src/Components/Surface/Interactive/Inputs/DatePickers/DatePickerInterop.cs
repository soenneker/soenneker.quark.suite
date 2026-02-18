using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="IDatePickerInterop"/>
public sealed class DatePickerInterop : IDatePickerInterop
{
    /// <inheritdoc />
    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return ValueTask.CompletedTask;
    }
}
