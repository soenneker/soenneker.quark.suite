using System;
using System.Globalization;

namespace Soenneker.Quark;

internal struct DateInputValueCache
{
    private DateTime _value;
    private string? _mode, _formatted;

    internal string Get(DateTime? date, DateOnly? dateOnly, string mode)
    {
        if (!date.HasValue && !dateOnly.HasValue) return string.Empty;
        DateTime value = date ?? dateOnly!.Value.ToDateTime(TimeOnly.MinValue);
        if (_formatted is null || _value != value || _mode != mode)
        {
            _value = value;
            _mode = mode;
            _formatted = value.ToString(mode switch
            {
                "datetime-local" => "yyyy-MM-ddTHH:mm",
                "month" => "yyyy-MM",
                _ => "yyyy-MM-dd"
            }, CultureInfo.InvariantCulture);
        }
        return _formatted;
    }
}
